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

        private const string invalidCommandError = "Invalid input, please try again!";
        private const string executeResult = "Please continue...";
        private const string invalidMoveError = "Invalid Move.";

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
                        if(ToyRobot.Place(paraRecorder.PositionX, paraRecorder.PositionY, paraRecorder.Direction))
                        {
                            ToyRobot.Hints = string.Format("{0},{1},{2}", paraRecorder.PositionX, paraRecorder.PositionY, paraRecorder.Direction.ToString());
                        }
                        else
                        {
                            ToyRobot.Hints = invalidCommandError;
                        }
                        break;
                    case Commands.MOVE:
                        if (ToyRobot.Move())
                        {
                            ToyRobot.Hints = executeResult;
                        }
                        else
                        {
                            ToyRobot.Hints = invalidMoveError;
                        }
                        break;
                    case Commands.LEFT:
                        if (ToyRobot.Left())
                        {
                            ToyRobot.Hints = executeResult;
                        }
                        else
                        {
                            ToyRobot.Hints = invalidCommandError;
                        }
                        break;
                    case Commands.RIGHT:
                        if (ToyRobot.Right())
                        {
                            ToyRobot.Hints = executeResult;
                        }
                        else
                        {
                            ToyRobot.Hints = invalidCommandError;
                        }
                        break;
                    case Commands.REPORT:
                        ToyRobot.Hints = ToyRobot.Report();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                return ToyRobot.Hints = invalidCommandError;
            }
            return ToyRobot.Hints;
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
                throw new Exception(invalidCommandError);
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
            ParamaterRecorder paramaterArgs = new ParamaterRecorder();
            try
            {
                var commandParameter = GetParameterString(userInput);
                var paramaters = commandParameter.Substring(commandParameter.IndexOf(" "));
                string[] parametersArray = paramaters.Split(',');
                if (parametersArray.Length == 3)
                {
                    paramaterArgs.PositionX = Convert.ToInt32(parametersArray[0]);
                    paramaterArgs.PositionY = Convert.ToInt32(parametersArray[1]);
                    paramaterArgs.Direction = (Direction)Enum.Parse(typeof(Direction), parametersArray[2], true);
                }
                else
                {
                    paramaterArgs = null;
                    ToyRobot.Hints = invalidCommandError;
                }
            }
            catch (Exception)
            {
                throw new Exception(invalidCommandError);
            }
            return paramaterArgs;
        }

        private bool CheckUserInput(string userInput)
        {
            try
            {
                if (Enum.IsDefined(typeof(Commands), userInput))
                {
                    if ((Commands)Enum.Parse(typeof(Commands), userInput, true) != Commands.PLACE)
                    {
                        if (CommandsCheck(userInput))
                        {
                            return true;
                        }
                        else
                        {
                            ToyRobot.Hints = invalidCommandError;
                            return false;
                        }
                    }
                    else
                    {
                        ToyRobot.Hints = invalidCommandError;
                        return false;
                    }
                }
                else
                {
                    if (userInput.IndexOf(" ") == -1)
                    {
                        ToyRobot.Hints = invalidCommandError;
                        return false;
                    }
                    else
                    {
                        string str = userInput.Substring(0, userInput.IndexOf(" "));
                        if (CommandsCheck(str))
                        {
                            return true;
                        }
                        else
                        {
                            ToyRobot.Hints = invalidCommandError;
                            return false;
                        }

                    }
                }
            }
            catch (Exception)
            {
                ToyRobot.Hints = invalidCommandError;
                return false;
            }
        }

        private bool CommandsCheck(string userInput)
        {
            try
            {
                if (Enum.IsDefined(typeof(Commands), userInput))
                {
                    if (ToyRobot.Direction == 0 && (Commands)Enum.Parse(typeof(Commands), userInput, true) != Commands.PLACE)
                    {
                        ToyRobot.Hints = invalidCommandError;
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    ToyRobot.Hints = invalidCommandError;
                    return false;
                }
            }
            catch (Exception)
            {
                ToyRobot.Hints = invalidCommandError;
                return false;
            }
        }
    }
}
