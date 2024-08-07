﻿using ElevatorSimulation_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulation_Core.Entities
{
    public class Building : IBuilding
    {
        public int NumberOfFloors { get; set; }
        public List<Elevator> Elevators { get; set; }
        public int MaxPassengersPerElevator { get; set; }

        public Building(int numberOfFloors, int numberOfElevators, int maxPassengersPerElevator)
        {
            NumberOfFloors = numberOfFloors;
            Elevators = new List<Elevator>();
            for (int i = 0; i < numberOfElevators; i++)
            {
                Elevators.Add(new Elevator(i + 1, maxPassengersPerElevator));
            }
            MaxPassengersPerElevator = maxPassengersPerElevator;
        }
    }
}
