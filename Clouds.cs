using System;
using System.Collections.Generic;
using System.Text;

namespace RPGTextBasedAdventure
{
    class Clouds
    {
        string userInput = "";
        Random rdm = new Random();

        public void Start()
        {
            Console.Clear();
            Console.WriteLine("You went up to the clouds!");
            Console.WriteLine("You end up completely surrounded by a cloud city.");
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
            else if(String.Compare(userInput, "go back", true) == 0 || String.Compare(userInput, "tower", true) == 0 || String.Compare(userInput, "back", true) == 0 || String.Compare(userInput, "the tower", true) == 0)
            {
                TheTower tower = new TheTower();
                Player.mission = false;
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
            switch(rdm.Next(1, 6))
            {
                case 1:
                    Console.WriteLine("You found a big crate!");
                    int xp = rdm.Next(1, 500);
                    Console.WriteLine($"You look inside the chest and find {xp} xp points and a lot of old jewelry!");

                    if(!Player.mission)
                    {
                        Console.WriteLine("You pick up the xp points");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"You gained {xp} xp points");
                        Console.ResetColor();
                        Player.xp += xp;
                        Player.CheckLevel();
                    }
                    else
                    {
                        Console.WriteLine("You back to the old lady to give her chest back!");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Lady: WOW! Thank you so much!");
                        Console.WriteLine("Lady: Here's your reward!");
                        Console.WriteLine($"\nYou gained {xp * 2} xp points!");
                        Console.ResetColor();
                        Player.xp += xp * 2;
                        Player.CheckLevel();
                        Player.mission = false;
                    }

                    Console.WriteLine("Which direction would you like to go now?");
                    Directions();
                    break;
                case 2:
                    if(!Player.mission)
                    {
                        Console.WriteLine("You see an old lady");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Lady: Hey there!");
                        LadyEncounter();
                    }
                    else
                    {
                        Console.WriteLine("You've encountered a ManBird!");
                        new Attacking(3);
                        Directions();
                    }
                    break;
                case 3:
                    Console.WriteLine("You've encountered a ManBird!");
                    new Attacking(3);
                    Directions();
                    break;
                case 4:
                    Console.WriteLine("You've encountered a Witch!");
                    new Attacking(1);
                    Directions();
                    break;
                case 5:
                    Console.WriteLine("You've encountered a Witch!");
                    new Attacking(1);
                    Directions();
                    break;
            }
        }

        private void LadyEncounter()
        {
            Console.WriteLine("Lady: Could you help me with something?");

            Console.ResetColor();
            userInput = Console.ReadLine();

            if (String.Compare(userInput, "yes", true) == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Lady: WOW! Thank you so much!");
                Console.WriteLine("Lady: I need help looking for an old chest of mine");
                Console.WriteLine("Lady: It's somewhere around here but I'm to old to find it");
                Console.WriteLine("Lady: If you help me find it, I will give you lots of xp as a reward");
                Console.ResetColor();
                Console.WriteLine("You decide to help the lady.");
                Console.WriteLine("Which way would you like to go?");
                Player.mission = true;
                Directions();
            }
            else if (String.Compare(userInput, "no", true) == 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Lady: WOW! So rude!");
                Console.ResetColor();
                Console.WriteLine("The lady walks away");
                Console.WriteLine("Which way would you like to go?");
                Directions();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                LadyEncounter();
            }
        }
    }
}
