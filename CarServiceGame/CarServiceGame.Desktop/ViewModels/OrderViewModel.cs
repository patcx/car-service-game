using CarServiceGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServiceGame.Desktop.ViewModels
{
    public class OrderViewModel
    {
        public string CarName
        {
            get
            {
                return repairOrder.CarName; 
            }
        }
        public int RequiredWork
        {
            get
            {
                return repairOrder.RequiredWork;
            }
        }

        private RepairOrder repairOrder;

        public OrderViewModel(RepairOrder repairOrder)
        {
            this.repairOrder = repairOrder;
        }
    }
}
