using System;
using System.Collections.Generic;
using System.Text;

namespace CarServiceGame.Domain.Entities
{
    public class RepairProcess
    {
        private RepairOrder Order { get; }
        private Worker AssignedWorker { get; }
    }
}
