using CricketSimulator.Model;
using Xunit;
using Xunit.Abstractions;

namespace CricketSimulatorTests
{


    public class scorekeeperTests
    {
        [Fact]
        public void test_innings_started()
        {
            Scorekeeper scorekeeper = new Scorekeeper();
            scorekeeper.StartInnings();
            Assert.Equal(0, scorekeeper.RunsScored(0));
            Assert.Equal(0, scorekeeper.WicketsLost(0));
            Assert.Equal(0, scorekeeper.OversBowled(0));
        }

        [Fact]
        public void test_runs_scored()
        {
            Scorekeeper scorekeeper = new Scorekeeper();
            scorekeeper.StartInnings();
            scorekeeper.BallOutcome(outcomes.TWO_RUNS);
            scorekeeper.BallOutcome(outcomes.BOWLED);
            Assert.Equal(2, scorekeeper.RunsScored(0));
            Assert.Equal(1, scorekeeper.WicketsLost(0));
            Assert.Equal(0, scorekeeper.OversBowled(0));

        }

        [Fact]
        public void test_complex_runs_scored()
        {
            Scorekeeper scorekeeper = new Scorekeeper();
            scorekeeper.StartInnings();
            scorekeeper.BallOutcome(outcomes.TWO_RUNS);
            scorekeeper.BallOutcome(outcomes.BOWLED);
            scorekeeper.BallOutcome(outcomes.FOUR_BYES);
            scorekeeper.BallOutcome(outcomes.CAUGHT); 
            scorekeeper.BallOutcome(outcomes.NO_BALL);
            scorekeeper.BallOutcome(outcomes.THREE_RUNS); 
            scorekeeper.BallOutcome(outcomes.SIX_RUNS);
            scorekeeper.BallOutcome(outcomes.STUMPED);
            Assert.Equal(16, scorekeeper.RunsScored(0));
            Assert.Equal(3, scorekeeper.WicketsLost(0));
            // Note: not incrementing over as this is umpire call
            Assert.Equal(0, scorekeeper.OversBowled(0));

        }


        [Fact]
        public void test_end_over()
        {
            Scorekeeper scorekeeper = new Scorekeeper();
            scorekeeper.StartInnings();
            scorekeeper.BallOutcome(outcomes.TWO_RUNS);
            scorekeeper.BallOutcome(outcomes.NO_BALL);
            scorekeeper.BallOutcome(outcomes.TWO_BYES);
            scorekeeper.BallOutcome(outcomes.CAUGHT);
            scorekeeper.BallOutcome(outcomes.NO_BALL);
            scorekeeper.BallOutcome(outcomes.FOUR_RUNS);
            scorekeeper.BallOutcome(outcomes.THREE_RUNS);
            scorekeeper.EndOfOver();
            scorekeeper.BallOutcome(outcomes.STUMPED);
            Assert.Equal(13, scorekeeper.RunsScored(0));
            Assert.Equal(2, scorekeeper.WicketsLost(0));
            Assert.Equal(1, scorekeeper.OversBowled(0));
        }



        [Fact]
        public void test_end_of_innings()
        {
            Scorekeeper scorekeeper = new Scorekeeper();
            scorekeeper.StartInnings();
            scorekeeper.BallOutcome(outcomes.TWO_RUNS);
            scorekeeper.BallOutcome(outcomes.NO_BALL);
            scorekeeper.BallOutcome(outcomes.TWO_BYES);
            scorekeeper.BallOutcome(outcomes.CAUGHT);
            scorekeeper.BallOutcome(outcomes.NO_BALL);
            scorekeeper.BallOutcome(outcomes.FOUR_RUNS);
            scorekeeper.BallOutcome(outcomes.THREE_RUNS);
            scorekeeper.EndOfOver();
            scorekeeper.BallOutcome(outcomes.STUMPED);
            scorekeeper.StartInnings();
            scorekeeper.BallOutcome(outcomes.FOUR_RUNS);
            scorekeeper.BallOutcome(outcomes.WIDE);
            scorekeeper.BallOutcome(outcomes.LBW);
            scorekeeper.BallOutcome(outcomes.RUN_OUT);
            scorekeeper.BallOutcome(outcomes.ONE_BYE);
            scorekeeper.BallOutcome(outcomes.ONE_LEGBYE);
            scorekeeper.BallOutcome(outcomes.ONE_RUN);
            scorekeeper.BallOutcome(outcomes.STUMPED);
            scorekeeper.EndOfOver();
            scorekeeper.EndOfOver();
            Assert.Equal(13, scorekeeper.RunsScored(0));
            Assert.Equal(2, scorekeeper.WicketsLost(0));
            Assert.Equal(1, scorekeeper.OversBowled(0));
            Assert.Equal(8, scorekeeper.RunsScored(1));
            Assert.Equal(3, scorekeeper.WicketsLost(1));
            Assert.Equal(2, scorekeeper.OversBowled(1));


        }

        [Fact]
        public void test_reset_scoreboard()
        {
            Scorekeeper scorekeeper = new Scorekeeper();
            scorekeeper.StartInnings();
            scorekeeper.BallOutcome(outcomes.TWO_RUNS);
            scorekeeper.BallOutcome(outcomes.NO_BALL);
            scorekeeper.BallOutcome(outcomes.TWO_BYES);
            scorekeeper.BallOutcome(outcomes.CAUGHT);
            scorekeeper.ResetScoreboard();
            Assert.Equal(0, scorekeeper.RunsScored(0));
            Assert.Equal(0, scorekeeper.WicketsLost(0));
            Assert.Equal(0, scorekeeper.OversBowled(0));

        }

        [Fact]
        public void test_num_innings()
        {
            Scorekeeper scorekeeper = new Scorekeeper();
            scorekeeper.StartInnings();
            scorekeeper.StartInnings();
            scorekeeper.StartInnings();
            Assert.Equal(3, scorekeeper.NumberInnings());

        }

    }
}