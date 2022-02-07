using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RPGTextBasedAdventure
{
    class Attacking
    {
        private List<AttackIndex> attacksOne = new List<AttackIndex>();
        private List<AttackIndex> attacksTwo = new List<AttackIndex>();
        private int raceOne;
        private int raceTwo;
        private int levelOne;
        private int levelTwo;
        private List<float> attDefHPOne = new List<float>();
        private List<float> attDefHPTwo = new List<float>();
        private bool battleFinished = false;
        private Random rmd = new Random();
        private string nameTwo;
        private bool countersTwo = false;
        private bool countersOne = false;

        public Attacking(int raceTwo)
        {
            for (int i = 0; i < Player.attacks.Count; i++)
                attacksOne.Add(new AttackIndex(Player.attacks[i]));

            raceOne = Player.race;
            levelOne = Player.level;

            this.raceTwo = raceTwo;

            Random rdm = new Random();
            attacksTwo.Add(new AttackIndex(1));
            switch(raceTwo)
            {
                case 1:
                    nameTwo = "Witch";
                    attacksTwo.Add(new AttackIndex(3));
                    attacksTwo.Add(new AttackIndex(4));
                    attacksTwo.Add(new AttackIndex(5));
                    if (raceOne == 3 || raceOne == 5)
                        countersOne = true;
                    if (raceOne == 2 || raceOne == 6)
                        countersTwo = true;
                    break;
                case 2:
                    nameTwo = "Golem";
                    attacksTwo.Add(new AttackIndex(6));
                    attacksTwo.Add(new AttackIndex(7));
                    attacksTwo.Add(new AttackIndex(8));
                    if (raceOne == 4 || raceOne == 1)
                        countersOne = true;
                    if (raceOne == 3 || raceOne == 6)
                        countersTwo = true;
                    break;
                case 3:
                    nameTwo = "ManBird";
                    attacksTwo.Add(new AttackIndex(2));
                    attacksTwo.Add(new AttackIndex(9));
                    attacksTwo.Add(new AttackIndex(10));
                    if (raceOne == 2 || raceOne == 6)
                        countersOne = true;
                    if (raceOne == 1 || raceOne == 4)
                        countersTwo = true;
                    break;
                case 4:
                    nameTwo = "Human";
                    attacksTwo.Add(new AttackIndex(2));
                    attacksTwo.Add(new AttackIndex(rdm.Next(1, 14)));
                    attacksTwo.Add(new AttackIndex(rdm.Next(1, 14)));
                    if (raceOne == 5 || raceOne == 3)
                        countersOne = true;
                    if (raceOne == 2 || raceOne == 5)
                        countersTwo = true;
                    break;
                case 5:
                    nameTwo = "Fire Spirit";
                    attacksTwo.Add(new AttackIndex(3));
                    attacksTwo.Add(new AttackIndex(5));
                    attacksTwo.Add(new AttackIndex(11));
                    if (raceOne == 4 || raceOne == 6)
                        countersOne = true;
                    if (raceOne == 4 || raceOne == 1)
                        countersTwo = true;
                    break;
                case 6:
                    nameTwo = "Swift";
                    attacksTwo.Add(new AttackIndex(9));
                    attacksTwo.Add(new AttackIndex(12));
                    attacksTwo.Add(new AttackIndex(13));
                    if (raceOne == 1 || raceOne == 2)
                        countersOne = true;
                    if (raceOne == 5 || raceOne == 3)
                        countersTwo = true;
                    break;
            }
            levelTwo = rdm.Next(Player.level - 3, Player.level + 4);

            if (levelTwo <= 0)
                levelTwo = 1;

            attDefHPOne = Player.LevelToStats(levelOne);
            attDefHPTwo = Player.LevelToStatsEnemy(levelTwo);

            Console.WriteLine("\n|    - Normal Attack");
            Console.WriteLine("||   - Ranged Attack");
            Console.WriteLine("|||  - Magic Attack");
            Console.WriteLine("|||| - Ranged Magic Attack\n");

            Console.WriteLine("Your attacks are:\n");
            for (int i = 0; i < attacksOne.Count; i++)
                Console.WriteLine($"{i + 1} - {attacksOne[i].attackName}");

            Console.WriteLine("");

            Console.WriteLine($"The enemy is level {levelTwo}");
            Console.WriteLine("\nFight!");
            Console.WriteLine("Choose an attack you would like to use");
            AttackLoop();
        }

        private void AttackLoop()
        {
            string userInput = Console.ReadLine();
            float attackDamage = 0;

            if (Int16.TryParse(userInput, out short number))
            {
                if (number > attacksOne.Count || number < 1)
                {
                    Console.WriteLine("You don't have that many attacks");
                    AttackLoop();
                    return;
                }
            }
            else
            {
                Console.WriteLine("Please choose a valid attack");
                AttackLoop();
                return;
            }

            Console.WriteLine($"You used {attacksOne[number - 1].attackName}");

            attackDamage = attacksOne[number - 1].damage * attDefHPOne[0];

            if (raceTwo == 3 && attacksOne[number - 1].meleeAttack)
            {
                Console.WriteLine("ManBird dodged the attack");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Hint: ManBirds always dodge melee attacks");
                Console.ResetColor();
                AttackingTwo();
            }
            else if (raceTwo == 2 && attacksOne[number - 1].magicAttack)
            {
                Console.WriteLine("It did nothing");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Hint: Golems are immune to magic attacks");
                Console.ResetColor();
                AttackingTwo();
            }
            else
            {
                if(rmd.Next(0, 101) <= attacksOne[number - 1].chanceOfHit)
                {
                    Console.WriteLine("It hit!");

                    if(countersTwo)
                        attackDamage *= 2;

                    attackDamage /= attDefHPTwo[1];

                    attackDamage = Convert.ToSingle(Math.Round(attackDamage));

                    attDefHPTwo[2] -= attackDamage;
                    if (attDefHPTwo[2] < 0)
                        attDefHPTwo[2] = 0;
                    Console.WriteLine($"The {nameTwo} took {attackDamage} damage and is left with {attDefHPTwo[2]} HP");

                    if (attacksOne[number - 1].attackName == "||| - Self Destruct")
                    {
                        Console.WriteLine($"The enemy took {attackDamage} damage as well");
                        attDefHPOne[2] -= attackDamage;
                        if (attDefHPOne[2] < 0)
                            attDefHPOne[2] = 0;

                        Console.WriteLine($"You are left with {attDefHPOne[2]} HP");
                    }

                    HealthCheck();
                    if(!battleFinished)
                        AttackingTwo();
                }
                else
                {
                    Console.WriteLine("You Missed!");
                    AttackingTwo();
                }
            }
        }

        private void AttackingTwo()
        {
            float attackDamage = 0;
            int playerTwoChoice = rmd.Next(0, attacksTwo.Count);

            Console.WriteLine($"{nameTwo} used {attacksTwo[playerTwoChoice].attackName}");

            attackDamage = attacksTwo[playerTwoChoice].damage * attDefHPTwo[0];

            if (raceOne == 3 && attacksTwo[playerTwoChoice].meleeAttack)
                Console.WriteLine("You dodged the attack");
            else if (raceOne == 2 && attacksTwo[playerTwoChoice].magicAttack)
                Console.WriteLine("It did nothing");
            else
            {
                if (rmd.Next(0, 101) <= attacksTwo[playerTwoChoice].chanceOfHit)
                {
                    Console.WriteLine("It hit!");

                    if (countersOne)
                        attackDamage *= 2;

                    attackDamage /= attDefHPOne[1];

                    attackDamage = Convert.ToSingle(Math.Round(attackDamage));

                    attDefHPOne[2] -= attackDamage;
                    if (attDefHPOne[2] < 0)
                        attDefHPOne[2] = 0;

                    Console.WriteLine($"You took {attackDamage} damage and are left with {attDefHPOne[2]} HP");

                    if(attacksTwo[playerTwoChoice].attackName == "||| - Self Destruct")
                    {
                        Console.WriteLine($"The enemy took {attackDamage} damage as well");
                        attDefHPTwo[2] -= attackDamage;
                        if (attDefHPTwo[2] < 0)
                            attDefHPTwo[2] = 0;

                        Console.WriteLine($"The enemy is left with {attDefHPTwo[2]} HP");
                    }
                }
                else
                    Console.WriteLine("It Missed!");
            }

            Console.WriteLine("\nWhich attack would you like to use?");
            HealthCheck();
            if (!battleFinished)
                AttackLoop();
        }
        
        private void HealthCheck()
        {
            if(attDefHPTwo[2] <= 0 && attDefHPOne[2] <= 0)
            {
                battleFinished = true;
                Console.WriteLine("Sadly you both died..");
                Console.WriteLine("You will be send back to the tower");
                Console.WriteLine("You lost a level");
                Player.level--;
                Player.xp = 0;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Player level = {Player.level}");
                Console.ResetColor();
                Thread.Sleep(10000);
                TheTower tower = new TheTower();
                Player.mission = false;
                tower.Start();
            }
            else if(attDefHPOne[2] <= 0)
            {
                battleFinished = true;
                Console.WriteLine("Too bad..");
                Console.WriteLine("You lost a level");
                Player.level--;
                Player.xp = 0;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Player level = {Player.level}");
                Console.ResetColor();
                Console.WriteLine("You will be send back to the tower to heal!");
                Thread.Sleep(10000);
                TheTower tower = new TheTower();
                Player.mission = false;
                tower.Start();
            }
            else if (attDefHPTwo[2] <= 0)
            {
                battleFinished = true;
                Console.WriteLine("Congratulations!");
                Console.ForegroundColor = ConsoleColor.Green;
                int xp = levelTwo * rmd.Next(6, 13);
                Console.WriteLine($"You gained {xp} xp!");
                Console.ResetColor();
                Player.xp += xp;
                Player.CheckLevel();
                Thread.Sleep(10000);
                Console.Clear();
                Console.WriteLine("You're fully healed and are ready to go on!");
                Console.WriteLine("Which way would you like to go?");
            }
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
                    MakeAttack(10, true, false, "| - Punch", 100);
                    break;
                case 2:
                    MakeAttack(30, false, false, "|| - Bow & Arrow", 80);
                    break;
                case 3:
                    MakeAttack(40, false, true, "|||| - Fireball", 60);
                    break;
                case 4:
                    MakeAttack(40, true, true, "||| - Sword of Light", 90);
                    break;
                case 5:
                    MakeAttack(35, false, true, "|||| - Fire Ring", 75);
                    break;
                case 6:
                    MakeAttack(50, false, false, "|| - Rock Throw", 50);
                    break;
                case 7:
                    MakeAttack(50, true, false, "| - Heavy Hit", 88);
                    break;
                case 8:
                    MakeAttack(999, true, true, "||| - Self Destruct", 100);
                    break;
                case 9:
                    MakeAttack(20, true, false, "| - Dash", 100);
                    break;
                case 10:
                    MakeAttack(25, false, false, "|| - Feather Rain", 80);
                    break;
                case 11:
                    MakeAttack(44, false, true, "|||| - Flame Breath", 97);
                    break;
                case 12:
                    MakeAttack(35, true, true, "||| - Swipe", 100);
                    break;
                case 13:
                    MakeAttack(40, false, true, "|||| - Blow", 98);
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
