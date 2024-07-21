using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulation_Core.Entities
{
    public class Elevator : BaseElevator
    {
        public int Id { get; set; }
        public int CurrentFloor { get; set; }
        public int MaxPassengers { get; set; }
        public int CurrentPassengers { get; set; }
        public bool IsMoving { get; set; }
        public string Direction { get; set; }
        public string LastDirection { get; set; } = "Stationary";
        public int? DestinationFloor { get; set; }

        public Elevator(int id, int maxPassengers) : base(id, maxPassengers)
        {
            Id = id;
            CurrentFloor = 0; // Start at ground floor
            MaxPassengers = maxPassengers;
            CurrentPassengers = 0;
            IsMoving = false;
            Direction = "Stationary";
        }

        public override void MoveUp()
        {
            CurrentFloor++;
            Direction = "Up";
            IsMoving = true;
        }

        public override void MoveDown()
        {
            CurrentFloor--;
            Direction = "Down";
            IsMoving = true;
        }

        public override void Stop()
        {
            IsMoving = false;
            Direction = "Stationary";
        }
    }
}
