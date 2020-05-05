using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CricketSimulatorUI.Models;
using CricketSimulator.Model;
using static CricketSimulator.Model.InningsMap;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace CricketSimulatorUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Play()
        {
            // Play Game
            IRandomNumberGenerator randomNumGen = new GetRandomNumber();
            Scorekeeper keeper = new Scorekeeper(ONE_DAY);
            Game game = new Game(randomNumGen);
            string[] team1 = new string[] {"Ian Botham", "Mike Gatting", "David Gower", "Joe Root", "Jos Buttler", "Ben Stokes",
                                  "Johnny Bairstow", "Stuart Broad", "Graham Swann", "Jofra Archer", "Jimmy Anderson" };
            string[] team2 = new string[] {"Michael Vaughan", "Alistair Cook", "Viv Richards", "Freddie Flintoff", "Geoff Miller", "Nasser Hussain",
                                  "Alex Stewart", "Darren Gough", "Bob Willis", "Phil Tuffnell", "Mike Hendrick" };
            
            keeper.ChangeBowler("Malcolm Marshall");
            keeper.SubmitTeam(0, team1);
            keeper.SubmitTeam(1, team2);

            Umpire umpey = new Umpire(game, keeper);
            umpey.playInnings();
            keeper.StartInnings(1);
            keeper.ChangeBowler("Ian Botham");
            umpey.playInnings();


            //// Map Scores to View Model
            PlayViewModel pvm = new PlayViewModel();
            var (r1,w1, b1, e1) = keeper.GetInningsScore(0);
            var (r2, w2, b2, e2) = keeper.GetInningsScore(1);


            pvm.runs = new int[2] { r1,r2 };
            pvm.wickets = new int[2] { w1,w2 };
            pvm.overs = new int[2] { b1, b2};
            pvm.extras = new int[2] { e1, e2 };

            pvm.team1 = team1;
            pvm.team2 = team2;

            pvm.scores1 = new int[11];
            pvm.scores2 = new int[11];

            for (int i = 0; i<11; i++)
            {
                pvm.scores1[i] = keeper.GetBatsmanScore(0, team1[i]);
                pvm.scores2[i] = keeper.GetBatsmanScore(1, team2[i]);
            }

            return View(pvm);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
