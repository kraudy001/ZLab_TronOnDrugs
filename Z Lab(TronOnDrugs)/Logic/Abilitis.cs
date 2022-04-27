using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Z_Lab_TronOnDrugs_.Logic
{
    public class Ghost : AbilityBase
    {
        public Ghost(string name, Point placement) : base(name, placement)
        {
        }

        public override void Cast(Motor motor)
        {
            motor.AbilitiActiveTurns = 100;
            motor.Invisible = true;
        }
    }
    public class SpeedUp : AbilityBase
    {
        public SpeedUp(string name, Point placement) : base(name, placement)
        {
        }

        public override void Cast(Motor motor)
        {
            motor.AbilitiActiveTurns = 10;
            motor.SpeedSet = 15;
        }
    }
    public class FullWall : AbilityBase
    {
        public FullWall(string name, Point placement) : base(name, placement)
        {
        }

        public override void Cast(Motor motor)
        {
            motor.AbilitiActiveTurns = 10;
            motor.FullLine = true;
        }
    }
}
