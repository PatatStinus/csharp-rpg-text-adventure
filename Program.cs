using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace RPGTextBasedAdventure
{
    class Program
    {
        private static List<string> fileNames = new List<string>();
        private static int chosenRace = 0;
        private static string userInput;
        private static int usedSaveFile = 0;
        private static bool characterSelected = false;

        static bool ConsoleEventCallback(int eventType)
        {
            if (eventType == 2)
            {
                Console.WriteLine("Closing window. Please wait...");
                if(characterSelected)
                {
                    StreamWriter newFile = File.CreateText("savefiles/" + fileNames[usedSaveFile]);
                    newFile.WriteLine(Player.name);
                    newFile.WriteLine(Player.level); //LVL
                    newFile.WriteLine(Player.xp); //XP
                    newFile.WriteLine(Player.race); //Race
                    for (int i = 0; i < Player.attacks.Count; i++)
                        newFile.WriteLine(Player.attacks[i]);
                    newFile.Close();
                }
                Console.ReadLine();
            }
            return false;
        }

        static ConsoleEventDelegate handler;
        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);

        static void Main(string[] args)
        {
            handler = new ConsoleEventDelegate(ConsoleEventCallback);
            SetConsoleCtrlHandler(handler, true);

            Console.WriteLine("Welcome! Press Enter to start the game!");

            userInput = Console.ReadLine();

            GetSaveFiles();

            Console.WriteLine("Please choose a save file!");

            SaveFiles();

            ChooseFile();
        }

        static void GetSaveFiles()
        {
            if (!Directory.Exists("savefiles"))
                Directory.CreateDirectory("savefiles");

            string[] files = Directory.GetFiles("savefiles", "*.*", SearchOption.AllDirectories);
            foreach(string file in files)
                fileNames.Add(Path.GetFileName(file));
        }

        static void SaveFiles()
        {
            StreamReader saveFiles;
            for (int i = 0; i < fileNames.Count; i++)
            {
                Console.WriteLine(i + 1 + ".");
                saveFiles = File.OpenText("savefiles/" + fileNames[i]);
                string line = saveFiles.ReadLine();
                int numbLine = 0;
                while(line != null)
                {
                    switch(numbLine)
                    {
                        case 0:
                            Console.WriteLine(" Name = " + line);
                            break;
                        case 1:
                            Console.WriteLine(" Level = " + line);
                            break;
                        case 2:
                            Console.WriteLine(" XP = " + line);
                            break;
                        case 3:
                            switch(Int16.Parse(line))
                            {
                                case 1:
                                    Console.WriteLine(" Race = Witch");
                                    break;
                                case 2:
                                    Console.WriteLine(" Race = Golem");
                                    break;
                                case 3:
                                    Console.WriteLine(" Race = ManBird");
                                    break;
                                case 4:
                                    Console.WriteLine(" Race = Human");
                                    break;
                                case 5:
                                    Console.WriteLine(" Race = Fire Spirit");
                                    break;
                                case 6:
                                    Console.WriteLine(" Race = Swift");
                                    break;
                            }
                            break;
                    }
                    line = saveFiles.ReadLine();
                    numbLine++;
                }
                saveFiles.Close();
            }

            Console.WriteLine((fileNames.Count + 1) + ".");
            Console.WriteLine(" New Save file");
            Console.WriteLine();
        }

        static void ChooseFile()
        {
            string typedFile = Console.ReadLine();

            if(Int16.TryParse(typedFile, out short j) && j <= fileNames.Count + 1 )
            {
                if (j == fileNames.Count + 1)
                {
                    Console.WriteLine("Please enter a name for your save file!");
                    fileNames.Add(Console.ReadLine() + ".txt");
                    StreamWriter newFile = File.CreateText("savefiles/" + fileNames[j - 1]);
                    usedSaveFile = j - 1;
                    Console.WriteLine("Enter your character name");
                    Player.name = Console.ReadLine();
                    newFile.WriteLine(Player.name);
                    newFile.WriteLine("1"); //LVL
                    newFile.WriteLine("0"); //XP
                    Console.WriteLine("Please Choose 1 of the 6 races you want your character to be:");
                    ChooseRace();
                    newFile.WriteLine(chosenRace); //Race
                    newFile.WriteLine("1"); //Base Attack
                    Player.level = 1;
                    Player.xp = 0;
                    Player.attacks.Add(1);
                    Player.race = chosenRace;
                    newFile.Close();
                    Console.WriteLine("Save File Created!");
                    characterSelected = true;
                    Console.WriteLine("\nYou will now be send to The Tower...");
                    Thread.Sleep(5000);
                    TheTower tower = new TheTower();
                    tower.Start();
                }
                else
                {
                    StreamReader saveFiles;
                    saveFiles = File.OpenText("savefiles/" + fileNames[j - 1]);
                    usedSaveFile = j - 1;
                    string line = saveFiles.ReadLine();
                    int numbLine = 0;
                    while (line != null)
                    {
                        switch (numbLine)
                        {
                            case 0:
                                Player.name = line;
                                break;
                            case 1:
                                Player.level = Int32.Parse(line);
                                break;
                            case 2:
                                Player.xp = Int32.Parse(line);
                                break;
                            case 3:
                                Player.race = Int16.Parse(line);
                                break;
                            default:
                                Player.attacks.Add(Int16.Parse(line));
                                break;
                        }
                        line = saveFiles.ReadLine();
                        numbLine++;
                    }
                    saveFiles.Close();
                    Console.WriteLine("Save file selected!");
                    characterSelected = true;
                    Console.WriteLine("\nYou will now be send to The Tower...");
                    Thread.Sleep(5000);
                    TheTower tower = new TheTower();
                    tower.Start();
                }
            }
            else
            {
                Console.WriteLine("Please type the number of the save file you want to select.");
                ChooseFile();
            }
        }

        static void ChooseRace()
        {
            Console.WriteLine("1. \n Witch");
            Console.WriteLine("2. \n Golem");
            Console.WriteLine("3. \n ManBird");
            Console.WriteLine("4. \n Human");
            Console.WriteLine("5. \n Fire Spirit");
            Console.WriteLine("6. \n Swift");
            Console.WriteLine("Type [Race Number] + help to learn more about the race. Example: 3help");
            RaceInput();
        }

        static void RaceInput()
        {
            while(chosenRace == 0)
            {
                userInput = Console.ReadLine();
                if (String.Compare(userInput, "1help", true) == 0)
                    Console.WriteLine("The Witch has magic powers that can shoot long and short range and also has good defense. \nCounters: ManBird + Fire Spirit");
                else if (String.Compare(userInput, "2help", true) == 0)
                    Console.WriteLine("The Golem is immune to magic powers, does really hard hitting melee attacks and has good defense but is very slow. \nCounters: Witch + Human");
                else if (String.Compare(userInput, "3help", true) == 0)
                    Console.WriteLine("The ManBird is the only race with flight, can dodge melee attacks. \nCounters: Golem + Swift");
                else if (String.Compare(userInput, "4help", true) == 0)
                    Console.WriteLine("The Human is the basic race, medium stats on everything. \nCounters: Fire Spirit + ManBird");
                else if (String.Compare(userInput, "5help", true) == 0)
                    Console.WriteLine("The Fire Spirit is really fast and does a lot of damage but all stats decrease when exposed to light. \nCounters: Human + Swift");
                else if (String.Compare(userInput, "6help", true) == 0)
                    Console.WriteLine("The Swift is the fastest race and can dodge some attacks randomly but has low damage and low defense. \nCounters: Witch + Golem");
                else if (String.Compare(userInput, "1", true) == 0)
                {
                    Console.WriteLine("You have chosen to become the Witch!");
                    chosenRace = Int16.Parse(userInput);
                    break;
                }
                else if (String.Compare(userInput, "2", true) == 0)
                {
                    Console.WriteLine("You have chosen to become the Golem!");
                    chosenRace = Int16.Parse(userInput);
                    break;
                }
                else if (String.Compare(userInput, "3", true) == 0)
                {
                    Console.WriteLine("You have chosen to become the ManBird!");
                    chosenRace = Int16.Parse(userInput);
                    break;
                }
                else if (String.Compare(userInput, "4", true) == 0)
                {
                    Console.WriteLine("You have chosen to become the Human!");
                    chosenRace = Int16.Parse(userInput);
                    break;
                }
                else if (String.Compare(userInput, "5", true) == 0)
                {
                    Console.WriteLine("You have chosen to become the Fire Spirit!");
                    chosenRace = Int16.Parse(userInput);
                    break;
                }
                else if (String.Compare(userInput, "6", true) == 0)
                {
                    Console.WriteLine("You have chosen to become the Swift!");
                    chosenRace = Int16.Parse(userInput);
                    break;
                }
                else
                    Console.WriteLine("Please choose a race you would like to be!");
            }
        }
    }
}
