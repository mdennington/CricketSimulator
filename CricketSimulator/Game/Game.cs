using System;
using System.Collections.Generic;
using System.Text;
using CricketSimulator.Common;

namespace CricketSimulator.Model
{
    public class Game : AggregateRoot
    {
        private Bowler _bowler;
        private Batsman _batsman;
        private Ball _ball;
        private IRandomNumberGenerator _rand;

        public Game()
        {

        }

        public Game(IRandomNumberGenerator randomNumGen)
        {
            _rand = randomNumGen;
            _bowler = new Bowler();
            _batsman = new Batsman(_rand);
            _ball = new Ball(_rand);
        }
        public virtual outcomes Play()
        {

            // Play
            outcomes outcome = _bowler.Bowl(_ball);
            outcomes outcomeFinal = _batsman.Play(outcome);

            return outcomeFinal;
        }
    }
}
