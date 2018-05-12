using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorLife.Game
{
    public class LifeInstance
    {
        public int X { get; set; }
        public int Y { get; set; }

        public LifeInstance(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"X: {X} Y: {Y}";
        }

        public override bool Equals(object obj)
        {
            LifeInstance compareInstance = (LifeInstance)obj;
            if (compareInstance != null && compareInstance.X == X && compareInstance.Y == Y)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public List<LifeInstance> GetNeighbouringCells()
        {
            List<LifeInstance> neighbouringCells = new List<LifeInstance>();
            for (int deltaX = -1; deltaX <2; deltaX++) {
                for (int deltaY = -1; deltaY < 2; deltaY++)
                {
                    var potentialCell = new LifeInstance(X + deltaX, Y + deltaY);
                    if (!potentialCell.Equals(this))
                        neighbouringCells.Add(potentialCell);
                }
            }
            return neighbouringCells;
        }
    }
}
