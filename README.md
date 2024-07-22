# Elevator Simulation Program

## Overview
The Elevator Simulation Program is a console application written in C# that simulates the movement of elevators within a building. It allows users to call elevators from specific floors, input the number of passengers, and specify destination floors. The program aims to optimize passenger transportation efficiently by adhering to Object-Oriented Programming (OOP) principles.

## Features
- Real-Time Elevator Status: Display the real-time status of each elevator, including its current floor, direction of movement, and the number of passengers it is carrying.
- Interactive Elevator Control: Allow users to interact with the elevators through the console application. Users can call an elevator to a specific floor and indicate the number of passengers waiting.
- Multiple Floors and Elevators Support: The application accommodates buildings with multiple floors and multiple elevators.
- Efficient Elevator Dispatching: An algorithm efficiently directs the nearest available elevator to respond to an elevator request, minimizing wait times for passengers.
- Passenger Limit Handling: The program considers the maximum passenger limit for each elevator and handles scenarios where additional elevators might be required.
- Error Handling: Comprehensive error handling and input validation ensure robustness and user-friendly interaction.

### Prerequisites
.NET Core 8.0 SDK or later

## Usage
Run the application:
 **Please clean and rebuild the application to restore nuget packages and build the program. Thanks**
 
bash
```
dotnet run --project ElevatorSimulation
cd ElevatorSimulation
```
```
dotnet build
```

### Interactive Commands:

Upon running the application, you will be presented with the real-time status of the elevators.
You can then choose from the following commands:
1. Call Elevator
2. Exit
Calling an Elevator:

If you choose to call an elevator, you will be prompted to enter the current floor number, destination floor number, and the number of passengers waiting.
You can enter multiple requests by repeating the above steps. Type done to finish entering requests.
Exiting the Application:

Choose the exit command (2. Exit) to terminate the application.
Example Interaction
plaintext
Copy code
```
Elevator 1: Floor 0, Direction: Stationary, Passengers: 0
Elevator 2: Floor 0, Direction: Stationary, Passengers: 0
Elevator 3: Floor 0, Direction: Stationary, Passengers: 0
This building has 10 floors and 3 elevator and 5 max passangers per elevator

Commands:
1. Call Elevator
2. Exit
Please enter your command: 1
At any point, enter done to end your selection
-------------------------------------------------------------------
Enter floor number your are currently on: 3
Enter destination floor number: 7
Enter number of passengers waiting: 4
-------------------------------------------------------------------
Enter floor number your are currently on: 5
Enter destination floor number: 2
Enter number of passengers waiting: 2
-------------------------------------------------------------------
Enter floor number your are currently on: done
-------------------------------------------------------------------
Elevator 1 is at floor 1 moving to Up
Elevator 1 is at floor 2 moving to Up
Elevator 1 is at floor 3 moving to Up
Elevator 1 has arrived at floor 3 and is now Stationary
Passengers boarded Elevator 1 at floor 3, 4 passangers are onboard
Elevator 1 is at floor 4 moving to Up
Elevator 1 is at floor 5 moving to Up
Elevator 1 is at floor 6 moving to Up
Elevator 1 is at floor 7 moving to Up
Elevator 1 has arrived at floor 7 and is now Stationary
Passengers exited Elevator 1 at floor 7, 0 people remaining
-------------------------------------------------------------------
Elevator 1 is at floor 6 moving to Down
Elevator 1 is at floor 5 moving to Down
Elevator 1 has arrived at floor 5 and is now Stationary
Passengers boarded Elevator 1 at floor 5, 2 passangers are onboard
Elevator 1 is at floor 4 moving to Down
Elevator 1 is at floor 3 moving to Down
Elevator 1 is at floor 2 moving to Down
Elevator 1 has arrived at floor 2 and is now Stationary
Passengers exited Elevator 1 at floor 2, 0 people remaining
-------------------------------------------------------------------

Press any key to continue...

Commands:
1. Call Elevator
2. Exit
Please enter your command:
```

## Error Handling
The application includes comprehensive error handling to manage various scenarios, such as invalid floor numbers, invalid passenger counts, and unavailable elevators. Specific error messages guide users to correct their inputs.

## License
This project is licensed under the MIT License.
