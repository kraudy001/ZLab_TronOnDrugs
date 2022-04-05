using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z_Lab_TronOnDrugs_.Logic
{
    internal class Logic : IGameLogic
    {
        public List<Motor> Motors { get; set; }

        public List<Vector> Vectors { get; set; }

        public event EventHandler Change;

        public Logic(List<Motor> motors)
        {
            this.Motors = motors;
            Vectors = new List<Vector>();
        }

        public void Turn()
        {
            foreach (Motor motor in Motors)
            {
                Vector vector = motor.Move();
                if (vector != null)
                {
                    Vectors.Add(vector);
                }


            }
            Change?.Invoke(null, null);
        }




    }
}
