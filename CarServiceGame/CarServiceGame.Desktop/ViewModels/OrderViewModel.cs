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
                return model.CarName; 
            }
        }
        public int RequiredWork
        {
            get
            {
                return model.RequiredWork;
            }
        }

        public decimal Reward
        {
            get { return model.Reward; }
        }

        public string Description
        {
            get { return model.Description; }
        }

        private RepairOrder model;

        public OrderViewModel(RepairOrder repairOrder)
        {
            this.model = repairOrder;
        }

        public RepairOrder GetModel()
        {
            return model;
        }
    }
}
