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
using System.Windows.Shapes;

namespace Semestral
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window, INotifyPropertyChanged
    {
        private string _theme = "";
        private string _timeToProcrastinate;
        public string Theme { get { return _theme; } set { _theme = value; } }
        public string TimeToProcrastinate { get { return _timeToProcrastinate; } set { _timeToProcrastinate = value; } }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public Settings()
        {
            _theme = "Beige";
            _timeToProcrastinate = "24";
            InitializeComponent();
        }

        public Settings(MainWindow mainWindow):this()
        {
            _theme = mainWindow.Theme;
            System.Diagnostics.Trace.WriteLine(SelectTheme.Items.IndexOf(_theme));
            System.Diagnostics.Trace.WriteLine(_theme);
            //SelectTheme.SelectedIndex = SelectTheme.Items.IndexOf(_theme);
            NotifyPropertyChanged("Theme");

            //https://social.msdn.microsoft.com/Forums/vstudio/en-US/b1b6107c-58e3-4141-bac2-3febd35218ce/how-to-get-list-of-comboboxitem-from-combobox-after-itemsource-?forum=wpf
            foreach(ComboBoxItem c in SelectTheme.Items)
            {
            System.Diagnostics.Trace.WriteLine(c.Content.ToString());
                if (c.Content.ToString() == _theme)
                {
                    SelectTheme.SelectedIndex = SelectTheme.Items.IndexOf(c);
                    break;
                }
            }

            // addRange() is not working :(
            //var numbers = Enumerable.Range(1, 30).ToArray();
            //ChooseTime.Items.AddRange();
            if (ChooseTime != null)
            {
                for (int i = 1; i < 25; i++)
                {
                    ChooseTime.Items.Add(i.ToString());
                }
                ChooseTime.SelectedIndex = mainWindow.TimeLeftGeneral - 1;
            }
        }

        private void OK_button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Trace.WriteLine(SelectTheme.Text);
            _theme = SelectTheme.Text;
            _timeToProcrastinate = ChooseTime.Text;
            DialogResult = true;
        }

        private void Cancel_button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
