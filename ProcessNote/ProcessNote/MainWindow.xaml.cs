using System;
using System.Collections;
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
            Process[] AllProcesses = Process.GetProcesses();
            List<ProcessInf> ProcessInfo = new List<ProcessInf>();


            foreach (Process p in AllProcesses)
            {
                PerformanceCounter myAppCpu =
                new PerformanceCounter(
                    "Process", "% Processor Time", p.ProcessName, true);
                PerformanceCounter myAppRam = new PerformanceCounter("Process", "ID Process", p.ProcessName, true);

                
                double pct = myAppCpu.NextValue();
                pct = myAppCpu.NextValue() / Environment.ProcessorCount;
                double ram = myAppRam.NextValue();


                ProcessThreadCollection t = p.Threads;
                
               
                ProcessInfo.Add(new ProcessInf() { Name = p.ProcessName, PID = p.Id, Memory = Math.Round(ram / 1024, 2).ToString() + " Mb", CPU = (Math.Round(pct, 2)).ToString() + "%", Threads = t.Count.ToString() });
                
            }

            lvProcesses.ItemsSource = ProcessInfo;

        }

        public class ProcessInf
        {
            public string Name { get; set; }

            public int PID { get; set; }

            public String Memory { get; set; }

            public String CPU { get; set; }

            public String Threads { get; set; }


        }



    }
}
