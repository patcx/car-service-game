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
        public int NumberOfWorkers { get; set; }
        public int NumberOfCompletedOrders { get; set; }
        public int Efficiency { get; set; }

        public GarageRanking()
        {

        }

        public GarageRanking(Guid garageId, string name, decimal cashBalance, int numberOfWorkers, int numberOfCompletedOrders, int efficiency)
        {
            GarageId = garageId;
            Name = name;
            CashBalance = cashBalance;
            NumberOfWorkers = numberOfWorkers;
            NumberOfCompletedOrders = numberOfCompletedOrders;
            Efficiency = efficiency;
        }

    }
}
