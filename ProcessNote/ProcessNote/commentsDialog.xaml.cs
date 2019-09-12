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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class commentsDialog : Window
    {
        public commentsDialog()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

            var window2 = Application.Current.Windows
                .Cast<Window>()
                .FirstOrDefault(window => window is MainWindow) as MainWindow;

            DataGridRow dgr = window2.processTable.ItemContainerGenerator.ContainerFromItem(window2.processTable.SelectedItem) as DataGridRow;
            SimpleProcess process = dgr.Item as SimpleProcess;

            process.Comments.Add(commentsBox.Text);
            window2.commentsList.ItemsSource = " ";
            window2.commentsList.ItemsSource = process.Comments;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
