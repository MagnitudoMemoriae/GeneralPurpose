using GeneralPurposeLibrary;
using GeneralPurposeLibrary.Comparers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

            Assert.AreEqual(true, !re.Match(2).Any(x => x == false));
        }

        [TestMethod]
        public void TestInt02()
        {
            Range<int> range1 = new Range<int>(0, 10, IntervalType.IN_IN, Comparer.Compare);
            Range<int> range2 = new Range<int>(1, 11, IntervalType.IN_IN, Comparer.Compare);
            RangeEngine<int> re = new RangeEngine<int>(new List<Range<int>>() { range1, range2 });

            Assert.AreEqual(true, !re.Match(10).Any(x => x == false));
        }

        [TestMethod]
        public void TestInt03()
        {
            Range<int> range1 = new Range<int>(0, 10, IntervalType.IN_IN, Comparer.Compare);
            Range<int> range2 = new Range<int>(1, 11, IntervalType.IN_IN, Comparer.Compare);
            RangeEngine<int> re = new RangeEngine<int>(new List<Range<int>>() { range1, range2 });

            Assert.AreEqual(true, re.Match(12).Any(x => x == false));
        }
    }
}