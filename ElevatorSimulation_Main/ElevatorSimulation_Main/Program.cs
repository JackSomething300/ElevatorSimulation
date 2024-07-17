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
                    Console.Write("Enter floor number to call the elevator: ");
                    var floor = int.Parse(Console.ReadLine());

                    Console.Write("Enter number of passengers waiting: ");
                    var passengers = int.Parse(Console.ReadLine());

                    elevatorService.CallElevator(floor, passengers);
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