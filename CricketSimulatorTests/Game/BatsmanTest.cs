using CricketSimulator.Model;
using Xunit;

namespace CricketSimulatorTests
{
    public class BatsmanTest
    {
        [Fact]
        public void test_playing_ball_results_in_same_outcome()
        {
            MockRandomNumberGenerator mock = new MockRandomNumberGenerator(1);
            outcomes outcome = new Batsman(mock).Play(outcomes.FOUR_RUNS);
            Assert.Equal(outcomes.FOUR_RUNS, outcome);
        }

        [Fact]
        public void test_playing_ball_results_in_different_outcome()
        {
            MockRandomNumberGenerator mock = new MockRandomNumberGenerator(0);
            outcomes outcome = new Batsman(mock).Play(outcomes.FOUR_RUNS);
            Assert.Equal(outcomes.NO_RUNS, outcome);
        }
    }
}
