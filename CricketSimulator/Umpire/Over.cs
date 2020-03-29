using CricketSimulator.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CricketSimulator.Model
{
    public class Over : Entity
    {

        private int _ball;
        private int _balls_per_over;

        public Over(int balls_per_over)
        {
            _ball = 0;
            _balls_per_over = balls_per_over;
        }

        public bool CountBalls(outcomes outcome)
        {

            outcomes[] extras = { outcomes.NO_BALL,
                         outcomes.WIDE,
                         outcomes.TWO_WIDES,
                         outcomes.THREE_WIDES,
                         outcomes.FOUR_WIDES };

            // Check if extra ball required
            if (extras.Contains(outcome))
                return false;

            // check for end of over
            _ball++;
            if (_ball == _balls_per_over) return true;
            return false;

        }

        public int BallCount { get { return _ball; } }
    }
}
