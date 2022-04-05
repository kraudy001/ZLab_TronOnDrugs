using System;
using System.Collections.Generic;

namespace Z_Lab_TronOnDrugs_.Logic
{
    internal interface IGameLogic
    {
        List<Motor> Motors { get; set; }
        public event EventHandler Change; 
        List<Vector> Vectors { get; set; }
        void Turn();
    }
}