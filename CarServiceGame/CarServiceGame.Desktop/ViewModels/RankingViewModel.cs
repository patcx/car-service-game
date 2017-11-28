using CarServiceGame.Domain.Concrete;
using CarServiceGame.Domain.Contracts;
using GalaSoft.MvvmLight;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Collections.ObjectModel;   

namespace CarServiceGame.Desktop.ViewModels
{
    public class RankingViewModel : ObservableObject
    {
        private IGarageRepository garageRepository;

        public ObservableCollection<GarageRankingViewModel> Garages { get; private set; }

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

        public RankingViewModel()
        {
            garageRepository = new GarageRepository();
        }

        public RankingViewModel(IGarageRepository garageRepository)
        {
            this.garageRepository = garageRepository;
        }

        public void Refresh()
        {
            var window = (Application.Current.MainWindow as MetroWindow);
            var progressDialog = window.ShowProgressAsync("Please wait...", "Refreshing...", false);

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() =>
            {
                progressDialog.Result.SetIndeterminate();
                var orders = garageRepository.GetGaragesRanking(10);
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
                    Garages = new ObservableCollection<GarageRankingViewModel>(from g in x.Result select new GarageRankingViewModel(g));
                    RaisePropertyChanged("Garages");
                }
            });
        }
    }
}
