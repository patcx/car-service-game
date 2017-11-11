using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using CarServiceGame.Domain.Entities;

namespace CarServiceGame.Desktop.ViewModels
{
    public class RepairProcessViewModel : ObservableObject
    {
        public OrderViewModel Order { get; }
        public WorkerViewModel AssignedWorker { get; }
        public bool Completed { get; set; }
        public int SecondsToEnd { get; set; }
        public int StallNumber { get; set; }
        public decimal Profit { get; }

        public RepairProcessViewModel(OrderViewModel order, WorkerViewModel worker, int stall)
        {
            Order = order;
            AssignedWorker = worker;
            Completed = false;
            SecondsToEnd = Order.RequiredWork / AssignedWorker.Efficiency;
            StallNumber = stall;
            Profit = Order.Reward - AssignedWorker.Salary;
        }

        public RepairProcessViewModel(RepairProcess repairProcess)
        {
            Order = new OrderViewModel(repairProcess.Order);
            AssignedWorker = new WorkerViewModel(repairProcess.AssignedWorker);
            Completed = false;
            SecondsToEnd = repairProcess.GetRequiredTime();
            Profit = repairProcess.CalculateProfit();
            StallNumber = repairProcess.StallNumber;
        }
    }
}
