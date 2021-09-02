using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Maxa_Dash
{
    public class NotifyNewData : INotifyPropertyChanged
    {
        private float _waterInTemp;
        public float waterInTemp
        {
            get { return _waterInTemp; }
            set
            {
                if(_waterInTemp != ((float)value)/10)
                {
                    _waterInTemp = ((float)value) / 10;
                    INotifyPropertyChanged("waterInTemp");
                }
            }
        }

        private Brush _waterInTempColor = Brushes.Black;
        public Brush waterInTempColor
        {
            get { return _waterInTempColor; }
            set
            {
                if (_waterInTempColor != value)
                {
                    _waterInTempColor = value;
                    INotifyPropertyChanged("waterInTempColor");
                }                
            }
        }

        private float _waterOutTemp;
        public float waterOutTemp
        {
            get { return _waterOutTemp; }
            set
            {
                if (_waterOutTemp != (((float)value) / 10) )
                {
                    _waterOutTemp = (((float)value) / 10);
                    //_waterOutTemp = value.ToString()+"°";
                    INotifyPropertyChanged("waterOutTemp");
                }
            }
        }

        private float _externalAirTemp;
        public float externalAirTemp
        {
            get { return _externalAirTemp; }
            set
            {
                if (_externalAirTemp != (((float)value) / 10))
                {
                    _externalAirTemp = (((float)value) / 10);
                    INotifyPropertyChanged("externalAirTemp");
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void INotifyPropertyChanged(string propertyName ="")
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
