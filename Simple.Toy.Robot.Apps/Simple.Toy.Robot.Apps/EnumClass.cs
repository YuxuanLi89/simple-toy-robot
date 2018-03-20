using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Toy.Robot.Apps
{
    public enum Direction
    {
        EAST = 1,
        SOUTH = 2,
        WEST = 3,
        NORTH = 4,
    }

    public enum Commands
    {

        PLACE = 1,
        MOVE = 2,
        LEFT = 3,
        RIGHT = 4,
        REPORT = 5
    }
}
