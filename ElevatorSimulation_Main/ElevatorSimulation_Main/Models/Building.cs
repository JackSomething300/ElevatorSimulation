using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulation_Main.Models
{
    public class Building
    {
        public int NumberOfFloors { get; set; }
        public List<Elevator> Elevators { get; set; }
    }
}
