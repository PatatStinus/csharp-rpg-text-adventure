using System;
using System.Threading;
namespace RPGTextBasedAdventure
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadKey();

            ShowSimplePercentage();

            Console.ReadKey();
        }

        static void ShowSimplePercentage()
        {
            for (int i = 0; i <= 100; i++)
            {
                Console.Write($"\rProgress: {i}%   ");
                if(i == 99)
                    Thread.Sleep(10000);
                else
                    Thread.Sleep(25);
            }

            Console.Write("\rDone!          ");
        }
    }
}
