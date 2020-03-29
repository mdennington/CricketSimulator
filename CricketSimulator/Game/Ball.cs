using CricketSimulator.Common;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using static CricketSimulator.Model.Ball;

namespace CricketSimulator.Model
{
    public class Ball : Entity
    {
        static IRandomNumberGenerator _rand;

        public Ball()
        {

        }

        public Ball(IRandomNumberGenerator rand)
        {
            _rand = rand;
        }

        public virtual outcomes Bowl()
        {
            Array values = Enum.GetValues(typeof(outcomes));
            int index = _rand.GetRandomNumber(0, values.Length-1);

            return (outcomes)values.GetValue((int)index);
        }

    }
}
