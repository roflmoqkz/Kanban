using NUnit.Framework;
using IntroSE.Kanban.Backend.BusinessLayer.BoardPackage;
using System;
using Moq;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(-1)]
        public void Test1(int ordinal)
        {
            Board b = new Board("sd");
            b.RemoveColumn("sd", ordinal);
            Assert.AreEqual(2,b.getNumColumns(), "the column wasnt removed properly");
        }
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(-1)]
        [TestCase(3)]
        public void Test2(int ordinal)
        {
            Board b = new Board("sd");
            b.MoveColumnRight("sd", ordinal);
            Assert.AreNotEqual("in progress",b.getNumColumns() , "the column didnt move properly");
        }
        [TestCase(0,"new")]
        [TestCase(1, "")]
        [TestCase(2,"in progress")]
        [TestCase(-1,"new")]
        [TestCase(3,"new")]
        [TestCase(1000,"new")]
        public void Test3(int ordinal,string name)
        {
            Board b = new Board("sd");
            b.AddColumn("sd", ordinal,name);
            Assert.AreNotEqual("in progress", b.getNumColumns(), "the column didnt move properly");
        }
    }
}