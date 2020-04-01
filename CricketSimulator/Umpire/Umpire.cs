using System;
using System.Collections.Generic;
using System.Text;

namespace CricketSimulator.Model
{
    public class Umpire : AggregateRoot
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
            playInnings();
            playInnings();

        }

        private void playInnings()
        {
            bool endOfInnings = false;
            thisScorekeeper.StartInnings();
            while (!endOfInnings)
            {

                Over thisOver = new Over(6);
                outcomes outcome;
                bool endOfOver = false;
                do
                {
                    outcome = thisGame.Play();
                    endOfOver = thisOver.CountBalls(outcome);
                    if (endOfOver) thisScorekeeper.EndOfOver();
                    endOfInnings = thisScorekeeper.BallOutcome(outcome);
                } while (!endOfOver && !endOfInnings);
            }
        }

    }
}
