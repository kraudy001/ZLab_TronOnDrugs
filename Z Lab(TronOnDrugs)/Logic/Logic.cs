using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z_Lab_TronOnDrugs_.Logic
{
    internal class Logic
    {
        List<Motor> motors;

        public Logic(List<Motor> motors)
        {
            this.motors = motors;
        }

        public void Turn()
        {
            foreach (Motor motor in motors) 
            {
                motor.move();
            }
            
        }




    }
}
