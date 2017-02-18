using NLog;
using System.IO;
using static MarsRoverChallenge.Types;
using System;
using System.Collections.Generic;

namespace MarsRoverChallenge
{
    public class CommandFileLoader
    {
        private static Logger logger = LogManager.GetLogger("CommandFileLoader");
        public CommandFileLoader(string fullPath)
        {
            this.FullPath = fullPath;

            if (ValidCommandFile())
            {
                this.Load();
            }
            else throw new Exception("Invalid Command File for deployment to Mars. A valid file must have a minimum of 3 rows with row1 = Grid Bounds, row2 = Rover Initial Position and row3 = Movement Commands");
        }

        private bool ValidCommandFile()
        {
            this.RawCommandLines = File.ReadAllLines(this.FullPath);

            if (!(RawCommandLines.Length > 2)) return false;
            if (RawCommandLines[0].Split(' ').Length != 2) return false;
            if (RawCommandLines[1].Split(' ').Length != 3) return false;
            if (RawCommandLines[2].Split(' ').Length != 1) return false;

            return true;
        }

        public void Load()
        {
            this.RawCommandLines = File.ReadAllLines(this.FullPath);
            logger.Log(LogLevel.Info, "Loading Command File");

            foreach (string commandLine in this.RawCommandLines)
            {
                string[] commandLineItems = commandLine.Split(' ');
                if (commandLineItems.Length == 1) StoreCommand(CommandLineType.movementCommands, commandLineItems, commandLine);
                else if (commandLineItems.Length == 2) StoreCommand(CommandLineType.zoneDimensions, commandLineItems, commandLine);
                else if (commandLineItems.Length == 3) StoreCommand(CommandLineType.startingLocation, commandLineItems, commandLine);
            }
        }

        private void StoreCommand(CommandLineType commandType, string[] commandLineItems, string commandLine)
        {
            switch (commandType)
            {
                case CommandLineType.zoneDimensions:
                    this.TerrainZone = new TerrainZone(commandLineItems, commandLine);
                    break;
                case CommandLineType.startingLocation:
                    this.Rover = new Rover(commandLineItems, TerrainZone);
                    break;
                case CommandLineType.movementCommands:
                    this.Rover.MovementCommands = commandLine.ToCharArray();
                    break;
                default:
                    break;
            }
        }

        public string FullPath { get; private set; }
        public string[] RawCommandLines { get; private set; }
        public TerrainZone TerrainZone { get; private set; }
        public Rover Rover { get; private set; }
    }
}