using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.Toy.Robot.Apps;

namespace Toy.Robot.Apps.Tests
{
    [TestClass]
    public class ToyRobotTests
    {
        [TestMethod]
        public void CannotBeMoved_IfNotInitializePosition()  //If not initialize a position, cannot be move
        {
            var robot = new ToyRobots();
            var result = robot.Move();
            Assert.IsFalse(result);
            Assert.AreEqual("Please 'PLACE' a postion before you execute any command.", robot.Hints);
        }

        [TestMethod]
        public void CannotBeTurned_IfNotInitializePosition()  //If not initialize a position, cannot be turn direction
        {
            var robot = new ToyRobots();
            var result = robot.Left();
            Assert.IsFalse(result);
            Assert.AreEqual("Please 'PLACE' a postion before you execute any command.", robot.Hints);
        }

        [TestMethod]
        public void CannotReportPosition_IfNotInitializePosition()  //If not initialize a position, cannot be report position
        {
            var robot = new ToyRobots();
            var result = robot.Report();
            Assert.AreEqual("Please 'PLACE' a postion before you execute any command.", robot.Hints);
        }

        [TestMethod]
        public void CannotBePlaced_IfPositionInvalid()  //Placed position must in table area of 5 x 5
        {
            var robot = new ToyRobots();
            var result = robot.Place(-2, -2, Direction.NORTH);
            Assert.IsFalse(result);
            Assert.AreEqual("Position out of table!", robot.Hints);

            result = robot.Place(6, 6, Direction.NORTH);
            Assert.IsFalse(result);
            Assert.AreEqual("Position out of table!", robot.Hints);
        }

        [TestMethod]
        public void CanReportPosition_IfPositionValid()  //Correct action of place and report position
        {
            var robot = new ToyRobots();
            var result = robot.Place(3, 2, Direction.EAST);
            var position = robot.Report();
            Assert.IsTrue(result);
            Assert.AreEqual("3,2,EAST", position);
        }

        [TestMethod]
        public void CanReportPositionCorrect_WhenTurnLeftOrRight()  //Robot can be turn to right or left only position has been initialized
        {
            var robot = new ToyRobots();
            robot.Place(0, 0, Direction.SOUTH);
            robot.Left();
            Assert.AreEqual("0,0,EAST", robot.Report());

            robot.Place(4, 4, Direction.WEST);
            robot.Right();
            Assert.AreEqual("4,4,NORTH", robot.Report());
        }

        [TestMethod]
        public void CannotBeMoved_RobotWillDropOfTable()  //Only can move inside of table size 5x5
        {
            var robot = new ToyRobots();
            robot.Place(0, 0, Direction.SOUTH);
            var result = robot.Move();
            Assert.IsFalse(result);
            Assert.AreEqual("Robot will drop of the table, invalid position.", robot.Hints);
            Assert.AreEqual("0,0,SOUTH", robot.Report());

            robot.Place(5, 5, Direction.EAST);
            result = robot.Move();
            Assert.IsFalse(result);
            Assert.AreEqual("Robot will drop of the table, invalid position.", robot.Hints);
            Assert.AreEqual("5,5,EAST", robot.Report());
        }

        [TestMethod]
        public void CanbePlaced_Moved_ReportPositionCorrect_1()  //Initialize validated position, move and report
        {
            var robot = new ToyRobots();
            robot.Place(1, 3, Direction.NORTH);
            robot.Move();
            Assert.AreEqual("1,4,NORTH", robot.Report());
        }

        [TestMethod]
        public void CanbePlaced_Moved_ReportPositionCorrect_2()  //Initialize validated position, move multiple times, turn direction and report
        {
            var robot = new ToyRobots();
            robot.Place(1, 1, Direction.NORTH);
            robot.Move();
            robot.Move();
            robot.Left();
            robot.Move();
            Assert.AreEqual("0,3,WEST", robot.Report());
        }
    }
}
