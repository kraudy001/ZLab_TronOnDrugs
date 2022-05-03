using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Z_Lab_TronOnDrugs_.Logic
{
    internal class GameLogic : IGameLogic
    {
        #region Propertys
        public List<Motor> Motors { get; set; }

        public List<Vectors> Vectors { get; set; }

        public List<AbilityBase> Abilities { get; set; }

        #endregion

        #region Events

        public event EventHandler Change;
        public event EventHandler EndGame;

        #endregion

        #region Variables
        double displayWidth;
        double displayHeight;
        Random random;
        #endregion
        public GameLogic(List<Motor> motors, double displayWidth, double displayHeight)
        {
            this.Motors = motors;
            Vectors = new List<Vectors>();
            Abilities = new List<AbilityBase>();
            this.displayWidth = displayWidth;
            this.displayHeight = displayHeight;
            Vectors.Add(new Vectors(displayWidth, displayHeight, 5));
            random = new Random();
        }

        public void Turn()
        {
            bool EndGameToken = false;
            foreach (Motor motor in Motors)
            {
                Vectors vector = motor.Move(Vectors, ref EndGameToken, displayWidth, displayHeight);
                if (EndGameToken)
                {
                    Motors.Remove(motor);
                    if(Motors.Count == 0)
                    {
                        EndGame?.Invoke(null, null);
                    }
                }
                if (vector != null)
                {
                    Vectors.Add(vector);
                }
                Abilities.Remove(motor.AbilityCollision(Abilities));
            }

            if (Abilities.Count < 5 && random.NextDouble() < 0.01)
            {

                switch (random.NextDouble())
                {
                    case < 0.33:
                        Abilities.Add(new Ghost("Ghost", new Point(random.Next(60, (int)displayWidth - 60), random.Next(60, (int)displayHeight - 60))));
                        break;
                    case < 0.66:
                        Abilities.Add(new SpeedUp("Speed", new Point(random.Next(60, (int)displayWidth - 60), random.Next(60, (int)displayHeight - 60))));
                        break;
                    default:
                        Abilities.Add(new FullWall("Wall", new Point(random.Next(60, (int)displayWidth - 60), random.Next(60, (int)displayHeight - 60))));
                        break;
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
