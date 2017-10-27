using System;
using System.Collections.Generic;
using System.Text;

namespace CarServiceGame.Domain.Entities
{
    public class Garage
    {
        public Guid GarageId { get; }

        public int Worksites { get; }
        public decimal CashBalance { get; }

        public List<Worker> EmployeedWorkers { get; }
        public List<CarPart> CarParts { get; }
    }
}
