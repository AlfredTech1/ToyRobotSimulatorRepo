using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ToyRobotSimulator.BusinessLayer;
using ToyRobotSimulator.BusinessLayer.Interfaces;

namespace ToyRobotSimulator.BusinessLayer.Tests
{
    [TestFixture]
    public class ToyRobotTest
    {
        /// <summary>
        /// Test valid movement in the table surface and output report
        /// This one from sample input and output in assessment.
        /// </summary>
        [Test]
        public void DoCommand_ValidReportSampleFromAssessment_Equal_1()
        {
            // arrange
            IToyRobot robot = new ToyRobot(new TableSurface(5, 5));

            // act
            robot.DoCommand("PLACE 0,0,NORTH");
            robot.DoCommand("MOVE");
            var output = robot.DoCommand("REPORT");

            // assert
            Assert.AreEqual("Report: 0,1,NORTH", output);
        }


        /// <summary>
        /// Test valid movement in the table surface and output report
        /// This one from sample input and output in assessment.
        /// </summary>
        [Test]
        public void DoCommand_ValidReportSampleFromAssessment_Equal_2()
        {
            // arrange
            IToyRobot robot = new ToyRobot(new TableSurface(5, 5));

            // act
            robot.DoCommand("PLACE 1,2,EAST");
            robot.DoCommand("MOVE");
            robot.DoCommand("MOVE");
            robot.DoCommand("LEFT");
            robot.DoCommand("MOVE");
            var output = robot.DoCommand("REPORT");

            // assert
            Assert.AreEqual("Report: 3,3,NORTH", output);
        }



        /// <summary>
        /// Test valid movement in the table surface and output report
        /// </summary>
        [Test]
        public void DoCommand_ValidReport_Equal()
        {
            // arrange
            IToyRobot robot = new ToyRobot(new TableSurface(5, 5));

            // act
            robot.DoCommand("PLACE 4,3,WEST");
            robot.DoCommand("MOVE");
            robot.DoCommand("MOVE");
            robot.DoCommand("LEFT");
            robot.DoCommand("MOVE");
            var output = robot.DoCommand("REPORT");

            // assert
            Assert.AreEqual("Report: 2,2,SOUTH", output);
        }


        /// <summary>
        /// Test toy turn left
        /// </summary>
        [Test]
        public void ShiftLeft_ValidToyRobotTurnLeft_Equal()
        {
            // arrange
            var robot = new ToyRobot(new TableSurface(5, 5));
            Location oldLocation = new Location(2, 2, Location.Direction.WEST);
            robot.Location = oldLocation;

            // act
            Location newLocation = robot.ShiftLeft();

            // assert
            Assert.AreEqual(Location.Direction.SOUTH, newLocation.F);
        }

        /// <summary>
        /// Test toy turn right
        /// </summary>
        [Test]
        public void ShiftRight_ValidToyRobotTurnRight_Equal()
        {
            // arrange
            var robot = new ToyRobot(new TableSurface(5, 5));
            Location oldLocation = new Location(2, 2, Location.Direction.WEST);
            robot.Location = oldLocation;

            // act
            Location newLocation = robot.ShiftRight();

            // assert
            Assert.AreEqual(Location.Direction.NORTH, newLocation.F);
        }


        /// <summary>
        /// Move forward toy robot one unit
        /// </summary>
        [Test]
        public void GetNextLocation_ValidToyRobotMove_Equal()
        {
            // arrange
            var robot = new ToyRobot(new TableSurface(5, 5));
            Location oldLocation = new Location(2, 2, Location.Direction.EAST);
            robot.Location = oldLocation;

            // act
            Location nextLocation = robot.GetNextLocation();

            // assert
            Assert.AreEqual(3, nextLocation.X);
            Assert.AreEqual(2, nextLocation.Y);
        }

        /// <summary>
        /// Set toy location test
        /// </summary>
        [Test]
        public void Place_ValidToyRobotLocation_Equal()
        {
            // arrange
            var robot = new ToyRobot(new TableSurface(5, 5));
            Location oldLocation = new Location(3, 3, Location.Direction.NORTH);

            // act
            robot.Place(oldLocation);

            // assert
            Assert.AreEqual(3, robot.Location.X);
            Assert.AreEqual(3, robot.Location.Y);
            Assert.AreEqual(Location.Direction.NORTH, robot.Location.F);
        }


        /// <summary>
        /// Test place command with valid parameters
        /// </summary>
        [Test]
        public void DoCommand_ValidPlace_LocationValid()
        {
            // arrange
            IToyRobot robot = new ToyRobot(new TableSurface(5,5));

            // act
            robot.DoCommand("PLACE 2,2,SOUTH");

            // assert
            Assert.AreEqual(2, robot.Location.X);
            Assert.AreEqual(2, robot.Location.Y);
            Assert.AreEqual(Location.Direction.SOUTH, robot.Location.F);
        }

        /// <summary>
        /// Test place command with an invalid parameter format
        /// </summary>
        [Test]
        public void PopulatePlaceParameters_TestInvalidPlaceParamsFormat_IsNull()
        {
            // arrange
            string[] input = "PLACE 1,1,WEST,5".Split(' ');
            ToyRobot robot = new ToyRobot(new TableSurface(5, 5));

            //act
            Location tmpLocation = robot.PopulatePlaceParameters(input);

            //assert
            Assert.IsNull(tmpLocation);
        }



        /// <summary>
        /// Test valid place command
        /// </summary>
        [Test]
        public void GetCommand_ValidPlaceCommand_Equal()
        {
            // arrange
            string[] Input = "PLACE".Split(' ');
            ToyRobot robot = new ToyRobot(new TableSurface(5, 5));

            // act
            var command = robot.GetCommand(Input);

            // assert
            Assert.AreEqual(ToyRobot.Command.PLACE, command);
        }


        /// <summary>
        /// Test invalid place command
        /// </summary>
        [Test]
        public void GetCommand_InvalidPlaceCommand_Equal()
        {
            // arrange
            string[] Input = "PLACEMOVE".Split(' ');
            ToyRobot robot = new ToyRobot(new TableSurface(5, 5));

            // act and assert
            var ex = Assert.Throws<ApplicationException>(delegate { robot.GetCommand(Input); });
            Assert.That(ex.Message, Is.EqualTo("Command Unknown.  Please try again."));
        }
    }
}
