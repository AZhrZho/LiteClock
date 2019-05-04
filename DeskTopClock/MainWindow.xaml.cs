using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DeskTopClock
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static Data Data;
        bool enable = true;
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();
        public MainWindow()
        {
            InitializeComponent();
            var rb = Microsoft.VisualBasic.Interaction.GetSetting("Clock", "BlurColor", "R");
            var gb = Microsoft.VisualBasic.Interaction.GetSetting("Clock", "BlurColor", "G");
            var bb = Microsoft.VisualBasic.Interaction.GetSetting("Clock", "BlurColor", "B");
            var rt = Microsoft.VisualBasic.Interaction.GetSetting("Clock", "TextColor", "R");
            var gt = Microsoft.VisualBasic.Interaction.GetSetting("Clock", "TextColor", "G");
            var bt = Microsoft.VisualBasic.Interaction.GetSetting("Clock", "TextColor", "B");
            var blur = Color.FromRgb(255, 255, 255);
            var text = Color.FromRgb(0, 0, 0);
            if (rb != string.Empty)
            {
                blur = Color.FromRgb(byte.Parse(rb), byte.Parse(gb), byte.Parse(bb));
            }
            if (rt != string.Empty)
            {
                text = Color.FromRgb(byte.Parse(rt), byte.Parse(gt), byte.Parse(bt));
            }
            Data = new Data()
            {
                BlurColor = blur,
                TextColor = text
            };
            DataContext = Data;
            Thread thread = new Thread(delegate ()
              {
                  while (enable)
                  {
                      Data.Time = DateTime.Now;
                      Thread.Sleep(500);
                  }
              });
            thread.Start();
        }

        #region
        [Flags]
        public enum ExtendedWindowStyles
        {
            // ...
            WS_EX_TOOLWINDOW = 0x00000080,
            // ...
        }

        public enum GetWindowLongFields
        {
            // ...
            GWL_EXSTYLE = (-20),
            // ...
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            int error = 0;
            IntPtr result = IntPtr.Zero;
            // Win32 SetWindowLong doesn't clear error on success
            SetLastError(0);

            if (IntPtr.Size == 4)
            {
                // use SetWindowLong
                Int32 tempResult = IntSetWindowLong(hWnd, nIndex, IntPtrToInt32(dwNewLong));
                error = Marshal.GetLastWin32Error();
                result = new IntPtr(tempResult);
            }
            else
            {
                // use SetWindowLongPtr
                result = IntSetWindowLongPtr(hWnd, nIndex, dwNewLong);
                error = Marshal.GetLastWin32Error();
            }

            if ((result == IntPtr.Zero) && (error != 0))
            {
                throw new System.ComponentModel.Win32Exception(error);
            }

            return result;
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr IntSetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern Int32 IntSetWindowLong(IntPtr hWnd, int nIndex, Int32 dwNewLong);

        private static int IntPtrToInt32(IntPtr intPtr)
        {
            return unchecked((int)intPtr.ToInt64());
        }

        [DllImport("kernel32.dll", EntryPoint = "SetLastError")]
        public static extern void SetLastError(int dwErrorCode);
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper wndHelper = new WindowInteropHelper(this);
            int exStyle = (int)GetWindowLong(wndHelper.Handle, (int)GetWindowLongFields.GWL_EXSTYLE);
            exStyle |= (int)ExtendedWindowStyles.WS_EX_TOOLWINDOW;
            SetWindowLong(wndHelper.Handle, (int)GetWindowLongFields.GWL_EXSTYLE, (IntPtr)exStyle);
        }

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Window1 window = new Window1() { Data = Data };
            window.ShowDialog();
        }
    }



    public class TimeConterter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var time = (DateTime)value;
            return time.ToString("HH:mm:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var format = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat;
            var time = (DateTime)value;
            string day = format.GetDayName(time.DayOfWeek);
            return time.ToString("yyyy-MM-dd") + "\t" + day;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new SolidColorBrush((Color)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
