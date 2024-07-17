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
                .OrderBy(e => e.IsMoving ? int.MaxValue : Math.Abs(e.CurrentFloor - floor))
                .ThenBy(e => e.IsMoving ? Math.Abs(e.CurrentFloor - floor) : 0)
                .FirstOrDefault();
        }

        private void MoveElevator(Elevator elevator, int floor)
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

                Console.WriteLine($"Elevator {elevator.Id} is at floor {elevator.CurrentFloor} moving {elevator.Direction}");
            }

            elevator.Stop();
            elevator.CurrentPassengers = 0; // Passengers exit the elevator
            Console.WriteLine($"Elevator {elevator.Id} has arrived at floor {floor} and is now {elevator.Direction}");
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
