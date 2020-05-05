using CricketSimulator.Model;
using Xunit;
using Xunit.Abstractions;
using static CricketSimulator.Common.outcomes;
using CricketSimulator.Common;
using System;
using static CricketSimulator.Model.InningsMap;
using System.Collections.Generic;
using Xunit.Sdk;
using System.Linq;

namespace CricketSimulatorTests
{


    public class scorekeeperTests
    {
        private Scorekeeper _keeper;

        // Simplified Interfaces expressing the Domain Model
        // (int runs, int wickets, int overs) GetInningsScore(int InningsNum) - returns a tuple run for wickets in overs
        // int runs GetBatsmanScore(BatsmanName)
        // string status GetBatsmanStatus(BatsmanName)
        // (int overs, int maidens, int runs, int wickets) GetBowlerStats(BowlerName)
        // string[] GetBatsmenNames(InningsNum)
        // string[] GetBowlerNames(InningsNum)
        // int GetNumberOfInnings()
        // void SubmitTeam(int teamID, string[] batsmenNames)
        // void ChangeBowler(int teamID, string bowlerName)
        // TODO : End of Innings - where do I put logic in keeper or card?


        public scorekeeperTests()
        {
            _keeper = new Scorekeeper(ONE_DAY);
            _keeper.SubmitTeam(0, new string[] { "Ian Botham",
                                   "David Gower",
                                   "Michael Vaughan",
                                   "Kevin Pietersen",
                                   "Freddie Flintoff",
                                   "Jos Buttler",
                                   "Ben Stokes",
                                   "Johnny Bairstow",
                                   "Stuart Broad",
                                   "Graham Swann",
                                   "James Anderson"});
            _keeper.ChangeBowler("Michael Holding");
        }

        [Fact]
        void test_game_start()
        {
            Assert.Equal(2, _keeper.GetNumberOfInnings());
            assert_score(0, 0, 0, 0);

        }

        [Fact]
        void test_simple_over()
        {
            event_sequence(TWO_RUNS, BOWLED);
            assert_score(2, 1, 2, 0);
        }


        [Fact]
        void test_bowled_maiden()
        {
            event_sequence(NO_RUNS, NO_RUNS, NO_RUNS);
            _keeper.EndOfOver();
            assert_score(0, 0, 3, 1);
        }

        [Fact]
        public void test_complex_runs_scored()
        {
            event_sequence(TWO_RUNS, BOWLED, FOUR_BYES, CAUGHT, NO_BALL, THREE_RUNS, SIX_RUNS, STUMPED);
            assert_score(16, 3, 7, 0);
        }

        [Fact]
        void test_batsmen_scores()
        {


            event_sequence(NO_RUNS, ONE_RUN, TWO_RUNS, BOWLED, ONE_RUN, NO_BALL, TWO_RUNS);
            _keeper.EndOfOver();
            event_sequence(BOWLED, SIX_RUNS, NO_RUNS, LBW, FOUR_BYES, THREE_RUNS);
            _keeper.EndOfOver();

            // Assert Scores
            assert_score(20, 3, 12, 0);

            Assert.Equal(3, _keeper.GetBatsmanScore(0, "Ian Botham"));
            Assert.Equal(2, _keeper.GetBatsmanScore(0, "David Gower"));
            Assert.Equal(1, _keeper.GetBatsmanScore(0, "Michael Vaughan"));
            Assert.Equal(6, _keeper.GetBatsmanScore(0, "Kevin Pietersen"));
            Assert.Equal(3, _keeper.GetBatsmanScore(0, "Freddie Flintoff"));
        }

