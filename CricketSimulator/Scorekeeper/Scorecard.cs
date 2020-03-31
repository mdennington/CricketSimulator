using CricketSimulator.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace CricketSimulator.Model
{
    public class Scorecard : Entity
    {
        List<Innings> match = new List<Innings>();
        private int _currentInnings;
        private Dictionary<outcomes, int> ScoreMap = new Dictionary<outcomes, int>
        { { outcomes.ONE_RUN, 1 },
          { outcomes.TWO_RUNS, 2 },
          { outcomes.THREE_RUNS, 3 },
          { outcomes.FOUR_RUNS, 4 },
          { outcomes.SIX_RUNS, 6 },
          { outcomes.ONE_BYE, 1 },
          { outcomes.TWO_BYES, 2 },
          { outcomes.THREE_BYES, 3 },
          { outcomes.FOUR_BYES, 4 },
          { outcomes.ONE_LEGBYE, 1 },
          { outcomes.TWO_LEGBYES, 2 },
          { outcomes.THREE_LEGBYES, 3 },
          { outcomes.FOUR_LEGBYES, 4 },
          { outcomes.NO_BALL, 1 },
          { outcomes.WIDE, 1 },
          { outcomes.TWO_WIDES, 2 },
          { outcomes.THREE_WIDES, 3 },
          { outcomes.FOUR_WIDES, 4 },
          };
        private List<outcomes> WicketList = new List<outcomes>
        { { outcomes.BOWLED },
          { outcomes.LBW },
          { outcomes.CAUGHT },
          { outcomes.STUMPED },
          { outcomes.RUN_OUT }
          };


        public Scorecard()
        {
            _currentInnings = 0;
        }

        public int StartInnings()
        {
            Innings innings = new Innings();
            match.Add(innings);
            match[_currentInnings].runs = 0;
            match[_currentInnings].wickets = 0;
            match[_currentInnings].overs = 0;
            _currentInnings++;
            return _currentInnings;
        }

        public bool WicketFallen(outcomes outcome)
        {
            if (WicketList.Contains(outcome))
            {
                match[_currentInnings - 1].wickets++;
            }
            
            // Check if end of innings
            if (match[_currentInnings - 1].wickets == 10) return true;

            return false;
            
        }

        public void RunsScored(outcomes outcome)
        {

            // translate outcomes into runs
            if (ScoreMap.ContainsKey(outcome))
            {
                int newRuns = ScoreMap[outcome];
                match[_currentInnings - 1].runs += newRuns;
            }

        }

        public void EndOfOver()
        {
            match[_currentInnings - 1].overs++;
        }

        // todo : write unit test for reset()
        public void Reset()
        {
            foreach (Innings inning in match)
            {
                inning.runs = 0;
                inning.wickets = 0;
                inning.overs = 0;
            }
        }

        public int Runs(int inningsNum)
        {
            return match[inningsNum].runs;
        }

        public int Wickets(int inningsNum)
        {
            return match[inningsNum].wickets;
        }

        public int Overs(int inningsNum)
        {
            return match[inningsNum].overs;
        }
        
        public int Innings()
        {
            return _currentInnings;
        }
    }


}
