using CricketSimulator.Model;
using CricketSimulator.Common;
using static CricketSimulator.Common.outcomes;
using System.Diagnostics.Tracing;
using Xunit;


namespace CricketSimulatorTests
{
    public class GameTests
    {
        [Fact]
        public void test_one_run()
        {
            MockRandomNumberGenerator mock = new MockRandomNumberGenerator(1);
            Game thisGame = new Game(mock);
            outcomes outcomeFinal = thisGame.Play();

            Assert.Equal(ONE_RUN, outcomeFinal);
        }

        [Fact]
        public void test_no_run()
        {
            MockRandomNumberGenerator mock = new MockRandomNumberGenerator(1);
            Game thisGame = new Game(mock);
            outcomes outcomeFinal = thisGame.Play();

            Assert.Equal(ONE_RUN, outcomeFinal);
        }
    }
}
