using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace ProcessNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {



        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel();
        }


        public class ViewModel
        {
            public ObservableCollection<Process> Processes { get; }
                = new ObservableCollection<Process>();

            public ViewModel()
            {
                var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(2) };
                timer.Tick += UpdateProcesses;
                timer.Start();
            }

            private void UpdateProcesses(object sender, EventArgs e)
            {
                var currentIds = Processes.Select(p => p.Id).ToList();

                foreach (var p in Process.GetProcesses())
                {
                    if (!currentIds.Remove(p.Id)) // it's a new process id
                    {
                        Processes.Add(p);
                    }
                }

                foreach (var id in currentIds) // these do not exist any more
                {
                    Processes.Remove(Processes.First(p => p.Id == id));
                }
            }
        }


    }
}
