using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulation_Main.Models.Abstractions
{
    public class PassengerElevator : BaseElevator
    {
        public PassengerElevator(int id, int maxPassengers) : base(id, maxPassengers)
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
            LastDirection = "Up";
        }

        public override void MoveDown()
        {
            CurrentFloor--;
            LastDirection = "Down";
        }

        public override void Stop()
        {
            LastDirection = "Stationary";
        }
    }
}
