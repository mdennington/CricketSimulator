using CricketSimulator.Common;
using System;
using System.Collections.Generic;
using System.Text;
using static CricketSimulator.Model.Ball;

namespace CricketSimulator.Model
{
    public class Bowler : Entity
    {
        // Instructed to Bowl
        // Creates random ball (to pass back)

        public outcomes Bowl(Ball ball)
        {
            return (ball.Bowl());
        }

    }



}

