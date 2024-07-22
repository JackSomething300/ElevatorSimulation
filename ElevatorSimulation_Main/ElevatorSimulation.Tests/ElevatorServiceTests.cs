using ElevatorSimulation_Core.Entities;
using ElevatorSimulation_Core.Interfaces;
using ElevatorSimulation_Main.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulation.Tests
{
    public class ElevatorServiceTests
    {

        private readonly IBuilding _building;
        private readonly Mock<IElevatorControlStrategy> _elevatorControlStrategy;

        public ElevatorServiceTests()
        {
            _elevatorControlStrategy = new Mock<IElevatorControlStrategy>();
            _building = new Building(10, 3, 5);
        }

        [Fact]
        public void Building_Initialization_Should_Create_Correct_Number_Of_Elevators()
        {
            var building = new Building(10, 3, 5);
            Assert.Equal(3, building.Elevators.Count);
        }

        [Fact]
        public void CallElevator_Should_Move_Elevator_To_Correct_Floor()
        {
            var mockBuilding = new Mock<IBuilding>();
            var mockStrategy = new Mock<IElevatorControlStrategy>();

            var elevator = new Elevator(1, 5) { CurrentFloor = 0 };
            mockBuilding.Setup(b => b.NumberOfFloors).Returns(10);
            mockBuilding.Setup(b => b.Elevators).Returns(new List<Elevator> { elevator });
            mockBuilding.Setup(b => b.MaxPassengersPerElevator).Returns(5);

            mockStrategy.Setup(strategy => strategy.FindNearestElevator(0))
                        .Returns(elevator);

            var service = new ElevatorService(mockBuilding.Object, mockStrategy.Object);

            // Act
            service.CallElevator(0, 5, 2);

            // Assert
            Assert.Equal(5, elevator.CurrentFloor);
            Assert.Equal(0, elevator.CurrentPassengers);
        }

        [Fact]
        public void CallElevator_Should_Not_Exceed_Capacity()
        {
            // Arrange
            var building = new Building(10, 3, 5);
            var mockStrategy = new Mock<IElevatorControlStrategy>();

            var elevator = building.Elevators.First();
            elevator.CurrentFloor = 0;

            mockStrategy.Setup(strategy => strategy.FindNearestElevator(0))
                        .Returns(elevator);

            var service = new ElevatorService(building, mockStrategy.Object);

            // Act
            service.CallElevator(0, 5, 6); // Attempt to call an elevator with 6 passengers when max cap is 5

            // Assert
            Assert.Equal(0, elevator.CurrentPassengers); // Don't add any passengers
            Assert.Equal(0, elevator.CurrentFloor); // Should not move
        }

        [Fact]
        public void CallElevator_Should_Assign_Nearest_Elevator()
        {
            // Arrange
            var building = new Building(10, 3, 5);
            var mockStrategy = new Mock<IElevatorControlStrategy>();

            var nearestElevator = building.Elevators[2];
            nearestElevator.CurrentFloor = 1;

            mockStrategy.Setup(strategy => strategy.FindNearestElevator(0))
                        .Returns(nearestElevator);

            var service = new ElevatorService(building, mockStrategy.Object);

            service.MoveElevator(building.Elevators[0], 0, 3);
            service.MoveElevator(building.Elevators[1], 0, 8);

            // Act
            service.CallElevator(0, 2, 2);

            // Assert
            Assert.Equal(2, nearestElevator.CurrentFloor);
        }

        [Fact]
        public void MoveElevatorToFloor_Should_Move_Elevator_Up_Successfully()
        {
            // Arrange
            var building = new Building(10, 3, 5);
            var mockStrategy = new Mock<IElevatorControlStrategy>();

            var elevator = building.Elevators.First();
            elevator.CurrentFloor = 0;

            var service = new ElevatorService(building, mockStrategy.Object);

            // Act
            service.MoveElevatorToFloor(elevator, 5);

            // Assert
            Assert.Equal(5, elevator.CurrentFloor);
            Assert.Equal("Stationary", elevator.Direction);
        }

        [Fact]
        public void MoveElevatorToFloor_Should_Move_Elevator_Down_Successfully()
        {
            // Arrange
            var building = new Building(10, 3, 5);
            var mockStrategy = new Mock<IElevatorControlStrategy>();

            var elevator = building.Elevators.First();
            elevator.CurrentFloor = 7;

            var service = new ElevatorService(building, mockStrategy.Object);

            // Act
            service.MoveElevatorToFloor(elevator, 3);

            // Assert
            Assert.Equal(3, elevator.CurrentFloor);
            Assert.Equal("Stationary", elevator.Direction);
        }
    }
}
