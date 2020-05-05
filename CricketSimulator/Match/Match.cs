using System;
using System.Collections.Generic;
using System.Text;
using static CricketSimulator.Model.InningsMap;

namespace CricketSimulator.Model
{
    public class Match
    {
        private Team _team1;
        private Team _team2;

        public Match()
        {

        }

        // Coordinating Class for a Match 
        public Match( string[] team1, string[] team2)
        {
            _team1 = new Team(team1);
            _team2 = new Team(team2);

        }

        public void Play(Umpire umpire)
        {

            // Call Umpire to start game
            umpire.playInnings();

        }
    }

}
