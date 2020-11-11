using System;
using System.Collections.Generic;

namespace BlazorLife.Game
{
    public class GameService
    {

        public HashSet<LifeInstance> AllLife { get; private set; }

        public int CurrentGenerationNumber { get; set; }

        public GameService()
        {
            Reset();
        }

        public void Reset()
        {
            AllLife = new HashSet<LifeInstance>();
            CurrentGenerationNumber = 0;
        }

        /// <summary>
        /// Calculates the next generation from the current generation. Any living cell with 2 or 3 living neighbours
        /// continues to the next generation. Any dead cell with exactly 3 living neighbours becomes alive. All other
        /// cells dies of overpopulation or starvation.
        /// </summary>
        /// <remarks>Lots of double checking of potential cells atm. Rome for optimization</remarks>
        public void CreateNextGeneration()
        {
            HashSet<LifeInstance> nextGeneration = new HashSet<LifeInstance>();
            
            foreach (var life in AllLife)
            {
                // If living cell has 2 or three living neighbours it lives on to the next generation
                var noOfLiveNeighbours = GetNumberOfNeighbours(life);
                if (noOfLiveNeighbours == 2 || noOfLiveNeighbours == 3)
                    nextGeneration.Add(life);

                // For all neighbouring cells of current generation life
                foreach (var neighbourCell in life.GetNeighbouringCells())
                {
                    // If this cell has 2 or 3 live neighbours, add it to the next generation. Everything else dies
                    if (!nextGeneration.Contains(neighbourCell))
                    {
                        noOfLiveNeighbours = GetNumberOfNeighbours(neighbourCell);
                        if (noOfLiveNeighbours == 3)
                            nextGeneration.Add(neighbourCell);
                    }
                }
            }

            // Set new calculated generation to current generation
            AllLife = nextGeneration;
            CurrentGenerationNumber++;
        }

        public void AddLife(IEnumerable<LifeInstance> life)
        {
            foreach (var newLife in life)
                AddLife(newLife);
        }

        public void AddLife(LifeInstance life)
        {
            AddLife(life.X, life.Y);
        }

        public void AddLife(int x, int y)
        {
            var newLife = new LifeInstance(x, y);
            if (!AllLife.Add(newLife))
                throw new InvalidOperationException($"Life {newLife} already exists");
        }

        public int GetNumberOfNeighbours(LifeInstance life)
        {
            int numberOfNeighbours = 0;

            var neighbouringCells = life.GetNeighbouringCells();
            foreach (var cell in neighbouringCells)
            {
                if (AllLife.Contains(cell))
                    numberOfNeighbours++;
            }

            return numberOfNeighbours;
        }
    }
}
