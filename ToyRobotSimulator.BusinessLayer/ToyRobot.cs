using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyRobotSimulator.BusinessLayer.Interfaces;

namespace ToyRobotSimulator.BusinessLayer
{
    public class ToyRobot : IToyRobot
    {
        private TableSurface _tableSurface;
        public ToyRobot(TableSurface tableSurface)
        {
            _tableSurface = tableSurface;
        }

        public Location Location { get; set; }

        public string DoCommand(string input)
        {
            string[] parseInput = input.Split(' ');
            Command command = GetCommand(parseInput);
            if (command != Command.PLACE && Location == null)
                return string.Empty;


            switch (command)
            {
                case Command.PLACE:
                    Location tmpLocation = PopulatePlaceParameters(parseInput);
                    if (_tableSurface.IsValidLocation(tmpLocation))
                    {
                        Place(tmpLocation);
                    }
                    break;
                case Command.MOVE:
                    Location newLocation = GetNextLocation();
                    if (_tableSurface.IsValidLocation(newLocation))
                    {
                        Location = newLocation;
                    }
                    break;
                case Command.LEFT:
                    newLocation = ShiftLeft();
                    if (_tableSurface.IsValidLocation(newLocation))
                    {
                        Location = newLocation;
                    }
                    break;
                case Command.RIGHT:
                    newLocation = ShiftRight();
                    if (_tableSurface.IsValidLocation(newLocation))
                    {
                        Location = newLocation;
                    }
                    break;
                case Command.REPORT:
                    return GetReport();
            }

            return string.Empty;
        }


        public Command GetCommand(string[] input)
        {
            if (!Enum.TryParse(input[0], true, out Command command))
                throw new ApplicationException("Command Unknown.  Please try again.");
            return command;
        }

        public Location PopulatePlaceParameters(string[] input)
        {
            //There should be 2 elements, Place command and comma-delimited parameters.
            if (input.Length != 2)
                return null;

            string[] cmdParams = input[1].Split(',');
            //There should be 3 comma-delimited parameters
            if (cmdParams.Length != 3)
                return null;

            //Check if direction is valid.
            if (!Enum.TryParse(cmdParams[cmdParams.Length - 1], true, out Location.Direction direction))
                return null;

            int x = Convert.ToInt32(cmdParams[0]);
            int y = Convert.ToInt32(cmdParams[1]);
            return new Location(x, y, direction);
        }

        public string GetReport()
        {
            return string.Format("Report: {0},{1},{2}", Location.X,
                Location.Y, Location.F.ToString().ToUpper());
        }

        public void Place(Location tmpLocation)
        {
            this.Location = tmpLocation;

        }


        // Gets the next location of the toy robot based on the direction it's currently facing.
        public Location GetNextLocation()
        {
            var newLocation = new Location(Location.X, Location.Y, Location.F);
            switch (Location.F)
            {
                case Location.Direction.NORTH:
                    newLocation.Y = newLocation.Y + 1;
                    break;
                case Location.Direction.SOUTH:
                    newLocation.Y = newLocation.Y - 1;
                    break;
                case Location.Direction.EAST:
                    newLocation.X = newLocation.X + 1;
                    break;
                case Location.Direction.WEST:
                    newLocation.X = newLocation.X - 1;
                    break;
            }
            return newLocation;
        }


        // Shift Toy Robot 90 degrees to the left.
        public Location ShiftLeft()
        {
            Location newLocation = new Location(Location.X, Location.Y, Location.F);
            switch (Location.F)
            {
                case Location.Direction.NORTH:
                    newLocation.F = Location.Direction.WEST;
                    break;
                case Location.Direction.SOUTH:
                    newLocation.F = Location.Direction.EAST;
                    break;
                case Location.Direction.WEST:
                    newLocation.F = Location.Direction.SOUTH;
                    break;
                case Location.Direction.EAST:
                    newLocation.F = Location.Direction.NORTH;
                    break;
            }

            return newLocation;
        }

        // Shift Toy Robot 90 degrees to the right.
        public Location ShiftRight()
        {
            Location newLocation = new Location(Location.X, Location.Y, Location.F);
            switch (Location.F)
            {
                case Location.Direction.NORTH:
                    newLocation.F = Location.Direction.EAST;
                    break;
                case Location.Direction.SOUTH:
                    newLocation.F = Location.Direction.WEST;
                    break;
                case Location.Direction.WEST:
                    newLocation.F = Location.Direction.NORTH;
                    break;
                case Location.Direction.EAST:
                    newLocation.F = Location.Direction.SOUTH;
                    break;
            }

            return newLocation;
        }

        public enum Command
        {
            PLACE,
            MOVE,
            LEFT,
            RIGHT,
            REPORT
        }
    }
}
