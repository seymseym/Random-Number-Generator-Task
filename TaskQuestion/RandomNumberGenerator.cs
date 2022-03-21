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

            foreach (var keyValuePair in _probabilityDict)
            {
                if (rand * _cdfSum <= _cumulativeSumList[index])
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
