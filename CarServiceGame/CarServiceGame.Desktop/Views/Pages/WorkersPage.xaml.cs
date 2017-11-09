using CarServiceGame.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
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
    /// Interaction logic for WorkersPage.xaml
    /// </summary>
    public partial class WorkersPage : Page
    {

        public WorkersPage()
        {
            InitializeComponent();
        }

        private void HiredWorkerListSort(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
        }

        private void UnemployedWorkerListSort(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
        }

        private ObservableCollection<WorkerViewModel> WorkerListSort(GridViewColumnHeader headerClicked, ObservableCollection<WorkerViewModel> workers)
        {
            if (headerClicked == null) return workers;
            switch (headerClicked.Content)
            {
                case "Name":
                    return new ObservableCollection<WorkerViewModel>(workers.OrderBy(x => x.Name));
                case "Salary":
                    return new ObservableCollection<WorkerViewModel>(workers.OrderBy(x => x.Salary));
                case "Efficiency":
                    return new ObservableCollection<WorkerViewModel>(workers.OrderBy(x => x.Efficiency));
                default:
                    break;
            }
            return workers;
        }
    }

}
