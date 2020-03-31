using CricketSimulator.Model;
using Xunit;

namespace CricketSimulatorTests
{


    public class scorecardTests
    {
        [Fact]
        public void test_innings_started()
        {
            Scorecard card = new Scorecard();
            Assert.Equal(1, card.StartInnings());
            Assert.Equal(2, card.StartInnings());
            Assert.Equal(3, card.StartInnings());
        }

        [Fact]
        public void test_runs_scored()
        {
            Scorecard card = new Scorecard();
            int inningsNum = card.StartInnings();
            card.RunsScored(outcomes.TWO_RUNS);
            Assert.Equal(2, card.Runs(inningsNum));
            Assert.Equal(0, card.Wickets(inningsNum));
            Assert.Equal(0, card.Overs(inningsNum));
        }

        [Fact]
        public void test_wicket_falls()
        {
            Scorecard card = new Scorecard();
            int inningsNum = card.StartInnings();
            card.WicketFallen(outcomes.BOWLED);
            Assert.Equal(0, card.Runs(inningsNum));
            Assert.Equal(1, card.Wickets(inningsNum));
            Assert.Equal(0, card.Overs(inningsNum));

        }

        [Fact]
        public void test_end_over()
        {
            Scorecard card = new Scorecard();
            int inningsNum = card.StartInnings();
            card.EndOfOver();
            Assert.Equal(0, card.Runs(inningsNum));
            Assert.Equal(0, card.Wickets(inningsNum));
            Assert.Equal(1, card.Overs(inningsNum));
        }



        [Fact]
        public void test_outcomes()
        {
            Scorecard card = new Scorecard();
            int inningsNum = card.StartInnings();
            card.RunsScored(outcomes.FOUR_BYES);
            card.RunsScored(outcomes.FOUR_RUNS);
            card.RunsScored(outcomes.NO_BALL);
            card.RunsScored(outcomes.WIDE);
            card.WicketFallen(outcomes.CAUGHT);
            card.WicketFallen(outcomes.BOWLED);
            card.WicketFallen(outcomes.LBW);
            card.EndOfOver();
            card.EndOfOver();
            Assert.Equal(10, card.Runs(inningsNum));
            Assert.Equal(3, card.Wickets(inningsNum));
            Assert.Equal(2, card.Overs(inningsNum));

        }

        [Fact]
        public void test_full_run_two_innings()
        {
            Scorecard card = new Scorecard();

            // Innings #1
            int inningsNum = card.StartInnings();
            card.RunsScored(outcomes.FOUR_BYES);
            card.WicketFallen(outcomes.CAUGHT);
            card.EndOfOver();
            card.EndOfOver();

            // Assertions #1
            Assert.Equal(4, card.Runs(0));
            Assert.Equal(1, card.Wickets(0));
            Assert.Equal(2, card.Overs(0));

            // Innings #2
            inningsNum = card.StartInnings();
            card.RunsScored(outcomes.TWO_RUNS);
            card.WicketFallen(outcomes.CAUGHT);
            card.WicketFallen(outcomes.STUMPED);
            card.EndOfOver();
            card.EndOfOver();
            card.EndOfOver(); 

            // Assertions #2
            Assert.Equal(4, card.Runs(0));
            Assert.Equal(1, card.Wickets(0));
            Assert.Equal(2, card.Overs(0));

            Assert.Equal(2, card.Runs(1));
            Assert.Equal(2, card.Wickets(1));
            Assert.Equal(3, card.Overs(1));


        }

    }
}