using ElevatorSimulation_Core.Entities;
using ElevatorSimulation_Core.Entities.Abstractions;
using ElevatorSimulation_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulation_Main.Services
{
    public class ElevatorService : IElevatorService
    {
        private readonly IBuilding _building;
        private readonly IElevatorControlStrategy _elevatorControlStrategy;

        public ElevatorService(IBuilding building, IElevatorControlStrategy elevatorControlStrategy)
        {
            _building = building;
            _elevatorControlStrategy = elevatorControlStrategy;
        }

        /// <summary>
        /// Handles the request to call an elevator to a specified floor and move passengers to a destination floor.
        /// </summary>
        /// <param name="currentFloor">The floor from which the elevator is being called.</param>
        /// <param name="destinationFloor">The floor to which the passengers wish to travel.</param>
        /// <param name="passengers">The number of passengers requesting the elevator.</param>
        /// <remarks>
        /// This method locates the nearest available elevator, checks its capacity, and moves it to the requested floor 
        /// if there is enough space for the passengers. It provides feedback if no elevators are available or if the nearest 
        /// elevator lacks sufficient capacity. All exceptions are caught and logged to ensure smooth operation.
        /// </remarks>
        public void CallElevator(int currentFloor, int destinationFloor, int passengers)
        {
            try
            {
                var nearestElevator = _elevatorControlStrategy.FindNearestElevator(currentFloor);

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
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while calling the elevator: {e.Message}");
            }
        }

        public void MoveElevator(Elevator elevator, int currentFloor, int destinationFloor)
        {
            if (elevator != null && currentFloor > -1 && destinationFloor > -1)
            {
                MoveElevatorToFloor(elevator, currentFloor);

                // Passengers board the elavator
                Console.WriteLine($"Passengers boarded Elevator {elevator.Id} at floor {currentFloor}, {elevator.CurrentPassengers} passangers are onboard");

                MoveElevatorToFloor(elevator, destinationFloor);

                // Passengers exit the elevator
                elevator.CurrentPassengers = 0;
                elevator.DestinationFloor = null;
                Console.WriteLine($"Passengers exited Elevator {elevator.Id} at floor {destinationFloor}, {elevator.CurrentPassengers} people remaining");
                Console.WriteLine("-------------------------------------------------------------------");
            }
            
        }

        //Call functions of the elevator object to move it to the desired floor
        public void MoveElevatorToFloor(Elevator elevator, int floor)
        {
            if (elevator != null && floor > -1)
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
