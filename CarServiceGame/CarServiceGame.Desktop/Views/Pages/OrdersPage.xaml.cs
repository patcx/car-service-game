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
        const string carNameColumnName = "Car Name";
        const string requiredWorkColumnName = "Required Work";

        public string CarNameColumnName
        {
            get
            {
                return carNameColumnName;
            }
        }

        public string RequiredWorkColumnName
        {
            get
            {
                return requiredWorkColumnName;
            }
        }

        private ObservableCollection<OrderViewTest> orders;

        public OrdersPage()
        {
            InitializeComponent();
            ListViewTest();
        }

        private void ListViewTest()
        {
            orders = new ObservableCollection<OrderViewTest>();
            orders.Add(new OrderViewTest(6));
            orders.Add(new OrderViewTest(1));
            orders.Add(new OrderViewTest(11));
            ordersListView.DataContext = orders;
        }

        private void OrderListSort(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            if (headerClicked == null) return;
            switch (headerClicked.Content)
            {
                case carNameColumnName:
                    orders = new ObservableCollection<OrderViewTest>(orders.OrderBy(x => x.CarName));
                    break;
                case requiredWorkColumnName:
                    orders = new ObservableCollection<OrderViewTest>(orders.OrderBy(x => x.RequiredWork));
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
