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
            start_innings();
            assert_score(0, 0, 0, 0);
        }

        [Fact]
        public void test_runs_scored()
        {
            start_innings();
            ball_sequence(TWO_RUNS, BOWLED);
            assert_score(0, 2, 1, 0);
        }

        [Fact]
        public void test_complex_runs_scored()
        {
            start_innings();
            ball_sequence(TWO_RUNS, BOWLED, FOUR_BYES, CAUGHT, NO_BALL, THREE_RUNS, SIX_RUNS, STUMPED);
            assert_score(0, 16, 3, 0);
        }


        [Fact]
        public void test_end_over()
        {
            start_innings();
            ball_sequence(TWO_RUNS, NO_BALL, TWO_BYES, CAUGHT, NO_BALL, FOUR_RUNS, THREE_RUNS);
            end_over();
            ball_sequence(STUMPED);

            assert_score(0, 13, 2, 1);
        }



        [Fact]
        public void test_end_of_innings()
        {
            start_innings();
            ball_sequence(TWO_RUNS, NO_BALL, TWO_BYES, CAUGHT, NO_BALL, FOUR_RUNS, THREE_RUNS);
            end_over();
            ball_sequence(STUMPED);
            start_innings();
            ball_sequence(FOUR_RUNS, WIDE, LBW, RUN_OUT, ONE_BYE, ONE_LEGBYE, ONE_RUN, STUMPED);
            end_over();
            end_over();
            assert_score(0, 13, 2, 1);
            assert_score(1, 8, 3, 2);
        }

        [Fact]
        public void test_reset_scoreboard()
        {
            start_innings();
            ball_sequence(TWO_RUNS, NO_BALL, TWO_BYES, CAUGHT);
            _scorekeeper.ResetScoreboard();
            assert_score(0, 0, 0, 0);
 
        }

        [Fact]
        public void test_num_innings()
        {
            start_innings();
            start_innings();
            start_innings();
            Assert.Equal(3, _scorekeeper.NumberInnings());

        }

        // Helper Functions
        void assert_score(int innings, int runs, int wickets, int overs)
        {
            Assert.Equal(runs, _scorekeeper.RunsScored(innings));
            Assert.Equal(wickets, _scorekeeper.WicketsLost(innings));
            Assert.Equal(overs, _scorekeeper.OversBowled(innings));
        }

        void ball_sequence(params outcomes[] sequence)
        {
            foreach ( var ball in sequence)
            {
                _scorekeeper.BallOutcome(ball);
            } 
        }

        void start_innings()
        {
            _scorekeeper.StartInnings();
        }

        void end_over()
        {
            _scorekeeper.EndOfOver();
        }
    }
}