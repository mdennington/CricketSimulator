using CricketSimulator.Model;
using CricketSimulator.Common;
using static CricketSimulator.Common.outcomes;
using Xunit;
using System.Security.Cryptography;

namespace CricketSimulatorTests
{
    public class inningsTests
    {

        public inningsTests()
        {

        }

        // Innings should store details of batsmen, bowlers figures and extras
        // Test set up
        // Innings( array of batsmen name)
        [Fact]
        void test_innings_set_up()
        {
            Innings thisInnings = new Innings("Joe Root", "Fred Flintoff", "Ben Stokes", "Jos Buttler");
            Assert.Equal("Fred Flintoff", thisInnings.GetBatsman(2));
        }

        // sets up batsmen with runs scored, balls received to zero
        [Fact]
        void test_batsmen_runs_scored()
        {
            Innings thisInnings = new Innings("Gary Sobers", "Simon Simple", "Third Batsman");
            thisInnings.RunsScored(TWO_RUNS);
            Assert.Equal(2, thisInnings.runs);
            Assert.Equal(2, thisInnings.GetBatsmanRuns(1));
            Assert.Equal(1, thisInnings.Striker);
            Assert.Equal(2, thisInnings.nonStriker);
        }

        [Fact]
        void test_batsmen_odd_runs_score()
        {
            Innings thisInnings = new Innings("Gary Sobers", "Simon Simple", "Third Batsman");
            thisInnings.RunsScored(THREE_RUNS);
            Assert.Equal(3, thisInnings.runs);
            Assert.Equal(3, thisInnings.GetBatsmanRuns(1));
            Assert.Equal(2, thisInnings.Striker);
            Assert.Equal(1, thisInnings.nonStriker);
        }

        [Fact]
        void test_even_byes_score()
        {
            Innings thisInnings = new Innings("Gary Sobers", "Simon Simple", "Third Batsman");
            thisInnings.RunsScored(TWO_BYES);
            Assert.Equal(2, thisInnings.runs);
            Assert.Equal(0, thisInnings.GetBatsmanRuns(1));
            Assert.Equal(1, thisInnings.Striker);
            Assert.Equal(2, thisInnings.nonStriker);
        }

        [Fact]
        void test_odd_byes_score()
        {
            Innings thisInnings = new Innings("Gary Sobers", "Simon Simple", "Third Batsman");
            thisInnings.RunsScored(THREE_BYES);
            Assert.Equal(3, thisInnings.runs);
            Assert.Equal(0, thisInnings.GetBatsmanRuns(1));
            Assert.Equal(2, thisInnings.Striker);
            Assert.Equal(1, thisInnings.nonStriker);
        }

        [Fact]
        void test_even_leg_byes_score()
        {
            Innings thisInnings = new Innings("Gary Sobers", "Simon Simple", "Third Batsman");
            thisInnings.RunsScored(FOUR_LEGBYES);
            Assert.Equal(4, thisInnings.runs);
            Assert.Equal(0, thisInnings.GetBatsmanRuns(1));
            Assert.Equal(1, thisInnings.Striker);
            Assert.Equal(2, thisInnings.nonStriker);
        }

        [Fact]
        void test_odd_leg_byes_score()
        {
            Innings thisInnings = new Innings("Gary Sobers", "Simon Simple", "Third Batsman");
            thisInnings.RunsScored(ONE_LEGBYE);
            Assert.Equal(1, thisInnings.runs);
            Assert.Equal(0, thisInnings.GetBatsmanRuns(1));
            Assert.Equal(2, thisInnings.Striker);
            Assert.Equal(1, thisInnings.nonStriker);
        }

        [Fact]
        void test_no_ball()
        {
            Innings thisInnings = new Innings("Gary Sobers", "Simon Simple", "Third Batsman");
            thisInnings.RunsScored(NO_BALL);
            Assert.Equal(1, thisInnings.runs);
            Assert.Equal(0, thisInnings.GetBatsmanRuns(1));
            Assert.Equal(1, thisInnings.Striker);
            Assert.Equal(2, thisInnings.nonStriker);
        }

        [Fact]
        void test_odd_wides()
        {
            Innings thisInnings = new Innings("Gary Sobers", "Simon Simple", "Third Batsman");
            thisInnings.RunsScored(WIDE);
            Assert.Equal(1, thisInnings.runs);
            Assert.Equal(0, thisInnings.GetBatsmanRuns(1));
            Assert.Equal(1, thisInnings.Striker);
            Assert.Equal(2, thisInnings.nonStriker);
        }

