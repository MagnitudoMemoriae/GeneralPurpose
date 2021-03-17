using GeneralPurposeLibrary.Algorithms.Sorting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GeneralPurposeLibraryTest
{
    [TestClass]
    public class FilpSortAlgorithmTest
    {
        [TestMethod]
        public void TestMethodAlgo0()
        {
            Random random = new Random();
            List<int> elements = new List<int>();

            for (int i = 0; i < 100; i++)
            {
                elements.Add(random.Next(0, 100));
            }

            FlipSortAlgorithm algo = new FlipSortAlgorithm(elements, 0);
            Console.WriteLine(algo.Outputs.Count);
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

            FlipSortAlgorithm algo = new FlipSortAlgorithm(elements, 1);
            Console.WriteLine(algo.Outputs.Count);
        }

        [TestMethod]
        public void TestMethodAlgo0_2()
        {
            Random random = new Random();
            List<int> elements = new List<int>();
            const int ELEMNUMBER = 100;
            const int ELEMMAXVALUE= 100;
            const int MULTIPLIER = 2;

            for (int i = 0; i < ELEMNUMBER; i++)
            {
                elements.Add(random.Next(0, ELEMMAXVALUE));
            }

            FlipSortAlgorithm algo1 = new FlipSortAlgorithm(elements, 0);

            for (int i = 0; i < ELEMNUMBER* MULTIPLIER; i++)
            {
                elements.Add(random.Next(0, ELEMMAXVALUE));
            }

            FlipSortAlgorithm algo2 = new FlipSortAlgorithm(elements, 0);
            double BigO = (((double)(((double)algo2.BigO)) / ((double)algo1.BigO))/(double)MULTIPLIER);

            Console.WriteLine("BigO : " + BigO) ;


        }

        [TestMethod]
        public void TestMethodAlgo2_1()
        {
            Random random = new Random();
            List<int> elements = new List<int>();
            const int ELEMNUMBER = 10;
            const int ELEMMAXVALUE = 100;
            const int MULTIPLIER = 1;

            for (int i = 0; i < ELEMNUMBER * MULTIPLIER; i++)
            {
                elements.Add(random.Next(0, ELEMMAXVALUE));
            }

            FlipSortAlgorithm algo = new FlipSortAlgorithm(elements, 2);
            Console.WriteLine(algo.Outputs.Count);
        }


        [TestMethod]
        public void TestMethodAlgo3_1()
        {
            Random random = new Random();
            List<int> elements = new List<int>();
            const int ELEMNUMBER = 10;
            const int ELEMMAXVALUE = 100;
            const int MULTIPLIER = 1;

            for (int i = 0; i < ELEMNUMBER * MULTIPLIER; i++)
            {
                elements.Add(random.Next(0, ELEMMAXVALUE));
            }

            FlipSortAlgorithm algo = new FlipSortAlgorithm(elements, 3);
            Debug.WriteLine("Big O :  " + algo.BigO);
            Debug.WriteLine(algo.Outputs.Count);
        }

        [TestMethod]
        public void TestMethodAlgo3_2()
        {
            Random random = new Random();
            List<int> elements = new List<int>();
            const int ELEMNUMBER = 100;
            const int ELEMMAXVALUE = 100;
            const int MULTIPLIER = 2;

            for (int i = 0; i < ELEMNUMBER; i++)
            {
                elements.Add(random.Next(0, ELEMMAXVALUE));
            }

            FlipSortAlgorithm algo1 = new FlipSortAlgorithm(elements, 3);

            for (int i = 0; i < ELEMNUMBER * MULTIPLIER; i++)
            {
                elements.Add(random.Next(0, ELEMMAXVALUE));
            }

            FlipSortAlgorithm algo2 = new FlipSortAlgorithm(elements, 3);
            double BigO = (((double)(((double)algo2.BigO)) / ((double)algo1.BigO)) / (double)MULTIPLIER);

            Debug.WriteLine("BigO : " + BigO);


        }
    }
}
