using ElevatorSimulation_Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulation_Main.Services
{
    public class ElevatorService : IElevatorService
    {
        private readonly Building _building;

        public ElevatorService(Building building)
        {
            _building = building;
        }

        public void CallElevator(int floor, int passengers)
        {
            var nearestElevator = FindNearestElevator(floor);

            if (nearestElevator != null)
            {
                if (passengers <= nearestElevator.MaxPassengers - nearestElevator.CurrentPassengers)
                {
                    nearestElevator.CurrentPassengers += passengers;
                    MoveElevator(nearestElevator, floor);
                }
                else
                {
                    Console.WriteLine("Not enough capacity in the nearest elevator. Please wait for the next one.");
                }
            }
            else
            {
                Console.WriteLine("No elevators available at the moment. Please wait.");
            }
        }
        private Elevator FindNearestElevator(int floor)
        {
            return _building.Elevators
                .Where(elevator => IsElevatorMovingTowardsOrStationary(elevator, floor))
                .OrderBy(elevator => Math.Abs(elevator.CurrentFloor - floor))
                .FirstOrDefault()!;
        }

        private bool IsElevatorMovingTowardsOrStationary(Elevator elevator, int floor)
        {
            return (elevator.LastDirection == "Stationary") ||
                   (elevator.LastDirection == "Up" && elevator.CurrentFloor < floor) ||
                   (elevator.LastDirection == "Down" && elevator.CurrentFloor > floor);
        }

        public void MoveElevator(Elevator elevator, int floor)
        {
            while (elevator.CurrentFloor != floor)
            {
                if (elevator.CurrentFloor < floor)
                {
                    elevator.MoveUp();
                    elevator.LastDirection = "Up";
                }
                else if (elevator.CurrentFloor > floor)
                {
                    elevator.MoveDown();
                    elevator.LastDirection = "Down";
                }

                Console.WriteLine($"Elevator {elevator.Id} is at floor {elevator.CurrentFloor} moving {elevator.Direction} carrying {elevator.CurrentPassengers} people");
            }

            elevator.Stop();
            elevator.CurrentPassengers = 0; // Passengers leave
            Console.WriteLine($"Elevator {elevator.Id} has arrived at floor {floor} and is now {elevator.Direction}, {elevator.CurrentPassengers} passengers remaining");
        }

        public void UpdateElevatorStatus()
        {
            foreach (var elevator in _building.Elevators)
            {
                Console.WriteLine($"Elevator {elevator.Id}: Floor {elevator.CurrentFloor}, Direction: {elevator.Direction}, Passengers: {elevator.CurrentPassengers}");
            }
        }
    }
}
