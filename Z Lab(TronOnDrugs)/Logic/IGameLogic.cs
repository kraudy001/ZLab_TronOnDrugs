﻿using System;
using System.Collections.Generic;

namespace Z_Lab_TronOnDrugs_.Logic
{
    internal interface IGameLogic
    {
        List<Motor> Motors { get; set; }
        public event EventHandler Change; 
        public event EventHandler EndGame; 
        List<Vectors> Vectors { get; set; }
        void Turn();
    }
}