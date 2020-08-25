using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            Process[] AllProcesses = Process.GetProcesses();
            List<ProcessInf> ProcessInfo = new List<ProcessInf>();

            foreach (Process p in AllProcesses)
            {
                PerformanceCounter myAppCpu =
                new PerformanceCounter(
                    "Process", "% Processor Time", p.ProcessName, true);
                PerformanceCounter myAppRam = new PerformanceCounter("Process", "Private Bytes", p.ProcessName, true);


                double pct = myAppCpu.NextValue();
   

                pct = myAppCpu.NextValue();
                double ram = Math.Round(myAppRam.NextValue() / Environment.ProcessorCount, 2);
                Console.WriteLine("OUTLOOK'S CPU % = " + pct);
                ProcessInfo.Add(new ProcessInf() { Name = p.ProcessName, PID = p.Id, CPU = string.Format("{0:0.00}", pct) + " %", Memory = Math.Round(ram /1024 / 1024,  2).ToString() + " MB/s" });
            }

            lvProcesses.ItemsSource = ProcessInfo;

        }

        public class ProcessInf
        {
            public string Name { get; set; }

            public int PID { get; set; }

            public string CPU { get; set; }

            public string  Memory { get; set; }

        }

        private void lvProcesses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("Selected Process");
        }
    }
}
