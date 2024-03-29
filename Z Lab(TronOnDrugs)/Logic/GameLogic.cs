﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Z_Lab_TronOnDrugs_.Logic
{
    internal class GameLogic : IGameLogic
    {
        #region Properties
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
        bool randomStones;
        bool MultyPlayer;
        #endregion

        #region Constructors
        public GameLogic(List<Motor> motors, double displayWidth, double displayHeight, List<Vectors> vectors, bool randomgen = false)
        {
            this.Motors = motors;
            this.Vectors = vectors;
            this.Abilities = new List<AbilityBase>();
            this.displayWidth = displayWidth;
            this.displayHeight = displayHeight;
            this.random = new Random();
            this.randomStones = randomgen;
            this.MultyPlayer = motors.Count > 1;
        }
        #endregion

        #region Turn
        public void Turn()
        {
            bool EndGameToken = false;
            //foreach (Motor motor in Motors)
            //{
            //    motor.Turning();
            //    Vectors vector = motor.Move(Vectors, ref EndGameToken, displayWidth, displayHeight);
            //    if (EndGameToken)
            //    {
            //        Motors.Remove(motor);
            //        if(Motors.Count == 0)
            //        {
            //            EndGame?.Invoke(null, null);
            //        }
            //    }
            //    if (vector != null)
            //    {
            //        Vectors.Add(vector);
            //    }
            //    Abilities.Remove(motor.AbilityCollision(Abilities));
            //}
            for (int i = Motors.Count-1; 0 <= i; i--)
            {
                Motors[i].Turning();
                Vectors vector = Motors[i].Move(Vectors, ref EndGameToken, displayWidth, displayHeight);
                if (EndGameToken)
                {
                    Motors.Remove(Motors[i]);
                    if (MultyPlayer && Motors.Count == 1 || Motors.Count == 0)
                    {
                        EndGame?.Invoke(null, null);
                    }
                }
                else
                {
                    if (vector != null)
                    {
                        Vectors.Add(vector);
                    }
                    Abilities.Remove(Motors[i].AbilityCollision(Abilities));
                }
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

            if (randomStones && random.NextDouble() < 0.05)
            {
                Vectors.Add(new Vectors(new Point(random.Next(20, (int)(displayWidth) - 20), random.Next(20, (int)(displayHeight) - 60)), "stone"));
            }

            Change?.Invoke(null, null);
                
        }
        #endregion
    }
}
