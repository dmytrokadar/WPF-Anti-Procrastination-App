using Semestral.DataEmulation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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

namespace Semestral
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public class App : INotifyPropertyChanged
        {
            private int _time;
            private string _name;

            public string Name { get { return _name; } set { _name = value; NotifyPropertyChanged("Name"); } }

            public int Time { get { return _time; } set { _time = value; NotifyPropertyChanged("Time"); } }

            public App(string name, int time)
            {
                _name = name;
                _time = time;
            }

            public event PropertyChangedEventHandler? PropertyChanged;
            private void NotifyPropertyChanged(string property)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(property));
                }
            }
        }

        public struct Reward
        {
            public string _imagePath;
            public string _dateObtained;
        }

        private bool _flag = false;
        private string _theme;
        private int _timeLeftGeneral;
        private int _timeLeft;

        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public string Theme { get { return _theme; } set { _theme = value; } }
        public int TimeLeft { get { return _timeLeft; } set { _timeLeft = value; NotifyPropertyChanged("TimeLeft"); } }
        public int TimeLeftGeneral { get { return _timeLeftGeneral; } set { _timeLeftGeneral = value;} }

        // TODO поміняти стрінг на спеціальний тип для апсу
        //public ObservableCollection<string> AppsList { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> AllAppsList { get; set; } = new ObservableCollection<string>();
        //public ObservableCollection<int> AppsTime { get; set; } = new ObservableCollection<int>();
        //public Dictionary<string, int> AppsMap { get; set; } = new Dictionary<string, int>();
        public ObservableCollection<App> Apps { get; set; } = new ObservableCollection<App>();
        public List<Reward> rewards1 { get; set; } = new List<Reward>();
        public MainWindow()
        {
            //_theme = "Beige";
            //_timeLeft = 24;
            readOptions();
            countLeftTime();
            var theme = (Color)ColorConverter.ConvertFromString(_theme);
            Background = new SolidColorBrush(theme);

            //this.AppsListBox.FormattingEnabled = true;
            //this.AppsListBox.HorizontalScrollbar = true;

            //AppsMap.Add("Rdr 2", 0);
            //AllAppsList.Add("Rdr 2");
            //AppsTime.Add(0);
            //Apps.Add(new App("Rdr 2", 0));
            InitializeComponent();
        }

        private void countLeftTime()
        {
            _timeLeft = _timeLeftGeneral;
            foreach(App app in Apps)
            {
                _timeLeft -= app.Time;
            }

            if (_timeLeft <= 0)
            {
                _timeLeft = 0;
                _flag = true;
                var stopNotification = new StopNotification(this);
                if (stopNotification.ShowDialog() == true)
                {

                }
                else
                {

                }
                System.Diagnostics.Trace.WriteLine("Stop it!!!");
            }
            NotifyPropertyChanged("TimeLeft");
        }

        private int readOptions()
        {
            string[] lines = File.ReadAllLines("saves.txt");

            //System.Diagnostics.Trace.WriteLine(lines[2]);
            _theme = lines[0];
            _timeLeftGeneral = int.Parse(lines[1]);
            for(int i = 2; i < lines.Length - 2; i++)
            {
                string[] parts = lines[i].Split(";%;");
                Apps.Add(new App(parts[0], int.Parse(parts[1])));
                AllAppsList.Add(parts[0]);
            }
            for (int i = lines.Length - 2; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(";%;");
                Reward r1 = new Reward();
                r1._imagePath = parts[0];
                if (r1._imagePath.Equals("x"))
                {
                    r1._dateObtained = "";
                }
                else
                {
                    r1._dateObtained = parts[1];
                }
                rewards1.Add(r1);
            }

            return 0;
        }

        private int saveOptions()
        {
            List<string> lines = new List<string>();
            lines.Add(_theme);
            lines.Add(_timeLeftGeneral.ToString()); ;
            //string[] lines = { _theme , _timeLeft.ToString()};
            foreach (App app in Apps)
            {
                if (_flag == true)
                    lines.Add(app.Name + ";%;" + "0");
                else
                    lines.Add(app.Name + ";%;" + app.Time.ToString());
                
            }

            foreach (Reward r in rewards1)
            {
                if(r._imagePath == "x")
                {
                    lines.Add(r._imagePath + ";%;-1");
                } else
                {
                    lines.Add(r._imagePath + ";%;" + r._dateObtained);
                }  
            }
            

            File.WriteAllLines("saves.txt", lines);
            return 0;
        }

        private void addApp(object sender, RoutedEventArgs e)
        {
            var addApp = new AddApp(this);
            if (addApp.ShowDialog() == true)
            {
                foreach (String app in addApp.LocalAppsList.ToList<String>())
                {
                    App app1 = Apps.Where(a => a.Name == app).FirstOrDefault();
                    if (app1 == null)
                    {
                        Apps.Add(new App(app, 0));
                    }


                }

                if(rewards1[0]._imagePath == "x")
                {
                    if (Apps.Count >= 2)
                    {
                        Reward r = rewards1[0];
                        r._imagePath = "done";
                        r._dateObtained = DateTime.Now.ToString("MM/dd/yyyy HH:mm");

                        rewards1[0] = r;
                    }
                }
            }
            else
            {

            }
        }
        private void settings(object sender, RoutedEventArgs e)
        {
            var settings = new Settings(this);
            Color theme;

            if (settings.ShowDialog() == true)
            {
                System.Diagnostics.Trace.WriteLine(settings.Theme);
                _theme = settings.Theme;
                _timeLeftGeneral = int.Parse(settings.TimeToProcrastinate);
                countLeftTime();
                
                //TODO переробити так шоб робило і без цього
                if (_theme != "" && _theme != null)
                {
                    theme = (Color)ColorConverter.ConvertFromString(_theme);
                }

                NotifyPropertyChanged("TimeLeft");
                System.Diagnostics.Trace.WriteLine(theme.ToString());
                Background = new SolidColorBrush(theme);
                System.Diagnostics.Trace.WriteLine(TimeLeft);
                System.Diagnostics.Trace.WriteLine(TimeLeft_.ToString());
                saveOptions();

                if (rewards1[1]._imagePath == "x")
                {
                    if (_timeLeftGeneral >= 10)
                    {
                        Reward r = rewards1[1];
                        r._imagePath = "done";
                        r._dateObtained = DateTime.Now.ToString("MM/dd/yyyy HH:mm");

                        rewards1[1] = r;
                    }
                }
            }
            else
            {

            }
        }

        private void rewards(object sender, RoutedEventArgs e)
        {
            var rewards = new Rewards(this);

            if (rewards.ShowDialog() == true)
            {
                
            }
            else
            {

            }
        }

        private void summonDataEmulator(object sender, RoutedEventArgs e)
        {
            var dataEmulator = new DataEmulator(this);

            if (dataEmulator.ShowDialog() == true)
            {
                //TODO обновляти тайм лефт, підрахувати суму файлів
                foreach(String app in dataEmulator.AppsList)
                {
                    System.Diagnostics.Trace.WriteLine("===================================");
                    if (!AllAppsList.Contains(app))
                        AllAppsList.Add(app);

                    App app1 = Apps.Where(a => a.Name == app).FirstOrDefault();

                    if (app1 != null)
                    {
                        System.Diagnostics.Trace.WriteLine(Apps[Apps.IndexOf(app1)].Time);
                        Apps[Apps.IndexOf(app1)].Time += dataEmulator.AppsDictionary[app];

                        System.Diagnostics.Trace.WriteLine(Apps[Apps.IndexOf(app1)].Time);
                        System.Diagnostics.Trace.WriteLine("===================================");
                    }
                    //else
                    //{
                    //    AppsMap.Add(app, dataEmulator.AppsDictionary[app]);
                    //}
                    //AppsTime.Clear();
                    NotifyPropertyChanged("Apps");
                    NotifyPropertyChanged("Time");

                }


                countLeftTime();

                //TODO зробити норм дікшонарі
                //foreach (KeyValuePair<string, int> entry in dataEmulator.AppsDictionary)
                //{
                //    if (AppsMap.ContainsValue())
                //    {
                //        AppsMap[entry.Value] += entry.Key;
                //    }
                //}
            }
            else
            {

            }
        }

        private void removeApp(object sender, RoutedEventArgs e)
        {
            
            System.Diagnostics.Trace.WriteLine(Apps_listBox.SelectedItem);
            //Apps_listBox.Items.Remove(Apps_listBox.SelectedItem);
            Apps.Remove((App)Apps_listBox.SelectedItem);
            countLeftTime();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            saveOptions();
        }
    }
}
