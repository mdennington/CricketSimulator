using CricketSimulator.Model;
using CricketSimulator.Common;
using Xunit;

namespace CricketSimulatorTests
{


    public class scorecardTests
    {
        private Scorecard _card;

        public scorecardTests()
        {
            _card = new Scorecard();
        }
        [Fact]
        public void test_innings_started()
        {
            Assert.Equal(1, _card.StartInnings());
            Assert.Equal(2, _card.StartInnings());
            Assert.Equal(3, _card.StartInnings());
        }

        [Fact]
        public void test_runs_scored()
        {
            int inningsNum = _card.StartInnings();
            _card.RunsScored(outcomes.TWO_RUNS);

            assert_score(inningsNum - 1, 2, 0, 0);
        }

        [Fact]
        public void test_wicket_falls()
        {
            int inningsNum = _card.StartInnings();
            _card.WicketFallen(outcomes.BOWLED);

            assert_score(inningsNum - 1, 0, 1, 0);
        }

        [Fact]
        public void test_end_over()
        {
            int inningsNum = _card.StartInnings();

            _card.EndOfOver();

            assert_score(inningsNum - 1, 0, 0, 1);
        }



        [Fact]
        public void test_outcomes()
        {
            // Arrange
            int inningsNum = _card.StartInnings();

            // Act
            _card.RunsScored(outcomes.FOUR_BYES);
            _card.RunsScored(outcomes.FOUR_RUNS);
            _card.RunsScored(outcomes.NO_BALL);
            _card.RunsScored(outcomes.WIDE);
            _card.WicketFallen(outcomes.CAUGHT);
            _card.WicketFallen(outcomes.BOWLED);
            _card.WicketFallen(outcomes.LBW);
            _card.EndOfOver();
            _card.EndOfOver();

            // Assert
            assert_score(inningsNum - 1, 10, 3, 2);


        }

        [Fact]
        public void test_full_run_two_innings()
        {

            // Innings #1
            int inningsNum = _card.StartInnings();
            _card.RunsScored(outcomes.FOUR_BYES);
            _card.WicketFallen(outcomes.CAUGHT);
            _card.EndOfOver();
            _card.EndOfOver();

            // Assertions #1
            Assert.Equal(4, _card.Runs(0));
            Assert.Equal(1, _card.Wickets(0));
            Assert.Equal(2, _card.Overs(0));

            // Innings #2
            inningsNum = _card.StartInnings();
            _card.RunsScored(outcomes.TWO_RUNS);
            _card.WicketFallen(outcomes.CAUGHT);
            _card.WicketFallen(outcomes.STUMPED);
            _card.EndOfOver();
            _card.EndOfOver();
            _card.EndOfOver();

            // Assertions #2
            assert_score(0, 4, 1, 2);
            assert_score(1, 2, 2, 3);

        }

        [Fact]
        public void test_reset()
        {
            int inningsNum = _card.StartInnings();
            _card.RunsScored(outcomes.FOUR_BYES);
            _card.WicketFallen(outcomes.CAUGHT);
            _card.EndOfOver();
            _card.EndOfOver();

            _card.Reset();
            assert_score(0, 0, 0, 0);

        }

        [Fact]
        public void test_innings_num()
        {
            int inningsNum = _card.StartInnings();
            inningsNum = _card.StartInnings();
            inningsNum = _card.StartInnings();
            inningsNum = _card.StartInnings();
            Assert.Equal(4, _card.Innings());


        }

        [Fact]
        public void test_10_wickets_returns_true()
        {
            int inningsNum = _card.StartInnings();
            Assert.False(_card.WicketFallen(outcomes.BOWLED));
            Assert.False(_card.WicketFallen(outcomes.BOWLED));
            Assert.False(_card.WicketFallen(outcomes.BOWLED));
            Assert.False(_card.WicketFallen(outcomes.BOWLED));
            Assert.False(_card.WicketFallen(outcomes.BOWLED));
            Assert.False(_card.WicketFallen(outcomes.BOWLED));
            Assert.False(_card.WicketFallen(outcomes.BOWLED));
            Assert.False(_card.WicketFallen(outcomes.BOWLED));
            Assert.False(_card.WicketFallen(outcomes.BOWLED));
            Assert.True(_card.WicketFallen(outcomes.BOWLED));


        }

        void assert_score(int innings, int runs, int wickets, int overs)
        {
            Assert.Equal(runs, _card.Runs(innings));
            Assert.Equal(wickets, _card.Wickets(innings));
            Assert.Equal(overs, _card.Overs(innings));
        }

    }
}