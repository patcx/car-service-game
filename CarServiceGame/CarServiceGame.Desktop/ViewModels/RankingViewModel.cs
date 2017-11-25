using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServiceGame.Desktop.ViewModels
{
    public class RankingViewModel : ObservableObject
    {
        public bool IsTabSelected
        {
            set
            {
                if (value)
                {
                    Refresh();
                }
            }
        }

        private void Refresh()
        {

        }
    }
}