        [Fact]
        void test_two_wides()
        {
            Innings thisInnings = new Innings("Gary Sobers", "Simon Simple", "Third Batsman");
            thisInnings.RunsScored(TWO_WIDES);
            Assert.Equal(2, thisInnings.runs);
            Assert.Equal(0, thisInnings.GetBatsmanRuns(1));
            Assert.Equal(2, thisInnings.Striker);
            Assert.Equal(1, thisInnings.nonStriker);
        }

        [Fact]
        void test_wicket()
        {
            Innings thisInnings = new Innings("Gary Sobers", "Simon Simple", "Third Batsman");
            bool endInnings = thisInnings.wicketFell(BOWLED);
            Assert.Equal(0, thisInnings.runs);
            Assert.Equal(0, thisInnings.GetBatsmanRuns(1));
            Assert.Equal(1, thisInnings.GetWickets());
            Assert.False(endInnings);
            Assert.Equal(3, thisInnings.Striker);
            Assert.Equal(2, thisInnings.nonStriker);
        }


        [Fact]
        void test_ten_wickets_innings_end()
        {
            Innings thisInnings = new Innings("Gary Sobers", "Simon Simple", "Third Batsman");
            bool endInnings = thisInnings.wicketFell(BOWLED);
            Assert.False(endInnings);
            endInnings = thisInnings.wicketFell(BOWLED);
            Assert.False(endInnings);
            endInnings = thisInnings.wicketFell(BOWLED);
            Assert.False(endInnings);
            endInnings = thisInnings.wicketFell(BOWLED);
            Assert.False(endInnings);
            endInnings = thisInnings.wicketFell(BOWLED);
            Assert.False(endInnings);
            endInnings = thisInnings.wicketFell(BOWLED);
            Assert.False(endInnings);
            endInnings = thisInnings.wicketFell(BOWLED);
            Assert.False(endInnings);
            endInnings = thisInnings.wicketFell(BOWLED);
            Assert.False(endInnings);
            endInnings = thisInnings.wicketFell(BOWLED);
            Assert.False(endInnings);
            endInnings = thisInnings.wicketFell(BOWLED);
            Assert.True(endInnings);
            Assert.Equal(0, thisInnings.runs);
            Assert.Equal(0, thisInnings.GetBatsmanRuns(1));
            Assert.Equal(10, thisInnings.GetWickets());
        }

        // test second batsmen out first
        [Fact]
        void test_second_batsmen_out_first()
        {
            Innings thisInnings = new Innings("Gary Sobers", "Simon Simple", "Third Batsman");
            thisInnings.RunsScored(ONE_RUN);
            bool endInnings = thisInnings.wicketFell(BOWLED);
            Assert.Equal(1, thisInnings.runs);
            Assert.Equal(1, thisInnings.GetBatsmanRuns(1));
            Assert.Equal(1, thisInnings.GetWickets());
            Assert.False(endInnings);
            Assert.Equal(3, thisInnings.Striker);
            Assert.Equal(1, thisInnings.nonStriker);

        }


        [Fact]
        void test_end_of_over()
        {
            Innings thisInnings = new Innings("Gary Sobers", "Simon Simple", "Third Batsman");
            thisInnings.endOfOver();
            Assert.Equal(0, thisInnings.runs);
            Assert.Equal(0, thisInnings.GetBatsmanRuns(1));
            Assert.Equal(0, thisInnings.GetWickets());
            Assert.Equal(1, thisInnings.overs);

        }



        // void test_new_bowler()
        [Fact]
        void test_new_bowler()
        {
            Innings thisInnings = new Innings("Gary Sobers", "Simon Simple", "Third Batsman");
            BowlingStats bowler = thisInnings.changeBowler("Ian Botham");
            thisInnings.RunsScored(NO_RUNS);
            thisInnings.RunsScored(TWO_RUNS);
            Assert.Equal(2, thisInnings.runs);
            Assert.Equal(2, thisInnings.GetBatsmanRuns(1));
            Assert.Equal(0, thisInnings.GetWickets());
            Assert.Equal(2, bowler.runs);
            Assert.Equal(0, bowler.maidens);
            Assert.Equal(0, bowler.overs);
            Assert.Equal(0, bowler.wickets);
            Assert.Equal(0, thisInnings.overs);

        }
        
