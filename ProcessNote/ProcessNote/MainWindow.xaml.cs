using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Diagnostics;
using System.Threading;


namespace ProcessNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<Process> _processess; 



    public MainWindow()
        {
            InitializeComponent();
            //DataContext = new MainWindowViewModel();

            Process[] processlist = Process.GetProcesses();

            /*foreach (Process theprocess in processlist)
            {
                Console.WriteLine("Process: {0} ID: {1}", theprocess.ProcessName, theprocess.Id);
                _processess.Add(new Process { PID = theprocess.Id, Name = theprocess.ProcessName });
            }*/


            _processess = new List<Process>(processlist);

            processTable.ItemsSource = _processess;




        }
        public class DataGridViewCellEventArgs : EventArgs
        {

        }




    private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                    Process obj = dgr.Item as Process;
                    var name = string.Empty;

                    foreach (var instance in new PerformanceCounterCategory("Process").GetInstanceNames())
                    {
                        if (instance.StartsWith(obj.ProcessName))
                        {
                            using (var processId = new PerformanceCounter("Process", "ID Process", instance, true))
                            {
                                if (obj.Id == (int)processId.NextValue())
                                {
                                    name = instance;
                                    break;
                                }
                            }
                        }
                    }

                    var cpuUsage = new PerformanceCounter("Process", "% Processor Time", name, true);
                    var ramUsage = new PerformanceCounter("Process", "Working Set - Private", name, true);


                    // Getting first initial values
                    //cpuUsage.NextValue();
                    //ramUsage.NextValue();

                    //Thread.Sleep(500);
   
                    //curTime = DateTime.Now;

                    //PerformanceCounter myAppCpu = new PerformanceCounter("ID Process", "% Processor Time", obj.Id.ToString(), true);
                    //var ramUsage = new PerformanceCounter("Process", "Working Set - Private", obj.ProcessName.ToString(), true);
                    //double pct = myAppCpu.RawValue;

                    var specifier = "P";

                    string id = obj.Id.ToString();
                    //CPU.Text = id;

                    StartTime.Text = obj.StartTime.ToUniversalTime().ToString("HH:mm:ss");
                    //CPU.Text = (pct/10000).ToString(specifier);
                    CPU.Text = Math.Round(cpuUsage.NextValue() / Environment.ProcessorCount, 2).ToString(specifier);
                    MemoryUsage.Text = (ramUsage.NextValue() / 1024).ToString();
                    RunningTime.Text = obj.UserProcessorTime.ToString();
                }
            }
        }

    }
}
