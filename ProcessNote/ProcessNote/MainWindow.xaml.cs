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
        private List<SimpleProcess> _processess;
        
        public MainWindow()
        {
            InitializeComponent();
            getProcesses();
        }


        private void Open_Click(object sender, RoutedEventArgs e)
        {

            DataGridRow dgr = processTable.ItemContainerGenerator.ContainerFromItem(processTable.SelectedItem) as DataGridRow;
            SimpleProcess process = dgr.Item as SimpleProcess;

            
            var dialog = new threadsDialog(process.ThreadsList);
            dialog.ShowDialog();

        }

        public class DataGridViewCellEventArgs : EventArgs
        {

        }

        private void getProcesses()
        {
            Process[] processlist = Process.GetProcesses();
            _processess = new List<SimpleProcess>();

            foreach (Process theprocess in processlist)
            {
                _processess.Add(new SimpleProcess(theprocess));
            }
            processTable.ItemsSource = _processess;
        }
               
        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                {
                    DataGridRow dgr = grid.ItemContainerGenerator.ContainerFromItem(grid.SelectedItem) as DataGridRow;
                    SimpleProcess process = dgr.Item as SimpleProcess;

                    //threadsTable.ItemsSource = process.ThreadsList;

                    StartTime.Text = process.StartTime;
                    CPU.Text = process.getCPU_Usage();
                    MemoryUsage.Text = process.getRamUsage();
                    RunningTime.Text = process.ElapsedTime;
                }
            }
        }


    }
}
