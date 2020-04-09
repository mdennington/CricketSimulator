using CricketSimulator.Model;
using CricketSimulator.Common;
using static CricketSimulator.Common.outcomes;
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
            Assert.Equal(1, start_innings());
            Assert.Equal(2, start_innings());
            Assert.Equal(3, start_innings());
        }

        [Fact]
        public void test_runs_scored()
        {
            int inningsNum = start_innings();
            runs_score(TWO_RUNS);
            assert_score(inningsNum - 1, 2, 0, 0);
        }

        [Fact]
        public void test_wicket_falls()
        {
            int inningsNum = start_innings();
            wicket_fell(BOWLED);
            assert_score(inningsNum - 1, 0, 1, 0);
        }

        [Fact]
        public void test_end_over()
        {
            int inningsNum = start_innings();
            end_over();
            assert_score(inningsNum - 1, 0, 0, 1);
        }



        [Fact]
        public void test_outcomes()
        {
            // Arrange
            int inningsNum = start_innings();

            // Act
            runs_score(FOUR_BYES, FOUR_RUNS, NO_BALL, WIDE);
            wicket_fell(CAUGHT, BOWLED, LBW);
            end_over();
            end_over();

            // Assert
            assert_score(inningsNum - 1, 10, 3, 2);


        }

        [Fact]
        public void test_full_run_two_innings()
        {

            // Innings #1
            int inningsNum = start_innings();
            runs_score(FOUR_BYES);
            wicket_fell(CAUGHT);
            end_over();
            end_over();

            // Assertions #1
            Assert.Equal(4, _card.Runs(0));
            Assert.Equal(1, _card.Wickets(0));
            Assert.Equal(2, _card.Overs(0));

            // Innings #2
            inningsNum = start_innings();
            runs_score(TWO_RUNS);
            wicket_fell(CAUGHT, STUMPED);
            end_over();
            end_over();
            end_over();

            // Assertions #2
            assert_score(0, 4, 1, 2);
            assert_score(1, 2, 2, 3);

        }

        [Fact]
        public void test_reset()
        {
            int inningsNum = start_innings();
            runs_score(FOUR_BYES);
            wicket_fell(CAUGHT);
            end_over();
            end_over();

            _card.Reset();
            assert_score(0, 0, 0, 0);

        }


        [Fact]
        public void test_10_wickets_returns_true()
        {
            int inningsNum = start_innings();
            Assert.False(_card.WicketFallen(BOWLED));
            Assert.False(_card.WicketFallen(BOWLED));
            Assert.False(_card.WicketFallen(BOWLED));
            Assert.False(_card.WicketFallen(BOWLED));
            Assert.False(_card.WicketFallen(BOWLED));

            Assert.False(_card.WicketFallen(BOWLED));
            Assert.False(_card.WicketFallen(BOWLED));
            Assert.False(_card.WicketFallen(BOWLED));
            Assert.False(_card.WicketFallen(BOWLED));
            Assert.True(_card.WicketFallen(BOWLED));


        }

        // Helper Functions
        void assert_score(int innings, int runs, int wickets, int overs)
        {
            Assert.Equal(runs, _card.Runs(innings));
            Assert.Equal(wickets, _card.Wickets(innings));
            Assert.Equal(overs, _card.Overs(innings));
        }

        int start_innings()
        {
            return _card.StartInnings();
        }

        void end_over()
        {
            _card.EndOfOver();
        }

        void runs_score(params outcomes[] runs)
        {
            foreach( outcomes scored in runs)
            {
                _card.RunsScored(scored);
            }
        }

        void wicket_fell(params outcomes[] wickets)
        {
            foreach( outcomes wicket in wickets)
            {
                _card.WicketFallen(wicket);
            }
        }
        
    }
}