using System;
using System.Collections.Generic;
using System.Text;

namespace RPGTextBasedAdventure
{
    class DemonRealm
    {
        string userInput = "";
        Random rdm = new Random();
        int fireSpiritsKilled = 0;

        public void Start()
        {
            Console.Clear();
            Console.WriteLine("You went to the demon realm!");
            Console.WriteLine("You're completely surrounded by fire.");
            Console.WriteLine("Which way would you like to go?");
            Directions();
        }

        private void Directions()
        {
            userInput = Console.ReadLine();

            if (String.Compare(userInput, "left", true) == 0 || String.Compare(userInput, "right", true) == 0 || String.Compare(userInput, "forward", true) == 0 || String.Compare(userInput, "backward", true) == 0 || String.Compare(userInput, "forwards", true) == 0 || String.Compare(userInput, "backwards", true) == 0 || String.Compare(userInput, "north", true) == 0 || String.Compare(userInput, "south", true) == 0 || String.Compare(userInput, "west", true) == 0 || String.Compare(userInput, "east", true) == 0)
            {
                Console.WriteLine($"You decide to go {userInput}");
                Console.WriteLine("As you look ahead of you, you notice something in the distance.");
                Encounter();
            }
            else if (String.Compare(userInput, "go back", true) == 0 || String.Compare(userInput, "tower", true) == 0 || String.Compare(userInput, "back", true) == 0 || String.Compare(userInput, "the tower", true) == 0)
            {
                TheTower tower = new TheTower();
                tower.Start();
            }
            else
            {
                Console.WriteLine("Please choose a valid direction!");
                Directions();
            }
        }

        private void Encounter()
        {
            switch (rdm.Next(1, 6))
            {
                case 1:
                    Console.WriteLine("You found a big crate!");
                    int xp = rdm.Next(1, 500);
                    Console.WriteLine($"You look inside the chest and find {xp} xp points and a lot of ash!");

                    Console.WriteLine("You pick up the xp points");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"You gained {xp} xp points");
                    Console.ResetColor();
                    Player.xp += xp;
                    Player.CheckLevel();

                    Console.WriteLine("Which direction would you like to go now?");
                    Directions();
                    break;
                case 2:
                    if (!Player.mission)
                    {
                        Console.WriteLine("You see an odd figure");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("???: HEY!");
                        DemonEncounter();

                        Console.WriteLine("Which direction would you like to go now?");
                        Directions();
                    }
                    else
                    {
                        Console.WriteLine("You've encountered a Fire Spirit!");
                        new Attacking(5);
                        if (Player.mission)
                        {
                            fireSpiritsKilled++;
                            CheckChallenge();
                        }
                        Directions();
                    }
                    break;
                case 3:
                    Console.WriteLine("You've encountered a Fire Spirit!");
                    new Attacking(5);
                    if (Player.mission)
                    {
                        fireSpiritsKilled++;
                        CheckChallenge();
                    }
                    Directions();
                    break;
                case 4:
                    Console.WriteLine("You've encountered a Swift!");
                    new Attacking(6);
                    Directions();
                    break;
                case 5:
                    Console.WriteLine("You've encountered a Swift!");
                    new Attacking(6);
                    Directions();
                    break;
            }
        }

        private void DemonEncounter()
        {
            Console.WriteLine("???: I need you to kill 5 Fire Spirits for me!");

            Console.ResetColor();
            Player.mission = true;
            Directions();
        }

        private void CheckChallenge()
        {
            if(fireSpiritsKilled == 5)
            {
                fireSpiritsKilled = 0;
                Player.mission = false;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("???: Thank you for killing those Fire Spirits for me.");
                Console.ForegroundColor = ConsoleColor.Green;
                int xp = rdm.Next(5, 1000);
                Console.WriteLine($"You gained {xp} xp");
                Console.ResetColor();
                Player.CheckLevel();
            }
        }
    }
}
