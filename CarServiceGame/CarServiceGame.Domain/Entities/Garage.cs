using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarServiceGame.Domain.Entities
{
    public class Garage
    {
        public Guid GarageId { get; set; }

        public decimal CashBalance { get; set; }
        public List<Worker> EmployeedWorkers { get; set; }
        public List<RepairProcess> RepairProcesses { get; set; }

        public Garage()
        {
            
        }

        public Garage(Guid id, decimal balance, IEnumerable<Worker> workers, IEnumerable<RepairProcess> repairs = null)
        {
            GarageId = id;
            CashBalance = balance;
            EmployeedWorkers = new List<Worker>(workers);
            if (repairs != null)
            {
                if(repairs.Count()>4)
                    throw new ArgumentOutOfRangeException("repairs", "too many repair orders");
                RepairProcesses = new List<RepairProcess>(repairs);
            }
            else
                RepairProcesses = new List<RepairProcess>();
        }

        public void HireWorker(Worker worker)
        {
            EmployeedWorkers.Add(worker);
        }

        public void FireWorker(Worker worker)
        {
            EmployeedWorkers.Remove(worker);
        }

    }
}
