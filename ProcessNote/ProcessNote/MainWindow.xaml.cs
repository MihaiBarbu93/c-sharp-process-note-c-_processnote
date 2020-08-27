using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Configuration;
using System.Collections.Concurrent;


namespace ProcessNote
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;

        private string sortMethod;
        public string SortMethod
        {
            get { return sortMethod; }
            set { sortMethod = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            ProcessInf.History.Clear();
            List<ProcessInf> stats = new List<ProcessInf>();
        
            lvProcesses.ItemsSource = ProcessInf.Stats;


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            generateGridView();
        }

        private void generateGridView()
        {
            if (ConfigurationManager.AppSettings.Get("IDVisibility").Equals("false"))
            {
                Console.WriteLine(GridViewMain.Columns.Remove(IDColumn));
            }
            if (ConfigurationManager.AppSettings.Get("NameVisibility").Equals("false"))
            {
                Console.WriteLine(GridViewMain.Columns.Remove(NameColumn));
            }
            if (ConfigurationManager.AppSettings.Get("NoteVisibility").Equals("false"))
            {
                Console.WriteLine(GridViewMain.Columns.Remove(NoteColumn));
            }
            if (ConfigurationManager.AppSettings.Get("CPUVisibility").Equals("false"))
            {
                Console.WriteLine(GridViewMain.Columns.Remove(CPUColumn));
            }
            if (ConfigurationManager.AppSettings.Get("MemoryVisibility").Equals("false"))
            {
                Console.WriteLine(GridViewMain.Columns.Remove(MemoryColumn));
            }
            if (ConfigurationManager.AppSettings.Get("StartedVisibility").Equals("false"))
            {
                Console.WriteLine(GridViewMain.Columns.Remove(StartedColumn));
            }
            if (ConfigurationManager.AppSettings.Get("ThreadVisibility").Equals("false"))
            {
                Console.WriteLine(GridViewMain.Columns.Remove(ThreadColumn));
            }

        }

        private async void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            List<ProcessInf> stats = new List<ProcessInf>();

            await ProcessInf.PopulateStats();

            lvProcesses.ItemsSource = ProcessInf.Stats;

        }

        private void statsSource_Loaded(object sender, RoutedEventArgs e)
        {
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 4);
            _timer.Tick += new EventHandler(dispatcherTimer_Tick);
            _timer.Start();
        }


        void lvProcesses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var AllProcesses = new List<Process>();
            AllProcesses.AddRange(Process.GetProcesses());


            var allThreadsForSelectedProcess = AllProcesses[lvProcesses.SelectedIndex].Threads;
            var processName = AllProcesses[lvProcesses.SelectedIndex].ProcessName;
            foreach (ProcessThread t in allThreadsForSelectedProcess)
            {
                MessageBox.Show("Threads for process: *" + processName + "<b>" + "Process ID: " +  t.Id.ToString() + ", current state: " + t.ThreadState.ToString());
            }

        }



        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal) {
                this.WindowState = WindowState.Maximized;
            } 
            else if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
       
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal || this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Minimized;
            } 
            else if (this.WindowState == WindowState.Minimized)
            {
                this.WindowState = WindowState.Normal;
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void close_task_button_MouseEnter(object sender, MouseEventArgs e)
        {
            close_task_button.Background = Brushes.Gray;
        }

        private void close_task_button_MouseLeave(object sender, MouseEventArgs e)
        {
            close_task_button.Background = Brushes.DimGray;
        }

        private void Button_MouseEnter(object sender, MouseEventArgs e)
        {
            info_button.Background = Brushes.Gray;
        }

        private void Button_MouseLeave(object sender, MouseEventArgs e)
        {
            info_button.Background = Brushes.DimGray;
        }

        private void Name_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Name Header clicked.");
            if (sortMethod.Equals("NameAscending"))
            {
                sortMethod = "NameDescending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
            else
            {
                sortMethod = "NameAscending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
        }

        private void ID_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("ID Header clicked.");
            if (sortMethod.Equals("IDAscending"))
            {
                sortMethod = "IDDescending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
            else
            {
                sortMethod = "IDAscending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
        }

        private void Note_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Note Header clicked.");
            if (sortMethod.Equals("NoteAscending"))
            {
                sortMethod = "NoteDescending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
            else
            {
                sortMethod = "NoteAscending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
        }

        private void CPU_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("CPU Header clicked.");
            if (sortMethod.Equals("CPUAscending"))
            {
                sortMethod = "CPUDescending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
            else
            {
                sortMethod = "CPUAscending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
        }

        private void Memory_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Memory Header clicked.");
            if (sortMethod.Equals("MemoryAscending"))
            {
                sortMethod = "MemoryDescending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
            else
            {
                sortMethod = "MemoryAscending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
        }

        private void Started_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Started Header clicked.");
            if (sortMethod.Equals("StartedAscending"))
            {
                sortMethod = "StartedDescending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
            else
            {
                sortMethod = "StartedAscending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
        }

        private void Thread_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Thread Header clicked.");
            if (sortMethod.Equals("ThreadAscending"))
            {
                sortMethod = "ThreadDescending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
            else
            {
                sortMethod = "ThreadAscending";
                Console.WriteLine("sortMethod changed to: " + sortMethod);
            }
        }

        private void ShowThreads_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var processId = GetProcessOnMenuClick(sender).ID;
                ThreadsWindow window = new ThreadsWindow(processId);
                window.Show();
            }
            catch (Exception exy)
            {
                Console.WriteLine(exy.Message);
            }
        }

        private ProcessInf GetProcessOnMenuClick(object sender)
        {
            var menuItem = (MenuItem)sender;
            var contextMenu = (ContextMenu)menuItem.Parent;
            var item = (ListView)contextMenu.PlacementTarget;
            return (ProcessInf)item.SelectedItem;
        }

        private void AddComment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var processId = GetProcessOnMenuClick(sender).ID;
                Console.WriteLine("Process id: " + processId);
                CommentWindow window = new CommentWindow(processId);
                window.Show();
            }
            catch (Exception exy)
            {
                Console.WriteLine(exy.Message);
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            this.Topmost = this.Topmost ? false : true;
        }

       


       
    }
}
