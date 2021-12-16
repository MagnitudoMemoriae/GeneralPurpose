using GeneralPurposeLibrary;
using GeneralPurposeLibrary.SetTheory;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GeneralPurposeLibraryTest
{
    [TestClass]
    public class FullOuterJoinTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<String> First = new List<String>() { "A", "C" };
            List<String> Second = new List<String>() { "A", "B" };

            FullOuterJoin<String> fullOuterJoin = new FullOuterJoin<String>(First, Second, String.Compare);
            fullOuterJoin.Process();

            Boolean Both = ((fullOuterJoin.Response.Both[0] == "A") && (fullOuterJoin.Response.Both.Count == 1));
            Boolean OnlyFirst = ((fullOuterJoin.Response.OnlyFirst[0] == "C") && (fullOuterJoin.Response.OnlyFirst.Count == 1));
            Boolean OnlySecond = ((fullOuterJoin.Response.OnlySecond[0] == "B") && (fullOuterJoin.Response.OnlySecond.Count == 1));

            Assert.AreEqual(true, Both && OnlyFirst && OnlySecond);
        }

        [TestMethod]
        public void TestMethod2()
        {
            Assert.AreEqual(true, true);
        }
    }
}