using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorLife.Game
{
    public static class Creatures
    {
        /// <summary>
        /// Pattern is:
        /// -X--
        /// --X-
        /// XXX-
        /// </summary>
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

        /// <summary>
        /// Smallest known pattern with infinite growth. Pattern is:
        /// 
        ///    0123456789012345678901234567890123456789
        ///  0 XXXXXXXX-XXXXX---XXX------XXXXXXX-XXXXX
        /// </summary>
        public static IEnumerable<LifeInstance> CreateInfinite(int xOffset, int yOffset)
        {
            List<LifeInstance> infinite = new List<LifeInstance>();

            for (int x = 0; x < 8; x++)
                infinite.Add(new LifeInstance(x + xOffset, 0 + yOffset));

            for (int x = 9; x < 14; x++)
                infinite.Add(new LifeInstance(x + xOffset, 0 + yOffset));

            for (int x = 17; x < 20; x++)
                infinite.Add(new LifeInstance(x + xOffset, 0 + yOffset));

            for (int x = 26; x < 33; x++)
                infinite.Add(new LifeInstance(x + xOffset, 0 + yOffset));

            for (int x = 34; x < 39; x++)
                infinite.Add(new LifeInstance(x + xOffset, 0 + yOffset));

            return infinite;
        }
    }
}
