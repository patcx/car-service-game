using CarServiceGame.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarServiceGame.Desktop.Views.Pages
{
    /// <summary>
    /// Interaction logic for OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        private ObservableCollection<OrderViewModel> orders;

        public OrdersPage()
        {
            InitializeComponent();
            orders = new ObservableCollection<OrderViewModel>();
        }


        private void OrderListSort(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            if (headerClicked == null) return;
            switch ((string)headerClicked.Content)
            {
                case "Car Name":
                    orders = new ObservableCollection<OrderViewModel>(orders.OrderBy(x => x.CarName));
                    break;
                case "Required Work":
                    orders = new ObservableCollection<OrderViewModel>(orders.OrderBy(x => x.RequiredWork));
                    break;
                default:
                    break;
            }
            ordersListView.DataContext = orders;
        }

        private void AcceptOrderButton_Click(object sender, RoutedEventArgs e)
        {
            //TODO
        }
    }
}
