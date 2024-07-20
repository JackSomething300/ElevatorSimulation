using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulation_Main.Models.Abstractions
{
    public class FreightElevator : BaseElevator
    {
        public FreightElevator(int id, int maxPassengers) : base(id, maxPassengers)
        {
            Id = id;
            CurrentFloor = 0; // Start at ground floor
            MaxPassengers = maxPassengers;
            CurrentPassengers = 0;
            IsMoving = false;
            Direction = "Stationary";
        }

        public override void MoveDown()
        {
            throw new NotImplementedException();
        }

        public override void MoveUp()
        {
            throw new NotImplementedException();
        }

        public override void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
