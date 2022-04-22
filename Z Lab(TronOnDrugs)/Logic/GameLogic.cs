using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z_Lab_TronOnDrugs_.Logic
{
    internal class GameLogic : IGameLogic
    {
        public List<Motor> Motors { get; set; }

        public List<Vectors> Vectors { get; set; }

        public event EventHandler Change;
        public event EventHandler EndGame;

        double displayWidth;
        double displayHeight;

        public GameLogic(List<Motor> motors, double displayWidth, double displayHeight)
        {
            this.Motors = motors;
            Vectors = new List<Vectors>();
            this.displayWidth = displayWidth;
            this.displayHeight = displayHeight;
            Vectors.Add(new Vectors(displayWidth, displayHeight, 5));
        }

        public void Turn()
        {
            bool EndGameToken = false;
            foreach (Motor motor in Motors)
            {
                Vectors vector = motor.Move(Vectors, ref EndGameToken, displayWidth, displayHeight);
                if (EndGameToken)
                {
                    EndGame?.Invoke(null, null);
                }
                if (vector != null)
                {
                    Vectors.Add(vector);
                }
            }
            Change?.Invoke(null, null);
        }

        public void SizeChange(double displayWidth, double displayHeight)
        {
            this.displayWidth = displayWidth;
            this.displayHeight = displayHeight;
        }
    }
}
