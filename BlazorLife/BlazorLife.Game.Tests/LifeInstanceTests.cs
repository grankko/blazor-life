using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlazorLife.Game;
using System;

namespace BlazorLife.Game.Tests
{
    [TestClass]
    public class LifeInstanceTests
    {
        [TestMethod]
        public void CanCreateLifeInstance()
        {
            var sut = new LifeInstance(0,0);
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void CanGetLifeInstanceNeighbouringCells()
        {
            var sut = new LifeInstance(0, 0);
            var cells = sut.GetNeighbouringCells();
            Assert.IsNotNull(cells);
            Assert.AreEqual(8, cells.Count); // 8 neighbours of cell in 2D space
        }

        [TestMethod]
        public void CanGetDifferentHashCodesForSimilarEntities()
        {
            var sut1 = new LifeInstance(1, 0).GetHashCode();
            var sut2 = new LifeInstance(0, 1).GetHashCode();

            Assert.AreNotEqual(sut1, sut2);
        }

        [TestMethod]
        public void CanGetSameHashCodesForSameEntities()
        {
            var sut1 = new LifeInstance(1, 0).GetHashCode();
            var sut2 = new LifeInstance(1, 0).GetHashCode();

            Assert.AreEqual(sut1, sut2);
        }
    }
}