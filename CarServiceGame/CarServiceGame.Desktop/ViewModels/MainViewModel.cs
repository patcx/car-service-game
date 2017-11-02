﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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

        public MainViewModel()
        {
            garageRepository = new MockRepository();
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
                progressDialog.Result.CloseAsync();
                return garage;

            }).ContinueWith(x =>
            {
                if (x.Result == null)
                {
                    window.ShowMessageAsync("", "Login error").Wait();
                }
                else
                {
                    Garage = new GarageViewModel(x.Result);
                    RaisePropertyChanged("Garage");
                    SelectedPage = "Pages/DashboardPage.xaml";
                    RaisePropertyChanged("SelectedPage");
                }
                LoginDetails.IsLoginButtonEnabled = true;
            });
           

           
        }, () => LoginDetails.IsLoginButtonEnabled);
    }
}
