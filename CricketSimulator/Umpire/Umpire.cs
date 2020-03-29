using System;
using System.Collections.Generic;
using System.Text;

namespace CricketSimulator.Model
{
    class Umpire : AggregateRoot
    {
        private Game thisGame;
        private Scorekeeper thisScorekeeper;

        public Umpire(Game game, Scorekeeper scorekeeper)
        {
            thisGame = game;
            thisScorekeeper = scorekeeper;
        }
        public void Start()
        {
            // Set Up First innings
            thisScorekeeper.ResetScoreboard();
            thisScorekeeper.StartInnings();

            bool endOfInnings = false;

            while (!endOfInnings)
            { 
                
                Over thisOver = new Over(6);
                outcomes outcome;
                bool endOfOver = false;
                do
                {
                    outcome = thisGame.Play();
                    endOfOver = thisOver.CountBalls(outcome);
                    endOfInnings = thisScorekeeper.BallOutcome(outcome);
                } while (!endOfOver && !endOfInnings);
            }
            
            
            

            
        }

    }
}
