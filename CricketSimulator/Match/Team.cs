using System;
using System.Collections.Generic;
using System.Text;

namespace CricketSimulator.Model
{
    public class Team
    {
        private List<string> _players;

        public Team(string[] players)
        {
            _players = new List<string>();

            foreach (var player in players)
            {
                _players.Add(player);
            }
        }

        public string Player(int index)
        {
            return (_players[index]);
        }

    }
}
