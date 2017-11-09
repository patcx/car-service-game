using System;
using System.Collections.Generic;

namespace CarServiceGame.Domain.Database
{
    public partial class RepairProcess
    {
        public Guid RepairProcessId { get; set; }
        public Guid GarageId { get; set; }
        public Guid WorkerId { get; set; }
        public Guid RepairOrderId { get; set; }
        public int StallNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? IsPickedUp { get; set; }

        public Garage Garage { get; set; }
        public RepairOrder RepairOrder { get; set; }
        public Worker Worker { get; set; }
    }
}
