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

        private int turnAmount = 5; //fine tune needed
        private int speed = 2;
        #endregion
        public Motor(int wericalStart, int horisontalStart, int startingOrientation)
        {
            this.Placement = new Point( wericalStart, horisontalStart);
            this.orientation = startingOrientation;
            dasCounter = 1;
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
        public Vectors Move(List<Vectors> vectors, ref bool EndGameToken) 
        {

            double angle = (orientation) * (Math.PI / 180);

            return PointChange(new Point(Placement.X + (int)(Math.Sin(angle) * speed), Placement.Y - (int)(Math.Cos(angle) * speed)), vectors, ref EndGameToken);
        }

        private Vectors PointChange(Point point, List<Vectors> vectors, ref bool EndGameToken)
        {
            Vectors toReturn = new Vectors(Placement, point);
            if (dasCounter == 1)
            {

                dasCounter--;
                Placement = point;
                EndGameToken = Collision(vectors,toReturn);
                toReturn.sorce = "MotorVector";
                return toReturn;
                
            }
            else
            {
                dasCounter = 1;
                Placement = point;
                EndGameToken = Collision(vectors, toReturn);
                return null;
            }
        }
        #endregion

        #region Collision detection
        public bool Collision(List<Vectors> vectors, Vectors vector)
        {
            if(vectors.Count > 1)
            {
                foreach (Vectors item in vectors)
                {
                    if (speed * 2 < new Vectors(item.StartPoint, Placement).DistanceBetweenEndPoints())
                    {
                        if (vector.VectorsIntersect(item))
                        {
                            return true;
                        }
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
                return new RectangleGeometry(new Rect(Placement, new Size(50,50)));
            }
        }
    }
}
