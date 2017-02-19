using System;
using static MarsRoverChallenge.Types;

namespace MarsRoverChallenge
{
    public class Rover
    {
        public Rover(string[] commandLineItems, TerrainZone terrainZone)
        {
            this.HorizontalPosition = Convert.ToInt32(commandLineItems[0]);
            this.VerticalPosition = Convert.ToInt32(commandLineItems[1]);
            this.Facing = commandLineItems[2];
            this.TerrainZone = terrainZone;
        }

        public string Move()
        {
            bool validMovement = true;

            switch (this.Facing)
            {
                case "S":
                    if (this.VerticalPosition - 1 > -1) this.VerticalPosition--;
                    else validMovement = false;
                    break;
                case "E":
                    if (this.HorizontalPosition + 1 <= (TerrainZone.HorizontalLength - 1)) this.HorizontalPosition++;
                    else validMovement = false;
                    break;
                case "N":
                    if (this.VerticalPosition + 1 <= (TerrainZone.VerticalLength - 1)) this.VerticalPosition++;
                    else validMovement = false;
                    break;
                case "W":
                    if (this.HorizontalPosition - 1 > -1) this.HorizontalPosition--;
                    else validMovement = false;
                    break;
            }

            if (!validMovement)
            {
                throw new Exception("You have requested an invalid move which would result in the Rover moving out of the Cartesian Zone.");
            }

            return this.CurrentPosition;
        }

        public string Rotate(RotationDirection rotationDirection)
        {
            Facing = ChangeDirection(rotationDirection);
            return this.CurrentPosition;
        }

        private string ChangeDirection(RotationDirection rotationDirection)
        {
            string resultingDirection = "N";

            switch (Facing)
            {
                case "N":
                    if (rotationDirection == RotationDirection.right) resultingDirection = "E";
                    else resultingDirection = "W";
                    break;
                case "E":
                    if (rotationDirection == RotationDirection.right) resultingDirection = "S";
                    else resultingDirection = "N";
                    break;
                case "S":
                    if (rotationDirection == RotationDirection.right) resultingDirection = "W";
                    else resultingDirection = "E";
                    break;
                case "W":
                    if (rotationDirection == RotationDirection.right) resultingDirection = "N";
                    else resultingDirection = "S";
                    break;
            }
            return resultingDirection;
        }

        internal string ExecuteCommand(char command)
        {
            string response = "";

            switch (command)
            {
                case 'M':
                    response = "Rover Moved 1 " + this.Facing + " to coordinates " + this.Move();
                    break;
                case 'R':
                    this.Rotate(RotationDirection.right);
                    response = "Rover rotated 90' R. Now facing " + this.Facing + " at coordinates " + this.CurrentPosition;
                    break;
                case 'L':
                    this.Rotate(RotationDirection.left);
                    response = "Rover rotated 90' L. Now facing " + this.Facing + " at coordinates " + this.CurrentPosition;
                    break;
            }
            return response;
        }

        public string ExecuteAllCommands()
        {
            foreach (var movementCommand in this.MovementCommands)
            {
                ExecuteCommand(movementCommand);
            }

            return this.CurrentPosition;
        }

        public string CurrentPosition
        {
            get
            {
                return this.HorizontalPosition + " " + this.VerticalPosition + " " + this.Facing;
            }
        }
        public string Facing { get; private set; }
        public int HorizontalPosition { get; private set; }
        public int VerticalPosition { get; private set; }
        public TerrainZone TerrainZone { get; private set; }
        public char[] MovementCommands { get; set; }
    }
}