using CricketSimulator.Model;
using System;

namespace CricketSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            IRandomNumberGenerator randomNumGen = new GetRandomNumber();
            Game thisGame = new Game(randomNumGen);
            Scorekeeper keeper = new Scorekeeper();
            Umpire matchUmpire = new Umpire(thisGame, keeper);
            matchUmpire.Start();

            // Display Final Scores
            int inningsNum = keeper.NumberInnings();
            for (int i = 0; i < inningsNum; i++)
            {
                int runs = keeper.RunsScored(i);
                int wickets = keeper.WicketsLost(i);
                int overs = keeper.OversBowled(i);
                Console.WriteLine($"Innings {i}: Runs {runs} for {wickets} in {overs} overs. ");
            }
        }
    }
}
