using CricketSimulator.Model;
using Xunit;
using Xunit.Abstractions;
using static CricketSimulator.Common.outcomes;
using CricketSimulator.Common;
using System;

namespace CricketSimulatorTests
{


    public class scorekeeperTests
    {
        private Scorekeeper _scorekeeper;

        public scorekeeperTests()
        {
            _scorekeeper = new Scorekeeper();
        }
        [Fact]
        public void test_innings_started()
        {
            _scorekeeper.StartInnings();
            assert_score(0, 0, 0, 0);
        }

        [Fact]
        public void test_runs_scored()
        {
            _scorekeeper.StartInnings();
            ball_sequence(new[] {TWO_RUNS, BOWLED });
            assert_score(0, 2, 1, 0);
        }

        [Fact]
        public void test_complex_runs_scored()
        {
            _scorekeeper.StartInnings();
            ball_sequence(new[] { TWO_RUNS, BOWLED, FOUR_BYES, CAUGHT, NO_BALL, THREE_RUNS, SIX_RUNS, STUMPED });
            assert_score(0, 16, 3, 0);
        }


        [Fact]
        public void test_end_over()
        {
            _scorekeeper.StartInnings();
            ball_sequence(new[] { TWO_RUNS, NO_BALL, TWO_BYES, CAUGHT, NO_BALL, FOUR_RUNS, THREE_RUNS } );
            _scorekeeper.EndOfOver();
            ball_sequence(new[] { STUMPED });

            assert_score(0, 13, 2, 1);
        }



        [Fact]
        public void test_end_of_innings()
        {
            _scorekeeper.StartInnings();
            ball_sequence( new[] { TWO_RUNS, NO_BALL, TWO_BYES, CAUGHT, NO_BALL, FOUR_RUNS, THREE_RUNS });
            _scorekeeper.EndOfOver();
            ball_sequence(new[] { STUMPED });
            _scorekeeper.StartInnings();
            ball_sequence(new[] { FOUR_RUNS, WIDE, LBW, RUN_OUT, ONE_BYE, ONE_LEGBYE, ONE_RUN, STUMPED } );
            _scorekeeper.EndOfOver();
            _scorekeeper.EndOfOver();
            assert_score(0, 13, 2, 1);
            assert_score(1, 8, 3, 2);
        }

        [Fact]
        public void test_reset_scoreboard()
        {
            _scorekeeper.StartInnings();
            ball_sequence( new[] { TWO_RUNS, NO_BALL, TWO_BYES, CAUGHT });
            _scorekeeper.ResetScoreboard();
            assert_score(0, 0, 0, 0);
 
        }

        [Fact]
        public void test_num_innings()
        {
            _scorekeeper.StartInnings();
            _scorekeeper.StartInnings();
            _scorekeeper.StartInnings();
            Assert.Equal(3, _scorekeeper.NumberInnings());

        }
        void assert_score(int innings, int runs, int wickets, int overs)
        {
            Assert.Equal(runs, _scorekeeper.RunsScored(innings));
            Assert.Equal(wickets, _scorekeeper.WicketsLost(innings));
            Assert.Equal(overs, _scorekeeper.OversBowled(innings));
        }

        void ball_sequence(outcomes[] sequence)
        {
            foreach ( var ball in sequence)
            {
                _scorekeeper.BallOutcome(ball);
            } 
        }

    }
}