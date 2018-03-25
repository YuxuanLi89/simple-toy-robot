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
        private const string invalidCommandError = "Invalid input, please try again!"; // Reminder for invalid user input
        private const string executeResult = "Please continue..."; // Reminder for valid robot execution
        private const string invalidMoveError = "Invalid Move."; // Reminder for invalid robot movement

        public string ProcessCommand(string userInput)
        {
            string result = string.Empty;
            string command = string.Empty;
            ParamaterRecorder paraRecorder = new ParamaterRecorder();
            try
            {
                // If user input contains correct command
                if (CheckUserInput(userInput))
                {
                    command = GetCommand(userInput);
                }
                else
                {
                    return ToyRobot.Hints;
                }

                // Execution based on command
                switch ((Commands)Enum.Parse(typeof(Commands), command, true))
                {
                    case Commands.PLACE: // If valid, show placed position
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
                    case Commands.MOVE: // If valid movement, reminder user continue play
                        if (ToyRobot.Move())
                        {
                            ToyRobot.Hints = executeResult;
                        }
                        else
                        {
                            ToyRobot.Hints = invalidMoveError;
                        }
                        break;
                    case Commands.LEFT: // If turn left is valid, reminder user continue play
                        if (ToyRobot.Left())
                        {
                            ToyRobot.Hints = executeResult;
                        }
                        else
                        {
                            ToyRobot.Hints = invalidCommandError;
                        }
                        break;
                    case Commands.RIGHT: // If turn right is valid, reminder user continue play
                        if (ToyRobot.Right())
                        {
                            ToyRobot.Hints = executeResult;
                        }
                        else
                        {
                            ToyRobot.Hints = invalidCommandError;
                        }
                        break;
                    case Commands.REPORT: // Report robot current position
                        ToyRobot.Hints = ToyRobot.Report();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
                // Invalid user input
                return ToyRobot.Hints = invalidCommandError;
            }
            return ToyRobot.Hints;
        }

        private string GetCommand(string userInput)
        {
            // Return command from 'Commands' Enum based on user input
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
            // Return string of position X,Y and direction
            string parameterString = string.Empty;
            if (!string.IsNullOrEmpty(userInput))
            {
                parameterString = userInput.Substring(userInput.IndexOf(" "));
            }
            return parameterString;
        }
        
        private ParamaterRecorder RecordParameter(string userInput)
        {
            // Split parameter string to list, and stored position X, position Y and robot direction into parameter recorder 
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
            // Check if user input is validated command and parameters or not
            try
            {
                if (Enum.IsDefined(typeof(Commands), userInput))
                {
                    if ((Commands)Enum.Parse(typeof(Commands), userInput, true) != Commands.PLACE)
                    {
                        if (CommandsReconganize(userInput))
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
                        if (CommandsReconganize(str))
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

        private bool CommandsReconganize(string userInput)
        {
            // Reconganize robot command match 'Commands' Enum or not, then validated robot action correct or wrong
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
