using CarServiceGame.Desktop.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Globalization;

namespace CarServiceGame.Desktop.Views
{
    /// <summary>
    /// Interaction logic for OrderAssignWindow.xaml
    /// </summary>
    public partial class OrderAssignWindow : MetroWindow
    {
        private OrderViewModel order;

        public bool IsAssigned { get; private set; }

        public OrderAssignWindow(OrderViewModel _order)
        {
            InitializeComponent();
            order = _order;
            IsAssigned = false;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }


        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)e.Source;
            Button_Click((int)b.Tag);
        }

        private async void Button_Click(int stallNumber)
        {
            var window = (Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive) as MetroWindow);
            WorkerViewModel worker = (WorkerViewModel)WorkersListView.SelectedItem;
            if (worker == null)
            {
                if (GlobalResources.Garage.AvailableWorkers.Count == 0)
                {
                    window.ShowMessageAsync("There are no workers available.", "Hire new worker!");
                    this.Close();
                    return;
                }
                else
                {
                    worker = GlobalResources.Garage.AvailableWorkers.First();
                }
            }
            var result = await window.ShowMessageAsync("Work will take " + TimeSpan.FromSeconds(order.RequiredWork / worker.Efficiency), "Are you Sure?",
                MessageDialogStyle.AffirmativeAndNegative, new MetroDialogSettings { AffirmativeButtonText = "Yes", NegativeButtonText = "No" });

            if (result == MessageDialogResult.Negative)
            {
                return;
            }
            GlobalResources.Garage.AssignOrderToStall(stallNumber, order, worker);
            IsAssigned = true;
            this.Close();
        }
    }

    /// <summary>
    /// Convert unset property to false
    /// </summary>
    public class ObjectToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool returnValue = false;
            if (value == DependencyProperty.UnsetValue || value == null)
            {
                returnValue = true;
            }
            return returnValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
