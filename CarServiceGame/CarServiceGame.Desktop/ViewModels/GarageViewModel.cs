using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CarServiceGame.Domain.Contracts;
using CarServiceGame.Domain.Entities;
using CarServiceGame.Domain.Mock;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace CarServiceGame.Desktop.ViewModels
{
    public class GarageViewModel : ObservableObject
    {
        private readonly int numberOfStalls = 4;

        private IWorkerRepository workersRepository;
        private Garage model;

        private RepairProcessViewModel[] stalls;

        public RepairProcessViewModel[] Stalls
        {
            get
            {
                return stalls;
            }
            set
            {
                stalls = value;
                RaisePropertyChanged("Stalls");
            }
        }

        public ObservableCollection<WorkerViewModel> EmployeedWorkers { get; }

        public ObservableCollection<WorkerViewModel> AvailableWorkers
        {
            get
            {
                return new ObservableCollection<WorkerViewModel>((EmployeedWorkers.Where(x =>
               {
                   foreach (var v in Stalls)
                   {
                       if (v != null && v.AssignedWorker == x) return false;
                   }
                   return true;
               })).ToList());
            }
        }

        public GarageViewModel(Garage model)
        {
            this.model = model;
            workersRepository = new MockRepository();
            Stalls = new RepairProcessViewModel[numberOfStalls];

            for (int i = 0; i < numberOfStalls && i < model.RepairProcesses.Count; i++)
            {
                Stalls[i] = new RepairProcessViewModel(model.RepairProcesses[i], i);
            }

            EmployeedWorkers = new ObservableCollection<WorkerViewModel>(from w in model.EmployeedWorkers select new WorkerViewModel(w));
            RaisePropertyChanged("EmployeedWorkers");
        }

        public ICommand FireWorker => new RelayCommand<WorkerViewModel>(w =>
        {
            var window = (Application.Current.MainWindow as MetroWindow);
            var progressDialog = window.ShowProgressAsync("Please wait...", "Firing worker...", false);

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() =>
            {
                progressDialog.Result.SetIndeterminate();

                workersRepository.FireWorker(w.GetModel().WorkerId);

                progressDialog.Result.CloseAsync();

            }).ContinueWith(x =>
            {
                if (x.Exception != null)
                {
                    window.ShowMessageAsync("", "Error while firing worker").Wait();
                }
                else
                {
                    GlobalResources.AvailableWorkers.Add(w);
                    model.FireWorker(w.GetModel());
                    EmployeedWorkers.Remove(w);
                }
            }, scheduler);

        }, w => model.RepairProcesses.All(x => x.AssignedWorker.WorkerId != w.GetModel().WorkerId));


        public ICommand FinishJob => new RelayCommand<RepairProcessViewModel>(rp =>
        {
            var window = (Application.Current.MainWindow as MetroWindow);
            var progressDialog = window.ShowProgressAsync("Please wait...", "Firing worker...", false);

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() =>
            {
                progressDialog.Result.SetIndeterminate();

                // TODO repository

                progressDialog.Result.CloseAsync();

            }).ContinueWith(x =>
            {
                if (x.Exception != null)
                {
                    window.ShowMessageAsync("", "Error while firing worker").Wait();
                }
                else
                {
                    Stalls[rp.StallNumber] = null;
                    RaisePropertyChanged("Stalls");
                    RaisePropertyChanged("AvailableWorkers");
                }
            }, scheduler);

        });


        public void AssignOrderToStall(int stallNumber, OrderViewModel order, WorkerViewModel worker)
        {
            RepairProcessViewModel repairProcess = new RepairProcessViewModel(order, worker, stallNumber);
            var window = (Application.Current.MainWindow as MetroWindow);
            var progressDialog = window.ShowProgressAsync("Please wait...", "Accpeting order...", false);
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() =>
            {
                progressDialog.Result.SetIndeterminate();

                // TODO repository change

                progressDialog.Result.CloseAsync();

            }).ContinueWith(x =>
            {
                if (x.Exception != null)
                {
                    window.ShowMessageAsync("", "Error while accepting order").Wait();
                }
                else
                {
                    Stalls[stallNumber] = repairProcess;
                    RaisePropertyChanged("Stalls");
                    RaisePropertyChanged("AvailableWorkers");
                }
            }, scheduler);
        }

        public void HireWorker(WorkerViewModel worker)
        {
            model.HireWorker(worker.GetModel());
            EmployeedWorkers.Add(worker);
        }

        public Garage GetModel()
        {
            return model;
        }

    }
}
