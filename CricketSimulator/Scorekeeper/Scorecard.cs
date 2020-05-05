using CricketSimulator.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace CricketSimulator.Model
{

    public class Scorecard : Entity
    {
        public List<Innings> MatchCard { get; set; }
 
        public Scorecard(int numInnings)
            {
                MatchCard = new List<Innings>();
                for (int i = 0; i < numInnings; i++)
                {
                    MatchCard.Add(new Innings());
                }

            }
        }


}

public class Innings
{
    public Innings()
    {
        bowlerList = new List<BowlerData>();
    }
    public List<BowlerData> bowlerList { get; set; }
    public BatsmanData[] batsmenList{ get; set; }

    public int runs { get; set; }
    public int wickets { get; set; }
    public int balls { get; set; }
    public int maidens { get; set; }
    public int extras { get; set; }

}

public class BowlerData
{
    public int balls { get; set; }
    public int maidens { get; set; }
    public int runs { get; set; }
    public int wickets { get; set; }
    public string name { get; set; }

}

public class BatsmanData
{
    public int runs { get; set; }
    public string name { get; set; }
}
