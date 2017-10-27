using System;
using System.Collections.Generic;
using System.Text;

namespace CarServiceGame.Domain.Entities
{
    public class RepairOrder
    {
        public Guid RepairOrderId { get; set; }

        public string CarName { get; }
        public int RequiredWork { get; }
        public IReadOnlyCollection<CarPart> RequiredParts { get; }
    }
}
