using System;
using System.Collections.Generic;
using System.Linq;

/*
 * Task Question:
 * 
 * Please write a function that returns a random value 
 * picked out of a pre-defined set, with an associated probability.
 *
 * Example output:
 *
 * func( (8, 0.2), (43, 0.2), (56, 0.6) ) should return 
 * 8 with 20% probability, 
 * or the value 43 with 20% probability, 
 * or the value 56 with 60% probability.
 */

namespace TaskQuestion
{
    public class MainProgram
    {
        public static void Main()
        {
            RandomNumberGenerator Generator = new RandomNumberGenerator();
            var inputPairsList = new List<KeyValuePair<int, double>>()
            {
                new KeyValuePair<int, double>(8, 0.2),
                new KeyValuePair<int, double>(43, 0.2),
                new KeyValuePair<int, double>(56, 0.6),
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

            var item = outputDictionary.Keys.FirstOrDefault();
            string prob = string.Format("{0:P}", outputDictionary[item]);

            Console.WriteLine($"Random Value {item} is generated with {prob} probability");
            Console.ReadLine();
        }
    }
}
