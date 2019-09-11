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
            //ProcessThreadCollection[] threadslist;
     

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


        private static DateTime lastTime;
        private static TimeSpan lastTotalProcessorTime;
        private static DateTime curTime;
        private static TimeSpan curTotalProcessorTime;


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

                    ProcessThread[] threadslist = obj.Threads.Cast<ProcessThread>().ToArray();
                    //_threads = new List<ProcessThread>(threadslist);
                    threadsTable.ItemsSource = threadslist;//_threads;

                    var cpuUsage = new PerformanceCounter("Process", "% Processor Time", name, true);
                    var ramUsage = new PerformanceCounter("Process", "Working Set - Private", name, true);

                    double CPUUsage = 0;
                    //Process[] pp = Process.GetProcessById(name);
                    //Process p = pp[0];

                    Process p = obj;
       
                    if (lastTime == null || lastTime == new DateTime())
                    {
                        lastTime = DateTime.Now;
                        lastTotalProcessorTime = p.TotalProcessorTime;
                    }
                    else
                    {
                        curTime = DateTime.Now;
                        curTotalProcessorTime = p.TotalProcessorTime;

                        CPUUsage = (curTotalProcessorTime.TotalMilliseconds - lastTotalProcessorTime.TotalMilliseconds) / curTime.Subtract(lastTime).TotalMilliseconds / Convert.ToDouble(Environment.ProcessorCount);
                        //Console.WriteLine("{0} CPU: {1:0.0}%", name, CPUUsage * 100);

                        lastTime = curTime;
                        lastTotalProcessorTime = curTotalProcessorTime;
                    }

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

                    StartTime.Text = obj.StartTime.ToString("HH:mm:ss");
                    //CPU.Text = (pct/10000).ToString(specifier);
                    //CPU.Text = Math.Round(cpuUsage.NextValue() / Environment.ProcessorCount, 2).ToString();
                    //CPU.Text = (cpuUsage.RawValue / Environment.ProcessorCount).ToString();
                    //CPU.Text = (cpuUsage.RawValue/Environment.ProcessorCount/10000).ToString();
                    CPU.Text = (CPUUsage).ToString(specifier);
                    MemoryUsage.Text = (ramUsage.NextValue() / 1024).ToString("0.00") + " KB";
                    RunningTime.Text = obj.UserProcessorTime.ToString();
                }
            }
        }


    }
}
