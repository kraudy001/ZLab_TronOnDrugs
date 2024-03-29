﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Z_Lab_TronOnDrugs_.Logic
{
    public abstract class AbilityBase
    {

        public string Name { get; set; }
        public Point Placement { get; set; }
        
        public AbilityBase(string name, Point placement)
        {
            Name = name;
            Placement = placement;
        }

        public AbilityBase Get()
        {
            return this;
        }


        public abstract void Cast(Motor motor);

        public Geometry Ability
        {
            get
            {
                return new EllipseGeometry(Placement,20,20);
            }
        }

    }
}
