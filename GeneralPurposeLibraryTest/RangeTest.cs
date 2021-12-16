using GeneralPurposeLibrary;
using GeneralPurposeLibrary.Comparers;
using GeneralPurposeLibrary.SetTheory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneralPurposeLibraryTest
{
    [TestClass]
    public class RangeTest
    {
        [TestMethod]
        public void TestInt01()
        {
            Range<int> range1 = new Range<int>(0, 10, IntervalType.IN_IN, Comparer.Compare);
            Range<int> range2 = new Range<int>(1, 11, IntervalType.IN_IN, Comparer.Compare);
            RangeEngine<int> re = new RangeEngine<int>(new List<Range<int>>() { range1, range2 });

            Assert.AreEqual(true, !re.Matches(2).Any(x => x == false));
            Assert.AreEqual(true, re.Match(2));
        }

        [TestMethod]
        public void TestInt02()
        {
            Range<int> range1 = new Range<int>(0, 10, IntervalType.IN_IN, Comparer.Compare);
            Range<int> range2 = new Range<int>(1, 11, IntervalType.IN_IN, Comparer.Compare);
            RangeEngine<int> re = new RangeEngine<int>(new List<Range<int>>() { range1, range2 });

            Assert.AreEqual(true, !re.Matches(10).Any(x => x == false));
            Assert.AreEqual(true, re.Match(10));
        }

        [TestMethod]
        public void TestInt03()
        {
            Range<int> range1 = new Range<int>(0, 10, IntervalType.IN_IN, Comparer.Compare);
            Range<int> range2 = new Range<int>(1, 11, IntervalType.IN_IN, Comparer.Compare);
            RangeEngine<int> re = new RangeEngine<int>(new List<Range<int>>() { range1, range2 });

            Assert.AreEqual(true, re.Matches(12).Any(x => x == false));
            Assert.AreEqual(false, re.Match(12));
        }

        [TestMethod]
        public void TestInt04()
        {
            Range<int> range1 = new Range<int>(0, 10, IntervalType.IN_IN, Comparer.Compare);
            Range<int> range2 = new Range<int>(1, 11, IntervalType.IN_IN, Comparer.Compare);
            RangeEngine<int> re = new RangeEngine<int>(new List<Range<int>>() { range1, range2 });

            Assert.AreEqual(true, re.Matches(-1).Any(x => x == false));
            Assert.AreEqual(false, re.Match(-1));
        }

        [TestMethod]
        public void TestIntGetRange01()
        {


            Range<int> range1 = new Range<int>(0, 10, IntervalType.IN_IN, Comparer.Compare);
            Range<int> range2 = new Range<int>(1, 11, IntervalType.IN_IN, Comparer.Compare);
            Range<int> range3 = new Range<int>(2, 12, IntervalType.IN_IN, Comparer.Compare);

            List<Range<int>> theList = new List<Range<int>>();

            theList.Add(range1);
            theList.Add(range2);
            theList.Add(range3);

            RangeEngine<int>.GetRange(theList);

            //Assert.AreEqual(true, re.Matches(-1).Any(x => x == false));
            //Assert.AreEqual(false, re.Match(-1));
        }


        [TestMethod]
        public void TestIntGetRange02()
        {

        }

        // GetOrRanges


        [TestMethod]
        public void TestGetOrRanges00X()
        {
            int A = 0;
            int B = 10;
            int C = 2;
            int D = 12;


            Range<int> range1 = new Range<int>(A, B, IntervalType.IN_IN, Comparer.Compare);
            Range<int> range2 = new Range<int>(C, D, IntervalType.IN_IN, Comparer.Compare);

            List<Range<int>> theList = RangeEngine<int>.GetOrRanges(range1, range2);

            Assert.AreEqual(true, theList.Count == 1);

            Assert.AreEqual(true, theList[0].Begin == A);
            Assert.AreEqual(true, theList[0].Finish == D);
        }

        [TestMethod]
        public void TestGetOrRanges001()
        {
            int A = 0;
            int B = 10;
            int C = A;
            int D = B;

            Range<int> range1 = new Range<int>(A, B, IntervalType.IN_IN, Comparer.Compare);
            Range<int> range2 = new Range<int>(C, D, IntervalType.IN_IN, Comparer.Compare);

            List<Range<int>> theList = RangeEngine<int>.GetOrRanges(range1, range2);

            Assert.AreEqual(true, theList.Count == 1);
            Assert.AreEqual(true, theList[0].Begin == A);
            Assert.AreEqual(true, theList[0].Finish == D);
            Assert.AreEqual(true, theList[0].BeginEdge == EdgeType.IN);
            Assert.AreEqual(true, theList[0].FinishEdge == EdgeType.IN);
        }


        [TestMethod]
        public void TestGetOrRanges002()
        {
            int A = 0;
            int B = 10;
            int C = A;
            int D = B;

            Range<int> range1 = new Range<int>(A, B, IntervalType.OUT_IN, Comparer.Compare);
            Range<int> range2 = new Range<int>(C, D, IntervalType.IN_IN, Comparer.Compare);

            List<Range<int>> theList = RangeEngine<int>.GetOrRanges(range1, range2);

            Assert.AreEqual(true, theList.Count == 1);
            Assert.AreEqual(true, theList[0].Begin == A);
            Assert.AreEqual(true, theList[0].Finish == D);
            Assert.AreEqual(true, theList[0].BeginEdge == EdgeType.IN);
            Assert.AreEqual(true, theList[0].FinishEdge == EdgeType.IN);
        }


        [TestMethod]
        public void TestGetOrRanges003()
        {
            int A = 0;
            int B = 10;
            int C = A;
            int D = B;

            Range<int> range1 = new Range<int>(A, B, IntervalType.IN_OUT, Comparer.Compare);
            Range<int> range2 = new Range<int>(C, D, IntervalType.IN_IN, Comparer.Compare);

            List<Range<int>> theList = RangeEngine<int>.GetOrRanges(range1, range2);

            Assert.AreEqual(true, theList.Count == 1);
            Assert.AreEqual(true, theList[0].Begin == A);
            Assert.AreEqual(true, theList[0].Finish == D);
            Assert.AreEqual(true, theList[0].BeginEdge == EdgeType.IN);
            Assert.AreEqual(true, theList[0].FinishEdge == EdgeType.IN);
        }


        [TestMethod]
        public void TestGetOrRangesPerfectOverlap()
        {
            int A = 0;
            int B = 10;
            int C = A;
            int D = B;

            for (int r1b = (int)EdgeType.OUT; r1b <= (int)EdgeType.IN; r1b++)
            {
                for (int r1f = (int)EdgeType.OUT; r1f <= (int)EdgeType.IN; r1f++)
                {
                    for (int r2b = (int)EdgeType.OUT; r2b <= (int)EdgeType.IN; r2b++)
                    {
                        for (int r2f = (int)EdgeType.OUT; r2f <= (int)EdgeType.IN; r2f++)
                        {
                            IntervalType int1 = (IntervalType) (r1b + (2 * r1f));
                            IntervalType int2 = (IntervalType) (r2b + (2 * r2f));

                            Range<int> range1 = new Range<int>(A, B, int1, Comparer.Compare);
                            Range<int> range2 = new Range<int>(C, D, int2, Comparer.Compare);

                            List<Range<int>> theList = RangeEngine<int>.GetOrRanges(range1, range2);

                            EdgeType begin = EdgeType.OUT;
                            if (r1b + r2b > 0)
                            {
                                begin = EdgeType.IN;
                            }

                            EdgeType finish = EdgeType.OUT;
                            if (r1f + r2f > 0)
                            {
                                finish = EdgeType.IN;
                            }

                            Assert.AreEqual(true, theList.Count == 1);
                            Assert.AreEqual(true, theList[0].Begin == A);
                            Assert.AreEqual(true, theList[0].Finish == D);
                            Assert.AreEqual(true, theList[0].BeginEdge  == begin);
                            Assert.AreEqual(true, theList[0].FinishEdge == finish);
                        }
                    }
                }
            }
        }
    }
}