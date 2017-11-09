using System;
using System.Collections.Generic;
using System.Text;

namespace CarServiceGame.Domain.Entities
{
    public class RepairOrder
    {
        public Guid RepairOrderId { get; set; }

        public string CarName { get; set; }
        public int RequiredWork { get; set; }
        public decimal Reward { get; set; }
        public string Description { get; set; }

        public RepairOrder() { }

        public RepairOrder(Guid id, string carName, int requiredWork, int reward, string description)
        {
            RepairOrderId = id;
            CarName = carName;
            RequiredWork = requiredWork;
            Reward = reward;
            Description = description;
        }


    }
}
