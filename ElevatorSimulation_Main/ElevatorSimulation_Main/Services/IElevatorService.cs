﻿using ElevatorSimulation_Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulation_Main.Services
{
    public interface IElevatorService
    {
        void CallElevator(int currentFloor, int floor, int passengers);
        void MoveElevator(Elevator elevator, int currentFloor, int destinationFloor);
        void UpdateElevatorStatus();
    }
}
