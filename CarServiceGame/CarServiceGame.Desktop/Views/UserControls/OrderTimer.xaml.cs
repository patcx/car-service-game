using CarServiceGame.Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
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
            DependencyProperty.Register("RepairProcess", typeof(RepairProcessViewModel), typeof(OrderTimer), new FrameworkPropertyMetadata(new PropertyChangedCallback(OnRepairProcessChanged)));

        public RepairProcessViewModel RepairProcess
        {
            get { return (RepairProcessViewModel)GetValue(RepairProcessProperty); }
            set { SetValue(RepairProcessProperty, value); }
        }

        public string Title { get; set; }

        private bool working;

        public OrderTimer()
        {
            InitializeComponent();
            if (RepairProcess != null)
            {
                working = true;
            }
            Title = "Stall";
            TitleBlock.Text = Title;
            DispatcherTimer dtClockTime = new DispatcherTimer();
            dtClockTime.Interval = new TimeSpan(0, 0, 1); //in Hour, Minutes, Second.
            dtClockTime.Tick += dtClockTime_Tick;

            dtClockTime.Start();
        }

        private static void OnRepairProcessChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var assosiatedObject = d as OrderTimer;
            if (assosiatedObject.RepairProcess != null && !assosiatedObject.RepairProcess.Completed)
            {
                assosiatedObject.Clock.Visibility = Visibility.Visible;
                assosiatedObject.EndButton.Visibility = Visibility.Hidden;
                assosiatedObject.EmptyLabel.Visibility = Visibility.Hidden;
                assosiatedObject.working = true;
            }
        }

        private void dtClockTime_Tick(object sender, EventArgs e)
        {
            if (RepairProcess?.SecondsToEnd <= 0 && working)
            {
                RepairProcess.Completed = true;
                Clock.Visibility = Visibility.Hidden;
                EndButton.Visibility = Visibility.Visible;
                EmptyLabel.Visibility = Visibility.Hidden;
            }
            else if (RepairProcess != null)
            {
                Clock.Content = TimeSpan.FromSeconds(--RepairProcess.SecondsToEnd).ToString();
            }
        }

        private void EndButton_Click(object sender, RoutedEventArgs e)
        {
            if (RepairProcess != null && !RepairProcess.Completed)
            {
                working = true;
                Clock.Visibility = Visibility.Visible;
                EndButton.Visibility = Visibility.Hidden;
                EmptyLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                working = false;
                Clock.Visibility = Visibility.Hidden;
                EndButton.Visibility = Visibility.Hidden;
                EmptyLabel.Visibility = Visibility.Visible;
            }
        }
    }
}
