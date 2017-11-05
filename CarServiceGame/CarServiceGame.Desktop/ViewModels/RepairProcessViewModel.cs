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

        public RepairProcessViewModel(OrderViewModel order, WorkerViewModel worker)
        {
            Order = order;
            AssignedWorker = worker;
        }
    }
}
