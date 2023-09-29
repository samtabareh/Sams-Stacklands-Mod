using System;
using System.Collections.Generic;
using System.Text;
using ExoticNS;

namespace ExoticNS.Classes
{
    public class Caviar : Food
    {
        public float SpoilTime = Exotic.world.MonthTime + Exotic.world.MonthTime / 2;
    }
}
