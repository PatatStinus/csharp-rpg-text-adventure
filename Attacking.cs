using System;
using System.Collections.Generic;
using System.Text;

namespace RPGTextBasedAdventure
{
    class Attacking
    {
        private List<AttackIndex> attacksOne = new List<AttackIndex>();
        private List<AttackIndex> attacksTwo = new List<AttackIndex>();
        private int raceOne;
        private int levelOne;
        private int raceTwo;
        private int levelTwo;

        public Attacking(int race, List<int> attacks, int level)
        {
            for (int i = 0; i < attacks.Count; i++)
                attacksOne.Add(new AttackIndex(attacks[i]));

            raceOne = race;
            levelOne = level;

            Random rdm = new Random();
            raceTwo = rdm.Next(1, 7);
            attacksTwo.Add(new AttackIndex(1));
            switch(raceTwo)
            {
                case 1:
                    attacksTwo.Add(new AttackIndex(3));
                    attacksTwo.Add(new AttackIndex(4));
                    attacksTwo.Add(new AttackIndex(5));
                    break;
                case 2:
                    attacksTwo.Add(new AttackIndex(6));
                    attacksTwo.Add(new AttackIndex(7));
                    attacksTwo.Add(new AttackIndex(8));
                    break;
                case 3:
                    attacksTwo.Add(new AttackIndex(2));
                    attacksTwo.Add(new AttackIndex(9));
                    attacksTwo.Add(new AttackIndex(10));
                    break;
                case 4:
                    attacksTwo.Add(new AttackIndex(2));
                    attacksTwo.Add(new AttackIndex(rdm.Next(1, 14)));
                    attacksTwo.Add(new AttackIndex(rdm.Next(1, 14)));
                    break;
                case 5:
                    attacksTwo.Add(new AttackIndex(3));
                    attacksTwo.Add(new AttackIndex(5));
                    attacksTwo.Add(new AttackIndex(11));
                    break;
                case 6:
                    attacksTwo.Add(new AttackIndex(9));
                    attacksTwo.Add(new AttackIndex(12));
                    attacksTwo.Add(new AttackIndex(13));
                    break;
            }
            levelTwo = rdm.Next(level - 3, level + 4);
        }
    }

    class AttackIndex 
    {
        public int damage;
        public bool meleeAttack;
        public bool magicAttack;
        public string attackName;
        public int chanceOfHit;

        public AttackIndex(int index)
        {
            switch(index)
            {
                case 1:
                    MakeAttack(10, true, false, "Punch", 100);
                    break;
                case 2:
                    MakeAttack(30, false, false, "Bow & Arrow", 80);
                    break;
                case 3:
                    MakeAttack(40, false, true, "Fireball", 60);
                    break;
                case 4:
                    MakeAttack(40, true, true, "Sword of Light", 90);
                    break;
                case 5:
                    MakeAttack(35, false, true, "Fire Ring", 75);
                    break;
                case 6:
                    MakeAttack(50, false, false, "Rock Throw", 50);
                    break;
                case 7:
                    MakeAttack(50, true, false, "Heavy Hit", 88);
                    break;
                case 8:
                    MakeAttack(999, true, true, "Self Destruct", 100);
                    break;
                case 9:
                    MakeAttack(20, true, false, "Dash", 100);
                    break;
                case 10:
                    MakeAttack(25, true, false, "Feather Rain", 80);
                    break;
                case 11:
                    MakeAttack(44, true, true, "Flame Breath", 97);
                    break;
                case 12:
                    MakeAttack(35, true, true, "Swipe", 100);
                    break;
                case 13:
                    MakeAttack(40, false, true, "Blow", 98);
                    break;
            }
        }

        private void MakeAttack(int dam, bool melee, bool magic, string name, int chance)
        {
            damage = dam;
            meleeAttack = melee;
            magicAttack = magic;
            attackName = name;
            chanceOfHit = chance;
        }
    }
}
