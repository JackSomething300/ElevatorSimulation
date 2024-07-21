using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulation_Core.Exceptions
{
    public class ElevatorCapacityExceededException : Exception
    {
        public ElevatorCapacityExceededException(string message) : base(message) { }
    }

    public class NoElevatorAvailableException : Exception
    {
        public NoElevatorAvailableException(string message) : base(message) { }

    }
}
