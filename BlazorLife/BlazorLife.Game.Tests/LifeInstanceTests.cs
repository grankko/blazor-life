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
    }
}