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
        private IWorkerRepository workersRepository;
        private Garage model;

        private RepairProcessViewModel stall1;
        private RepairProcessViewModel stall2;
        private RepairProcessViewModel stall3;
        private RepairProcessViewModel stall4;



        public RepairProcessViewModel Stall1
        {
            get { return stall1; }
            set
            {
                stall1 = value;
                RaisePropertyChanged("Stall1");
            }
        }
        public RepairProcessViewModel Stall2
        {
            get { return stall2; }
            set
            {
                stall2 = value;
                RaisePropertyChanged("Stall2");
            }
        }
        public RepairProcessViewModel Stall3
        {
            get { return stall3; }
            set
            {
                stall3 = value;
                RaisePropertyChanged("Stall3");
            }
        }
        public RepairProcessViewModel Stall4
        {
            get { return stall4; }
            set
            {
                stall4 = value;
                RaisePropertyChanged("Stall4");
            }
        }

        public ObservableCollection<WorkerViewModel> EmployeedWorkers { get; }

        public GarageViewModel(Garage model)
        {
            this.model = model;
            workersRepository = new MockRepository();

            EmployeedWorkers = new ObservableCollection<WorkerViewModel>(from w in model.EmployeedWorkers select new WorkerViewModel(w));
            RaisePropertyChanged("EmployeedWorkers");

            stall1 = new RepairProcessViewModel(new OrderViewModel(new RepairOrder(Guid.NewGuid(), "Audi", 1000, 100, "Desc")),
                new WorkerViewModel(new Worker(Guid.NewGuid(), "Name", 250, 1000)));
            stall2 = new RepairProcessViewModel(new OrderViewModel(new RepairOrder(Guid.NewGuid(), "Audi", 10000, 100, "Car Broken")),
                new WorkerViewModel(new Worker(Guid.NewGuid(), "Janusz", 500, 1000)));
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

        public void AssignOrderToStall(int stallNumber, OrderViewModel order, WorkerViewModel worker)
        {
            RepairProcessViewModel repairProcess = new RepairProcessViewModel(order, worker);
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
                    switch (stallNumber)
                    {
                        case 1:
                            Stall1 = repairProcess;
                            break;
                        case 2:
                            Stall2 = repairProcess;
                            break;
                        case 3:
                            Stall3 = repairProcess;
                            break;
                        case 4:
                            Stall4 = repairProcess;
                            break;
                    }
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
