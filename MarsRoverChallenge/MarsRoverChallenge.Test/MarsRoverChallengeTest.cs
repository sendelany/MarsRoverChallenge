using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Configuration;

namespace MarsRoverChallenge.Test
{
    [TestClass]
    public class MarsRoverChallengeTest
    {
       
        [TestMethod]
        public void TestCommandFileRead()
        {
            Assert.IsTrue(File.Exists(@"C:\rover\Commands.txt"));
        }

        [TestMethod]
        public void TestCommandFileValid()
        {
            string[] RawCommandLines = File.ReadAllLines(@"C:\rover\Commands.txt");

            string[] gridSize = RawCommandLines[0].Split(' ');
            string[] roverInitialPosition = RawCommandLines[1].Split(' ');
            char[] roverMovementCommands = RawCommandLines[2].ToCharArray();

            CommandFileLoader ldr = new CommandFileLoader(@"C:\rover\Commands.txt");

            Assert.AreEqual(ldr.TerrainZone.HorizontalLength.ToString(), gridSize[0]);
            Assert.AreEqual(ldr.TerrainZone.VerticalLength.ToString(), gridSize[1]);

            Assert.AreEqual(ldr.Rover.HorizontalPosition.ToString(), roverInitialPosition[0]);
            Assert.AreEqual(ldr.Rover.VerticalPosition.ToString(), roverInitialPosition[1]);
            Assert.AreEqual(ldr.Rover.Facing.ToString(), roverInitialPosition[2]);

            Assert.AreEqual(new string(ldr.Rover.MovementCommands), new string(roverMovementCommands));
        }

        [TestMethod]
        public void TestRoverManeuvers()
        {
            string[] RawCommandLines = File.ReadAllLines(@"C:\rover\Commands.txt");

            string[] roverInitialPosition = new string[] { "1", "2", "E" };
            char[] roverMovementCommands = new char[] { 'M', 'M', 'L', 'M', 'R', 'M', 'M', 'R', 'R', 'M', 'M', 'L' };

            CommandFileLoader ldr = new CommandFileLoader(@"C:\rover\Commands.txt");


            //The file could have changed. We are going to load known parameters and see if the rover lands in 
            //a spot we expect.
            ldr.TerrainZone.HorizontalLength = 8;
            ldr.TerrainZone.VerticalLength = 8;
            ldr.TerrainZone.CartesianZoneGrid = new string[8, 8];

            ldr.Rover = new Rover(roverInitialPosition, ldr.TerrainZone);
            ldr.Rover.MovementCommands = roverMovementCommands;

            string resultingLocation = ldr.Rover.ExecuteAllCommands();

            Assert.AreEqual(resultingLocation, "3 3 S");

        }

    }
}
