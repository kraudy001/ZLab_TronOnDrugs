
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Z_Lab_TronOnDrugs_.Logic
{
    public class Motor
    {
        #region Property
        public Point Placement { get; set; }    // placement of objet (proportionally to grid)

        public int Orientation { get => orientation ; set => orientation = value; }
        #endregion

        #region Variable
        //calculated in degree  (0-360)
        int orientation;

        IAbility special;  //currentli usable ability

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
        public void TurnLeft()
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

        public void TurnRight()
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
                special.CastAbility(Placement);
            }
            special = null;
        }
        public void GetAbility(IAbility ability)
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

            return PointChange(nextPlacement, vectors, ref EndGameToken, 
                new Vectors(new Point(Placement.X - (int)(Math.Sin(angle) * (radiusX)), Placement.Y + (int)(Math.Cos(angle) * radiusY)), 
                new Point(Placement.X - (int)(Math.Sin(angle) * (radiusX / 2)), Placement.Y + (int)(Math.Cos(angle) * (radiusY / 2)))));
        }

        private Vectors PointChange(Point point, List<Vectors> vectors, ref bool EndGameToken, Vectors dreg)
        {
            Vectors toReturn = new Vectors(Placement, point);
            if (dasCounter > 9)
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
            if(vectors.Count > 1)
            {
                foreach (Vectors VectorToCheck in vectors)
                {
                    if (radiusX/2.9 > new Vectors(VectorToCheck.StartPoint, point).DistanceBetweenEndPoints())
                    {
                        return true;
                    }
                    else if (radiusX/2.9 > new Vectors(VectorToCheck.EndPoint, point).DistanceBetweenEndPoints())
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion

        public Geometry Area
        {
            get
            {
                return new EllipseGeometry(Placement,radiusX,radiusY);
            }
        }
    }
}
