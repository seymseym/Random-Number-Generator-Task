using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskQuestion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

/*
 * Test Routine Algorithm:
 * 
 * Step 1: Take the given number-probability inputs as pairs list.
 * Step 2: Create a dictionary for the random number generator outputs.
 * Step 3: Round the double KeyValuePair Values to be able to make an equality comparison between probabilities.
 * Step 4: Check if the given probability values are equal to the output dictonary values.
 * Step 5: Test is successful if the given and the generated probabilities are the same.
 *
 * Note:
 * If total sum of the given probabilities are not 1:
 * Create an expected output pair list with the expected probabilities. (Which the Weigths are preserved)
 * Check if the expected probability values are equal to the output dictonary values.
 *
 */


namespace TaskQuestion.Tests
{
    [TestClass()]
    public class RandomNumberGeneratorTests
    {
        [TestMethod()]
        public void GeneratedProbabilityValues_ShouldMatch_GivenProbabilityValues_WhenProbabilitySumIs1()
        {
            RandomNumberGenerator Generator = new RandomNumberGenerator();
            var inputPairsList = new List<KeyValuePair<int, double>>()
            {
                new KeyValuePair<int, double>(1, 0.2),
                new KeyValuePair<int, double>(3, 0.3),
                new KeyValuePair<int, double>(9, 0.5),
            };
            Generator.AddPairs(inputPairsList);

            int testCount = 100000;
            Dictionary<int, double> test = new Dictionary<int, double>();

            for (int i = 0; i < testCount; i++)
            {
                int random = Generator.GenerateRandomNumber();

                if (test.ContainsKey(random))
                {
                    double probability = test[random];
                    probability += 1.0 / testCount;
                    test[random] = probability;
                }
                else
                {
                    test.Add(random, 1.0 / testCount);
                }
            }
            var orderedPairs = test.OrderBy(d => d.Key);
            string probability1 = "";
            string probability2 = "";

            foreach (var keyValuePair in orderedPairs)
            {
                probability1 = string.Format("{0:0.00}", Math.Round(keyValuePair.Value, 2).ToString());
            }
            for (int i = 0; i < test.Count; i++)
            {
                probability2 = string.Format("{0:0.00}", Math.Round(inputPairsList[i].Value, 2).ToString());
            }
            Assert.AreEqual(probability1, probability2);
        }

        [TestMethod()]
        public void GeneratedProbabilityValues_ShouldMatch_GivenProbabilityValues_WhenProbabilitySumLessThan1()
        {
            RandomNumberGenerator Generator = new RandomNumberGenerator();
            var inputPairsList = new List<KeyValuePair<int, double>>()
            {
                new KeyValuePair<int, double>(1, 0.2),
                new KeyValuePair<int, double>(3, 0.3),
            };

            var outputPairsList = new List<KeyValuePair<int, double>>()
            {
                new KeyValuePair<int, double>(1, 0.4),
                new KeyValuePair<int, double>(3, 0.6),
            };

            Generator.AddPairs(inputPairsList);

            int testCount = 100000;
            Dictionary<int, double> outputDictionary = new Dictionary<int, double>();

            for (int i = 0; i < testCount; i++)
            {
                int random = Generator.GenerateRandomNumber();

                if (outputDictionary.ContainsKey(random))
                {
                    double probability = outputDictionary[random];
                    probability += 1.0 / testCount;
                    outputDictionary[random] = probability;
                }
                else
                {
                    outputDictionary.Add(random, 1.0 / testCount);
                }
            }
            var orderedPairs = outputDictionary.OrderBy(d => d.Key);
            string probability1 = "";
            string probability2 = "";

            foreach (var keyValuePair in orderedPairs)
            {
                probability1 = string.Format("{0:0.00}", Math.Round(keyValuePair.Value, 2).ToString());
            }
            for (int i = 0; i < outputDictionary.Count; i++)
            {
                probability2 = string.Format("{0:0.00}", Math.Round(outputPairsList[i].Value, 2).ToString());
            }
            Assert.AreEqual(probability1, probability2);
        }

        [TestMethod()]
        public void GeneratedProbabilityValues_ShouldMatch_GivenProbabilityValues_WhenProbabilitySumMoreThan1()
        {
            RandomNumberGenerator Generator = new RandomNumberGenerator();
            var inputPairsList = new List<KeyValuePair<int, double>>()
            {
                new KeyValuePair<int, double>(1, 3),
                new KeyValuePair<int, double>(3, 3),
            };

            var outputPairsList = new List<KeyValuePair<int, double>>()
            {
                new KeyValuePair<int, double>(1, 0.5),
                new KeyValuePair<int, double>(3, 0.5),
            };

            Generator.AddPairs(inputPairsList);

            int testCount = 100000;
            Dictionary<int, double> outputDictionary = new Dictionary<int, double>();

            for (int i = 0; i < testCount; i++)
            {
                int random = Generator.GenerateRandomNumber();

                if (outputDictionary.ContainsKey(random))
                {
                    double probability = outputDictionary[random];
                    probability += 1.0 / testCount;
                    outputDictionary[random] = probability;
                }
                else
                {
                    outputDictionary.Add(random, 1.0 / testCount);
                }
            }
            var orderedPairs = outputDictionary.OrderBy(d => d.Key);
            string probability1 = "";
            string probability2 = "";

            foreach (var keyValuePair in orderedPairs)
            {
                probability1 = string.Format("{0:0.00}", Math.Round(keyValuePair.Value, 2).ToString());
            }
            for (int i = 0; i < outputDictionary.Count; i++)
            {
                probability2 = string.Format("{0:0.00}", Math.Round(outputPairsList[i].Value, 2).ToString());
            }
            Assert.AreEqual(probability1, probability2);
        }
    }
}