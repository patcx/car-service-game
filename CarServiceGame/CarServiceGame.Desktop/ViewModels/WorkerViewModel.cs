using CarServiceGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServiceGame.Desktop.ViewModels
{
    class WorkerViewModel
    {
        public string Name
        {
            get
            {
                return Worker.Name;
            }
        }
        public int Efficiency {
            get
            {
                return Worker.Efficiency;
            }
        }
        public decimal Salary {
            get
            {
                return Worker.Salary;
            }
        }

        private Worker Worker;

        public WorkerViewModel()
        {

        }

        public WorkerViewModel(Worker worker)
        {
            this.Worker = worker;
        }

    }
}
