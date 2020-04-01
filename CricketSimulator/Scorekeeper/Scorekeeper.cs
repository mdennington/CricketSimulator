using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace CricketSimulator.Model
{
    public class Scorekeeper : AggregateRoot
    {
        // to do - build factory pattern to create ScoreKeeper
        Scorecard _card;

        public Scorekeeper()
        {
            _card = new Scorecard();
        }
        // Handle ball outcome
        public virtual bool BallOutcome(outcomes outcome)
        {
            _card.RunsScored(outcome);
            return _card.WicketFallen(outcome);
        }
        // Handle end of over
        public virtual bool EndOfOver()
        {
            _card.EndOfOver();
            return true;
        }


        // Handle end of innings
        public virtual bool StartInnings()
        {
            _card.StartInnings();
            return true;
        }

        // Reset Scoreboard
        public bool ResetScoreboard()
        {
            _card.Reset();
            return true;
        }

        
        public int NumberInnings()
        {
            return _card.Innings();
        }

        public int RunsScored(int innings)
        {
            return _card.Runs(innings);
        }

        public int WicketsLost(int innings)
        {
            return _card.Wickets(innings);
        }
        public int OversBowled(int innings)
        {
            return _card.Overs(innings);
        }


    }
}
