using Models;
using System;
using System.Collections.Generic;

namespace Logic
{
    public class Game
    {
        List<Motor> Players;

        public void Turn()
        {
            foreach (Motor item in Players)
            {
                item.move();
            }
        }




    }
}
