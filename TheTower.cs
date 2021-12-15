using System;
using System.Collections.Generic;
using System.Text;

namespace RPGTextBasedAdventure
{
    class TheTower
    {
        private int level;
        private int race;
        private float xp;
        private string name;

        public void AssignPlayer(int level, int race, float xp, string name)
        {
            this.level = level;
            this.race = race;
            this.xp = xp;
            this.name = name;
        }
    }
}
