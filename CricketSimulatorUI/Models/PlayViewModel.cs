using Microsoft.Extensions.Localization.Internal;
using Microsoft.JSInterop;
using System;

namespace CricketSimulatorUI.Models
{
    public class PlayViewModel
    {
        public int[] runs;
        public int[] wickets;
        public int[] overs;

        public string[] team1;
        public string[] team2;
        public int[] scores1;
        public int[] scores2;

        public int[] extras;

    }
}
