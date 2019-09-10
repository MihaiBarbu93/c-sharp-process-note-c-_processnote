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

            _processess = new List<Process>();

            processTable.ItemsSource = _processess;

            _processess.Add(new Process { PID = 1, Name = "Test1" });
            _processess.Add(new Process { PID = 2, Name = "Test2" });
            _processess.Add(new Process { PID = 3, Name = "Test3" });
            _processess.Add(new Process { PID = 4, Name = "Test4" });
            _processess.Add(new Process { PID = 5, Name = "new" });
        }

    }
}
