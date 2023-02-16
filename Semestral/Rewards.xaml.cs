using Semestral.Reward;
using Semestral.ViewModel;
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
    /// Interaction logic for Rewards.xaml
    /// </summary>
    public partial class Rewards : Window
    {
        private string _theme;
        private MainWindow _mw;
        public string Theme { get { return _theme; } set { _theme = value; } }
        public ObservableCollection<AchievementViewModel> AchievementsVM { get; set; } = new ObservableCollection<AchievementViewModel>();

        public Rewards()
        {
            _theme = "White";
            AchievementsVM.Add(new AchievementViewModel("Beginner", "Add 2 apps to list", "x"));
            AchievementsVM.Add(new AchievementViewModel("Procrastinator", "Change procrastinate hours to 10 or more", "x"));
            //AchievementsVM[0].DateObtained = "28.01.2023";
            InitializeComponent();
        }
        public Rewards(MainWindow mainWindow) : this()
        {
            _mw = mainWindow;
            _theme = mainWindow.Theme;
            for(int i = 0; i < AchievementsVM.Count; i++)
            {
                AchievementsVM[i].Image = mainWindow.rewards1[i]._imagePath;
                AchievementsVM[i].DateObtained = mainWindow.rewards1[i]._dateObtained;
            }
        }

        

        private void Close_Window(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        private void Clear_Rewards_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < AchievementsVM.Count; i++)
            {
                AchievementsVM[i].Image = "x";
                AchievementsVM[i].DateObtained = "";
            }

            for (int i = 0; i < _mw.rewards1.Count; i++)
            {
                MainWindow.Reward r = _mw.rewards1[i];

                r._imagePath = "x";
                r._dateObtained = "";

                _mw.rewards1[i] = r;
            }
        }
    }
}
