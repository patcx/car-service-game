using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace CarServiceGame.Desktop.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        public LoginDetailsViewModel LoginDetails { get; set; } = new LoginDetailsViewModel();

        public string SelectedPage { get; set; } = "Pages/LoginPage.xaml";

        public ICommand Login => new RelayCommand(() =>
        {
            SelectedPage = "Pages/DashboardPage.xaml";
            RaisePropertyChanged("SelectedPage");
        });
    }
}
