using ElevatorSimulation_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulation_Core.Interfaces
{
    public interface IBuilding
    {
        int NumberOfFloors { get; }
        List<Elevator> Elevators { get; }
        int MaxPassengersPerElevator { get; }
    }
}
