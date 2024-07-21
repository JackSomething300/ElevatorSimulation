using ElevatorSimulation_Core.Exceptions;
using ElevatorSimulation_Core.Entities;
using ElevatorSimulation_Main.Services;
using ElevatorSimulation_Core.Interfaces;
using System;
using Microsoft.Extensions.DependencyInjection;
using ElevatorSimulation_Application.Services;

namespace ElevatorSimulation
{
    public class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConfigureServices();
            var elevatorService = serviceProvider.GetService<IElevatorService>();

            while (true)
            {
                try
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
                        var requests = CollectRequests(serviceProvider.GetService<IBuilding>());

                        foreach (var request in requests)
                        {
                            try
                            {
                                elevatorService.CallElevator(request.CurrentFloor, request.DestinationFloor, request.Passengers);
                            }
                            catch (ElevatorCapacityExceededException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (NoElevatorAvailableException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                            }
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
                catch (Exception e)
                {
                    Console.WriteLine($"An error occurred: {e.Message}");
                }
            }
        }

        private static List<(int CurrentFloor, int DestinationFloor, int Passengers)> CollectRequests(IBuilding building)
        {
            var requests = new List<(int, int, int)>();
            Console.WriteLine("At any point, enter done to end your selection");

            while (true)
            {
                Console.WriteLine("-------------------------------------------------------------------");
                Console.Write("Enter floor number you are currently on: ");
                var currentFloorInput = Console.ReadLine();

                if (currentFloorInput.ToLower() == "done") break;

                if (!int.TryParse(currentFloorInput, out int currentFloor) || currentFloor < 0 || currentFloor >= building.NumberOfFloors)
                {
                    Console.WriteLine("Invalid floor number. Please try again.");
                    continue;
                }

                Console.Write("Enter destination floor number: ");
                var destinationFloorInput = Console.ReadLine();

                if (!int.TryParse(destinationFloorInput, out int destinationFloor) || destinationFloor < 0 || destinationFloor > building.NumberOfFloors)
                {
                    Console.WriteLine("Invalid floor number. Please try again.");
                    continue;
                }

                Console.Write("Enter number of passengers waiting: ");
                var passengersInput = Console.ReadLine();

                if (!int.TryParse(passengersInput, out int passengers) || passengers <= 0)
                {
                    Console.WriteLine("Invalid number of passengers. Please try again.");
                    continue;
                }

                requests.Add((currentFloor, destinationFloor, passengers));
            }

            return requests;
        }

        private static ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<IBuilding>(new Building(10, 3, 5)) // Manually create the Building instance
                .AddSingleton<IElevatorService, ElevatorService>()
                .AddSingleton<IElevatorControlStrategy, ElevatorControlStrategy>()
                .BuildServiceProvider();
        }
    }
}