using CricketSimulator.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;


namespace CricketSimulator.Model
{
    public enum InningsMap
    {
        TEST_MATCH = 4,
        ONE_DAY = 2
    }

    /// <summary>
    /// Score Keeper Aggregate which maintains score for the game
    /// </summary>
    public class Scorekeeper : AggregateRoot
    {

        private int _numInnings { get; set; }
        private int _currentInnings;
        private int _runsThisOver;
        private int _striker;
        private int _nonStriker;
        private int _currentBowler;

        Scorecard _card;

        public Scorekeeper(InningsMap matchType)
        {
            _numInnings = (int)matchType;
            _card = new Scorecard(_numInnings);
            _currentInnings = 0;
            _runsThisOver = 0;
            _striker = 0;
            _nonStriker = 1;
        }


        public void SubmitTeam(int inningsNum, string[] batsmenNames)
        {
            int index = 0;
            _card.MatchCard[inningsNum].batsmenList = new BatsmanData[batsmenNames.Length];

            foreach (string batsman in batsmenNames)
            {
                _card.MatchCard[inningsNum].batsmenList[index] = new BatsmanData();
                _card.MatchCard[inningsNum].batsmenList[index].name = batsman;
                _card.MatchCard[inningsNum].batsmenList[index].runs = 0;
                index++;
            }
        }

        public Tuple<int, int, int, int> GetInningsScore(int InningsNum)
        {
            var returnValue = new Tuple<int, int, int, int>(_card.MatchCard[InningsNum].runs,
                                                      _card.MatchCard[InningsNum].wickets,
                                                      _card.MatchCard[InningsNum].balls,
                                                      _card.MatchCard[InningsNum].extras
                                                      );
            return returnValue;
        }

        public virtual bool HandleEvent(outcomes gameEvent)
        {

            int newRuns = 0;

            if (ScoreMap.runs.ContainsKey(gameEvent))
            {
                newRuns = ScoreMap.runs[gameEvent];
                _card.MatchCard[_currentInnings].runs += (int)newRuns;
                _card.MatchCard[_currentInnings].batsmenList[_striker].runs += (int)newRuns;
                _card.MatchCard[_currentInnings].bowlerList[_currentBowler].runs += (int)newRuns;

            }
            else if (ScoreMap.extras.ContainsKey(gameEvent))
            {
                newRuns = ScoreMap.extras[gameEvent];
                _card.MatchCard[_currentInnings].extras += (int)newRuns;
                _card.MatchCard[_currentInnings].runs += (int)newRuns;
                _card.MatchCard[_currentInnings].bowlerList[_currentBowler].runs += (int)newRuns;

                //runsThisOver += newRuns;
            }
            else if (ScoreMap.wickets.Contains(gameEvent))
            {
                _card.MatchCard[_currentInnings].wickets++;
                _card.MatchCard[_currentInnings].bowlerList[_currentBowler].wickets++;


                if (_striker > _nonStriker)
                {
                    _striker++;
                }
                else
                {
                    _striker = _nonStriker + 1;
                }
            }
            if (ScoreMap.changeEnds.Contains(gameEvent))
            {
                int v = _striker;
                _striker = _nonStriker;
                _nonStriker = v;
            }
            _runsThisOver += newRuns;

            if (!ScoreMap.extraBall.Contains(gameEvent))
            {
                _card.MatchCard[_currentInnings].balls++;
                _card.MatchCard[_currentInnings].bowlerList[_currentBowler].balls++;

            }

            return (_card.MatchCard[_currentInnings].wickets == 10);


        }

        public int GetBatsmanScore(int inningsNum, string batsmanName)
        {
            int returnValue = 0;

            foreach (BatsmanData batsman in _card.MatchCard[inningsNum].batsmenList)
            {
                if (batsman.name == batsmanName) returnValue = batsman.runs;
            }

            return returnValue;
        }

        public int GetNumberOfInnings()
        {
            return _numInnings;
        }

        public virtual void EndOfOver()
        {
            if (_runsThisOver == 0)
            {
                _card.MatchCard[_currentInnings].maidens++;
                _card.MatchCard[_currentInnings].bowlerList[_currentBowler].maidens++;
            }

            int v = _striker;
            _striker = _nonStriker;
            _nonStriker = v;

            _runsThisOver = 0;

        }

        public int ChangeBowler(string bowlerName)
        {
            // Check if this is first bowler
            if (_card.MatchCard[_currentInnings].bowlerList.Count != 0)
            {
                // Check if Bowler has bowled already, set bowler index and return Index if so
                if (_card.MatchCard[_currentInnings].bowlerList.Any(p => p.name == bowlerName))
                {
                    _currentBowler = _card.MatchCard[_currentInnings].bowlerList.FindIndex(p => p.name == bowlerName);
                    return _currentBowler;
                }
            }

            // Add new bowler to list and return index
            BowlerData newBowler = new BowlerData();
            newBowler.name = bowlerName;

            _card.MatchCard[_currentInnings].bowlerList.Add(newBowler);

            _currentBowler = _card.MatchCard[_currentInnings].bowlerList.Count - 1;
            return _currentBowler;
        }

        public Tuple<int, int, int, int> GetBowlerStats(int inningsNum, string bowlerName)
        {
            Tuple<int, int, int, int> returnValue = new Tuple<int, int, int, int> 
                                                    (_card.MatchCard[inningsNum].bowlerList.Find(p => p.name == bowlerName).balls,
                                                     _card.MatchCard[inningsNum].bowlerList.Find(p => p.name == bowlerName).maidens,
                                                     _card.MatchCard[inningsNum].bowlerList.Find(p => p.name == bowlerName).runs,
                                                     _card.MatchCard[inningsNum].bowlerList.Find(p => p.name == bowlerName).wickets);

            return returnValue;
        }

        public void StartInnings(int InningsNum)
        {
            _currentInnings = InningsNum;
            _runsThisOver = 0;
            _striker = 0;
            _nonStriker = 1;
        }

    }
}
