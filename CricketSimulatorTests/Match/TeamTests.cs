using Xunit;
using CricketSimulator.Model;
using CricketSimulator.Common;
using static CricketSimulator.Common.outcomes;
using Moq;
using System.Collections.Generic;

namespace CricketSimulatorTests
{
    public class TeamTests
    {

        [Fact]
        void test_load_team()
        {
            string[] team1 = new string[] {   "Ian Botham",
                                              "David Gower",
                                              "Michael Vaughan",
                                              "Kevin Pieterson",
                                              "Freddie Flintoff",
                                              "Joe Root",
                                              "Jos Buttler",
                                              "Ben Stokes",
                                              "Stuart Broad",
                                              "Graham Swann",
                                              "James Anderson"
                                              };
            string[] team2 = new string[] {   "Desmond Haynes",
                                              "Gordon Greenidge",
                                              "Viv Richards",
                                              "Alvin Kallicharan",
                                              "Clive Lloyd",
                                              "Carl Hooper",
                                              "David Murray",
                                              "Malcolm Marshall",
                                              "Michael Holding",
                                              "Joel Garner",
                                              "Andy Roberts"
                                              };

            Team firstTeam = new Team(team1);
            Team secondTeam = new Team(team2);

            Assert.Equal("Ian Botham", firstTeam.Player(0));
            Assert.Equal("David Gower", firstTeam.Player(1));
            Assert.Equal("Michael Vaughan", firstTeam.Player(2));
            Assert.Equal("Kevin Pieterson", firstTeam.Player(3));
            Assert.Equal("Freddie Flintoff", firstTeam.Player(4));
            Assert.Equal("Joe Root", firstTeam.Player(5));
            Assert.Equal("Jos Buttler", firstTeam.Player(6));
            Assert.Equal("Ben Stokes", firstTeam.Player(7));
            Assert.Equal("Stuart Broad", firstTeam.Player(8));
            Assert.Equal("Graham Swann", firstTeam.Player(9));
            Assert.Equal("James Anderson", firstTeam.Player(10));

            Assert.Equal("Desmond Haynes", secondTeam.Player(0));
            Assert.Equal("Gordon Greenidge", secondTeam.Player(1));
            Assert.Equal("Viv Richards", secondTeam.Player(2));
            Assert.Equal("Alvin Kallicharan", secondTeam.Player(3));
            Assert.Equal("Clive Lloyd", secondTeam.Player(4));
            Assert.Equal("Carl Hooper", secondTeam.Player(5));
            Assert.Equal("David Murray", secondTeam.Player(6));
            Assert.Equal("Malcolm Marshall", secondTeam.Player(7));
            Assert.Equal("Michael Holding", secondTeam.Player(8));
            Assert.Equal("Joel Garner", secondTeam.Player(9));
            Assert.Equal("Andy Roberts", secondTeam.Player(10));
        }

    }
}
