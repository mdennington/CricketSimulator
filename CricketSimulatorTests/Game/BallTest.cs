using CricketSimulator.Model;
using CricketSimulator.Common;
using Xunit;

namespace CricketSimulatorTests
{


    public class BallTest
    {
        [Fact]
        public void test_a_valid_event_is_returned()
        {
            MockRandomNumberGenerator mock = new MockRandomNumberGenerator(2);
            Ball ball = new Ball(mock);
            outcomes outcome = ball.Bowl();
            Assert.Equal(2, (int)outcome);   
        }


    }
}
