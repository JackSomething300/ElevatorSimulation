using ElevatorSimulation_Main.Models;
using ElevatorSimulation_Main.Services;

namespace ElevatorSimulation
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Initialize building with 10 floors, 3 elevators, and each elevator has a max capacity of 5 passengers
            var building = new Building(10, 3, 5);
            var elevatorService = new ElevatorService(building);

            while (true)
            {
                Console.Clear();
                elevatorService.UpdateElevatorStatus();

                Console.WriteLine("\nCommands:");
                Console.WriteLine("1. Call Elevator");
                Console.WriteLine("2. Exit");

                Console.Write("Please enter your command: ");
                var command = Console.ReadLine();

                if (command == "1")
                {
                    List<(int CurrentFloor, int DestinationFloor, int Passengers)> requests = new List<(int, int, int)>();

                    Console.WriteLine("At any point, enter done to end your selection");

                    while (true)
                    {
                        Console.WriteLine("-------------------------------------------------------------------");
                        Console.Write("Enter floor number your are currently on: ");
                        var currentFloorInput = Console.ReadLine();

                        if (currentFloorInput.ToLower() == "done") break;

                        int currentFloor;
                        if (!int.TryParse(currentFloorInput, out currentFloor) || currentFloor < 0 || currentFloor >= building.NumberOfFloors)
                        {
                            Console.WriteLine("Invalid floor number. Please try again.");
                            continue;
                        }

                        Console.Write("Enter destination floor number: ");
                        var destinationFloorInput = Console.ReadLine();

                        int destinationFloor;
                        if (!int.TryParse(destinationFloorInput, out destinationFloor) || destinationFloor < 0 || destinationFloor > building.NumberOfFloors)
                        {
                            Console.WriteLine("Invalid floor number. Please try again.");
                            continue;
                        }

                        Console.Write("Enter number of passengers waiting: ");
                        var passengersInput = Console.ReadLine();

                        int passengers;
                        if (!int.TryParse(passengersInput, out passengers) || passengers <= 0)
                        {
                            Console.WriteLine("Invalid number of passengers. Please try again.");
                            continue;
                        }

                        requests.Add((currentFloor, destinationFloor, passengers));
                    }
                    Console.WriteLine("-------------------------------------------------------------------");

                    foreach (var request in requests)
                    {
                        elevatorService.CallElevator(request.CurrentFloor, request.DestinationFloor, request.Passengers);
                    }
                }
                else if (command == "2")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid command. Please try again.");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}