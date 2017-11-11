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
using CarServiceGame.Desktop.Views;

namespace CarServiceGame.Desktop.ViewModels
{
    public class OrdersCollectionViewModel : ObservableObject
    {
        private IOrderRepository ordersRepository;

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

        public bool StatTabSelected
        {
            set
            {
                if (value)
                {
                    RefreshHistory();
                }
            }
        }

        public ObservableCollection<OrderViewModel> Orders { get; private set; }

        public ObservableCollection<RepairProcessViewModel> HistoryOrder { get; private set; }

        public OrdersCollectionViewModel(IOrderRepository orderRepository)
        {
            this.ordersRepository = orderRepository;
        }

        public OrdersCollectionViewModel()
        {
            ordersRepository = new OrderRepository();
        }

        public ICommand AcceptOrder => new RelayCommand<OrderViewModel>(o =>
        {
            OrderAssignWindow orderAssignWindow = new OrderAssignWindow(o);
            orderAssignWindow.ShowDialog();
        });

        public void Refresh(bool isHistory = false)
        {
            var window = (Application.Current.MainWindow as MetroWindow);
            var progressDialog = window.ShowProgressAsync("Please wait...", "Refreshing...", false);

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() =>
            {
                progressDialog.Result.SetIndeterminate();
                var orders = ordersRepository.GetAvailableOrders(0, 20);
                progressDialog.Result.CloseAsync();
                return orders;

            }).ContinueWith(x =>
            {
                if (x.Result == null)
                {
                    window.ShowMessageAsync("", "Error while refreshing data").Wait();
                }
                else
                {
                    Orders = new ObservableCollection<OrderViewModel>(from o in x.Result select new OrderViewModel(o));
                    RaisePropertyChanged("Orders");
                }
            });

        }

        public void RefreshHistory()
        {
            var window = (Application.Current.MainWindow as MetroWindow);
            var progressDialog = window.ShowProgressAsync("Please wait...", "Refreshing...", false);

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() =>
            {
                progressDialog.Result.SetIndeterminate();
                var orders = ordersRepository.GetHistoryOrders(GlobalResources.Garage.GetModel().GarageId, 0, 20);
                progressDialog.Result.CloseAsync();
                return orders;

            }).ContinueWith(x =>
            {
                if (x.Result == null)
                {
                    window.ShowMessageAsync("", "Error while refreshing data").Wait();
                }
                else
                {
                    HistoryOrder = new ObservableCollection<RepairProcessViewModel>(from rp in x.Result select new RepairProcessViewModel(rp));
                    RaisePropertyChanged("Orders");
                }
            });
        }

    }
}
