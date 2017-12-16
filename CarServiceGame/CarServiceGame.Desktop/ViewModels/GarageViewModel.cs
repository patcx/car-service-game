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
        private IOrderRepository ordersRepository;
        private IGarageRepository garageRepository;
        private Garage model;
        public int NumberOfStalls
        {
            get
            {
                return model.GarageLevel;
            }
            set
            {
                model.GarageLevel = value;
                RaisePropertyChanged("NumberOfStalls");
            }
        }

        private StallViewModel[] stalls;

        public StallViewModel[] Stalls
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

        public decimal Balance
        {
            get
            {
                return model.CashBalance;
            }
            set
            {
                model.CashBalance = value;
                RaisePropertyChanged("Balance");
            }
        }

        public ObservableCollection<WorkerViewModel> EmployeedWorkers { get; private set; }

        public ObservableCollection<WorkerViewModel> AvailableWorkers
        {
            get
            {
                return
                    new ObservableCollection<WorkerViewModel>(
                        (EmployeedWorkers.Where(x => Stalls.All(t => t.RepairProcess?.AssignedWorker.GetModel().WorkerId != x.GetModel().WorkerId))));
            }
        }

        public GarageViewModel(Garage model, IWorkerRepository workerRepository, IOrderRepository orderRepository, IGarageRepository garageRepository)
        {
            this.model = model;
            this.workersRepository = workerRepository;
            this.ordersRepository = orderRepository;
            this.garageRepository = garageRepository;

            EmployeedWorkers = new ObservableCollection<WorkerViewModel>(from w in model.EmployeedWorkers select new WorkerViewModel(w));
            RaisePropertyChanged("EmployeedWorkers");
            RaisePropertyChanged("NumberOfStalls");

            Stalls = new StallViewModel[NumberOfStalls];
            for (int i=0;i<NumberOfStalls;i++)
            {
                Stalls[i] = new StallViewModel(i);
            }
            foreach (var v in model.RepairProcesses)
            {
                Stalls[v.StallNumber].RepairProcess = (new RepairProcessViewModel(v));
            }
            RaisePropertyChanged("Stalls");
            RaisePropertyChanged("AvailableWorkers");
        }


        public ICommand FireWorker => new RelayCommand<WorkerViewModel>(w =>
        {
            var window = (Application.Current.MainWindow as MetroWindow);
            var progressDialog = window.ShowProgressAsync("Please wait...", "Firing worker...", false);

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() =>
            {
                progressDialog.Result.SetIndeterminate();

                workersRepository.FireWorker(model.GarageId, w.GetModel().WorkerId);

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


        public ICommand UpgradeWorker => new RelayCommand<WorkerViewModel>(w =>
        {
            var window = (Application.Current.MainWindow as MetroWindow);
            var progressDialog = window.ShowProgressAsync("Please wait...", "Upgrading worker...", false);

            var cost = w.Efficiency * 50;

            if (cost > GlobalResources.Garage.Balance)
            {
                window.ShowMessageAsync("", "Not enough money to upgrade worker").Wait();
                return;
            }

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() =>
            {
                progressDialog.Result.SetIndeterminate();

                workersRepository.UpgradeWorker(model.GarageId, w.GetModel().WorkerId, cost);

                progressDialog.Result.CloseAsync();

            }).ContinueWith(x =>
            {
                if (x.Exception != null)
                {
                    window.ShowMessageAsync("", "Error while upgrading worker").Wait();
                }
                else
                {
                    w.Efficiency += 10;
                    EmployeedWorkers.Remove(w);
                    EmployeedWorkers.Add(w);
                }
            }, scheduler);

        }, w => model.RepairProcesses.All(x => x.AssignedWorker.WorkerId != w.GetModel().WorkerId) && w.Efficiency < 90);


        public ICommand FinishJob => new RelayCommand<RepairProcessViewModel>(rp =>
        {
            var window = (Application.Current.MainWindow as MetroWindow);
            var progressDialog = window.ShowProgressAsync("Please wait...", "Finishing job...", false);

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() =>
            {
                progressDialog.Result.SetIndeterminate();

                ordersRepository.FinishOrder(model.GarageId, rp.Order.GetModel().RepairOrderId);
                var newBalance = garageRepository.GetGarageBalance(model.GarageId);
                model.SetCashBalance(newBalance);

                progressDialog.Result.CloseAsync();

            }).ContinueWith(x =>
            {
                if (x.Exception != null)
                {
                    window.ShowMessageAsync("", "Error while finishing job").Wait();
                }
                else
                {
                    Stalls[rp.StallNumber].RepairProcess = null;
                    model.FinishOrder(rp.Order.GetModel().RepairOrderId);
                    RaisePropertyChanged("Stalls");
                    RaisePropertyChanged("AvailableWorkers");
                    RaisePropertyChanged("Balance");
                }
            }, scheduler);

        });

        public ICommand CancelJob => new RelayCommand<RepairProcessViewModel>(rp =>
        {
            var window = (Application.Current.MainWindow as MetroWindow);
            var progressDialog = window.ShowProgressAsync("Please wait...", "Canceling job...", false);

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() =>
            {
                progressDialog.Result.SetIndeterminate();

                ordersRepository.CancelOrder(model.GarageId, rp.Order.GetModel().RepairOrderId);
                var newBalance = garageRepository.GetGarageBalance(model.GarageId);
                model.SetCashBalance(newBalance);

                progressDialog.Result.CloseAsync();

            }).ContinueWith(x =>
            {
                if (x.Exception != null)
                {
                    window.ShowMessageAsync("", "Error while canceling job").Wait();
                }
                else
                {
                    Stalls[rp.StallNumber].RepairProcess = null;
                    model.CancelOrder(rp.Order.GetModel().RepairOrderId);
                    RaisePropertyChanged("Stalls");
                    RaisePropertyChanged("AvailableWorkers");
                    RaisePropertyChanged("Balance");
                }
            }, scheduler);

        });

        public ICommand UpgradeGarage => new RelayCommand(() =>
        {
            var window = (Application.Current.MainWindow as MetroWindow);
            var progressDialog = window.ShowProgressAsync("Please wait...", "Upgrading garage...", false);

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() =>
            {
                progressDialog.Result.SetIndeterminate();

                garageRepository.UpgradeGarage(model.GarageId);

                progressDialog.Result.CloseAsync();

            }).ContinueWith(x =>
            {
                if (x.Exception != null)
                {
                    window.ShowMessageAsync("", "Error while upgrading garage").Wait();
                }
                else
                {
                    NumberOfStalls += 2;
                    RaisePropertyChanged("NumberOfStalls");
                    var newStalls = new StallViewModel[NumberOfStalls];
                    for (int i = 0; i < NumberOfStalls - 2; i++)
                    {
                        newStalls[i] = Stalls[i];
                    }
                    newStalls[NumberOfStalls - 2] = new StallViewModel(NumberOfStalls - 2);
                    newStalls[NumberOfStalls - 1] = new StallViewModel(NumberOfStalls - 1);
                    Stalls = newStalls;
                }
            }, scheduler);

        }, () => model.GarageLevel < 20);


        public void AssignOrderToStall(int stallNumber, OrderViewModel order, WorkerViewModel worker)
        {
            RepairProcessViewModel repairProcess = new RepairProcessViewModel(order, worker, stallNumber);
            var window = (Application.Current.MainWindow as MetroWindow);
            var progressDialog = window.ShowProgressAsync("Please wait...", "Accpeting order...", false);
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() =>
            {
                progressDialog.Result.SetIndeterminate();

                ordersRepository.AssignOrder(model.GarageId, order.GetModel().RepairOrderId, worker.GetModel().WorkerId, stallNumber);

                progressDialog.Result.CloseAsync();

            }).ContinueWith(x =>
            {
                if (x.Exception != null)
                {
                    window.ShowMessageAsync("", "Error while accepting order").Wait();
                }
                else
                {
                    Stalls[stallNumber].RepairProcess = repairProcess;
                    model.AddRepairProcess(repairProcess.GetModel());
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
