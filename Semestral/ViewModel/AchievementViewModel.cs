using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Inspired by CVUT FEE code

namespace Semestral.ViewModel
{
    public class AchievementViewModel : ViewModelBase
    {

        public AchievementViewModel(string achName, string description, string icon)
        {
            _achName = achName;
            _description = description;
            _icon = icon;
            _dateObtained = "";
        }

        private string _achName;
        private string _description;
        private string _icon;
        private string _dateObtained;

        public string AchName
        {
            get => _achName;
            set => SetProperty(ref _achName, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string Image
        {
            get => _icon;
            set => SetProperty(ref _icon, value);
        }

        public string DateObtained
        {
            get => _dateObtained ?? string.Empty;
            set => SetProperty(ref _dateObtained, value);
        }


    }
}
