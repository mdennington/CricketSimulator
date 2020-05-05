using Xunit;
using CricketSimulator.Model;
using CricketSimulator.Common;
using static CricketSimulator.Common.outcomes;
using static CricketSimulator.Model.InningsMap;
using Moq;
using System.Collections.Generic;

namespace CricketSimulatorTests
{
    public class UmpireTests
    {

        [Fact]
        void test_play_ball()
        {
            // Arrange
            Mock<Game> thisGame = new Mock<Game>();
            Mock<Scorekeeper> thisScorekeeper = new Mock<Scorekeeper>(ONE_DAY);

            thisGame.Setup(x => x.Play()).Returns(TWO_RUNS);
            thisScorekeeper.Setup(x => x.HandleEvent(It.IsAny<outcomes>())).Returns(true);  // end of innings - true

            // Act
            Umpire thisUmpire = new Umpire(thisGame.Object, thisScorekeeper.Object);
            thisUmpire.playInnings();

            // Assertions
            thisGame.Verify(x => x.Play(), Times.Exactly(1));
            thisScorekeeper.Verify(x => x.HandleEvent(It.IsAny<outcomes>()), Times.Exactly(1));
            thisScorekeeper.Verify(x => x.EndOfOver(), Times.Never());
        }

        [Fact]
        void test_play_full_over()
        {
            // need to enqueue six returns from Play (called six times)
            // Arrange
            Mock<Game> thisGame = new Mock<Game>();
            Mock<Scorekeeper> thisScorekeeper = new Mock<Scorekeeper>(ONE_DAY);

            thisGame.Setup(x => x.Play()).Returns(TWO_RUNS);
            Queue<bool> results = new Queue<bool>(new bool[] { false, false, false, false, false, true });
            thisScorekeeper.Setup(x => x.HandleEvent(It.IsAny<outcomes>())).Returns(()=>results.Dequeue());  // end of innings - true

            // Act
            Umpire thisUmpire = new Umpire(thisGame.Object, thisScorekeeper.Object);
            thisUmpire.playInnings();

            // Assertions
            thisGame.Verify(x => x.Play(), Times.Exactly(6));
            thisScorekeeper.Verify(x => x.HandleEvent(It.IsAny<outcomes>()), Times.Exactly(6));
            thisScorekeeper.Verify(x => x.EndOfOver(), Times.Exactly(1));

        }


    }

}


