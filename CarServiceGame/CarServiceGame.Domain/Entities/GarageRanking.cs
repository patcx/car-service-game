using System;
using System.Collections.Generic;
using System.Text;

namespace CarServiceGame.Domain.Entities
{
    public class GarageRanking
    {
        public Guid GarageId { get; set; }
        public string Name { get; set; }
        public decimal CashBalance { get; set; }

        public GarageRanking()
        {

        }

        public GarageRanking(Guid garageId, string name, decimal cashBalance)
        {
            GarageId = garageId;
            Name = name;
            CashBalance = cashBalance;
        }

    }
}
