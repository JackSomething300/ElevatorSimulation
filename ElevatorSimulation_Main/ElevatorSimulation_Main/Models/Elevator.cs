﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulation_Main.Models
{
    public class Elevator
    {
        public int Id { get; set; }
        public int CurrentFloor { get; set; }
        public int MaxPassengers { get; set; }
        public int CurrentPassengers { get; set; }
        public bool IsMoving { get; set; }
        public string Direction { get; set; }
    }
}
