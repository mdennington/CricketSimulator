using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CricketSimulatorUI.Models;
using CricketSimulator.Model;

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
            Scorekeeper keeper = new Scorekeeper();
            Game game = new Game(randomNumGen);
            Umpire umpey = new Umpire(game, keeper);
            umpey.Start();

            // Map Scores to View Model
            PlayViewModel pvm = new PlayViewModel();
            pvm.runs = new int[2] { keeper.RunsScored(0), keeper.RunsScored(1) };
            pvm.wickets = new int[2] { keeper.WicketsLost(0), keeper.WicketsLost(1) };
            pvm.overs = new int[2] { keeper.OversBowled(0), keeper.OversBowled(1) };

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
