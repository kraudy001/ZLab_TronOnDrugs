using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Z_Lab_TronOnDrugs_.Logic
{
    public class AbilityBase
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


        public void Cast()
        {

        }

        public Geometry Ability
        {
            get
            {
                return new EllipseGeometry(Placement,10,10);
            }
        }

    }
}