        [Fact]
        void test_assign_second_bowler()
        {
            Innings thisInnings = new Innings("Gary Sobers", "Simon Simple", "Third Batsman");
            BowlingStats bowler1 = thisInnings.changeBowler("Ian Botham");
            thisInnings.RunsScored(NO_RUNS);
            thisInnings.RunsScored(TWO_RUNS);
            thisInnings.endOfOver();
            BowlingStats bowler2 = thisInnings.changeBowler("Devon Malcolm");
            thisInnings.RunsScored(THREE_RUNS);
            thisInnings.RunsScored(WIDE);
            thisInnings.RunsScored(FOUR_RUNS);
            thisInnings.RunsScored(NO_RUNS);


            Assert.Equal(10, thisInnings.runs);
            Assert.Equal(5, thisInnings.GetBatsmanRuns(1));
            Assert.Equal(4, thisInnings.GetBatsmanRuns(2));
            Assert.Equal(1, thisInnings.Extras);
            Assert.Equal(0, thisInnings.GetWickets());

            Assert.Equal(2, bowler1.runs);
            Assert.Equal(0, bowler1.maidens);
            Assert.Equal(1, bowler1.overs);
            Assert.Equal(0, bowler1.wickets);
            Assert.Equal(8, bowler2.runs);
            Assert.Equal(0, bowler2.maidens);
            Assert.Equal(0, bowler2.overs);
            Assert.Equal(0, bowler2.wickets);
            Assert.Equal(1, thisInnings.overs);

        }

        // Test Bowler Stats for Maidens
        [Fact]
        void test_bowler_maidens()
        {
            Innings thisInnings = new Innings("Gary Sobers", "Simon Simple", "Third Batsman");
            BowlingStats bowler1 = thisInnings.changeBowler("Ian Botham");
            thisInnings.RunsScored(NO_RUNS);
            thisInnings.RunsScored(NO_RUNS);
            thisInnings.endOfOver();
            BowlingStats bowler2 = thisInnings.changeBowler("Devon Malcolm");
            thisInnings.RunsScored(TWO_RUNS);
            thisInnings.RunsScored(FOUR_RUNS);

            Assert.Equal(6, thisInnings.runs);
            Assert.Equal(0, thisInnings.GetBatsmanRuns(1));
            Assert.Equal(6, thisInnings.GetBatsmanRuns(2));
            Assert.Equal(0, thisInnings.Extras);
            Assert.Equal(0, thisInnings.GetWickets());

            Assert.Equal(0, bowler1.runs);
            Assert.Equal(1, bowler1.maidens);
            Assert.Equal(1, bowler1.overs);
            Assert.Equal(0, bowler1.wickets);
            Assert.Equal(6, bowler2.runs);
            Assert.Equal(0, bowler2.maidens);
            Assert.Equal(0, bowler2.overs);
            Assert.Equal(0, bowler2.wickets);
            Assert.Equal(1, thisInnings.overs);

        }

        // Test Bowler Stats for Wickets 
        [Fact]
        void test_bowler_wickets()
        {
            Innings thisInnings = new Innings("Gary Sobers", "Simon Simple", "Third Batsman");
            BowlingStats bowler1 = thisInnings.changeBowler("Ian Botham");
            thisInnings.RunsScored(NO_RUNS);
            thisInnings.RunsScored(BOWLED);
            thisInnings.RunsScored(THREE_RUNS);


            Assert.Equal(3, thisInnings.runs);
            Assert.Equal(0, thisInnings.GetBatsmanRuns(1));
            Assert.Equal(0, thisInnings.GetBatsmanRuns(2));
            Assert.Equal(3, thisInnings.GetBatsmanRuns(3));
            Assert.Equal(0, thisInnings.Extras);
            Assert.Equal(1, thisInnings.GetWickets());

            Assert.Equal(3, bowler1.runs);
            Assert.Equal(0, bowler1.maidens);
            Assert.Equal(0, bowler1.overs);
            Assert.Equal(1, bowler1.wickets);
            Assert.Equal(0, thisInnings.overs);

        }


        // void test_full_innings() including retrieving all scores and how out and bowling figures
        // Design a complex scenario with several sets of data to test and check scorecard at end

        // # TODO parameterise tests
        // # TODO set up constructor to create Innings 
        // # TODO clean up tests and add helpers
        // # TODO refactor code where possible

        // New Bowler (name)
        // check if Bowler bowled
        // sets current bowler
        // adds to list if not already bowled
    }

}
