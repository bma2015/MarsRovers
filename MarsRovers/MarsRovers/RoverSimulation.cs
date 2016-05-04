using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRovers
{
    /// <summary>
    /// This class represents a rover simulation on a plateau on Mars.  
    /// </summary>
    class RoverSimulation
    {
        // The dimenstions of the rectangular plateau
        private int _xDist, _yDist;

        // The rovers to be deployed on the plateau
        private Queue<Rover> rovers = new Queue<Rover>();

        public RoverSimulation(int _xDist, int _yDist)
        {
            this._xDist = _xDist;
            this._yDist = _yDist;
        }

        public int XDist
        {
            get { return _xDist; }
        }

        public int YDist
        {
            get { return _yDist; }
        }

        public void Enqueue(Rover rover)
        {
            rovers.Enqueue(rover);
        }

        public Rover Dequeue()
        {
            return rovers.Dequeue();
        }

        /// <summary>
        /// Executes a series of spin and move commands on the most recent rover added to the simulation.
        /// </summary>
        /// <param name="commands"></param>
        public void ExecuteCommandsOnCurrentRover(string commands)
        {
            Rover currentRover = rovers.Last();
            currentRover.ExecuteCommands(commands, _xDist, _yDist);
        }

        /// <summary>
        /// Outputs the final data for each rover using the order in which they were processed
        /// </summary>
        public void PrintRoverData()
        {
            foreach (Rover r in rovers)
            {
                Console.WriteLine(r);
            } 
        }

    }
}
