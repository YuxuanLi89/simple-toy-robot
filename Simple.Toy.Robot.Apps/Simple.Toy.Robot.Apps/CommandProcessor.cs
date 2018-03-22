using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Toy.Robot.Apps
{
    public class CommandProcessor
    {
        public CommandProcessor(ToyRobots robot)
        {
            this.ToyRobot = robot;
        }

        public ToyRobots ToyRobot { get; set; }

        public string ProcessCommand(string userInput)
        {
            string result = string.Empty;
            string command = string.Empty;
            ParamaterRecorder paraRecorder = new ParamaterRecorder();

            try
            {
                if (CheckUserInput(userInput))
                {
                    command = GetCommand(userInput);
                }
                else
                {
                    return ToyRobot.Hints;
                }

                switch ((Commands)Enum.Parse(typeof(Commands), command, true))
                {
                    case Commands.PLACE:
                        paraRecorder = RecordParameter(userInput);
                        ToyRobot.Place(paraRecorder.PositionX, paraRecorder.PositionY, paraRecorder.direction);
                        result = string.Format("{0},{1},{2}", paraRecorder.PositionX, paraRecorder.PositionY, paraRecorder.direction.ToString());
                        break;
                    case Commands.MOVE:
                        ToyRobot.Move();
                        break;
                    case Commands.LEFT:
                        ToyRobot.Left();
                        break;
                    case Commands.RIGHT:
                        ToyRobot.Right();
                        break;
                    case Commands.REPORT:
                        result = ToyRobot.Report();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                Console.Write("Unknown command please try again.");
            }

            

            return result;
        }

        private string GetCommand(string userInput)
        {
            string command = string.Empty;
            try
            {
                if (Enum.IsDefined(typeof(Commands), userInput))
                {
                    command = userInput;
                }
                else
                {
                    command = userInput.Substring(0, userInput.IndexOf(" "));
                }
            }
            catch (Exception)
            {
                throw new Exception("Invalid Command coming!");
            }

            return command;
        }

        private string GetParameterString(string userInput)
        {
            string parameterString = string.Empty;
            if (!string.IsNullOrEmpty(userInput))
            {
                parameterString = userInput.Substring(userInput.IndexOf(" "));
            }
            return parameterString;
        }
        
        private ParamaterRecorder RecordParameter(string userInput)
        {
            ParamaterRecorder pr = new ParamaterRecorder();
            try
            {
                var commandParameter = GetParameterString(userInput);
                var paramaters = commandParameter.Substring(commandParameter.IndexOf(" "));
                string[] parametersArray = paramaters.Split(',');
                if (parametersArray.Length == 3)
                {
                    pr.PositionX = Convert.ToInt32(parametersArray[0]);
                    pr.PositionY = Convert.ToInt32(parametersArray[1]);
                    pr.direction = (Direction)Enum.Parse(typeof(Direction), parametersArray[2], true);
                }

            }
            catch (Exception)
            {

                throw new Exception("Cannot get position X!");
            }
            return pr;
        }

        private bool CheckUserInput(string userInput)
        {
            try
            {
                if (Enum.IsDefined(typeof(Commands), userInput))
                {
                    if ((Commands)Enum.Parse(typeof(Commands), userInput, true) != Commands.PLACE)
                    {
                        if (CommandsInitializeChecking(userInput))
                        {
                            return true;
                        }
                        else
                        {
                            Console.Write("Unknown command please try again.");
                            return false;
                        }
                    }
                    else
                    {
                        Console.Write("Unknown command please try again.");
                        return false;
                    }
                }
                else
                {
                    if (userInput.IndexOf(" ") == -1)
                    {
                        Console.Write("Unknown command please try again.");
                        return false;
                    }
                    else
                    {
                        string str = userInput.Substring(0, userInput.IndexOf(" "));
                        if (CommandsInitializeChecking(str))
                        {
                            return true;
                        }
                        else
                        {
                            Console.Write("Unknown command please try again.");
                            return false;
                        }

                    }
                }
            }
            catch (Exception)
            {

                Console.Write("Unknown error please try again.");
                return true;
                throw new Exception();
            }
        }

        private bool CommandsInitializeChecking(string userInput)
        {
            try
            {
                if (Enum.IsDefined(typeof(Commands), userInput))
                {
                    if ((Commands)Enum.Parse(typeof(Commands), userInput, true) != Commands.PLACE)
                    {
                        Console.Write("Please 'PLACE' a position to start.");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    Console.Write("Unknown command please try again.");
                    return false;
                }
            }
            catch (Exception)
            {
                Console.Write("Unknown error please try again.");
                return true;
                throw new Exception();
            }
        }

        private bool CheckParameter(string userInput)
        {
            return true;
        }
    }
}
