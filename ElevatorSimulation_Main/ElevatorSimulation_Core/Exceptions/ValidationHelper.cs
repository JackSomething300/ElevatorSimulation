using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulation_Core.Exceptions
{
    public static class ValidationHelper
    {
        public static void ValidateFloor(int floor, int numberOfFloors)
        {
            if (floor < 0 || floor > numberOfFloors)
            {
                throw new ArgumentOutOfRangeException(nameof(floor), "Invalid floor number.");
            }
        }

        public static void ValidatePassengers(int passengers, int maxPassengersPerElevator)
        {
            if (passengers <= 0 || passengers > maxPassengersPerElevator)
            {
                throw new ArgumentOutOfRangeException(nameof(passengers), "Number of passengers exceeds elevator capacity. Invalid number of passengers");
            }
        }
    }
}
