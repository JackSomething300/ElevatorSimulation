using System;
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

        public Elevator(int id, int maxPassengers)
        {
            Id = id;
            CurrentFloor = 0; // Start at ground floor
            MaxPassengers = maxPassengers;
            CurrentPassengers = 0;
            IsMoving = false;
            Direction = "Stationary";
        }

        public void MoveUp()
        {
            CurrentFloor++;
            Direction = "Up";
            IsMoving = true;
        }

        public void MoveDown()
        {
            CurrentFloor--;
            Direction = "Down";
            IsMoving = true;
        }

        public void Stop()
        {
            IsMoving = false;
            Direction = "Stationary";
        }
    }
}
