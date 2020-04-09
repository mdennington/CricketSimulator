using Xunit;
using CricketSimulator.Model;
using CricketSimulator.Common;
using static CricketSimulator.Common.outcomes;

namespace CricketSimulatorTests
{

    public class OverTest
    {
        [Fact]
        public void test_over_increments()
        {
            Over thisOver = new Over(3);
            bool retVal = thisOver.CountBalls(NO_RUNS);
            Assert.False(retVal);
            retVal = thisOver.CountBalls(NO_RUNS);
            Assert.False(retVal);
            retVal = thisOver.CountBalls(NO_RUNS);
            Assert.True(retVal);
            Assert.Equal(3, thisOver.BallCount);
        }

        [Fact]
        public void test_extras_adds_extra_ball()
        {
            Over thisOver = new Over(3);
            bool retVal = thisOver.CountBalls(NO_RUNS);
            Assert.False(retVal);
            retVal = thisOver.CountBalls(NO_RUNS);
            Assert.False(retVal);
            retVal = thisOver.CountBalls(WIDE);
            Assert.False(retVal);
            retVal = thisOver.CountBalls(TWO_WIDES);
            Assert.False(retVal);
            retVal = thisOver.CountBalls(THREE_WIDES);
            Assert.False(retVal);
            retVal = thisOver.CountBalls(FOUR_WIDES);
            Assert.False(retVal);
            retVal = thisOver.CountBalls(NO_BALL);
            Assert.False(retVal);
            retVal = thisOver.CountBalls(NO_RUNS);
            Assert.True(retVal);
        }


    }
}