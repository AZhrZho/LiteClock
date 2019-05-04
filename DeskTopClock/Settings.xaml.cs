using System;
using System.Collections.Generic;
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

namespace DeskTopClock
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Data Data { get; set; }
        Color c1;
        Color c2;
        public Window1()
        {
            InitializeComponent();
        }

        private void R_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (blur.IsChecked.Value)
            {
                Data.BlurColor = Color.FromRgb((byte)r.Value, Data.BlurColor.G, Data.BlurColor.B);
            }
            if (text.IsChecked.Value)
            {
                Data.TextColor = Color.FromRgb((byte)r.Value, Data.TextColor.G, Data.TextColor.B);
            }
        }

        private void G_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (blur.IsChecked.Value)
            {
                Data.BlurColor = Color.FromRgb(Data.BlurColor.R, (byte)g.Value, Data.BlurColor.B);
            }
            if (text.IsChecked.Value)
            {
                Data.TextColor = Color.FromRgb(Data.TextColor.R, (byte)g.Value, Data.TextColor.B);
            }
        }

        private void B_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (blur.IsChecked.Value)
            {
                Data.BlurColor = Color.FromRgb(Data.BlurColor.R, Data.BlurColor.G, (byte)b.Value);
            }
            if (text.IsChecked.Value)
            {
                Data.TextColor = Color.FromRgb(Data.TextColor.R, Data.TextColor.G, (byte)b.Value);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Data;
            c1 = Color.FromRgb(Data.TextColor.R, Data.TextColor.G, Data.TextColor.B);
            c2 = Color.FromRgb(Data.BlurColor.R, Data.BlurColor.G, Data.BlurColor.B);
            r.Value = c2.R;
            g.Value = c2.G;
            b.Value = c2.B;
        }

        private void Text_Click(object sender, RoutedEventArgs e)
        {
            r.Value = Data.TextColor.R;
            g.Value = Data.TextColor.G;
            b.Value = Data.TextColor.B;
        }

        private void Blur_Click(object sender, RoutedEventArgs e)
        {
            r.Value = Data.BlurColor.R;
            g.Value = Data.BlurColor.G;
            b.Value = Data.BlurColor.B;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Data.BlurColor = c2;
            Data.TextColor = c1;
            if (text.IsChecked.Value)
            {
                r.Value = Data.TextColor.R;
                g.Value = Data.TextColor.G;
                b.Value = Data.TextColor.B;
            }
            if (blur.IsChecked.Value)
            {
                r.Value = Data.BlurColor.R;
                g.Value = Data.BlurColor.G;
                b.Value = Data.BlurColor.B;
            }

        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.VisualBasic.Interaction.SaveSetting("Clock", "BlurColor", "R", Data.BlurColor.R.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Clock", "BlurColor", "G", Data.BlurColor.G.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Clock", "BlurColor", "B", Data.BlurColor.B.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Clock", "TextColor", "R", Data.TextColor.R.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Clock", "TextColor", "G", Data.TextColor.G.ToString());
            Microsoft.VisualBasic.Interaction.SaveSetting("Clock", "TextColor", "B", Data.TextColor.B.ToString());
            Close();
        }
    }
}
