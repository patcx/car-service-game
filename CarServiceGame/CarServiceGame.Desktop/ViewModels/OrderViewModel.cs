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

        public decimal Reward
        {
            get { return repairOrder.Reward; }
        }

        public string Description
        {
            get { return repairOrder.Description; }
        }

        private RepairOrder repairOrder;

        public OrderViewModel(RepairOrder repairOrder)
        {
            this.repairOrder = repairOrder;
        }
    }
}
