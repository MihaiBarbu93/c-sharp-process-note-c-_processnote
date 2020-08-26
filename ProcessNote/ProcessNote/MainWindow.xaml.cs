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
            lvProcesses.MouseDoubleClick += new MouseButtonEventHandler(lvProcesses_MouseDoubleClick);


            //void lvProcesses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
            //{
            //    try
            //    {
            //        AllProcesses[lvProcesses.SelectedIndex].Refresh();

            //    }
            //    catch(Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }

            //}




        }

        public class ProcessInf
        {
            public string Name { get; set; }

            public int PID { get; set; }

            public String Memory { get; set; }

            public String CPU { get; set; }

            public String Threads { get; set; }


        }

        private void lvProcesses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListView processList = sender as ListView;
            var p = processList.SelectedItem as ProcessInf;
            var x = Process.GetProcessById(p.PID);
            PerformanceCounter myAppCpu =
                new PerformanceCounter(
                    "Process", "% Processor Time", x.ProcessName, true);

            double pct = myAppCpu.NextValue();
            x.Refresh();
            double pct1 = myAppCpu.NextValue();
            // navigate to the list view item 
            DependencyObject dep = (DependencyObject)e.OriginalSource;
            while ((dep != null) && !(dep is ListViewItem))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            ListViewItem item = (ListViewItem)dep;
            
           
            object myDataObject = item.Content;
         
            System.Diagnostics.Debug.WriteLine(myDataObject);
           
        }
    }
}
