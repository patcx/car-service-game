using System;
using System.Collections.Generic;
using System.Text;

namespace CarServiceGame.Domain.Entities
{
    public class Garage
    {
        public Guid GarageId { get; }

        public decimal CashBalance { get; private set; }
        public List<Worker> EmployeedWorkers { get; }
        public List<RepairProcess> RepairProcesses { get; }

        public Garage(Guid id, decimal cash, IEnumerable<Worker> employees, IEnumerable<RepairProcess> repairProcesses = null)
        {
            GarageId = id;
            CashBalance = cash;
            EmployeedWorkers = new List<Worker>(employees);
            RepairProcesses = repairProcesses == null ?  new List<RepairProcess>() : new List<RepairProcess>(repairProcesses);
        }

        public void HireWorker(Worker worker)
        {
            EmployeedWorkers.Add(worker);
        }

        public void FireWorker(Worker worker)
        {
            EmployeedWorkers.Remove(worker);
        }

        public void SetCashBalance(int value)
        {
            CashBalance = value;
        }
    }
}
