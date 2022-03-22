using System;
using System.Collections.Generic;
using System.Text; 

namespace TaskQuestion
{
    public class RandomNumberGenerator
    {
        #region Fields 
        private IDictionary<int, double> _probabilityDict;
        private List<double> _cumulativeSumList;
        private double _cdfSum;
        private Random _random;
        #endregion

        #region Constructor
        public RandomNumberGenerator()
        {
            _probabilityDict = new Dictionary<int, double>();
            _cumulativeSumList = new List<double>();
            _random = new Random();
        }
        #endregion

        #region Methods 
        public int GenerateRandomNumber()
        {
            double rand = _random.NextDouble();
            int index = 0;

            // For ex: numbers: 1,2,3 ---> probs:| 0.2 | 0.3 | 0.5 |
            //                             cdf  :| 0.2 | 0.5 |  1  |  
            //                                   |     |     |     |
            // Generated Random Values           |     |     |     |
            // random [0, 1) ---> 0.1 ---------->| 1   |     |     |
            // random [0, 1) ---> 0.3 ---------->|     |  2  |     |
            // random [0, 1) ---> 0.7 ---------->|     |     |  3  |
            // ----------------------------------------------------
            //After calling 10.000 times ------->| 11  | 222 | 33333 (proportionally)



            foreach (var keyValuePair in _probabilityDict)
            {
                // Feed uniformly distributed random numbers from 0..1 interval
                // into the inverse cumulative distribution function.

                if (rand <= _cumulativeSumList[index] / _cdfSum)
                {
                    return keyValuePair.Key;
                }
                index++;
            }
            return 0;
        }
        public void AddPairs(List<KeyValuePair<int, double>> numsAndProbs)
        {
            foreach (var pair in numsAndProbs)
            {
                if (_probabilityDict.TryGetValue(pair.Key, out double oldDist))
                {
                    _probabilityDict.Remove(pair.Key);
                    _cdfSum -= oldDist;
                    _cumulativeSumList.Remove(_cdfSum);
                }
                _probabilityDict.Add(new KeyValuePair<int, double>(pair.Key, pair.Value));
                _cdfSum += pair.Value;
                _cumulativeSumList.Add(_cdfSum);
            }
        }
        #endregion
    }
} 