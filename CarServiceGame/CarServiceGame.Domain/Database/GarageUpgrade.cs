using System;
using System.Collections.Generic;
using System.Text;

namespace CarServiceGame.Domain.Database
{
    public class GarageUpgrade
    {
        public Guid GarageUpgradeId { get; set; }
        public Guid GarageId { get; set; }
        public decimal Cost { get; set; }
        public int ResultLevel { get; set; }

        public Garage Garage { get; set; }
    }
}
