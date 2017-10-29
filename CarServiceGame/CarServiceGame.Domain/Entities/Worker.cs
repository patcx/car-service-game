using System;
using System.Collections.Generic;
using System.Text;

namespace CarServiceGame.Domain.Entities
{
    public class Worker
    {
        public Guid WorkerId { get; }

        public string Name { get; }
        public int Efficiency { get; }
        public decimal Salary { get; }

        public Worker(Guid id, string name, int efficiency, decimal salary)
        {
            WorkerId = id;
            Name = name;
            Efficiency = efficiency;
            Salary = salary;
        }
    }
}
