using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using CarServiceGame.Domain.Contracts;
using CarServiceGame.Domain.Concrete;

namespace CarServiceGame.Desktop.ViewModels
{
    public class HistoryOrdersCollectionViewModel : ObservableObject
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

        public ObservableCollection<RepairProcessViewModel> Orders { get; private set; }

        public HistoryOrdersCollectionViewModel(IOrderRepository orderRepository)
        {
            this.ordersRepository = orderRepository;
        }

        public HistoryOrdersCollectionViewModel()
        {
            ordersRepository = new OrderRepository();
        }

        public void Refresh()
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
                    Orders = new ObservableCollection<RepairProcessViewModel>(from rp in x.Result select new RepairProcessViewModel(rp));
                    RaisePropertyChanged("Orders");
                }
            });
        }
    }
}
