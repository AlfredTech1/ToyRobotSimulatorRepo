using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotSimulator.BusinessLayer
{
    /// <summary>
    /// This class represents the location of the toy robot on the table surface.
    /// </summary>
    public class Location
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Direction F { get; set; }
        public enum Direction
        {
            NORTH,
            EAST,
            SOUTH,
            WEST
        }

        public Location(int x, int y, Direction f)
        {
            this.X = x;
            this.Y = y;
            this.F = f;
        }
    }
}
