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
    /// Interaction logic for CommentWindow.xaml
    /// </summary>
    public partial class CommentWindow : Window
    {
        private int _processId;

        public CommentWindow(int processId)
        {
            InitializeComponent();
            _processId = processId;
        }

        private void SaveComment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ProcessInf.Notes.Count < 1) ProcessInf.Notes.Add(_processId, Comment.Text);
                else ProcessInf.Notes[_processId] = Comment.Text;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Close();
            }
        }

        private void CloseComment_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
