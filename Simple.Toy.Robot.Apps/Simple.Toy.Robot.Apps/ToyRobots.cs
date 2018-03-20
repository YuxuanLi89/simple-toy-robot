using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Toy.Robot.Apps
{
    public class ToyRobots
    {
        #region Properties
        private int boundLimits = 5;
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public Direction Direction { get; set; }
        public Commands Commands { get; set; }
        public string _hints = string.Empty;
        #endregion
        public string Hints
        {
            get { return _hints; }
            set { _hints = value; }
        }

        public string Report()
        {
            if (PositionX <=5 && PositionY <=5)
            {
                return String.Format("{0},{1},{2}", PositionX, PositionY, Direction.ToString());
            }
            return "";
        }

        public bool Move()
        {
            if (CheckCommand("move"))
            {
                int newPositionX = LeftRightMove();
                int newPositionY = UpDownMove();
                if (CheckPosition(newPositionX, newPositionY))
                {
                    PositionX = newPositionX;
                    PositionY = newPositionY;
                    return true;
                }
            }
            return false;
        }

        private int LeftRightMove()
        {
            if (Direction == Direction.EAST)
            {
                return PositionX + 1;
            }
            if (Direction == Direction.WEST)
            {
                return PositionX - 1;
            }

            return PositionX;
        }

        private int UpDownMove()
        {
            if (Direction == Direction.NORTH)
            {
                return PositionY + 1;
            }
            if (Direction == Direction.SOUTH)
            {
                return PositionY - 1;
            }

            return PositionY;
        }

        public bool Left()
        {
            try
            {
                Direction = (Convert.ToInt32(Direction) > 1) ? Direction - 1 : Direction + 3;
            }
            catch (Exception)
            {
                return false;
                throw new Exception("Toy Robot cannot move to left");
            }
            return true;
        }

        public bool Right()
        {
            try
            {
                Direction = (Convert.ToInt16(Direction) < 4) ? Direction + 1 : Direction - 3;
            }
            catch (Exception)
            {
                return false;
                throw new Exception("Toy Robot cannot move to right");
            }
            return true;
        }

        public bool CheckPosition(int x, int y)
        {
            if (x < 0 || y < 0 || x > boundLimits || y > boundLimits)
            {
                Hints = ("Toy Robot will drop down from table");
                return false;
            }
            return true;
        }

        public bool CheckCommand(string Commands)
        {
            if (PositionX >= boundLimits || PositionY >= boundLimits)
            {
                Hints = String.Format("Cannot {0}, please select a valid Position.", Commands);
                return false;
            }
            return true;
        }

    }
}
