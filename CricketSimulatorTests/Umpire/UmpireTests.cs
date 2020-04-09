using Xunit;
using CricketSimulator.Model;
using CricketSimulator.Common;
using static CricketSimulator.Common.outcomes;
using Moq;
using System.Collections.Generic;

namespace CricketSimulatorTests
{
    public class UmpireTests
    {
        [Fact]
        public void test_umpire_initialises()
        {
            // Create Umpire with mock game and mock scoreboard
            Mock<Game> mockGame;
            Mock<Scorekeeper> mockScorekeeper;
            mockGame = new Mock<Game>();
            mockScorekeeper = new Mock<Scorekeeper>();
            mockGame.Setup(m => m.Play()).Returns(BOWLED);
            mockScorekeeper.Setup(m => m.EndOfOver()).Returns(true);
            // Set up returns true-endOfInnings
            var ballOutcomes = new Queue<bool>();
            ballOutcomes.Enqueue(false);
            ballOutcomes.Enqueue(false);
            ballOutcomes.Enqueue(true);
            ballOutcomes.Enqueue(false);
            ballOutcomes.Enqueue(true);
            mockScorekeeper.Setup(m => m.BallOutcome(It.IsAny<outcomes>())).Returns(ballOutcomes.Dequeue);
            mockScorekeeper.Setup(m => m.StartInnings()).Returns(true);

            Umpire thisUmpire = new Umpire(mockGame.Object, mockScorekeeper.Object);
            thisUmpire.Start();

            // Assert 
            mockGame.Verify(m => m.Play(), Times.Exactly(5));
            mockScorekeeper.Verify(m => m.BallOutcome(It.IsAny<outcomes>()), Times.Exactly(5));
            mockScorekeeper.Verify(m => m.StartInnings(), Times.Exactly(2));
            mockScorekeeper.Verify(m => m.EndOfOver(), Times.Never);


        }

        [Fact]
        public void test_umpire_end_of_over()
        {
            // Create Umpire with mock game and mock scoreboard
            Mock<Game> mockGame;
            Mock<Scorekeeper> mockScorekeeper;
            mockGame = new Mock<Game>();
            mockScorekeeper = new Mock<Scorekeeper>();
            mockGame.Setup(m => m.Play()).Returns(BOWLED);
            mockScorekeeper.Setup(m => m.EndOfOver()).Returns(true);
            // Set up returns true-endOfInnings
            var ballOutcomes = new Queue<bool>();
            ballOutcomes.Enqueue(false);
            ballOutcomes.Enqueue(false);
            ballOutcomes.Enqueue(false);
            ballOutcomes.Enqueue(false);
            ballOutcomes.Enqueue(false);
            ballOutcomes.Enqueue(false);
            ballOutcomes.Enqueue(true);
            ballOutcomes.Enqueue(true);
            mockScorekeeper.Setup(m => m.BallOutcome(It.IsAny<outcomes>())).Returns(ballOutcomes.Dequeue);
            mockScorekeeper.Setup(m => m.StartInnings()).Returns(true);

            Umpire thisUmpire = new Umpire(mockGame.Object, mockScorekeeper.Object);
            thisUmpire.Start();

            // Assert 
            mockScorekeeper.Verify(m => m.EndOfOver(), Times.Exactly(1));


        }
    }
}


