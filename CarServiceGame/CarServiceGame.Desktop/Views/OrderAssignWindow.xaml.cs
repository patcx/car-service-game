using CarServiceGame.Desktop.ViewModels;
using MahApps.Metro.Controls;
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

namespace CarServiceGame.Desktop.Views
{
    /// <summary>
    /// Interaction logic for OrderAssignWindow.xaml
    /// </summary>
    public partial class OrderAssignWindow : MetroWindow
    {
        private OrderViewModel order;

        public OrderAssignWindow(OrderViewModel _order)
        {
            InitializeComponent();
            order = _order;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }


        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Button_Click(1);
        }
        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Button_Click(2);
        }
        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Button_Click(3);
        }
        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            Button_Click(4);
        }

        private void Button_Click(int stallNumber)
        {
            WorkerViewModel worker = (WorkerViewModel)WorkersListView.SelectedItem;
            if (worker != null)
            {
                GlobalResources.Garage.AssignOrderToStall(stallNumber, order, worker);
                this.Close();
            } else
            {
                if (GlobalResources.Garage.EmployeedWorkers.Count == 0)
                {
                    //TODO message
                }
                else
                {
                    GlobalResources.Garage.AssignOrderToStall(stallNumber, order, GlobalResources.Garage.EmployeedWorkers.First());
                    this.Close();
                }
            }
        }
    }
}
