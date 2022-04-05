using System;
using System.Windows;

namespace Z_Lab_TronOnDrugs_.Logic
{
    public class Motor
    {
        #region Property
        public Point Placement { get; set; }    // placement of objet (proportionally to grid)
        #endregion

        #region Variable
        int orientation;  //calculated in degree  (0-360)

        IAbility special;  //currentli usable ability

        int dasCounter;

        private int turnAmount = 3; //fine tune needed
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
            if (orientation + turnAmount > 360)
            {
                orientation = 360 - orientation + turnAmount;
            }
            else
            {
                orientation = orientation + turnAmount;
            }
        }

        public void TurnRight()
        {
            if(orientation - turnAmount < 0)
            {
                orientation = 360 + orientation - turnAmount;
            }
            else
            {
                orientation = orientation - turnAmount;
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

        public Vector Move() 
        {
            if(0>=orientation||90< orientation)
            {
                Placement = new Point(Placement.X - (int)Math.Sin(orientation) / speed, Placement.Y + (int)Math.Cos(orientation) / speed);
                //werticalPlacement = werticalPlacement - (int)Math.Sin(orientation)/speed;
                //horisontalPlacement = horisontalPlacement + (int)Math.Cos(orientation)/speed;
                return PointChange(new Point(Placement.X - (int)Math.Sin(orientation) / speed, Placement.Y + (int)Math.Cos(orientation) / speed));
            }
            else if(90 >= orientation || 180 < orientation)
            {
                Placement = new Point(Placement.X - (int)Math.Sin(180 - orientation) / speed, Placement.Y - (int)Math.Cos(180 - orientation) / speed);
                //werticalPlacement = werticalPlacement - (int)Math.Sin(180 - orientation) / speed;
                //horisontalPlacement = horisontalPlacement - (int)Math.Cos(180 - orientation) / speed;
                return PointChange(new Point(Placement.X - (int)Math.Sin(180 - orientation) / speed, Placement.Y - (int)Math.Cos(180 - orientation) / speed));
            }
            else if (180 >= orientation || 270 < orientation)
            {
                Placement = new Point(Placement.X + (int)Math.Sin(orientation - 180) / speed, Placement.Y - (int)Math.Cos(orientation - 180) / speed);
                //werticalPlacement = werticalPlacement + (int)Math.Sin(orientation - 180) / speed;
                //horisontalPlacement = horisontalPlacement - (int)Math.Cos(orientation - 180) / speed;
                return PointChange(new Point(Placement.X + (int)Math.Sin(orientation - 180) / speed, Placement.Y - (int)Math.Cos(orientation - 180) / speed));
            }
            else if (270 >= orientation || 360 <= orientation)
            {
                //werticalPlacement = werticalPlacement + (int)Math.Sin(360 - orientation) / speed;
                //horisontalPlacement = horisontalPlacement + (int)Math.Cos(360 - orientation) / speed;
                return PointChange(new Point(Placement.X + (int)Math.Sin(360 - orientation) / speed, Placement.Y + (int)Math.Cos(360 - orientation) / speed));
            }
            throw new Exception("Motor movement error");
        }

        private Vector PointChange(Point point)
        {
            if(dasCounter == 1)
            {
                dasCounter--;

                Vector toReturn = new Vector(Placement, point);
                Placement = point;
                
                return toReturn;
                
            }
            else
            {
                dasCounter = 1;

                Placement = point;
                return null;
            }
        }


    }
}
