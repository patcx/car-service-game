using System;
using System.Collections.Generic;

namespace CarServiceGame.Domain.Database
{
    public partial class GarageUpgrade
    {
        public Guid GarageUpgradeId { get; set; }
        public Guid GarageId { get; set; }
        public decimal Cost { get; set; }
        public int ResultLevel { get; set; }

        public Garage Garage { get; set; }
    }
}
