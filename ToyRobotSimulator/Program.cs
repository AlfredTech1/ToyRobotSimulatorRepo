using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyRobotSimulator.BusinessLayer;
using ToyRobotSimulator.BusinessLayer.Interfaces;
using System.IO;

namespace ToyRobotSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Welcome to TOY ROBOT SIMULATOR");
            sb.AppendLine("");
            sb.AppendLine("Instructions for standard input: ");
            sb.AppendLine("");
            sb.AppendLine("1. Place the toy robot within a 5 x 5 table ");
            sb.AppendLine("   surface using the following command (case-sensitive):");
            sb.AppendLine("");
            sb.AppendLine("   PLACE X,Y,F (where X and Y are the coordinates "); 
            sb.AppendLine("   and F (Facing) must be either NORTH, SOUTH,");
            sb.AppendLine("   EAST OR WEST. Example: PLACE 2,3,NORTH");
            sb.AppendLine("");
            sb.AppendLine("2. After placing toy robot, you can do the following");
            sb.AppendLine("   actions or commands (case-sensitive):");
            sb.AppendLine("");
            sb.AppendLine("   MOVE   - Moves the toy robot one unit forward in the");
            sb.AppendLine("            direction it is currently facing.");
            sb.AppendLine("   LEFT   - Shift the toy robot 90 degrees counter-clockwise");
            sb.AppendLine("            without changing the location.");
            sb.AppendLine("   RIGHT  - Shift the toy robot 90 degrees clockwise");
            sb.AppendLine("            without changing the location.");
            sb.AppendLine("   REPORT - displays the location of the toy robot.");
            sb.AppendLine("");
            sb.AppendLine("Instructions for command text file processing: ");
            sb.AppendLine("");
            sb.AppendLine("1. FILE  - Opens up location to enter the full path and filename");
            sb.AppendLine("");
            sb.AppendLine("2. Provide the location path and name of the command file");
            sb.AppendLine(@"  (e.g. Enter Full Path and Filename:  C:\CommandFiles\CmdBatchFile1.txt");         
            sb.AppendLine();sb.AppendLine();
            
            Console.WriteLine(sb.ToString());

            IToyRobot robot = new ToyRobot(new TableSurface(5,5));

            while(true)
            {
                Console.Write("Enter Command: ");
                string command = Console.ReadLine();

                if (!command.Contains("FILE"))
                {

                    if (string.IsNullOrEmpty(command))
                        continue;

                    try
                    {
                        string output = robot.DoCommand(command);
                        if (!string.IsNullOrEmpty(output))
                            Console.WriteLine(output);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    //Process File
                    Console.Write("Enter Full Path and Filename: ");
                    string fileName = Console.ReadLine();
                    string line;
                    if(File.Exists(fileName))
                    {
                        StreamReader file = new StreamReader(fileName);
                        while((line = file.ReadLine()) != null)
                        {
                            if (string.IsNullOrEmpty(line.Trim()))
                                continue;
                            try
                            {
                                Console.WriteLine(line.Trim());
                                string output = robot.DoCommand(line.Trim());
                                if (!string.IsNullOrEmpty(output))
                                    Console.WriteLine(output);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Command file does not exists. Try again.");
                    }
                }
            }

        }
    }
}
