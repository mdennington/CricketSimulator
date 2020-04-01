using Xunit;
using CricketSimulator.Model;

namespace CricketSimulatorTests
{

    public class OverTest
    {
        [Fact]
        public void test_over_increments()
        {
            Over thisOver = new Over(3);
            bool retVal = thisOver.CountBalls(outcomes.NO_RUNS);
            Assert.False(retVal);
            retVal = thisOver.CountBalls(outcomes.NO_RUNS);
            Assert.False(retVal);
            retVal = thisOver.CountBalls(outcomes.NO_RUNS);
            Assert.True(retVal);
            Assert.Equal(3, thisOver.BallCount);
        }

        [Fact]
        public void test_extras_adds_extra_ball()
        {
            Over thisOver = new Over(3);
            bool retVal = thisOver.CountBalls(outcomes.NO_RUNS);
            Assert.False(retVal);
            retVal = thisOver.CountBalls(outcomes.NO_RUNS);
            Assert.False(retVal);
            retVal = thisOver.CountBalls(outcomes.WIDE);
            Assert.False(retVal);
            retVal = thisOver.CountBalls(outcomes.TWO_WIDES);
            Assert.False(retVal);
            retVal = thisOver.CountBalls(outcomes.THREE_WIDES);
            Assert.False(retVal);
            retVal = thisOver.CountBalls(outcomes.FOUR_WIDES);
            Assert.False(retVal);
            retVal = thisOver.CountBalls(outcomes.NO_BALL);
            Assert.False(retVal);
            retVal = thisOver.CountBalls(outcomes.NO_RUNS);
            Assert.True(retVal);
        }


    }
}