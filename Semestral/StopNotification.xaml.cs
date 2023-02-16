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

namespace Semestral
{
    /// <summary>
    /// Interaction logic for StopNotification.xaml
    /// </summary>
    public partial class StopNotification : Window
    {
        private string _theme;
        public string Theme { get { return _theme; } set { _theme = value; } }

        public StopNotification()
        {
            InitializeComponent();
        }
        public StopNotification(MainWindow mainWindow) : this()
        {
            _theme = mainWindow.Theme;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
