using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DeskTopClock
{
    public class Data :INotifyPropertyChanged
    {
        private DateTime _time;
        private Color _textColor;
        private Color _blurColor;

        public DateTime Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Time"));
            }
        }

        public Color TextColor
        {
            get
            {
                return _textColor;
            }
            set
            {
                _textColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TextColor"));
            }
        }

        public Color BlurColor
        {
            get
            {
                return _blurColor;
            }
            set
            {
                _blurColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("BlurColor"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
