using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace CarServiceGame.Desktop.ViewModels
{
    public class RepairProcessViewModel : ObservableObject
    {
        public OrderViewModel Order { get; }
        public WorkerViewModel AssignedWorker { get; }
        public bool Completed { get; set; }
        public int SecondsToEnd { get; set;}

        public RepairProcessViewModel(OrderViewModel order, WorkerViewModel worker)
        {
            Order = order;
            AssignedWorker = worker;
            Completed = false;
            SecondsToEnd = Order.RequiredWork / AssignedWorker.Efficiency;
        }
    }
}
