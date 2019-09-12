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


        private void Open_ThreadsWindow(object sender, RoutedEventArgs e)
        {
            if (processTable.SelectedItems != null && processTable.SelectedItems.Count == 1)
            {
                DataGridRow dgr = processTable.ItemContainerGenerator.ContainerFromItem(processTable.SelectedItem) as DataGridRow;
                SimpleProcess process = dgr.Item as SimpleProcess;


                var dialog = new threadsDialog(process.ThreadsList);
                dialog.ShowDialog();
            }

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
               
        private void dataGrid_selectedRow(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                refreshData();
            }
        }

        private void dataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid grid = sender as DataGrid;
                refreshData();
            }
        }

        private void refreshData()
        {
            if (processTable.SelectedItems != null && processTable.SelectedItems.Count == 1)
            {
                DataGridRow dgr = processTable.ItemContainerGenerator.ContainerFromItem(processTable.SelectedItem) as DataGridRow;
                SimpleProcess process = dgr.Item as SimpleProcess;

                StartTime.Text = process.StartTime;
                CPU.Text = process.getCPU_Usage();
                MemoryUsage.Text = process.getRamUsage();
                RunningTime.Text = process.ElapsedTime;
                commentsList.ItemsSource = process.Comments;
            }
        }

        private void AddComment_Click(object sender, RoutedEventArgs e)
        {
            if (processTable.SelectedItems != null && processTable.SelectedItems.Count == 1)
            {
                var dialog = new commentsDialog();
                dialog.ShowDialog();
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (processTable.SelectedItems != null && processTable.SelectedItems.Count == 1)
            {
                DataGridRow dgr = processTable.ItemContainerGenerator.ContainerFromItem(processTable.SelectedItem) as DataGridRow;
                SimpleProcess process = dgr.Item as SimpleProcess;
                Process.Start("http://google.com/search?q="+process.Name);
            }
            else
            {
                Process.Start("https://github.com/CodecoolGlobal/c-sharp-process-note-c-_processnote");
            }
        }
    }
}
