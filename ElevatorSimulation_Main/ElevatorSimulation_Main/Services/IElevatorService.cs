using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulation_Main.Services
{
    public interface IElevatorService
    {
        void CallElevator(int floor, int passengers);
        void UpdateElevatorStatus();
    }
}
