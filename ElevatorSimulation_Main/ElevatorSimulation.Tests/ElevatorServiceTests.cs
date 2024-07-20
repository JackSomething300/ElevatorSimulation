using ElevatorSimulation_Main.Models;
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
        private Building _building;

        public ElevatorServiceTests()
        {
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
            var building = new Building(10, 3, 5);
            var service = new ElevatorService(building);

            service.CallElevator(5, 2);

            var elevator = building.Elevators.First();
            Assert.Equal(5, elevator.CurrentFloor);
            Assert.Equal(2, elevator.CurrentPassengers);
        }

        [Fact]
        public void CallElevator_Should_Not_Exceed_Capacity()
        {
            var building = new Building(10, 3, 5);
            var service = new ElevatorService(building);

            // Attempt to call an elevator with 6 passengers when max cap is 5
            service.CallElevator(5, 6);

            var elevator = building.Elevators.First();
            // Dont add any passengers
            Assert.Equal(0, elevator.CurrentPassengers);
            // Should not move
            Assert.Equal(1, elevator.CurrentFloor);
        }

        [Fact]
        public void CallElevator_Should_Assign_Nearest_Elevator()
        {
            var building = new Building(10, 3, 5);
            var service = new ElevatorService(building);

            //This should be the nearest elevator, because it is not moving and it is 1 floor away
            building.Elevators[2].CurrentFloor = 1;

            service.MoveElevator(building.Elevators[0], 3);
            service.MoveElevator(building.Elevators[1], 8);

            service.CallElevator(2, 2);

            Assert.Equal(2, building.Elevators[2].CurrentFloor);
        }
    }
}
