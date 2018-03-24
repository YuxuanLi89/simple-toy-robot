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
        public int? PositionX { get; set; }
        public int? PositionY { get; set; }
        public Direction Direction { get; set; }
        public Commands Commands { get; set; }
        public string _hints = string.Empty;
        #endregion
        public string Hints
        {
            get { return _hints; }
            set { _hints = value; }
        }

        public bool Place(int positionX, int positionY, Direction direction)
        {
            try
            {
                if (positionX < 0 || positionY < 0 || positionX > boundLimits || positionY > boundLimits)
                {
                    return false;
                }
                else
                {
                    PositionX = positionX;
                    PositionY = positionY;
                    Direction = direction;
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
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
            if (CheckMove())
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

        public int LeftRightMove()
        {
            if (Direction == Direction.EAST)
            {
                return PositionX.Value + 1;
            }
            if (Direction == Direction.WEST)
            {
                return PositionX.Value - 1;
            }

            return PositionX.Value;
        }

        public int UpDownMove()
        {
            if (Direction == Direction.NORTH)
            {
                return PositionY.Value + 1;
            }
            if (Direction == Direction.SOUTH)
            {
                return PositionY.Value - 1;
            }

            return PositionY.Value;
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
                Direction = (Convert.ToInt32(Direction) < 4) ? Direction + 1 : Direction - 3;
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
                return false;
            }
            return true;
        }

        public bool CheckMove()
        {
            if (PositionX.HasValue && PositionY.HasValue )
            {
                if (PositionX > boundLimits || PositionY > boundLimits)
                {
                    return false;
                }
                return true;
            }
            else
            {
                Hints = String.Format("Please 'PLACE' a postion before you 'MOVE'.");
                return false;
            }

        }

    }
}
