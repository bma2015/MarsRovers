using System;

namespace MarsRovers
{
    enum Direction { East = 0, North, West, South };

    /// <summary>
    /// This class represents a robotic rover that is to be used in a RoverSimulation.
    /// </summary>
    class Rover
    {
        private double _xCoord, _yCoord;
        private Direction direction;

        public Rover(int _xCoord, int _yCoord, string directionChar)
        {
            this._xCoord = _xCoord;
            this._yCoord = _yCoord;

            switch (directionChar)
            {
                case "E":
                    direction = Direction.East;
                    break;
                case "N":
                    direction = Direction.North;
                    break;
                case "W":
                    direction = Direction.West;
                    break;
                case "S":
                    direction = Direction.South;
                    break;
                default:
                    throw new RoverSimulationInputException("Error: Orientation must be \"E\", \"N\", \"W\", or \"S\".");
            }
        }

        public double XCoord
        {
            get { return _xCoord; }
        }

        public double YCoord
        {
            get { return _yCoord; }
        }


        /// <summary>
        /// Executes a series of L, R, and M commands given in a string.
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="xDist"></param>
        /// <param name="yDist"></param>
        public void ExecuteCommands(string commands, int xDist, int yDist)
        {
            for (int i = 0; i < commands.Length; i++)
            {
                try
                {
                    Update(commands[i], xDist, yDist);
                }
                catch (RoverSimulationInputException e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        /// <summary>
        /// Updates the rover by either spinning to the right or left or moving forward one unit.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="xDist"></param>
        /// <param name="yDist"></param>
        public void Update(char command, int xDist, int yDist)
        {
            switch(command)
            {
                case 'L':
                    direction = (Direction)(((int)direction + 1) % 4);
                    break;
                case 'R':
                    int directionInt = (((int)direction - 1) % 4);

                    // C# modulus operator gives negative result for negative dividends
                    if (directionInt < 0)
                    {
                        direction = (Direction)(directionInt + 4);
                    }
                    else
                    {
                        direction = (Direction)directionInt;
                    }

                    break;
                case 'M':
                    moveRover(xDist, yDist); 
                    break;
                default:
                    throw new RoverSimulationInputException("Error: Valid commands are \"L\" (spin left), \"R\" (spin right), and \"M\" (move forward).");
            }
        }

        /// <summary>
        /// Moves the rover forward, provided that the rover will remain on the plateau.
        /// </summary>
        /// <param name="xDist"></param>
        /// <param name="yDist"></param>
        private void moveRover(int xDist, int yDist)
        {
            switch (direction)
            {
                case Direction.East:
                    if (_xCoord < xDist)
                    {
                        _xCoord++;
                    }
                    break;
                case Direction.North:
                    if (_yCoord < yDist)
                    {
                        _yCoord++;
                    }
                    break;
                case Direction.West:
                    if (_xCoord > 0)
                    {
                        _xCoord--;
                    }
                    break;
                case Direction.South:
                    if (_yCoord > 0)
                    {
                        _yCoord--;
                    }
                    break;
            }
        }

        public override string ToString() {
            string output = _xCoord + " " + _yCoord + " ";

            switch (direction)
            {
                case Direction.East:
                    output += "E";
                    break;
                case Direction.North:
                    output += "N";
                    break;
                case Direction.West:
                    output += "W";
                    break;
                case Direction.South:
                    output += "S";
                    break;
            }

            return output;
        }

    }
}
