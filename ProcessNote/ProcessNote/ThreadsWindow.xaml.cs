using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace ProcessNote
{
    /// <summary>
    /// Interaction logic for ThreadsWindow.xaml
    /// </summary>
    public partial class ThreadsWindow : Window
    {
        ThreadModel model;

        public ThreadsWindow(int processId)
        {
            InitializeComponent();
            model = new ThreadModel();
            model.GetAllProcessThreads(processId);
            threadSource.ItemsSource = model.GetThreadsList();
        }
    }
}
