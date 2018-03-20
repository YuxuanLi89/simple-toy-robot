using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simple.Toy.Robot.Apps
{
    public class ToyRobots
    {
        public ToyRobots(){}

        private int horizontalX;
        private int verticalY;
        private int boundaryLimits = 5;
        private Direction direction;
        public string _hits = string.Empty;
        public string Hits
        {
            get { return _hits; }
            set { _hits = value; }
        }

        public string Report()
        {
            if (horizontalX <=5 && verticalY <=5)
            {
                return String.Format("{0},{1},{2}", horizontalX, verticalY, direction.ToString());
            }
            return "";
        }

        public bool Move()
        {
            if (CheckCommand("move"))
            {
                int newhorizontalX = HorizontalMove();
                int newverticalY = VerticalMove();
                if (CheckPosition(newhorizontalX, newverticalY))
                {
                    horizontalX = newhorizontalX;
                    verticalY = newverticalY;
                    return true;
                }
            }
            return false;
        }

        private int HorizontalMove()
        {
            if (direction == Direction.EAST)
            {
                return horizontalX + 1;
            }
            if (direction == Direction.WEST)
            {
                return horizontalX - 1;
            }

            return horizontalX;
        }

        private int VerticalMove()
        {
            if (direction == Direction.NORTH)
            {
                return verticalY + 1;
            }
            if (direction == Direction.SOUTH)
            {
                return verticalY - 1;
            }

            return verticalY;
        }

        public bool CheckPosition(int x, int y)
        {
            if (x > 5 || y > 5)
            {
                Hits = ("Toy Robot will drop down from table");
                return false;
            }
            return true;
        }

        public bool CheckCommand(string commands)
        {
            if (horizontalX >=5 || verticalY >= 5)
            {
                Hits = String.Format("Cannot {0}, please select a valid position.", commands);
                return false;
            }
            return true;
        }

    }
}
