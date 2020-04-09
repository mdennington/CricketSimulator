using CricketSimulator.Model;
using CricketSimulator.Common;
using static CricketSimulator.Common.outcomes;
using Moq;
using System.Diagnostics.Tracing;
using Xunit;

namespace CricketSimulatorTests
{
    public class BowlerTest
    {

        [Fact]
        public void test_bowling_a_ball_returns_an_outcome()
        {
            Mock<Ball> mockBall;
            Bowler bowler = new Bowler();
            mockBall = new Mock<Ball>();
            mockBall.Setup(m => m.Bowl()).Returns(()=>BOWLED);
            outcomes outcome = bowler.Bowl((Ball)mockBall.Object);
            Assert.Equal(BOWLED, outcome);
        }
    }
}

