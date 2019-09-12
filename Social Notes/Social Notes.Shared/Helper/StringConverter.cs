using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace Social_Notes.Helper
{
    class StringConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string result;
            switch(value as string){
                case "People": result = "ms-appx:///images/texto.png";
                    break;
                case "Video": result = "ms-appx:///images/video.png";
                    break;
                default: result = "ms-appx:///images/texto.png";
                    break;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return "Video";
        }
    }
}
