using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Toy.Robot.Apps
{
    public class ParamaterRecorder
    {
        //Object of store current robot position and facing direction parameters
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public Direction Direction { get; set; }
    }
}
