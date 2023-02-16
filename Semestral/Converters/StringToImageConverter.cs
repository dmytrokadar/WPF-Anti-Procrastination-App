using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

//Inspired by CVUT FEE code

namespace Semestral.Converters
{
    internal class StringToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var image = new Image();
            // image source 
            // https://icon-library.com/images/all-done-icon/all-done-icon-7.jpg
            // https://icon-library.com/images/x-icon-vector/x-icon-vector-4.jpg
            var fullFilePath = String.Format("Images/{0}.jpg", value);

            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(fullFilePath, UriKind.Relative);
            bitmap.EndInit();


            return bitmap;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}
