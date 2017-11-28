using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarServiceGame.Domain.Entities;

namespace CarServiceGame.Desktop.ViewModels
{
    public class GarageRankingViewModel
    {
        private GarageRanking model;

        public string GarageName
        {
            get
            {
                return model.Name;
            }
        }

        public decimal GarageCashBalance
        {
            get
            {
                return model.CashBalance;
            }
        }



        public GarageRankingViewModel(GarageRanking model)
        {
            this.model = model;
        }
    }
}
