using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRobotSimulator.BusinessLayer.Interfaces
{
    public interface IToyRobot
    {
        // location of toy robot in the table surface.
        Location Location { get; set; }

        // Execute toy robot action on the table surface.
        string DoCommand(string input);

    }
}
