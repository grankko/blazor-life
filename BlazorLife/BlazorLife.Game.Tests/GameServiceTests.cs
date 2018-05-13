using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlazorLife.Game;
using System;

namespace BlazorLife.Game.Tests
{
    [TestClass]
    public class GameServiceTests
    {
        [TestMethod]
        public void CanCreateService()
        {
            var sut = new GameService();
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void CanAddLife()
        {
            var sut = new GameService();
            Assert.AreEqual(0, sut.AllLife.Count);
            sut.AddLife(0, 1);
            Assert.AreEqual(1, sut.AllLife.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CanNotAddSameLife()
        {
            var sut = new GameService();
            sut.AddLife(0, 1);
            sut.AddLife(0, 1);
        }

        [TestMethod]
        public void CanCalculateNoOfNeighbours()
        {
            var sut = new GameService();
            var lifeUnderTest = new LifeInstance(10, 10);
            sut.AddLife(lifeUnderTest);
            var result = sut.GetNumberOfNeighbours(lifeUnderTest);
            Assert.AreEqual(0, result);

            sut.AddLife(9, 10); // Add life one position to the left
            result = sut.GetNumberOfNeighbours(lifeUnderTest);
            Assert.AreEqual(1, result);

            sut.AddLife(8, 10); // Add life two positions to the left, not a neighbour
            result = sut.GetNumberOfNeighbours(lifeUnderTest);
            Assert.AreEqual(1, result);

            sut.AddLife(10, 11); // Add life one position above
            result = sut.GetNumberOfNeighbours(lifeUnderTest);
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void CanCalculateNextGenerationWithTwoLiveCells()
        {
            var sut = new GameService();
            sut.AddLife(0, 0);
            sut.AddLife(0, 1);
            sut.CreateNextGeneration();

            Assert.AreEqual(1, sut.CurrentGenerationNumber);
            Assert.AreEqual(0, sut.AllLife.Count);
        }

        [TestMethod]
        public void CanCalculateNextGenerationWithThreeLiveCellsInARow()
        {
            var sut = new GameService();
            sut.AddLife(0, 0);
            sut.AddLife(0, 1);
            sut.AddLife(0, 2);
            sut.CreateNextGeneration();

            Assert.AreEqual(1, sut.CurrentGenerationNumber);
            Assert.AreEqual(3, sut.AllLife.Count);
        }
    }
}
