using System;
using System.Collections.Generic;

namespace CarServiceGame.Domain.Database
{
    public partial class Worker
    {
        public Worker()
        {
            RepairProcess = new HashSet<RepairProcess>();
            WorkerUpgrade = new HashSet<WorkerUpgrade>();
        }

        public Guid WorkerId { get; set; }
        public Guid? GarageId { get; set; }
        public string Name { get; set; }
        public int Efficiency { get; set; }
        public decimal Salary { get; set; }

        public Garage Garage { get; set; }
        public ICollection<RepairProcess> RepairProcess { get; set; }
        public ICollection<WorkerUpgrade> WorkerUpgrade { get; set; }
    }
}
