using System;
using System.Collections.Generic;
using System.Text;

namespace CarServiceGame.Domain.Entities
{
    public class RepairProcess
    {
        public RepairOrder Order { get; }
        public Worker AssignedWorker { get; }

        public RepairProcess(RepairOrder order, Worker worker)
        {
            if(order == null || worker == null)
                throw new ArgumentException("order or worker is null");

            Order = order;
            AssignedWorker = worker;
        }
    }
}
