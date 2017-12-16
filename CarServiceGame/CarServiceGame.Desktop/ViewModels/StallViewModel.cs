using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServiceGame.Desktop.ViewModels
{
    public class StallViewModel
    {
        public int StallNumber { get; set; }
        public RepairProcessViewModel repairProcess { get; set; }

        public StallViewModel()
        {
                
        }
    }
}
