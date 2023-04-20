using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace wp05_bikeshop.Logics
{
    internal class MyConverter : IValueConverter
    {
        // 대상에다가 표현한 값 변환 (OneWay)
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString() + " km/h";
        }
        // 대상의 값이 바뀌어도 원본 값 변환 (TwoWay)
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return int.Parse((string)value) * 3;
        }
    }
}
