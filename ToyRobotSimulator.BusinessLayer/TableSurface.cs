using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotSimulator.BusinessLayer
{
    public class TableSurface
    {
        public int NumRows { get; set; }
        public int NumColumns { get; set; }

        public TableSurface(int rows, int columns)
        {
            this.NumRows = rows;
            this.NumColumns = columns;
        }

        // Validate if location specified is within the table surface.
        public bool IsValidLocation(Location location)
        {
            bool IsValid = (location.X < NumColumns && location.X >= 0 && location.Y < NumRows && location.Y >= 0);
            return IsValid;
        }
    }
}
