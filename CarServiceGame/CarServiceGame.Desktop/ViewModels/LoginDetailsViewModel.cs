using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace CarServiceGame.Desktop.ViewModels
{
    public class LoginDetailsViewModel : ObservableObject
    {
        private bool isLoginButtonEnabled = true;

        public bool IsLoginButtonEnabled
        {
            get { return isLoginButtonEnabled; }
            set
            {
                isLoginButtonEnabled = value;
                RaisePropertyChanged("IsLoginButtonEnabled");
            }
        }
        public string GarageName { get; set; }
        public string Password { get; set; }
    }
}
