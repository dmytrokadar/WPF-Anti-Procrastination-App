using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;

//Inspired by CVUT FEE code

namespace Semestral.Reward
{
    internal class Achievement : ToggleButton
    {
        static Achievement()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Achievement), new FrameworkPropertyMetadata(typeof(Achievement)));
        }

        public string AchName
        {
            get => (string)GetValue(AchNameProperty); set => SetValue(AchNameProperty, value); 
        }

        public string Description 
        { 
            get => (string)GetValue(DescriptionProperty); set => SetValue(DescriptionProperty, value); 
        }

        public string DateObtained
        {
            get => (string)GetValue(DateObtainedProperty); set => SetValue(DateObtainedProperty, value);
        }

        public string Image
        {
            get => (string)GetValue(ImageProperty); set => SetValue(ImageProperty, value);
        }

        public static readonly DependencyProperty AchNameProperty = 
            DependencyProperty.Register("AchName", typeof(string), typeof(Achievement));

        public static readonly DependencyProperty DescriptionProperty = 
            DependencyProperty.Register("Description", typeof(string), typeof(Achievement));

        public static readonly DependencyProperty DateObtainedProperty = 
            DependencyProperty.Register("DateObtained", typeof(string), typeof(Achievement));

        public static readonly DependencyProperty ImageProperty = 
            DependencyProperty.Register("Image", typeof(string), typeof(Achievement));


    }
}
