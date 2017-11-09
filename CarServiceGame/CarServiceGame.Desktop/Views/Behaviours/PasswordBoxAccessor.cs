using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace CarServiceGame.Desktop.Views.Behaviours
{
    public class PasswordBoxAccessor : Behavior<PasswordBox>
    {


        public string Password
        {
            get { return (string)GetValue(PasswordProperty); }
            set { SetValue(PasswordProperty, value); }
        }

        public static readonly DependencyProperty PasswordProperty =
            DependencyProperty.Register("Password", typeof(string), typeof(PasswordBoxAccessor));


        protected override void OnAttached()
        {
            AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;
        }

        private void AssociatedObject_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            Password = AssociatedObject.Password;
        }
    }
}
