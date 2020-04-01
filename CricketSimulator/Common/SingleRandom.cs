using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace CricketSimulator.Model
{
    public interface IRandomNumberGenerator
    {
        int GetRandomNumber( int minNumber, int maxNumber);
    }

    public class SingleRandom : Random
    {
        static SingleRandom _instance;
        public static SingleRandom Instance
        { get
            {
                if (_instance == null) _instance = new SingleRandom();
                return _instance;
            }
        }

        private SingleRandom() { }
    }

    public class GetRandomNumber : IRandomNumberGenerator
    {
        int IRandomNumberGenerator.GetRandomNumber(int minNumber, int maxNumber)
        {
            SingleRandom rand = SingleRandom.Instance;
            int ReturnVal = rand.Next(minNumber, maxNumber);
            return ReturnVal;
        }
    }

}
 