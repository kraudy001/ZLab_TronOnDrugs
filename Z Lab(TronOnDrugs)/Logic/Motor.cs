using System;

namespace Z_Lab_TronOnDrugs_.Logic
{
    public class Motor
    {
        #region Property
        public int werticalPlacement { get; set; }
        public int horisontalPlacement { get; set; }  // placement of objet (proportionally to grid)
        #endregion

        #region Variable
        int orientation;  //calculated in degree  (0-360)

        IAbility special;  //currentli usable ability

        private int turnAmount = 3; //fine tune needed
        private int speed = 2;
        #endregion
        public Motor(int wericalStart, int horisontalStart, int startingOrientation)
        {
            this.werticalPlacement = wericalStart;
            this.horisontalPlacement = horisontalStart;
            this.orientation = startingOrientation;
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
                special.CastAbility(werticalPlacement, horisontalPlacement);
            }
            special = null;
        }
        public void GetAbility(IAbility ability)
        {
            this.special = ability;
        }
        #endregion

        public void move() 
        {
            if(0>=orientation||90< orientation)
            {
                werticalPlacement = werticalPlacement - (int)Math.Sin(orientation)/speed;
                horisontalPlacement = horisontalPlacement + (int)Math.Cos(orientation)/speed;
            }
            else if(90 >= orientation || 180 < orientation)
            {
                werticalPlacement = werticalPlacement - (int)Math.Sin(180 - orientation) / speed;
                horisontalPlacement = horisontalPlacement - (int)Math.Cos(180 - orientation) / speed;
            }
            else if (180 >= orientation || 270 < orientation)
            {
                werticalPlacement = werticalPlacement + (int)Math.Sin(orientation - 180) / speed;
                horisontalPlacement = horisontalPlacement - (int)Math.Cos(orientation - 180) / speed;
            }
            else if (270 >= orientation || 360 <= orientation)
            {
                werticalPlacement = werticalPlacement + (int)Math.Sin(360 - orientation) / speed;
                horisontalPlacement = horisontalPlacement + (int)Math.Cos(360 - orientation) / speed;
            }
        }


    }
}
