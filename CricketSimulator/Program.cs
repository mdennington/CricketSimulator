using CricketSimulator.Model;
using System;

namespace CricketSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            Game thisGame = new Game();
            Scorekeeper keeper = new Scorekeeper();
            Umpire matchUmpire = new Umpire(thisGame, keeper);
            matchUmpire.Start();

        }
    }
}
