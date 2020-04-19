using CricketSimulator.Common;
using static CricketSimulator.Common.outcomes;
using static CricketSimulator.Common.ScoreMap;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices.ComTypes;
using System.Net.NetworkInformation;
using System.Linq;

namespace CricketSimulator.Model
{
    public class Innings : Entity
    {
        private static readonly int MAX_BATSMEN = 11;

        
        public int runs { get; set; }
        public int wickets { get; set; }
        public int overs { get; set; }
        public int Striker { get; set; }
        public int nonStriker { get; set; }
        public int currentBowler { get;  set; }
        public int Extras { get; set; }

        private List<BowlingStats> bowlerStats = new List<BowlingStats>();
        private List<string> _names = new List<string>();
        private List<int> _runs = new List<int>();

        public Innings(params string[] batsmen)
        {
            foreach (string batsmanName in batsmen)
            {
                _names.Add(batsmanName);
                _runs.Add(0);
            }

            Striker = 1;
            nonStriker = 2;
            runs = 0;
            wickets = 0;
            overs = 0;
            currentBowler = 0;
            Extras = 0;

        }



        public string GetBatsman(int position)
        {
            return (_names[position - 1]);
        }

        public void RunsScored(outcomes outcome)
        {
            int newRuns = 0;

            if (ScoreMap.runs.ContainsKey(outcome))
            {
                newRuns = ScoreMap.runs[outcome];
                _runs[this.Striker - 1] += (int)newRuns;
                runs += (int)newRuns;
                bowlerStats[currentBowler].runs += newRuns;
            }
            else if (ScoreMap.extras.ContainsKey(outcome))
            {
                newRuns = ScoreMap.extras[outcome];
                runs += (int)newRuns;
                bowlerStats[currentBowler].runs += newRuns;
                Extras += (int)newRuns;
            }

            if (ScoreMap.changeEnds.Contains(outcome))
            {
                int v = Striker;
                Striker = nonStriker;
                nonStriker = v;
            }

        }

        public int GetBatsmanRuns(int position)
        {
            return _runs[position - 1];
        }

        public bool wicketFell(outcomes outcome)
        {

            // Add one to wickets
            wickets++;

            // Calculate next batsman
            if (Striker > nonStriker)
            {
                Striker++;
            }
            else
            {
                Striker = nonStriker + 1;
            }

            // If greater than MAX_BATSMEN return TRUE to signify end of Innings or FALSE to carry on
            if (Striker > MAX_BATSMEN)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public int GetWickets()
        {
            return wickets;
        }

        public void endOfOver()
        {
            bowlerStats[currentBowler].overs++;
            overs++;
        }

        public BowlingStats changeBowler(string bowlerName)
        {
            // Check if Bowler has bowled already
            if( bowlerStats.Any(p => p.name == bowlerName))
            {
                currentBowler = bowlerStats.FindIndex(p => p.name == bowlerName);
                return bowlerStats.First(p => p.name == bowlerName);
            }

            // Add new bowler to list 
            BowlingStats newBowler = new BowlingStats();
            newBowler.name = bowlerName;
            bowlerStats.Add(newBowler);
            currentBowler = bowlerStats.Count - 1;  
            return newBowler;
        }
    }

    public class BowlingStats
    {
        public  string name { get; set; }
        public int runs { get; set; }
        public int overs { get; set; }
        public int wickets { get; set; }
        public int maidens { get; set; }
    }
}
