using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CarServiceGame.Domain.Entities;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace CarServiceGame.Desktop.ViewModels
{
    public class GarageViewModel : ObservableObject
    {
        private Garage model;

        public ObservableCollection<WorkerViewModel> EmployeedWorkers { get; }

        public GarageViewModel(Garage model)
        {
            this.model = model;

            EmployeedWorkers = new ObservableCollection<WorkerViewModel>(from w in model.EmployeedWorkers select new WorkerViewModel(w));
            RaisePropertyChanged("EmployeedWorkers");
        }

        public ICommand FireWorker => new RelayCommand<WorkerViewModel>(w =>
        {
            
        });

    }
}
