using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorLife.Game
{
    public static class Creatures
    {
        public static IEnumerable<LifeInstance> CreateGlider(int xOffset, int yOffset)
        {
            List<LifeInstance> glider = new List<LifeInstance>();
            glider.Add(new LifeInstance(1 + xOffset, 0 + yOffset));
            glider.Add(new LifeInstance(2 + xOffset, 1 + yOffset));
            glider.Add(new LifeInstance(0 + xOffset, 2 + yOffset));
            glider.Add(new LifeInstance(1 + xOffset, 2 + yOffset));
            glider.Add(new LifeInstance(2 + xOffset, 2 + yOffset));

            return glider;
        }
    }
}
