using System;
using System.Collections.Generic;
using System.Text;

namespace CarServiceGame.Domain.Entities
{
    public class RepairProcess
    {
        public RepairOrder Order { get; set; }
        public Worker AssignedWorker { get; set; }
        public int StallNumber { get; set; }
        public DateTime CreatedDate { get; set; }

        public RepairProcess() { }

        public RepairProcess(RepairOrder order, Worker worker)
        {
            if(order == null || worker == null)
                throw new ArgumentException("order or worker is null");

            Order = order;
            AssignedWorker = worker;
        }

        public int GetRequiredTime()
        {
            return  Order.RequiredWork/AssignedWorker.Efficiency - (int) DateTime.Now.Subtract(CreatedDate).TotalSeconds;
        }
    }
}
