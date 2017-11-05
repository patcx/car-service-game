using CarServiceGame.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace CarServiceGame.Desktop.Views.UserControls
{
    /// <summary>
    /// Interaction logic for OrderTimer.xaml
    /// </summary>
    public partial class OrderTimer : UserControl
    {
        public static readonly DependencyProperty RepairProcessProperty =
            DependencyProperty.Register("RepairProcess", typeof(RepairProcessViewModel), typeof(OrderTimer));

        public RepairProcessViewModel RepairProcess
        {
            get { return (RepairProcessViewModel)GetValue(RepairProcessProperty); }
            set { SetValue(RepairProcessProperty, value); }
        }

        public string Title { get; set; }

        private int timer;

        private bool working;

        public OrderTimer()
        {
            InitializeComponent();
            if (RepairProcess != null)
            {
                timer = RepairProcess.Order.RequiredWork / RepairProcess.AssignedWorker.Efficiency;
                working = true;
            }
            Title = "Stall";
            TitleBlock.Text = Title;
            DispatcherTimer dtClockTime = new DispatcherTimer();
            dtClockTime.Interval = new TimeSpan(0, 0, 1); //in Hour, Minutes, Second.
            dtClockTime.Tick += dtClockTime_Tick;

            dtClockTime.Start();
        }

        private void dtClockTime_Tick(object sender, EventArgs e)
        {

            if (RepairProcess != null && !working)
            {
                CarNameLabel.Content = RepairProcess.Order.CarName;
                CarDescLabel.Content = RepairProcess.Order.Description;
                WorkerNameLabel.Content = RepairProcess.AssignedWorker.Name;
                timer = timer = RepairProcess.Order.RequiredWork / RepairProcess.AssignedWorker.Efficiency;
                clock.Visibility = Visibility.Visible;
                EndButton.Visibility = Visibility.Hidden;
                EmptyLabel.Visibility = Visibility.Hidden;
                working = true;
            }
            if (timer <= 0 && working)
            {
                clock.Visibility = Visibility.Hidden;
                EndButton.Visibility = Visibility.Visible;
                EmptyLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                clock.Content = TimeSpan.FromSeconds(--timer).ToString();
            }
        }

        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            if (RepairProcess != null)
            {
                working = true;
                timer = RepairProcess.Order.RequiredWork / RepairProcess.AssignedWorker.Efficiency;
                clock.Visibility = Visibility.Visible;
                EndButton.Visibility = Visibility.Hidden;
                EmptyLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                working = false;
                clock.Visibility = Visibility.Hidden;
                EndButton.Visibility = Visibility.Hidden;
                EmptyLabel.Visibility = Visibility.Visible;
            }
        }
    }
}
