using CricketSimulator.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CricketSimulator.Model
{
    public class Batsman : Entity
    {
        static IRandomNumberGenerator _rand;

        public Batsman(IRandomNumberGenerator rand)
        {
            _rand = rand;
        }
        public outcomes Play(outcomes outcome)
        {
            int flipCoin = _rand.GetRandomNumber(0,2);
            if (flipCoin == 1) return outcome;
            return outcomes.NO_RUNS;             
        }
    }
}
