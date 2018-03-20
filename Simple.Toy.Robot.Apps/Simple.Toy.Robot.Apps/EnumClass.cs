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
        
        Move = 0,
        Left = 1,
        Right = 2,
        Place = 3,
        Report = 4,
    }
}
