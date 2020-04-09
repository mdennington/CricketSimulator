using CricketSimulator.Model;
using CricketSimulator.Common;
using static CricketSimulator.Common.outcomes;
using Xunit;

namespace CricketSimulatorTests
{
    public class BatsmanTest
    {
        [Fact]
        public void test_playing_ball_results_in_same_outcome()
        {
            MockRandomNumberGenerator mock = new MockRandomNumberGenerator(1);
            outcomes outcome = new Batsman(mock).Play(FOUR_RUNS);
            Assert.Equal(FOUR_RUNS, outcome);
        }

        [Fact]
        public void test_playing_ball_results_in_different_outcome()
        {
            MockRandomNumberGenerator mock = new MockRandomNumberGenerator(0);
            outcomes outcome = new Batsman(mock).Play(FOUR_RUNS);
            Assert.Equal(NO_RUNS, outcome);
        }
    }
}
