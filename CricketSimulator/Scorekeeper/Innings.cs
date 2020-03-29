using CricketSimulator.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CricketSimulator.Model
{
    class Innings : Entity
    {
        public int runs { get; set; }
        public int wickets { get; set; }
        public int overs { get; set; }

        public Innings()
        {
            runs = 0;
            wickets = 0;
            overs = 0;
        }

    }
}
