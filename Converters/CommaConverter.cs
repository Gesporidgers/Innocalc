using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Innocalc.Converters
{
    public class CommaConverter :IValueConverter
    {
        public object Convert(object value, Type targetType,object par,CultureInfo cultureInfo)
        {
            return value.ToString().Replace('.',',');
        }

        public object ConvertBack(object value, Type type,object par, CultureInfo cultureInfo)
        {
            return System.Convert.ToSingle(value.ToString().Replace('.',','));
        }
    }
}
