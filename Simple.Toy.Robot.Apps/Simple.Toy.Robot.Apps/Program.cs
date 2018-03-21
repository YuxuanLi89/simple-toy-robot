using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Toy.Robot.Apps
{
    public class Program
    {
        public static void Main(string[] args)
        {
            StartGaming();
            ToyRobots lee = new ToyRobots();
            CommandProcessor cp = new CommandProcessor(lee);

            while (true)
            {
                string command = Console.ReadLine();

                if (command != "")
                {
                    Console.WriteLine(cp.ProcessCommand(command));
                }
            }
        }

        public static void StartGaming()
        {
            Console.WriteLine("***************Welcome to Toy Robot Game***************");
            Console.WriteLine("Please follow the instruction to start!");
            Console.WriteLine("Good luck! Cheers!");
        }
    }
}
