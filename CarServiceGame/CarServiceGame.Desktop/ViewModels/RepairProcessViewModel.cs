﻿using GalaSoft.MvvmLight;
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
        private RepairProcess model;

        public OrderViewModel Order { get; }
        public WorkerViewModel AssignedWorker { get; }
        public bool Completed { get; set; }
        public int SecondsToEnd { get; set;}
        public int StallNumber { get; set; }

        public RepairProcessViewModel(OrderViewModel order, WorkerViewModel worker, int stall)
        {
            Order = order;
            AssignedWorker = worker;
            Completed = false;
            SecondsToEnd = Order.RequiredWork / AssignedWorker.Efficiency;
            StallNumber = stall;

            model = new RepairProcess(order.GetModel(), worker.GetModel())
            {
                CreatedDate = DateTime.Now,
                StallNumber = stall
            };
        }

        public RepairProcessViewModel(RepairProcess repairProcess, int stall)
        {
            Order = new OrderViewModel(repairProcess.Order);
            AssignedWorker = new WorkerViewModel(repairProcess.AssignedWorker);
            Completed = false;
            SecondsToEnd = repairProcess.GetRequiredTime();
            StallNumber = stall;
        }

        public RepairProcess GetModel()
        {
            return model;
        }
    }
}
