
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Z_Lab_TronOnDrugs_.Logic
{
    public class Motor
    {
        #region Property
        public Point Placement { get; set; }    // placement of objet (proportionally to grid)

        public int Orientation { get => orientation ; set => orientation = value; }

        public int SpeedSet { set { speed = value; } }
        #endregion

        #region Variable
        //calculated in degree  (0-360)
        int orientation;

        public AbilityBase special;  //currentli usable ability
        public int AbilitiActiveTurns = 0;
        public bool Invisible = false;
        public bool FullLine = false;

        public bool TurnLeft;
        public bool TurnRight;

        int dasCounter;

        private int turnAmount = 10; //fine tune needed
        private int speed = 8;

        double radiusX = 40, radiusY = 40;

        #endregion
        public Motor(int wericalStart, int horisontalStart, int startingOrientation)
        {
            this.Placement = new Point( wericalStart, horisontalStart);
            this.orientation = startingOrientation;
            dasCounter = 18;
        }

        #region Turning
        public void turnLeft()
        {

            if ((orientation - turnAmount) < 0)
            {
                orientation = 360 + orientation - turnAmount;
            }
            else
            {
                orientation = orientation - turnAmount;
            }
        }

        public void turnRight()
        {
            if(orientation + turnAmount > 360)
            {
                orientation = 360 - orientation + turnAmount;
            }
            else
            {
                orientation = orientation + turnAmount;
            }
        }
        #endregion

        #region Ability
        public void UseAbility()
        {
            if(special != null)
            {
                special.Cast(this);
            }
            special = null;
        }

        public void Reset()
        {
            Invisible = false;
            FullLine = false;
            speed = 8;
        }


        public void GetAbility(AbilityBase ability)
        {
            this.special = ability;
        }
        #endregion

        #region Move
        public Vectors Move(List<Vectors> vectors, ref bool EndGameToken, double displayWidth, double displayHeight) 
        {

            double angle = (orientation) * (Math.PI / 180);
            #region WallCollision
            Point nextPlacement = new Point(Placement.X + (int)(Math.Sin(angle) * speed), Placement.Y - (int)(Math.Cos(angle) * speed));
            if (nextPlacement.X < radiusX/2 || nextPlacement.Y < radiusY / 2 || nextPlacement.X > displayWidth - radiusX / 2 || nextPlacement.Y > displayHeight - radiusY / 2)
            {
                EndGameToken = true;
                return null;
            }
            #endregion

            if(AbilitiActiveTurns > 0)
            {
                AbilitiActiveTurns--;
            }
            else if(AbilitiActiveTurns == 0)
            {
                Reset();
                AbilitiActiveTurns--;
            }
            

            return PointChange(nextPlacement, vectors, ref EndGameToken, 
                new Vectors(new Point(Placement.X - (int)(Math.Sin(angle) * (radiusX)), Placement.Y + (int)(Math.Cos(angle) * radiusY)), 
                new Point(Placement.X - (int)(Math.Sin(angle) * (radiusX / 2)), Placement.Y + (int)(Math.Cos(angle) * (radiusY / 2)))));
        }

        private Vectors PointChange(Point point, List<Vectors> vectors, ref bool EndGameToken, Vectors dreg)
        {
            Vectors toReturn = new Vectors(Placement, point);
            if (dasCounter > 9 || FullLine)
            {

                dasCounter--;
                Placement = point;
                EndGameToken = Collision(vectors,toReturn, point);
                toReturn.sorce = "MotorVector";
                return dreg;
                
            }
            else 
            {
                dasCounter--;
                if (dasCounter == 0)
                {
                    dasCounter = 18;
                }
                Placement = point;
                EndGameToken = Collision(vectors, toReturn, point);
                return null;
            }
        }
        #endregion

        #region Collision detection
        public bool Collision(List<Vectors> vectors, Vectors vector , Point point)
        {
            if(vectors.Count > 1 && !Invisible)
            {
                foreach (Vectors VectorToCheck in vectors)
                {
                    if (radiusX / 2 + speed / 2 > new Vectors(VectorToCheck.CenterPoint, point).DistanceBetweenEndPoints()) 
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public AbilityBase AbilityCollision(List<AbilityBase> abilitys)
        {
            if (abilitys.Count > 0)
            {
                foreach (AbilityBase AbilityToCheck in abilitys)
                {
                    if (radiusX / 2 + speed / 2 > new Vectors(this.Placement, AbilityToCheck.Placement).DistanceBetweenEndPoints())
                    {
                        GetAbility(AbilityToCheck);
                        return AbilityToCheck;
                    }
                }
            }
            return null;
        }

        public Geometry Area
        {
            get
            {
                return new EllipseGeometry(Placement,radiusX,radiusY);
            }
        }

        #endregion
    }
}
