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
            Process[] AllProcesses = Process.GetProcesses();
            List<ProcessInf> ProcessInfo = new List<ProcessInf>();

            foreach(Process p in AllProcesses)
            {
                ProcessInfo.Add(new ProcessInf() { Name = p.ProcessName, PID = p.Id});
            }

            lvProcesses.ItemsSource = ProcessInfo;

        }

        public class ProcessInf
        {
            public string Name { get; set; }

            public int PID { get; set; }

        }



    }
}
