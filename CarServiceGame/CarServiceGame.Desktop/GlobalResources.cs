using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CarServiceGame.Desktop.ViewModels;

namespace CarServiceGame.Desktop
{
    public static class GlobalResources
    {
        public static GarageViewModel Garage => (Application.Current.FindResource("viewModelsLocator") as ViewModelsLocator)?.MainViewModel?.Garage;
        public static ObservableCollection<WorkerViewModel> AvailableWorkers => 
            (Application.Current.FindResource("viewModelsLocator") as ViewModelsLocator)?.AvailableWorkersCollection?.AvailableWorkers;
    }
}
