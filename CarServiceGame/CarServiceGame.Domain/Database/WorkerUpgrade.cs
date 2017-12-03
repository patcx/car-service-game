using System;
using System.Collections.Generic;

namespace CarServiceGame.Domain.Database
{
    public partial class WorkerUpgrade
    {
        public Guid WorkerUpgradeId { get; set; }
        public Guid GarageId { get; set; }
        public Guid WorkerId { get; set; }
        public decimal Cost { get; set; }
        public decimal EfficiencyIncrease { get; set; }

        public Garage Garage { get; set; }
        public Worker Worker { get; set; }
    }
}