        [Theory]
        [InlineData(NO_RUNS, 0, 0, 1, 0, 0, 0)]
        [InlineData(ONE_RUN, 1, 0, 1, 0, 1, 0)]
        [InlineData(TWO_RUNS, 2, 0, 1, 0, 2, 0)]
        [InlineData(THREE_RUNS, 3, 0, 1, 0, 3, 0)]
        [InlineData(FOUR_RUNS, 4, 0, 1, 0, 4, 0)]
        [InlineData(SIX_RUNS, 6, 0, 1, 0, 6, 0)]
        [InlineData(NO_BALL, 1, 0, 0, 0, 0, 0)]
        [InlineData(WIDE, 1, 0, 0, 0, 0, 0)]
        [InlineData(TWO_WIDES, 2, 0, 0, 0, 0, 0)]
        [InlineData(THREE_WIDES, 3, 0, 0, 0, 0, 0)]
        [InlineData(FOUR_WIDES, 4, 0, 0, 0, 0, 0)]
        [InlineData(ONE_LEGBYE, 1, 0, 1, 0, 0, 0)]
        [InlineData(TWO_LEGBYES, 2, 0, 1, 0, 0, 0)]
        [InlineData(THREE_LEGBYES, 3, 0, 1, 0, 0, 0)]
        [InlineData(FOUR_LEGBYES, 4, 0, 1, 0, 0, 0)]
        [InlineData(ONE_BYE, 1, 0, 1, 0, 0, 0)]
        [InlineData(TWO_BYES, 2, 0, 1, 0, 0, 0)]
        [InlineData(THREE_BYES, 3, 0, 1, 0, 0, 0)]
        [InlineData(FOUR_BYES, 4, 0, 1, 0, 0, 0)]
        [InlineData(BOWLED, 0, 1, 1, 0, 0, 0)]
        [InlineData(CAUGHT, 0, 1, 1, 0, 0, 0)]
        [InlineData(LBW, 0, 1, 1, 0, 0, 0)]
        [InlineData(STUMPED, 0, 1, 1, 0, 0, 0)]
        [InlineData(RUN_OUT, 0, 1, 1, 0, 0, 0)]
        void test_score_combinations(outcomes outcome, int runs, int wickets, int balls, int maidens, int score1, int score2)
        {
            _keeper.SubmitTeam(0, new string[] { "Ian Botham",
                                   "David Gower" });
            event_sequence(outcome);
            assert_score(runs, wickets, balls, maidens);
            Assert.Equal(score1, _keeper.GetBatsmanScore(0, "Ian Botham"));
            Assert.Equal(score2, _keeper.GetBatsmanScore(0, "David Gower"));
        }

        [Fact]
        void test_change_bowler()
        {
            int bowlerId = _keeper.ChangeBowler("Michael Holding");
            var (balls, maidens, runs, wickets) = _keeper.GetBowlerStats(0, "Michael Holding");
            Assert.Equal(0, balls);
            Assert.Equal(0, maidens);
            Assert.Equal(0, runs);
            Assert.Equal(0, wickets);

        }

        [Fact]
        void test_bowler_stats()
        {

            _keeper.SubmitTeam(0, new string[] { "Ian Botham",
                                   "David Gower" });
            int bowlerId = _keeper.ChangeBowler("Michael Holding");
            event_sequence(TWO_RUNS, ONE_RUN, BOWLED);
            var (balls, maidens, runs, wickets) = _keeper.GetBowlerStats(0, "Michael Holding");
            Assert.Equal(3, balls);
            Assert.Equal(0, maidens);
            Assert.Equal(3, runs);
            Assert.Equal(1, wickets);

        }

        [Fact]
        void test_multiple_bowlers()
        {
            event_sequence(THREE_RUNS, FOUR_RUNS, RUN_OUT);
            _keeper.EndOfOver();
            int bowlerid = _keeper.ChangeBowler("Joel Garner");
            event_sequence(NO_RUNS, NO_RUNS, NO_RUNS);
            _keeper.EndOfOver();
            var (balls, maidens, runs, wickets) = _keeper.GetBowlerStats(0, "Michael Holding");
            Assert.Equal(3, balls);
            Assert.Equal(0, maidens);
            Assert.Equal(7, runs);
            Assert.Equal(1, wickets);
            (balls, maidens, runs, wickets) = _keeper.GetBowlerStats(0, "Joel Garner");
            Assert.Equal(3, balls);
            Assert.Equal(1, maidens);
            Assert.Equal(0, runs);
            Assert.Equal(0, wickets);
        }


