using CricketSimulator.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CricketSimulatorTests
{
    class MockRandomNumberGenerator : IRandomNumberGenerator
    {
        int _value;

        public MockRandomNumberGenerator(int value)
        {
            _value = value;
        }
        public int GetRandomNumber(int minNumber, int maxNumber)
        {
            return (_value);
        }
    }
}
