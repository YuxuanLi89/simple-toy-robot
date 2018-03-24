using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Simple.Toy.Robot.Apps;

namespace Toy.Robot.Apps.Tests
{
    [TestClass]
    public class ToyRobotTests
    {
        [TestMethod]
        public void NotPlaced_CannotBeMoved()
        {
            var robot = new ToyRobots();
            var result = robot.Move();
            Assert.IsFalse(result);
            Assert.AreEqual("Please 'PLACE' a postion before you 'MOVE'.", robot.Hints);
        }
    }
}
