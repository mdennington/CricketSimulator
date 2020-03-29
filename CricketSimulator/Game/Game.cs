﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CricketSimulator.Model
{
    public class Game : AggregateRoot
    {
        private Bowler _bowler;
        private Batsman _batsman;
        private Ball _ball;
        private GetRandomNumber _rand;

        public Game()
        {
            _rand = new GetRandomNumber();
            _bowler = new Bowler();
            _batsman = new Batsman(_rand);
            _ball = new Ball(_rand);
        }
        public outcomes Play()
        {

            // Initialise
            GetRandomNumber rand = new GetRandomNumber();

            // Play
            outcomes outcome = _bowler.Bowl(_ball);
            outcomes outcomeFinal = _batsman.Play(outcome);

            // Display Result
            Console.WriteLine($"Outcome: {outcomeFinal}");

            return outcomeFinal;
        }
    }
}