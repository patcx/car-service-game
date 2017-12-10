using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CarServiceGame.Desktop.Helpers;
using CarServiceGame.Domain.Concrete;
using CarServiceGame.Domain.Contracts;
using CarServiceGame.Domain.Mock;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace CarServiceGame.Desktop.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public IGarageRepository garageRepository;

        public GarageViewModel Garage { get; private set; }
        public LoginDetailsViewModel LoginDetails { get; set; } = new LoginDetailsViewModel();

        public string SelectedPage { get; set; } = "Pages/LoginPage.xaml";

        public MainViewModel(IGarageRepository garageRepository)
        {
            this.garageRepository = garageRepository;
        }

        public ICommand Login => new RelayCommand(() =>
        {
            LoginDetails.IsLoginButtonEnabled = false;
            var window = (Application.Current.MainWindow as MetroWindow);
            var progressDialog = window.ShowProgressAsync("Please wait...", "Logging...", false);

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() =>
            {
                progressDialog.Result.SetIndeterminate();
                var garage = garageRepository.GetGarage(LoginDetails.GarageName, LoginDetails.Password);
                return garage;

            }).ContinueWith(x =>
            {
                progressDialog.Result.CloseAsync();
                if (x.Exception != null)
                {
                    window.ShowMessageAsync("", "Connection error");
                }
                else if (x.Result == null)
                {
                    window.ShowMessageAsync("", "Wrong login data");
                }
                else
                {
                    Garage = new GarageViewModel(x.Result, NinjectBinder.Get<IWorkerRepository>(), NinjectBinder.Get<IOrderRepository>(), NinjectBinder.Get<GarageRepository>());
                    RaisePropertyChanged("Garage");
                    SelectedPage = "Pages/DashboardPage.xaml";
                    RaisePropertyChanged("SelectedPage");
                }
                LoginDetails.IsLoginButtonEnabled = true;
            }, scheduler);



        }, () => LoginDetails.IsLoginButtonEnabled);


        public ICommand CreateAccount => new RelayCommand(() =>
        {
            LoginDetails.IsLoginButtonEnabled = false;
            var window = (Application.Current.MainWindow as MetroWindow);
            var progressDialog = window.ShowProgressAsync("Please wait...", "Creating Account...", false);

            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(() =>
            {
                progressDialog.Result.SetIndeterminate();
                var garage = garageRepository.CreateGarage(LoginDetails.GarageName, LoginDetails.Password);
                return garage;

            }).ContinueWith(x =>
            {
                progressDialog.Result.CloseAsync();

                if (x.Exception != null)
                {
                    window.ShowMessageAsync("", "Connection error");
                }
                else if (x.Result == null)
                {
                    window.ShowMessageAsync("", "Login already exists");
                }
                else
                {
                    Garage = new GarageViewModel(x.Result, NinjectBinder.Get<IWorkerRepository>(), NinjectBinder.Get<IOrderRepository>(), NinjectBinder.Get<GarageRepository>());
                    RaisePropertyChanged("Garage");
                    SelectedPage = "Pages/DashboardPage.xaml";
                    RaisePropertyChanged("SelectedPage");
                }
                LoginDetails.IsLoginButtonEnabled = true;
            }, scheduler);



        }, () => LoginDetails.IsLoginButtonEnabled);
    }
}
