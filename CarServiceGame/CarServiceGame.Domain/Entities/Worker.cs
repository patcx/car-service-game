using System;
using System.Collections.Generic;
using System.Text;

namespace CarServiceGame.Domain.Entities
{
    public class Worker
    {
        public Guid WorkerId { get; set; }

        public string Name { get; set; }
        public int Efficiency { get; set; }
        public decimal Salary { get; set; }

        public Worker()
        {
            
        }
        public Worker(Guid id, string name, int efficiency, decimal salary)
        {
            WorkerId = id;
            Name = name;
            Efficiency = efficiency;
            Salary = salary;
        }
    }
}
