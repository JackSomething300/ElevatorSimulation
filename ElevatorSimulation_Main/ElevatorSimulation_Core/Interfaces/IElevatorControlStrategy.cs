using ElevatorSimulation_Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulation_Core.Interfaces
{
    public interface IElevatorControlStrategy
    {
        Elevator FindNearestElevator(int floor);
        bool IsElevatorMovingTowardsOrStationary(Elevator elevator, int floor);
    }
}
