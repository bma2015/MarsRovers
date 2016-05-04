using System;

namespace MarsRovers
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo keyInfo;
            RoverSimulation rs = promptForRoverSimulation();

            do
            {
                addRoverToSimulation(rs);
                readRoverCommands(rs);
                Console.Write("Press the space bar to exit and any other key to add another rover: ");
                keyInfo = Console.ReadKey();
                Console.WriteLine();
            } while (keyInfo.Key != ConsoleKey.Spacebar);
            
            rs.PrintRoverData();
        }

        /// <summary>
        /// Prompts for plateau dimensions.  Invokes a function to read and create a rover simulation.
        /// </summary>
        /// <returns>the generated RoverSimulation</returns>
        static RoverSimulation promptForRoverSimulation()
        {
            string line;
            RoverSimulation rs = null;

            do
            {
                Console.Write("Enter plateau dimensions: ");
                line = Console.ReadLine();
                string[] dims = splitInputStr(line);

                if (dims.Length == 2)
                {
                    rs = readPlateauDimsAndCreateRoverSimulation(dims);
                }

                if (rs == null)
                {
                    Console.WriteLine("Input Format Error: Expected input format \"x y\" where " +
                        "x and y are both integers.");
                }
            } while (rs == null);

            return rs;
        }

        /// <summary>
        /// Attempts to read plateau dimensions from given array.  Once successfully read, this function
        /// creates a RoverSimulation.
        /// </summary>
        /// <param name="dims"></param>
        /// <returns>a generated RoverSimulation if read is successful, and null otherwise</returns>
        private static RoverSimulation readPlateauDimsAndCreateRoverSimulation(string[] dims)
        {
            int readIndex = 0;
            int xDist, yDist;
            bool parseSuccess;
            RoverSimulation rs = null;

            parseSuccess = int.TryParse(dims[readIndex++], out xDist);

            if (parseSuccess)
            {
                parseSuccess = int.TryParse(dims[readIndex++], out yDist);

                if (parseSuccess)
                {
                    rs = new RoverSimulation(xDist, yDist);
                }
            }

            return rs;
        }

        /// <summary>
        /// Prompts for initial rover position and orientation.  Invokes a function to read and parse this data to
        /// create a Rover for the RoverSimulation.
        /// </summary>
        /// <param name="rs"></param>
        private static void addRoverToSimulation(RoverSimulation rs)
        {
            string line;
            int readIndex = 0;
            bool parseSuccess;

            do
            {
                parseSuccess = true;

                Console.Write("Enter initial rover position and orientation: ");
                line = Console.ReadLine();
                string[] roverData = splitInputStr(line);

                if (roverData.Length != 3)
                {
                    parseSuccess = false;
                }

                if (parseSuccess)
                {
                    parseSuccess = readRoverDataAndCreateRover(roverData, readIndex, rs);
                }

                if (!parseSuccess)
                {
                    Console.WriteLine("Input Format Error: Expected input format \"x y D\" where " +
                        "x and y are both integers and D is \"E\", \"N\", \"W\", or \"S\".");
                    readIndex = 0;
                }

            } while (!parseSuccess);
        }

        /// <summary>
        /// Reads the initial x- and y-coordinates, and orientation, of the rover.  If read is successful,
        /// this function creates a new Rover and adds it to the RoverSimulation.
        /// </summary>
        /// <param name="roverData"></param>
        /// <param name="readIndex"></param>
        /// <param name="rs"></param>
        /// <returns>true if Rover data is read successfully and false otherwise</returns>
        private static bool readRoverDataAndCreateRover(string[] roverData, int readIndex, RoverSimulation rs)
        {
            int xInit, yInit;
            bool parseSuccess = int.TryParse(roverData[readIndex++], out xInit);

            if (parseSuccess)
            {
                parseSuccess = int.TryParse(roverData[readIndex++], out yInit);

                if (parseSuccess)
                {
                    try
                    {
                        Rover rover = new Rover(xInit, yInit, roverData[readIndex++]);
                        rs.Enqueue(rover);
                    }
                    catch (RoverSimulationInputException e)
                    {
                        Console.WriteLine(e.Message);
                        parseSuccess = false;
                    }
                }
            }

            return parseSuccess;
        }

        /// <summary>
        /// Prompts for a string of rover commands and uses the most recently added rover to execute them.
        /// </summary>
        /// <param name="rs"></param>
        private static void readRoverCommands(RoverSimulation rs)
        {
            string commands;

            Console.Write("Enter commands for this rover: ");
            commands = Console.ReadLine();
            rs.ExecuteCommandsOnCurrentRover(commands);
        }

        /// <summary>
        /// Returns an array of strings split from a string by whitespace, with no empty entries.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] splitInputStr(string str)
        {
            return str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
