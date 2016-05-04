using System;

namespace MarsRovers
{
    /// <summary>
    /// Custom Exception type used for input errors leading to rover going out-of-bounds or en
    /// </summary>
    class RoverSimulationInputException : Exception
    {
        public RoverSimulationInputException()
        {
        }

        public RoverSimulationInputException(string message)
        : base(message)
        {
        }

        public RoverSimulationInputException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
