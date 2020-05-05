using Xunit;
using CricketSimulator.Model;
using CricketSimulator.Common;
using static CricketSimulator.Common.outcomes;
using System.Collections.Generic;
using Moq;

namespace CricketSimulatorTests
{
    public class MatchTests
    {

        [Fact]
        void test_play_match()
        {
            string[] team1 = new string[] { "P1", "P2" };
            string[] team2 = new string[] { "P3", "P4" };

            Mock<Umpire> umpire = new Mock<Umpire>();

            CricketSimulator.Model.Match thisMatch = new CricketSimulator.Model.Match(team1, team2);
            thisMatch.Play(umpire.Object);

            umpire.Verify(x => x.playInnings(), Times.Exactly(1));

        }

    }
}

