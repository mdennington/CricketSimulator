using System;
using System.Collections.Generic;
using System.Text;

namespace CricketSimulator.Common
{

    public enum outcomes
    {
        NO_RUNS,
        ONE_RUN,
        TWO_RUNS,
        THREE_RUNS,
        FOUR_RUNS,
        SIX_RUNS,
        BOWLED,
        LBW,
        CAUGHT,
        STUMPED,
        RUN_OUT,
        NO_BALL,
        WIDE,
        TWO_WIDES,
        THREE_WIDES,
        FOUR_WIDES,
        ONE_BYE,
        TWO_BYES,
        THREE_BYES,
        FOUR_BYES,
        ONE_LEGBYE,
        TWO_LEGBYES,
        THREE_LEGBYES,
        FOUR_LEGBYES
    }

    public static class ScoreMap
    {

        public static readonly Dictionary<outcomes, int> runs = new Dictionary<outcomes, int>
        { { outcomes.ONE_RUN, 1 },
          { outcomes.TWO_RUNS, 2 },
          { outcomes.THREE_RUNS, 3 },
          { outcomes.FOUR_RUNS, 4 },
          { outcomes.SIX_RUNS, 6 }
          };

        public static readonly Dictionary<outcomes, int> extras = new Dictionary<outcomes, int>
        { { outcomes.ONE_BYE, 1 },
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

        public static readonly List<outcomes> wickets = new List<outcomes>
        { { outcomes.BOWLED },
          { outcomes.LBW },
          { outcomes.CAUGHT },
          { outcomes.STUMPED },
          { outcomes.RUN_OUT }
          };

        public static readonly List<outcomes> changeEnds = new List<outcomes>
        { outcomes.ONE_RUN,
          outcomes.THREE_RUNS,
          outcomes.ONE_BYE,
          outcomes.THREE_BYES,
          outcomes.ONE_LEGBYE,
          outcomes.THREE_LEGBYES,
          outcomes.TWO_WIDES};
    }
}
