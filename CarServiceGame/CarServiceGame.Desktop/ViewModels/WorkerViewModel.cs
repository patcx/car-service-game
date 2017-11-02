using CarServiceGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServiceGame.Desktop.ViewModels
{
    public class WorkerViewModel
    {
        public string Name
        {
            get
            {
                return worker.Name;
            }
        }
        public int Efficiency {
            get
            {
                return worker.Efficiency;
            }
        }
        public decimal Salary {
            get
            {
                return worker.Salary;
            }
        }

        private Worker worker;

        public WorkerViewModel(Worker worker)
        {
            this.worker = worker;
        }

        public Worker GetModel()
        {
            return worker;
        }
    }
}
