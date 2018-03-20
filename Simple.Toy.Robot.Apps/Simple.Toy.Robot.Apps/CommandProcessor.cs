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

        public string ProcessCommand(string command)
        {
            return string.Empty;
        }
    }
}
