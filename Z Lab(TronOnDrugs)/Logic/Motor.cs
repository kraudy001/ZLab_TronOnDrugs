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

        public int Orientation { get; set; }
        #endregion

        #region Variable
        //calculated in degree  (0-360)

        IAbility special;  //currentli usable ability

        int dasCounter;

        private int turnAmount = 3; //fine tune needed
        private int speed = 5;
        #endregion
        public Motor(int wericalStart, int horisontalStart, int startingOrientation)
        {
            this.Placement = new Point( wericalStart, horisontalStart);
            this.Orientation = startingOrientation;
            dasCounter = 1;
        }

        #region Turning
        public void TurnLeft()
        {
            if ((Orientation - turnAmount) < 0)
            {
                Orientation = 360 + Orientation - turnAmount;
            }
            else
            {
                Orientation = Orientation - turnAmount;
            }
        }

        public void TurnRight()
        {
            if(Orientation + turnAmount > 360)
            {
                Orientation = 360 - Orientation + turnAmount;
            }
            else
            {
                Orientation = Orientation + turnAmount;
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
            if (0 <= Orientation && 90 > Orientation)
            {
                //Placement = new Point(Placement.X - (int)Math.Sin(orientation) / speed, Placement.Y + (int)Math.Cos(orientation) / speed);
                //werticalPlacement = werticalPlacement - (int)Math.Sin(orientation)/speed;
                //horisontalPlacement = horisontalPlacement + (int)Math.Cos(orientation)/speed;
                return PointChange(new Point(Placement.X - (int)(Math.Sin(Orientation) * speed), Placement.Y - (int)(Math.Cos(Orientation) * speed)), vectors, ref EndGameToken);
            }
            else if(90 <= Orientation && 180 > Orientation)
            {
                //Placement = new Point(Placement.X - (int)Math.Sin(180 - orientation) / speed, Placement.Y - (int)Math.Cos(180 - orientation) / speed);
                //werticalPlacement = werticalPlacement - (int)Math.Sin(180 - orientation) / speed;
                //horisontalPlacement = horisontalPlacement - (int)Math.Cos(180 - orientation) / speed;
                return PointChange(new Point(Placement.X + (int)(Math.Sin(180 - Orientation) * speed), Placement.Y - (int)(Math.Cos(180 - Orientation) * speed)), vectors, ref EndGameToken);
            }
            else if (180 <= Orientation && 270 > Orientation)
            {
                //Placement = new Point(Placement.X + (int)Math.Sin(orientation - 180) / speed, Placement.Y - (int)Math.Cos(orientation - 180) / speed);
                
                return PointChange(new Point(Placement.X + (int)(Math.Sin(Orientation - 180) * speed), Placement.Y + (int)(Math.Cos(Orientation - 180) * speed)), vectors, ref EndGameToken);
            }
            else if (270 <= Orientation && 360 >= Orientation)
            {
                //werticalPlacement = werticalPlacement + (int)Math.Sin(360 - orientation) / speed;
                //horisontalPlacement = horisontalPlacement + (int)Math.Cos(360 - orientation) / speed;
                return PointChange(new Point(Placement.X - (int)(Math.Sin(360 - Orientation) * speed), Placement.Y + (int)(Math.Cos(360 - Orientation) * speed)), vectors, ref EndGameToken);
            }
            throw new Exception("Motor movement error");
        }

        private Vectors PointChange(Point point, List<Vectors> vectors, ref bool EndGameToken)
        {
            Vectors toReturn = new Vectors(Placement, point);
            if (dasCounter == 1)
            {

                dasCounter--;
                Placement = point;
                EndGameToken = Collision(vectors,toReturn);
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

            return false;
        }
        #endregion

        public Geometry Area
        {
            get
            {
                return new RectangleGeometry(new Rect(Placement, new Size(200, 200)));
            }
        }
    }
}
