using GeneralPurposeLibrary.Algorithms.Sorting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GeneralPurposeLibraryTest
{
    [TestClass]
    public class ClimbSortAlgorithmTest
    {
        [TestMethod]
        public void TestMethodAlgo0()
        {
            Random random = new Random();
            List<int> elements = new List<int>();

            for (int i = 0; i < 100; i++)
            {
                elements.Add(random.Next(0,100));
            }

            ClimbSortAlgorithm algo = new ClimbSortAlgorithm(elements,0);
        }

        [TestMethod]
        public void TestMethodAlgo1()
        {
            Random random = new Random();
            List<int> elements = new List<int>();

            for (int i = 0; i < 100; i++)
            {
                elements.Add(random.Next(0, 100));
            }

            ClimbSortAlgorithm algo = new ClimbSortAlgorithm(elements, 1);
        }

        [TestMethod]
        public void TestMethodAlgo2()
        {
            Random random = new Random();
            List<int> elements = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                elements.Add(random.Next(0, 100));
            }

            ClimbSortAlgorithm algo = new ClimbSortAlgorithm(elements, 2);
            Console.WriteLine(algo.Outputs.Count);
        }

        [TestMethod]
        public void TestMethodAlgo1_2()
        {
            Random random = new Random();
            List<int> elements = new List<int>();
            const int ELEMNUMBER = 100;
            const int ELEMMAXVALUE = 100;
            const int MULTIPLIER = 100;

            for (int i = 0; i < ELEMNUMBER; i++)
            {
                elements.Add(random.Next(0, ELEMMAXVALUE));
            }

            ClimbSortAlgorithm algo1 = new ClimbSortAlgorithm(elements, 1);

            for (int i = 0; i < ELEMNUMBER * MULTIPLIER; i++)
            {
                elements.Add(random.Next(0, ELEMMAXVALUE));
            }

            ClimbSortAlgorithm algo2 = new ClimbSortAlgorithm(elements, 1);
            double BigO = (((double)(((double)algo2.BigO)) / ((double)algo1.BigO)) / (double)MULTIPLIER);
        }
    }
}
