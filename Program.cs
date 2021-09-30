using System;
using System.Threading;
namespace RPGTextBasedAdventure
{
    class Program
    {
        private static string name = "";
        
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome! Press Enter to load game.");
            Console.ReadKey();

            ShowSimplePercentage();

            GetName();
        }

        static void ShowSimplePercentage()
        {
            for (int i = 0; i <= 100; i++)
            {
                Console.Write($"\rLoading: {i}%");
                if (i == 98 || i == 99)
                    Thread.Sleep(5000);
                else if (i >= 64 && i <= 72)
                    Thread.Sleep(1000);
                else
                    Thread.Sleep(25);
            }

            Console.Write("\rLoading complete! Welcome to the game!");
            Console.WriteLine("\nFirst off, please enter your name!");
        }

        static void GetName()
        {
            name = Console.ReadLine();
            if(name == "")
            {
                Console.WriteLine("Please write a correct name.");
                GetName();
            }
        }
    }
}
