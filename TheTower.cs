using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RPGTextBasedAdventure
{
    class TheTower
    {
        string userInput;
        bool worldOne = false;
        bool worldTwo = false;
        bool worldThree = false;
        bool worldFour = false;
        string raceName = "";

        public void Start()
        {
            Console.Clear();
            Console.WriteLine("Welcome to The Tower.");
            Console.WriteLine("Here you can train to get more strength or go to a different world to test it!");
            Console.WriteLine("Please choose whether you would like to travel or train");
            FightOrTrain();
        }

        private void FightOrTrain()
        {
            userInput = Console.ReadLine();

            if (String.Compare(userInput, "travel", true) == 0)
            {
                Console.WriteLine("What world would you like to go to?");
                switch(Player.race)
                {
                    case 1:
                        raceName = "Witch";
                        worldOne = true;
                        worldTwo = false;
                        worldThree = true;
                        worldFour = true;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("1. Clouds");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("2. Demon Realm");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("3. Earth");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("4. Underground");
                        Console.ResetColor();
                        break;
                    case 2:
                        raceName = "Golem";
                        worldOne = false;
                        worldTwo = false;
                        worldThree = true;
                        worldFour = true;
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("1. Clouds");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("2. Demon Realm");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("3. Earth");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("4. Underground");
                        Console.ResetColor();
                        break;
                    case 3:
                        raceName = "ManBird";
                        worldOne = true;
                        worldTwo = false;
                        worldThree = true;
                        worldFour = true;
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("1. Clouds");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("2. Demon Realm");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("3. Earth");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("4. Underground");
                        Console.ResetColor();
                        break;
                    case 4:
                        raceName = "Human";
                        worldOne = false;
                        worldTwo = false;
                        worldThree = true;
                        worldFour = true;
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("1. Clouds");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("2. Demon Realm");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("3. Earth");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("4. Underground");
                        Console.ResetColor();
                        break;
                    case 5:
                        raceName = "Fire Spirit";
                        worldOne = false;
                        worldTwo = true;
                        worldThree = false;
                        worldFour = true;
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("1. Clouds");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("2. Demon Realm");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("3. Earth");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("4. Underground");
                        Console.ResetColor();
                        break;
                    case 6:
                        raceName = "Swift";
                        worldOne = false;
                        worldTwo = true;
                        worldThree = true;
                        worldFour = true;
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine("1. Clouds");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("2. Demon Realm");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("3. Earth");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("4. Underground");
                        Console.ResetColor();
                        break;
                }
                ChooseWorld();
            }
            else if (String.Compare(userInput, "train", true) == 0)
            {
                Random rdm = new Random();
                switch(rdm.Next(1, 6))
                {
                    case 1:
                        Console.WriteLine("You start running for 30 minutes.");
                        break;
                    case 2:
                        Console.WriteLine("You start air boxing for 20 minutes.");
                        break;
                    case 3:
                        Console.WriteLine("You start doing 100 sit-ups.");
                        break;
                    case 4:
                        Console.WriteLine("You start doing 100 push-ups.");
                        break;
                    case 5:
                        Console.WriteLine("You go completely zen for 5 days.");
                        break;
                }
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(3000);
                Console.Write(".");
                Thread.Sleep(1000);
                int xp = rdm.Next(90, 150);
                Console.WriteLine($"\nYour training was successful and you gained {xp} XP");
                Player.xp += xp;
                Player.CheckLevel();
                FightOrTrain();
            }
            else
            {
                Console.WriteLine("Please choose whether you would like to travel or train");
                FightOrTrain();
            }
        }

        private void ChooseWorld()
        {
            userInput = Console.ReadLine();

            if (String.Compare(userInput, "1", true) == 0 || String.Compare(userInput, "clouds", true) == 0)
            {
                if(worldOne)
                {
                    Console.WriteLine("Traveling to the Clouds...");
                    Clouds clouds = new Clouds();
                    Thread.Sleep(5000);
                    clouds.Start();
                }
                else
                {
                    Console.WriteLine($"You can't go to this world because you're a {raceName}");
                    ChooseWorld();
                }
            }
            else if (String.Compare(userInput, "2", true) == 0 || String.Compare(userInput, "demon realm", true) == 0)
            {
                if (worldTwo)
                {
                    Console.WriteLine("Traveling to the Demon Realm...");
                    DemonRealm demon = new DemonRealm();
                    Thread.Sleep(5000);
                    demon.Start();
                }
                else
                {
                    Console.WriteLine($"You can't go to this world because you're a {raceName}");
                    ChooseWorld();
                }
            }
            else if (String.Compare(userInput, "3", true) == 0 || String.Compare(userInput, "earth", true) == 0)
            {
                if (worldThree)
                {
                    Console.WriteLine("Traveling to Earth...");
                }
                else
                {
                    Console.WriteLine($"You can't go to this world because you're a {raceName}");
                    ChooseWorld();
                }
            }
            else if (String.Compare(userInput, "4", true) == 0 || String.Compare(userInput, "underground", true) == 0)
            {
                if (worldFour)
                {
                    Console.WriteLine("Traveling to the Underground...");
                }
                else
                {
                    Console.WriteLine($"You can't go to this world because you're a {raceName}");
                    ChooseWorld();
                }
            }
        }
    }
}
