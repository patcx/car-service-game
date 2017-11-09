using System;
using System.Collections.Generic;

namespace CarServiceGame.Domain.Database
{
    public partial class RepairOrder
    {
        public Guid RepairOrderId { get; set; }
        public string CarName { get; set; }
        public int RequiredWork { get; set; }
        public decimal Reward { get; set; }
        public string Description { get; set; }

        public RepairProcess RepairProcess { get; set; }
    }
}
