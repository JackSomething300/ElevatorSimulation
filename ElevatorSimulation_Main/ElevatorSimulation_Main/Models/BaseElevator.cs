using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulation_Main.Models
{
    public abstract class BaseElevator
    {
        public int Id { get; set; }
        public int CurrentFloor { get; set; }
        public int MaxPassengers { get; set; }
        public int CurrentPassengers { get; set; }
        public bool IsMoving { get; set; }
        public string Direction { get; set; }
        public string LastDirection { get; set; } = "Stationary";
        public int? DestinationFloor { get; set; }

        protected BaseElevator(int id, int maxPassengers)
        {
            Id = id;
            CurrentFloor = 0; // Start at ground floor
            MaxPassengers = maxPassengers;
            CurrentPassengers = 0;
            IsMoving = false;
            Direction = "Stationary";
        }

        public abstract void MoveUp();

        public abstract void MoveDown();

        public abstract void Stop();
    }
}