        [Fact]
        void _test_full_innings()
        {
            event_sequence(NO_RUNS, NO_RUNS, NO_RUNS, NO_RUNS, NO_RUNS, NO_RUNS);
            _keeper.EndOfOver();
            int bowlerid = _keeper.ChangeBowler("Joel Garner");
            event_sequence(NO_RUNS, NO_RUNS, NO_RUNS, ONE_RUN, TWO_RUNS, THREE_RUNS);
            _keeper.EndOfOver();
            bowlerid = _keeper.ChangeBowler("Michael Holding");
            event_sequence(NO_RUNS, NO_RUNS, NO_RUNS, ONE_RUN, TWO_RUNS, THREE_RUNS);
            _keeper.EndOfOver();
            bowlerid = _keeper.ChangeBowler("Joel Garner");
            event_sequence(SIX_RUNS, BOWLED, NO_RUNS, THREE_RUNS, NO_BALL, TWO_RUNS, THREE_RUNS);
            _keeper.EndOfOver();
            bowlerid = _keeper.ChangeBowler("Michael Holding");
            event_sequence(THREE_BYES, LBW, NO_RUNS, FOUR_RUNS, FOUR_RUNS, NO_RUNS);
            _keeper.EndOfOver();
            bowlerid = _keeper.ChangeBowler("Joel Garner");
            event_sequence(TWO_RUNS, CAUGHT, NO_RUNS, THREE_LEGBYES, FOUR_RUNS, TWO_RUNS);
            _keeper.EndOfOver();
            bowlerid = _keeper.ChangeBowler("Michael Holding");
            event_sequence(STUMPED, LBW, NO_RUNS, TWO_RUNS, FOUR_RUNS, NO_RUNS);
            _keeper.EndOfOver();
            bowlerid = _keeper.ChangeBowler("Carl Hooper");
            event_sequence(BOWLED, CAUGHT, NO_RUNS, FOUR_BYES, NO_RUNS, TWO_RUNS);
            _keeper.EndOfOver();
            bowlerid = _keeper.ChangeBowler("Colin Croft");
            event_sequence(STUMPED, LBW, NO_RUNS, TWO_RUNS, FOUR_RUNS, NO_RUNS);
            _keeper.EndOfOver();
            bowlerid = _keeper.ChangeBowler("Carl Hooper");
            event_sequence(TWO_RUNS, BOWLED);
            _keeper.EndOfOver();

            var (balls, maidens, runs, wickets) = _keeper.GetBowlerStats(0, "Michael Holding");
            Assert.Equal(24, balls);
            Assert.Equal(1, maidens);
            Assert.Equal(23, runs);
            Assert.Equal(3, wickets);
            (balls, maidens, runs, wickets) = _keeper.GetBowlerStats(0, "Joel Garner");
            Assert.Equal(18, balls);
            Assert.Equal(0, maidens);
            Assert.Equal(32, runs);
            Assert.Equal(2, wickets);
            (balls, maidens, runs, wickets) = _keeper.GetBowlerStats(0, "Carl Hooper");
            Assert.Equal(8, balls);
            Assert.Equal(0, maidens);
            Assert.Equal(8, runs);
            Assert.Equal(3, wickets);
            (balls, maidens, runs, wickets) = _keeper.GetBowlerStats(0, "Colin Croft");
            Assert.Equal(6, balls);
            Assert.Equal(0, maidens);
            Assert.Equal(6, runs);
            Assert.Equal(2, wickets);

            Assert.Equal(13, _keeper.GetBatsmanScore(0, "Ian Botham"));
            Assert.Equal(12, _keeper.GetBatsmanScore(0, "David Gower"));
            Assert.Equal(3, _keeper.GetBatsmanScore(0, "Michael Vaughan"));
            Assert.Equal(14, _keeper.GetBatsmanScore(0, "Kevin Pietersen"));
            Assert.Equal(0, _keeper.GetBatsmanScore(0, "Freddie Flintoff"));
            Assert.Equal(0, _keeper.GetBatsmanScore(0, "Jos Buttler"));
            Assert.Equal(6, _keeper.GetBatsmanScore(0, "Ben Stokes"));
            Assert.Equal(0, _keeper.GetBatsmanScore(0, "Johnny Bairstow"));
            Assert.Equal(4, _keeper.GetBatsmanScore(0, "Stuart Broad"));
            Assert.Equal(0, _keeper.GetBatsmanScore(0, "Graham Swann"));
            Assert.Equal(6, _keeper.GetBatsmanScore(0, "James Anderson"));
        }


        // ****************
        // Helper Functions
        // ****************
        void event_sequence(params outcomes[] sequence)
        {
            foreach (var gameEvent in sequence)
            {
                _keeper.HandleEvent(gameEvent);
            }
        }

        void assert_score(int expectedRuns, int expectedWickets, int expectedBalls, int expectedMaidens)
        {
            var (runs, wickets, balls, maidens) = _keeper.GetInningsScore(0);
            Assert.Equal(expectedRuns, runs);
            Assert.Equal(expectedWickets, wickets);
            Assert.Equal(expectedBalls, balls);
            Assert.Equal(expectedMaidens, maidens);

        }


    }



}