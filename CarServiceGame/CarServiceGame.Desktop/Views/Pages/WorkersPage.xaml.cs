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
        private ObservableCollection<WorkerViewTest> hiredWorkers;

        private ObservableCollection<WorkerViewTest> unemployedWorkers;

        public WorkersPage()
        {
            InitializeComponent();
            ListViewTest();
        }

        private void ListViewTest()
        {
            hiredWorkers = new ObservableCollection<WorkerViewTest>();
            unemployedWorkers = new ObservableCollection<WorkerViewTest>();
            WorkerViewTest hw = new WorkerViewTest(1);
            WorkerViewTest uw = new WorkerViewTest(2);
            hiredWorkers.Add(hw);
            unemployedWorkers.Add(uw);
            hiredWorkers.Add(new WorkerViewTest(3));
            unemployedWorkers.Add(new WorkerViewTest(5));
            unemployedWorkers.Add(new WorkerViewTest(10));
            hiredListView.DataContext = hiredWorkers;
            unemployedListView.DataContext = unemployedWorkers;
        }

        private void HireButton_Click(object sender, RoutedEventArgs e)
        {
            var worker = (WorkerViewTest)((Button)sender).DataContext;
            unemployedWorkers.Remove(worker);
            hiredWorkers.Add(worker);
        }

        private void DismissButton_Click(object sender, RoutedEventArgs e)
        {
            var worker = (WorkerViewTest)((Button)sender).DataContext;
            hiredWorkers.Remove(worker);
            unemployedWorkers.Add(worker);
        }

        private void WorkerListSort(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;

            if (headerClicked == null) return;
            switch (headerClicked.Content)
            {
                case "Name":
                    hiredWorkers =  SortWorkerList(hiredWorkers.OrderBy(x => x.Name));
                    unemployedWorkers = SortWorkerList(unemployedWorkers.OrderBy(x => x.Name));
                    break;
                case "Salary":
                    hiredWorkers = SortWorkerList(hiredWorkers.OrderBy(x => x.Salary));
                    unemployedWorkers = SortWorkerList(unemployedWorkers.OrderBy(x => x.Salary));
                    break;
                case "Efficiency":
                    hiredWorkers = SortWorkerList(hiredWorkers.OrderBy(x => x.Efficiency));
                    unemployedWorkers = SortWorkerList(unemployedWorkers.OrderBy(x => x.Efficiency));
                    break;
                default:
                    break;
            }

        }

        private ObservableCollection<WorkerViewTest> SortWorkerList(IOrderedEnumerable<WorkerViewTest> sortedList)
        {
            ObservableCollection<WorkerViewTest> newWorkerList = new ObservableCollection<WorkerViewTest>();
            foreach (var v in sortedList)
            {
               newWorkerList.Add(v);
            }
            return newWorkerList;
        }
    }

}
