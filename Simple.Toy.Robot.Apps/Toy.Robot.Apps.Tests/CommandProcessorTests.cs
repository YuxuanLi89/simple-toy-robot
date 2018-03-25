using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.Toy.Robot.Apps;

namespace Toy.Robot.Apps.Tests
{
    [TestClass]
    public class CommandProcessorTests
    {
        [TestMethod]
        public void EmptyCommand_IsInvalidated() //If user input is space or blank, report as invalid input 
        {
            var processor = new CommandProcessor(new ToyRobots());
            var response = processor.ProcessCommand("");
            Assert.AreEqual("Invalid input, please try again!", response);
        }

        [TestMethod]
        public void UnknownCommand_IsInvalidated() //If user input out of rules, report as invalid input 
        {
            var processor = new CommandProcessor(new ToyRobots());
            var response = processor.ProcessCommand("abc@# 1");
            Assert.AreEqual("Invalid input, please try again!", response);
        }

        [TestMethod]
        public void ValidatedCommandButNotInitializePosition_IsInvalidated() //Must initialize position before move, report as invalid input
        {
            var processor = new CommandProcessor(new ToyRobots());
            var response = processor.ProcessCommand("MOVE");
            Assert.AreEqual("Invalid input, please try again!", response);
        }

        [TestMethod]
        public void OnlyPlaceCommandWithOutParameters_IsInvalidated() //Initialize position without correct command format, report as invalid input
        {
            var processor = new CommandProcessor(new ToyRobots());
            var response = processor.ProcessCommand("PLACE");
            Assert.AreEqual("Invalid input, please try again!", response);
        }

        [TestMethod]
        public void ValidatedCommandWithInvalidatedParameters_IsInvalidated() //Few examples of invalid user input
        {
            var processor = new CommandProcessor(new ToyRobots());
            var response = processor.ProcessCommand("PLACEE");
            Assert.AreEqual("Invalid input, please try again!", response);

            response = processor.ProcessCommand("1,1,EAST");
            Assert.AreEqual("Invalid input, please try again!", response);

            response = processor.ProcessCommand("PLACE 1,1");
            Assert.AreEqual("Invalid input, please try again!", response);

            response = processor.ProcessCommand("PLACE A,A");
            Assert.AreEqual("Invalid input, please try again!", response);

            response = processor.ProcessCommand("PLACE 1,1,2,WEST");
            Assert.AreEqual("Invalid input, please try again!", response);

            response = processor.ProcessCommand("PLACE 11NORTH");
            Assert.AreEqual("Invalid input, please try again!", response);

            response = processor.ProcessCommand("PLACE,1,1,NORTH");
            Assert.AreEqual("Invalid input, please try again!", response);

            response = processor.ProcessCommand("PLACE 1,1,ABC");
            Assert.AreEqual("Invalid input, please try again!", response);

            response = processor.ProcessCommand("PLACE 1 1 SOUTH");
            Assert.AreEqual("Invalid input, please try again!", response);

            response = processor.ProcessCommand("MOVE 1 1 SOUTH");
            Assert.AreEqual("Invalid input, please try again!", response);

            response = processor.ProcessCommand("LEFT 0");
            Assert.AreEqual("Invalid input, please try again!", response);

            response = processor.ProcessCommand("REPORT NORTH");
            Assert.AreEqual("Invalid input, please try again!", response);
        }

        [TestMethod]
        public void TurnedLeftOrRight_AndReportCorrectPosition() //Initialize position and change direction to left or right
        {
            var processor = new CommandProcessor(new ToyRobots());
            processor.ProcessCommand("PLACE 1,1,NORTH");
            processor.ProcessCommand("LEFT");
            Assert.AreEqual("1,1,WEST", processor.ProcessCommand("REPORT"));

            processor.ProcessCommand("PLACE 1,1,NORTH");
            processor.ProcessCommand("RIGHT");
            Assert.AreEqual("1,1,EAST", processor.ProcessCommand("REPORT"));
        }

        [TestMethod]
        public void BoundryPositionCannotBeMoved_ReportPosition() //Robot movement must in table area of 5 x 5
        {
            var processor = new CommandProcessor(new ToyRobots());
            processor.ProcessCommand("PLACE 5,5,NORTH");
            processor.ProcessCommand("MOVE");
            Assert.AreEqual("5,5,NORTH", processor.ProcessCommand("REPORT"));
        }

        [TestMethod]
        public void BoundryPositionCanBeMoved_ReportPosition() //Robot can take action if it is inside of table area of 5 x 5
        {
            var processor = new CommandProcessor(new ToyRobots());
            processor.ProcessCommand("PLACE 5,5,NORTH");
            processor.ProcessCommand("LEFT");
            processor.ProcessCommand("MOVE");
            Assert.AreEqual("4,5,WEST", processor.ProcessCommand("REPORT"));
        }

        [TestMethod]
        public void PlacedMovedAndTurned_ReportPositiont() //Initialize validated position, move multiple times, turn direction and report
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
