using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServiceGame.Desktop.ViewModels
{
    public class StallViewModel : ObservableObject
    {
        private RepairProcessViewModel repairProcess;

        public int StallNumber { get; set; }
        public RepairProcessViewModel RepairProcess
        {
            get
            {
                return repairProcess;
            }
            set
            {
                repairProcess = value;
                RaisePropertyChanged("RepairProcess");
            }
        }

        public StallViewModel(int number)
        {
            StallNumber = number;
        }
    }
}
