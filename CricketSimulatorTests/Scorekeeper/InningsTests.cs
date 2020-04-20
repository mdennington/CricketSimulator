using CricketSimulator.Model;
using CricketSimulator.Common;
using static CricketSimulator.Common.outcomes;
using Xunit;
using System.Security.Cryptography;
using Xunit.Sdk;

namespace CricketSimulatorTests
{
    public class inningsTests
    {
        private Innings _innings;
        private BowlingStats _bowler;

        public inningsTests()
        {
            _innings = new Innings("Gary Sobers", "Simon Simple", "Third Batsman");
            _bowler = _innings.changeBowler("Ian Botham");
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



        // Outcome (single ball tests)
        // Parameters
        // Ball Event, Runs, Batsman 1 Runs, Striker, Non-Striker, Extras
        // 1. Even Runs Scored
        // 2. Odd Runs Scored (batsmen change ends)
        // 3. Even Leg Byes Scored
        // 4. Odd Leg Byes Scored
        // 5. Even Byes Scored
        // 6. Odd Byes Scored
        // 7. No Ball
        // 8. One Wide (batsmen stay put)
        // 9. Two Wides (batsmen change ends)
        // 10. Three Wides (batsmen stay put)
        [Theory]
        [InlineData(TWO_RUNS, 2, 2, 1, 2, 0)]
        [InlineData(THREE_RUNS, 3, 3, 2, 1, 0)]
        [InlineData(TWO_LEGBYES, 2, 0, 1, 2, 2)]
        [InlineData(THREE_LEGBYES, 3, 0, 2, 1, 3)]
        [InlineData(TWO_BYES, 2, 0, 1, 2, 2)]
        [InlineData(THREE_BYES, 3, 0, 2, 1, 3)]
        [InlineData(NO_BALL, 1, 0, 1, 2, 1)]
        [InlineData(WIDE, 1, 0, 1, 2, 1)]
        [InlineData(TWO_WIDES, 2, 0, 2, 1, 2)]
        [InlineData(THREE_WIDES, 3, 0, 1, 2, 3)]
        void test_batsmen_runs_scored(outcomes ballEvent, int runs, int batsmenRuns, int striker, int nonStriker, int extras)
        {
            _innings.RunsScored(ballEvent);
            Assert.Equal(runs, _innings.runs);
            assertBatsmanScore(1, batsmenRuns);
            assertBatsmen(striker, nonStriker);
            Assert.Equal(extras, _innings.Extras);
        }

        [Fact]
        void test_wicket()
        {
            bool endInnings = _innings.wicketFell(BOWLED);
            Assert.Equal(0, _innings.runs);
            Assert.Equal(0, _innings.GetBatsmanRuns(1));
            Assert.Equal(1, _innings.GetWickets());
            Assert.False(endInnings);
            assertBatsmen(3, 2);

        }


        [Fact]
        void test_ten_wickets_innings_end()
        {
            bool endInnings;

            for (int i = 0; i < 9; i++)
            {
                endInnings = _innings.wicketFell(BOWLED);
                Assert.False(endInnings);
            }

            endInnings = _innings.wicketFell(BOWLED);

            Assert.True(endInnings);
            assertRunsWicketsOvers(0, 10, 0);
            assertBatsmanScore(1, 0);
        }

        // test second batsmen out first
        [Fact]
        void test_second_batsmen_out_first()
        {
            _innings.RunsScored(ONE_RUN);
            bool endInnings = _innings.wicketFell(BOWLED);
            assertBatsmanScore(1, 1);
            assertRunsWicketsOvers(1, 1, 0);
            Assert.False(endInnings);
            assertBatsmen(3, 1);
        }


        [Fact]
        void test_end_of_over()
        {
            _innings.endOfOver();
            assertRunsWicketsOvers(0, 0, 1);
            assertBatsmanScore(1, 0);
            assertBatsmen(2, 1);
        }


        [Fact]
        void test_new_bowler()
        {
            _innings.RunsScored(NO_RUNS);
            _innings.RunsScored(TWO_RUNS);
            assertRunsWicketsOvers(2, 0, 0);
            assertBatsmanScore(1, 2);
            assertBowlerStats(_bowler, 2, 0, 0, 0);
        }

        [Fact]
        void test_second_bowler()
        {
            BowlingStats bowler1 = _innings.changeBowler("Ian Botham");
            _innings.RunsScored(NO_RUNS);
            _innings.RunsScored(TWO_RUNS);
            _innings.endOfOver();
            BowlingStats bowler2 = _innings.changeBowler("Devon Malcolm");
            _innings.RunsScored(THREE_RUNS);
            _innings.RunsScored(WIDE);
            _innings.RunsScored(FOUR_RUNS);
            _innings.RunsScored(NO_RUNS);

            assertBatsmanScore(1, 6);
            assertBatsmanScore(2, 3);
            assertRunsWicketsOvers(10, 0, 1);
            Assert.Equal(1, _innings.Extras);

            assertBowlerStats(bowler1, 2, 0, 1, 0);
            assertBowlerStats(bowler2, 8, 0, 0, 0);
        }

        // Test Bowler Stats for Maidens
        [Fact]
        void test_bowler_maidens()
        {
            BowlingStats bowler1 = _innings.changeBowler("Ian Botham");
            _innings.RunsScored(NO_RUNS);
            _innings.RunsScored(NO_RUNS);
            _innings.endOfOver();
            BowlingStats bowler2 = _innings.changeBowler("Devon Malcolm");
            _innings.RunsScored(TWO_RUNS);
            _innings.RunsScored(FOUR_RUNS);

            assertRunsWicketsOvers(6, 0, 1);
            assertBatsmanScore(1, 0);
            assertBatsmanScore(2, 6);
            Assert.Equal(0, _innings.Extras);

            assertBowlerStats(bowler1, 0, 1, 1, 0);
            assertBowlerStats(bowler2, 6, 0, 0, 0);
        }

        // Test Bowler Stats for Wickets 
        [Fact]
        void test_bowler_wickets()
        {
            BowlingStats bowler1 = _innings.changeBowler("Ian Botham");
            _innings.RunsScored(NO_RUNS);
            _innings.wicketFell(BOWLED);
            _innings.RunsScored(THREE_RUNS);

            assertRunsWicketsOvers(3, 1, 0);

            assertBatsmanScore(1, 0);
            assertBatsmanScore(2, 0);
            assertBatsmanScore(3, 3);
            Assert.Equal(0, _innings.Extras);

            assertBowlerStats(bowler1, 3, 0, 0, 1);
        }


        // void test_full_innings() including retrieving all scores and how out and bowling figures
        // Design a complex scenario with several sets of data to test and check scorecard at end

        // # TODO clean up tests and add helpers
        // # TODO refactor code where possible

        void assertBatsmen(int expStriker, int expNonStriker)
        {
            Assert.Equal(expStriker, _innings.Striker);
            Assert.Equal(expNonStriker, _innings.nonStriker);
        }

        void assertRunsWicketsOvers(int runs, int wickets, int overs)
        {
            Assert.Equal(_innings.runs, runs);
            Assert.Equal(_innings.wickets, wickets);
            Assert.Equal(_innings.overs, overs);

        }

        void assertBatsmanScore(int index, int runs)
        {
            Assert.Equal(runs, _innings.GetBatsmanRuns(index));
        }

        void assertBowlerStats(BowlingStats bowler, int runs, int maidens, int overs, int wickets)
        {
            Assert.Equal(runs, bowler.runs);
            Assert.Equal(maidens, bowler.maidens);
            Assert.Equal(overs, bowler.overs);
            Assert.Equal(wickets, bowler.wickets);
        }
    }

}
