using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddApp.xaml
    /// </summary>
    public partial class AddApp : Window
    {
        private MainWindow _mainWindow;
        private string _theme = "";
        public string Theme { get { return _theme; } set { _theme = value; } }
        public ObservableCollection<string> LocalAppsList { get; set; } = new ObservableCollection<string>();

        public AddApp()
        {
            InitializeComponent();
        }
        public AddApp(MainWindow mainWindow) : this()
        {
            _theme = mainWindow.Theme;
            _mainWindow = mainWindow;
            foreach (string app in mainWindow.AllAppsList)
            {
                ChooseApp.Items.Add(app);
            }

            //for (int i = 0; i < 25; i++)
            //{
            //    SetHours.Items.Add(i);
            //}
            ChooseApp.SelectedIndex = 0;
            //SetHours.SelectedIndex = 0;
        }

        private void OK_button_(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Cancel_button_(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Add_app_Click(object sender, RoutedEventArgs e)
        {
            //string text = AppName_t.Text;
            string text = ChooseApp.Text;
            LocalAppsList.Add(text);
            DialogResult = true;
        }
    }
}
