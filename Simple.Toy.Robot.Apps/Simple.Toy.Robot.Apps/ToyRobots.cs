using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Toy.Robot.Apps
{
    public class ToyRobots
    {
        private int boundLimits = 5;    // Default table size is 5 x 5, only in this area is validated
        public int? PositionX { get; set; }
        public int? PositionY { get; set; }
        public Direction Direction { get; set; }
        public Commands Commands { get; set; }
        public string _hints = string.Empty;    // To record robot response after took actions

        public string Hints
        {
            get { return _hints; }
            set { _hints = value; }
        }

        public bool Place(int positionX, int positionY, Direction direction)
        {
            // If position x and y not inside of 5 x 5, report!
            try
            {
                if (positionX < 0 || positionY < 0 || positionX > boundLimits || positionY > boundLimits)
                {
                    Hints = "Position out of table!";
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
                // Unknown error
                return false;
            }
        }

        public string Report()
        {
            // Return current robot position
            if (CheckPosition())
            {
                return String.Format("{0},{1},{2}", PositionX, PositionY, Direction.ToString());
            }
            Hints = "Please 'PLACE' a postion before you execute any command.";
            return Hints;
        }

        public bool Move()
        {
            // Execute 'Move' command
            if (CheckPosition())
            {
                int newPositionX = LeftRightMove();
                int newPositionY = UpDownMove();
                if (CheckIfOnTable(newPositionX, newPositionY))
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
            // Moving horizontally 
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
            // Moving vertically
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
            // Change robot facing direction, turn to left
            try
            {
                if (CheckPosition())
                {
                    Direction = (Convert.ToInt32(Direction) > 1) ? Direction - 1 : Direction + 3;
                }
                else
                {
                    Hints = "Please 'PLACE' a postion before you execute any command.";
                    return false;
                }
            }
            catch (Exception)
            {
                throw new Exception("Toy Robot cannot move to left");
            }
            return true;
        }

        public bool Right()
        {
            // Change robot facing direction, turn to right
            try
            {
                if (CheckPosition())
                {
                    Direction = (Convert.ToInt32(Direction) < 4) ? Direction + 1 : Direction - 3;
                }
                else
                {
                    Hints = "Please 'PLACE' a postion before you execute any command.";
                    return false;
                }
            }
            catch (Exception)
            {
                throw new Exception("Toy Robot cannot move to right");
            }
            return true;
        }

        public bool CheckIfOnTable(int x, int y)
        {
            // Check if inside of default table area
            if (x < 0 || y < 0 || x > boundLimits || y > boundLimits)
            {
                Hints = "Robot will drop of the table, invalid position.";
                return false;
            }
            return true;
        }

        public bool CheckPosition()
        {
            // Check if robot has been initialize or not, as well check position is valid or not
            if (PositionX.HasValue && PositionY.HasValue )
            {
                if (PositionX > boundLimits || PositionY > boundLimits)
                {
                    Hints = "Robot will drop of the table, invalid position.";
                    return false;
                }
                return true;
            }
            else
            {
                Hints = "Please 'PLACE' a postion before you execute any command.";
                return false;
            }

        }

    }
}
