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

        public void CallElevator(int currentFloor, int destinationFloor, int passengers)
        {
            var nearestElevator = FindNearestElevator(currentFloor);

            if (nearestElevator != null)
            {
                if (passengers <= nearestElevator.MaxPassengers - nearestElevator.CurrentPassengers)
                {
                    nearestElevator.CurrentPassengers += passengers;
                    nearestElevator.DestinationFloor = destinationFloor;
                    MoveElevator(nearestElevator, currentFloor, destinationFloor);
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
                .ThenBy(elevator => elevator.LastDirection == "Stationary" ? 0 : 1) // Prefer stationary elevators
                .FirstOrDefault()!;
        }

        private bool IsElevatorMovingTowardsOrStationary(Elevator elevator, int floor)
        {
            return elevator.DestinationFloor == null || // Exclude elevators already assigned a destination
                   elevator.LastDirection == "Stationary" ||
                   (elevator.LastDirection == "Up" && elevator.CurrentFloor < floor) ||
                   (elevator.LastDirection == "Down" && elevator.CurrentFloor > floor);
        }

        public void MoveElevator(Elevator elevator, int currentFloor, int destinationFloor)
        {
            MoveElevatorToFloor(elevator, currentFloor);

            // Passengers board the elevator
            Console.WriteLine($"Passengers boarded Elevator {elevator.Id} at floor {currentFloor}, {elevator.CurrentPassengers} passangers are onboard");

            MoveElevatorToFloor(elevator, destinationFloor);

            // Passengers exit the elevator
            elevator.CurrentPassengers = 0;
            elevator.DestinationFloor = null;
            Console.WriteLine($"Passengers exited Elevator {elevator.Id} at floor {destinationFloor}, {elevator.CurrentPassengers} people remaining");
            Console.WriteLine("-------------------------------------------------------------------");
        }

        private void MoveElevatorToFloor(Elevator elevator, int floor)
        {
            while (elevator.CurrentFloor != floor)
            {
                if (elevator.CurrentFloor < floor)
                {
                    elevator.MoveUp();
                }
                else if (elevator.CurrentFloor > floor)
                {
                    elevator.MoveDown();
                }

                Console.WriteLine($"Elevator {elevator.Id} is at floor {elevator.CurrentFloor} moving to {elevator.Direction}");
            }

            elevator.Stop();
            Console.WriteLine($"Elevator {elevator.Id} has arrived at floor {floor} and is now {elevator.Direction}");
        }

        public void UpdateElevatorStatus()
        {
            foreach (var elevator in _building.Elevators)
            {
                Console.WriteLine($"Elevator {elevator.Id}: Floor {elevator.CurrentFloor}, Direction: {elevator.Direction}, Passengers: {elevator.CurrentPassengers}");
            }
            Console.WriteLine($"This building has {_building.NumberOfFloors} floors and {_building.Elevators.Count()} elevator and {_building.MaxPassengersPerElevator} max passangers per elevator");
        }
    }
}
