using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoverChallenge
{
    public class Types
    {
        public enum CommandLineType
        {
            zoneDimensions, startingLocation, movementCommands
        }

        public enum RotationDirection
        {
            right, left
        }
    }
}
