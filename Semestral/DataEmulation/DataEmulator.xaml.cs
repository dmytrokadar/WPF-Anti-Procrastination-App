using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Semestral.DataEmulation
{
    /// <summary>
    /// Interaction logic for DataEmulator.xaml
    /// </summary>
    public partial class DataEmulator : Window, INotifyPropertyChanged
    {
        private string _theme = "White";
        private int _appTime = 0;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public int AppTime { get { return _appTime; } set { _appTime = value; } }
        public string Theme { get { return _theme; } set { _theme = value; } }

        public ObservableCollection<string> AppsList { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<Dictionary<string, int>> AppsDictionary1 { get; set; } = new ObservableCollection<Dictionary<string, int>>();
        public Dictionary<string, int> AppsDictionary { get; set; } = new Dictionary<string, int>();

        public DataEmulator()
        {
            //AppsList.Add("Rdr 2");
            //AppsDictionary.Add(new Dictionary<string, int>);
            //AppsDictionary.Add("Rdr 2", 0);

            //foreach (string app in AppsList)
            //{
            //    ComboBoxItem comboBoxItem = new ComboBoxItem();
            //    comboBoxItem.Content = app;
            //    SelectApp_Combobox.Items.Add(comboBoxItem);
            //}
            InitializeComponent();
        }
        
        public DataEmulator(MainWindow mainWindow) : this()
        {
            SelectApp_Combobox.SelectedIndex = 0;
            foreach (string app in mainWindow.AllAppsList)
            {
                ComboBoxItem comboBoxItem = new ComboBoxItem();
                comboBoxItem.Content = app;
                SelectApp_Combobox.Items.Add(comboBoxItem);
                AppsDictionary.Add(app, 0);
                AppsList.Add(app);
            }
            _theme = mainWindow.Theme;

            if (InputHours != null)
            {
                for (int i = 1; i < 25; i++)
                {
                    InputHours.Items.Add(i.ToString());
                }
                InputHours.SelectedIndex = 0;
            }
        }

        private void addApp_Click(object sender, RoutedEventArgs e)
        {
            string text = AddAppName.Text;
            AppsList.Add(text);

            ComboBoxItem comboBoxItem = new ComboBoxItem();
            comboBoxItem.Content = text;
            SelectApp_Combobox.Items.Add(comboBoxItem);
            AppsDictionary.Add(text, 0);

            AddAppName.Clear();
        }

        private void AddHours(object sender, RoutedEventArgs e)
        {
            if(SelectApp_Combobox.SelectedItem != null)
            {
                string app = SelectApp_Combobox.Text;
                System.Diagnostics.Trace.WriteLine("Added hours to :" + app);
                //TODO якшо не буде працювати то https://www.techiedelight.com/increment-a-numeric-value-in-a-dictionary-in-csharp/
                AppsDictionary[app] += int.Parse(InputHours.Text);
                _appTime = AppsDictionary[SelectApp_Combobox.Text];
                NotifyPropertyChanged("AppTime");
            }
        }

        //https://stackoverflow.com/questions/2961118/combobox-selectionchanged-event-has-old-value-not-new-value
        private void SelectApp_Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = (e.AddedItems[0] as ComboBoxItem).Content as string;
            if(SelectApp_Combobox.Text != "")
                _appTime = AppsDictionary[text];
            //System.Diagnostics.Trace.WriteLine(text);
            //System.Diagnostics.Trace.WriteLine(SelectApp_Combobox.Text);
            NotifyPropertyChanged("AppTime");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
        
        private void OK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
