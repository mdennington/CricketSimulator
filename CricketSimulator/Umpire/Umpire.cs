using System;
using System.Collections.Generic;
using System.Text;
using CricketSimulator.Common;

namespace CricketSimulator.Model
{
    public class Umpire : AggregateRoot
    {
        private Game thisGame;
        private Scorekeeper thisScorekeeper;

        public Umpire()
        {

        }

        public Umpire(Game game, Scorekeeper scorekeeper)
        {
            thisGame = game;
            thisScorekeeper = scorekeeper;
        }


        public virtual void playInnings()
        {
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
                    if (endOfOver) thisScorekeeper.EndOfOver();
                    endOfInnings = thisScorekeeper.HandleEvent(outcome);
                } while (!endOfOver && !endOfInnings);
            }
        }

    }
}
