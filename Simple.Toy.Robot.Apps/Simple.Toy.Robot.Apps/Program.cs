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
            ToyRobots robot = new ToyRobots();
            CommandProcessor processor = new CommandProcessor(robot);
            while (true)
            {
                string userInput = Console.ReadLine().ToUpper();

                if (userInput == "EXIT" || userInput == "CLEAN" || userInput == "")
                {
                    if (userInput == "EXIT") //Quit game
                    {
                        Environment.Exit(0);
                    }
                    else if (userInput == "CLEAN") //Clean console screen
                    {
                        Console.Clear();
                        StartGaming();
                    }
                }
                else
                {
                    Console.WriteLine(processor.ProcessCommand(userInput)); //Execute validate command
                }
            }
        }

        public static void StartGaming()
        {
            Console.WriteLine("*******************Welcome to Toy Robot Game*******************");
            Console.WriteLine("1. Input must be like: Command PositionX PositionY Direction");
            Console.WriteLine("2. Command including: PLACE, MOVE, REPORT, LEFT, RIGHT");
            Console.WriteLine("3. Position X and Y must be Int number between 0 and 5");
            Console.WriteLine("4. Direction including: EAST, WEST, NORTH, SOUTH");
            Console.WriteLine("5. Type 'CLEAN' to clean console screen, 'EXIT' to quit gaming");
            Console.WriteLine("***************************************************************");
        }
    }
}
