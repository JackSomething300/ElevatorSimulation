using ElevatorSimulation_Core.Entities;
using ElevatorSimulation_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulation_Application.Services
{
    public class ElevatorControlStrategy : IElevatorControlStrategy
    {
        private readonly IBuilding _building;

        public ElevatorControlStrategy(IBuilding building)
        {
            _building = building;
        }

        public Elevator FindNearestElevator(int floor)
        {
            try
            {
                return _building.Elevators
                    .Where(elevator => IsElevatorMovingTowardsOrStationary(elevator, floor))
                    .OrderBy(elevator => Math.Abs(elevator.CurrentFloor - floor))
                    .ThenBy(elevator => elevator.LastDirection == "Stationary" ? 0 : 1) // Prefer stationary elevators
                    .FirstOrDefault()!;
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while finding the nearest elevator: {e.Message}");
                return null;
            }
        }

        public bool IsElevatorMovingTowardsOrStationary(Elevator elevator, int floor)
        {
            try
            {
                return elevator.DestinationFloor == null || // Exclude elevators already assigned a destination
                       elevator.LastDirection == "Stationary" ||
                       (elevator.LastDirection == "Up" && elevator.CurrentFloor < floor) ||
                       (elevator.LastDirection == "Down" && elevator.CurrentFloor > floor);
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while checking elevator direction: {e.Message}");
                return false;
            }
        }
    }
}
