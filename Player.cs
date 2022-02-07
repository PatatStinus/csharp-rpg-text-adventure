using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RPGTextBasedAdventure
{
    static class Player
    {
        public static int level = 0;
        public static int xp = 0;
        public static int race = 0;
        public static List<int> attacks = new List<int>();
        public static string name = "";
        public static bool mission = false;
        private static Random rmd = new Random();

        public static List<float> LevelToStats(int level)
        {
            float damage = 1f + level / 100f;
            float defense = 1f + level / 100f;
            float health = 100f + level * 12f;
            List<float> attackDefenseHealth = new List<float>();
            attackDefenseHealth.Add(damage);
            attackDefenseHealth.Add(defense);
            attackDefenseHealth.Add(health);
            return attackDefenseHealth;
        }

        public static List<float> LevelToStatsEnemy(int level)
        {
            float damage = 1f + level / rmd.Next(80, 120);
            float defense = 1f + level / rmd.Next(80, 120);
            float health = 100f + level * rmd.Next(8, 15);
            List<float> attackDefenseHealth = new List<float>();
            attackDefenseHealth.Add(damage);
            attackDefenseHealth.Add(defense);
            attackDefenseHealth.Add(health);
            return attackDefenseHealth;
        }

        public static void CheckLevel()
        {
            int xpNeeded = level * 12 + 100;
            while(xpNeeded <= xp)
            {
                xp -= xpNeeded;
                level++;
                xpNeeded = level * 12 + 100;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nYou leveled up to level {level}!");
                Console.ResetColor();
                Thread.Sleep(5000);
                if (level == 10 || level == 20 || level == 35)
                {
                    Console.WriteLine("You learned a new attack!");
                    NewAttack();
                }
            }
        }

        private static void NewAttack()
        {
            switch(race)
            {
                case 1:
                    switch(level)
                    {
                        case 10:
                            attacks.Add(3);
                            Console.WriteLine($"You learned: {new AttackIndex(3).attackName}");
                            break;
                        case 20:
                            attacks.Add(4);
                            Console.WriteLine($"You learned: {new AttackIndex(4).attackName}");
                            break;
                        case 35:
                            attacks.Add(5);
                            Console.WriteLine($"You learned: {new AttackIndex(5).attackName}");
                            break;
                    }
                    break;
                case 2:
                    switch (level)
                    {
                        case 10:
                            attacks.Add(6);
                            Console.WriteLine($"You learned: {new AttackIndex(6).attackName}");
                            break;
                        case 20:
                            attacks.Add(7);
                            Console.WriteLine($"You learned: {new AttackIndex(7).attackName}");
                            break;
                        case 35:
                            attacks.Add(8);
                            Console.WriteLine($"You learned: {new AttackIndex(8).attackName}");
                            break;
                    }
                    break;
                case 3:
                    switch (level)
                    {
                        case 10:
                            attacks.Add(2);
                            Console.WriteLine($"You learned: {new AttackIndex(2).attackName}");
                            break;
                        case 20:
                            attacks.Add(9);
                            Console.WriteLine($"You learned: {new AttackIndex(9).attackName}");
                            break;
                        case 35:
                            attacks.Add(10);
                            Console.WriteLine($"You learned: {new AttackIndex(10).attackName}");
                            break;
                    }
                    break;
                case 4:
                    switch (level)
                    {
                        case 10:
                            attacks.Add(2);
                            Console.WriteLine($"You learned: {new AttackIndex(2).attackName}");
                            break;
                        case 20:
                            int attack = rmd.Next(3, 14);
                            attacks.Add(attack);
                            Console.WriteLine($"You learned: {new AttackIndex(attack).attackName}");
                            break;
                        case 35:
                            int attackA = rmd.Next(3, 14);
                            attacks.Add(attackA);
                            Console.WriteLine($"You learned: {new AttackIndex(attackA).attackName}");
                            break;
                    }
                    break;
                case 5:
                    switch (level)
                    {
                        case 10:
                            attacks.Add(3);
                            Console.WriteLine($"You learned: {new AttackIndex(3).attackName}");
                            break;
                        case 20:
                            attacks.Add(5);
                            Console.WriteLine($"You learned: {new AttackIndex(5).attackName}");
                            break;
                        case 35:
                            attacks.Add(11);
                            Console.WriteLine($"You learned: {new AttackIndex(11).attackName}");
                            break;
                    }
                    break;
                case 6:
                    switch (level)
                    {
                        case 10:
                            attacks.Add(9);
                            Console.WriteLine($"You learned: {new AttackIndex(9).attackName}");
                            break;
                        case 20:
                            attacks.Add(12);
                            Console.WriteLine($"You learned: {new AttackIndex(12).attackName}");
                            break;
                        case 35:
                            attacks.Add(13);
                            Console.WriteLine($"You learned: {new AttackIndex(13).attackName}");
                            break;
                    }
                    break;
            }
        }
    }
}
