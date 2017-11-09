using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CarServiceGame.Domain.Concrete;
using CarServiceGame.Domain.Contracts;
using CarServiceGame.Domain.Mock;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace CarServiceGame.Desktop.ViewModels
{
    public class AvailableWorkersCollectionViewModel : ObservableObject
    {
        private IWorkerRepository workersRepository;

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

        public ObservableCollection<WorkerViewModel> AvailableWorkers { get; private set; }

        public AvailableWorkersCollectionViewModel(IWorkerRepository workerRepository)
        {
            this.workersRepository = workerRepository;
        }

        public AvailableWorkersCollectionViewModel()
        {
            workersRepository = new WorkerRepository();
        }

        public ICommand HireWorker => new RelayCommand<WorkerViewModel>(w =>
        {
            var window = (Application.Current.MainWindow as MetroWindow);
            var progressDialog = window.ShowProgressAsync("Please wait...", "Employeeing worker...", false);

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() =>
            {
                progressDialog.Result.SetIndeterminate();
                
                workersRepository.EmployWorker(GlobalResources.Garage.GetModel().GarageId, w.GetModel().WorkerId);

                progressDialog.Result.CloseAsync();

            }).ContinueWith(x =>
            {
                if (x.Exception != null)
                {
                    window.ShowMessageAsync("", "Error while employeeing worker").Wait();
                }
                else
                {
                    GlobalResources.Garage.HireWorker(w);
                    AvailableWorkers.Remove(w);
                }
            }, scheduler);
        });

        public void Refresh()
        {
            var window = (Application.Current.MainWindow as MetroWindow);
            var progressDialog = window.ShowProgressAsync("Please wait...", "Refreshing...", false);

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() =>
            {
                progressDialog.Result.SetIndeterminate();
                var workers = workersRepository.GetUnemployedWorkers(0, 20);
                progressDialog.Result.CloseAsync();
                return workers;

            }).ContinueWith(x =>
            {
                if (x.Result == null)
                {
                    window.ShowMessageAsync("", "Error while refreshing data").Wait();
                }
                else
                {
                    AvailableWorkers = new ObservableCollection<WorkerViewModel>(from w in x.Result select new WorkerViewModel(w));
                    RaisePropertyChanged("AvailableWorkers");
                }
            });
        }
    }
}
