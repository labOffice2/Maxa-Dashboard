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
        // temperatures
        private float _waterInTemp;
        public float waterInTemp
        {
            get { return _waterInTemp; }
            set
            {
                if(_waterInTemp != ((float)value)/10)
                {
                    _waterInTemp = ((float)value) / 10;
                    INotifyPropertyChanged(nameof(waterInTemp));
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
                    INotifyPropertyChanged(nameof(waterInTempColor));
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
                    INotifyPropertyChanged(nameof(waterOutTemp));
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
                    INotifyPropertyChanged(nameof(externalAirTemp));
                }
            }
        }

        private float _DHWTemp;
        public float DHWTemp
        {
            get { return _DHWTemp; }
            set
            {
                if (_DHWTemp != (((float)value) / 10))
                {
                    _DHWTemp = (((float)value) / 10);
                    INotifyPropertyChanged(nameof(DHWTemp));
                }
            }
        }

        private float _comp1DisTemp;
        public float comp1DisTemp
        {
            get { return _comp1DisTemp; }
            set
            {
                if (_comp1DisTemp != (((float)value) / 10))
                {
                    _comp1DisTemp = (((float)value) / 10);
                    INotifyPropertyChanged(nameof(comp1DisTemp));
                }
            }
        }

        private float _comp2DisTemp;
        public float comp2DisTemp
        {
            get { return _comp2DisTemp; }
            set
            {
                if (_comp2DisTemp != (((float)value) / 10))
                {
                    _comp2DisTemp = (((float)value) / 10);
                    INotifyPropertyChanged(nameof(comp2DisTemp));
                }
            }
        }

        private float _comp1Circuit2DisTemp;
        public float comp1Circuit2DisTemp
        {
            get { return _comp1Circuit2DisTemp; }
            set
            {
                if (_comp1Circuit2DisTemp != (((float)value) / 10))
                {
                    _comp1Circuit2DisTemp = (((float)value) / 10);
                    INotifyPropertyChanged(nameof(comp1Circuit2DisTemp));
                }
            }
        }

        private float _comp2Circuit2DisTemp;
        public float comp2Circuit2DisTemp
        {
            get { return _comp2Circuit2DisTemp; }
            set
            {
                if (_comp2Circuit2DisTemp != (((float)value) / 10))
                {
                    _comp2Circuit2DisTemp = (((float)value) / 10);
                    INotifyPropertyChanged(nameof(comp2Circuit2DisTemp));
                }
            }
        }

        private float _storagePlantTemp;
        public float storagePlantTemp
        {
            get { return _storagePlantTemp; }
            set
            {
                if (_storagePlantTemp != (((float)value) / 10))
                {
                    _storagePlantTemp = (((float)value) / 10);
                    INotifyPropertyChanged(nameof(storagePlantTemp));
                }
            }
        }

        private float _evaporationTemp;
        public float evaporationTemp
        {
            get { return _evaporationTemp; }
            set
            {
                if (_evaporationTemp != (((float)value) / 10))
                {
                    _evaporationTemp = (((float)value) / 10);
                    INotifyPropertyChanged(nameof(evaporationTemp));
                }
            }
        }

        private float _condendsationTemp;
        public float condendsationTemp
        {
            get { return _condendsationTemp; }
            set
            {
                if (_condendsationTemp != (((float)value) / 10))
                {
                    _condendsationTemp = (((float)value) / 10);
                    INotifyPropertyChanged(nameof(condendsationTemp));
                }
            }
        }

        // analog outputs
        private float _fanAnalogOut;
        public float fanAnalogOut
        {
            get { return _fanAnalogOut; }
            set
            {
                if (_fanAnalogOut != (((float)value) / 10))
                {
                    _fanAnalogOut = (((float)value) / 10);
                    INotifyPropertyChanged(nameof(fanAnalogOut));
                }
            }
        }

        private float _fanAnalogOut2;
        public float fanAnalogOut2
        {
            get { return _fanAnalogOut2; }
            set
            {
                if (_fanAnalogOut2 != (((float)value) / 10))
                {
                    _fanAnalogOut2 = (((float)value) / 10);
                    INotifyPropertyChanged(nameof(fanAnalogOut2));
                }
            }
        }

        private float _pumpAnalogOut;
        public float pumpAnalogOut
        {
            get { return _pumpAnalogOut; }
            set
            {
                if (_pumpAnalogOut != (((float)value) / 10))
                {
                    _pumpAnalogOut = (((float)value) / 10);
                    INotifyPropertyChanged(nameof(pumpAnalogOut));
                }
            }
        }

        // pressures
        private float _highPressure;
        public float highPressure
        {
            get { return _highPressure; }
            set
            {
                if (_highPressure != (((float)value) / 100))
                {
                    _highPressure = ((float)value) / 100;
                    INotifyPropertyChanged(nameof(highPressure));
                }
            }
        }

        private float _lowPressure;
        public float lowPressure
        {
            get { return _lowPressure; }
            set
            {
                if (_lowPressure != (((float)value) / 100))
                {
                    _lowPressure = ((float)value) / 100;
                    INotifyPropertyChanged(nameof(lowPressure));
                }
            }
        }

        private float _highPressure2;
        public float highPressure2
        {
            get { return _highPressure2; }
            set
            {
                if (_highPressure2 != (((float)value) / 100))
                {
                    _highPressure2 = ((float)value) / 100;
                    INotifyPropertyChanged(nameof(highPressure2));
                }
            }
        }

        private float _lowPressure2;
        public float lowPressure2
        {
            get { return _lowPressure2; }
            set
            {
                if (_lowPressure2 != (((float)value) / 100))
                {
                    _lowPressure2 = ((float)value) / 100;
                    INotifyPropertyChanged(nameof(lowPressure2));
                }
            }
        }

        // machine mode
        private GeneralState _generalState;
        public GeneralState generalState
        {
            get { return _generalState; }
            set
            {
                if (_generalState != value)
                {
                    _generalState = value;
                    INotifyPropertyChanged(nameof(generalState));
                }
            }
        }

        private PlantMode _plantMode;
        public PlantMode plantMode
        {
            get { return _plantMode; }
            set
            {
                if (_plantMode != value)
                {
                    _plantMode = value;
                    INotifyPropertyChanged(nameof(plantMode));
                }
            }
        }

        private PlantMode _machineMode;
        public PlantMode machineMode
        {
            get { return _machineMode; }
            set
            {
                if (_machineMode != value)
                {
                    _machineMode = value;
                    INotifyPropertyChanged(nameof(machineMode));
                }
            }
        }

        private SanitaryMode _sanitaryMode;
        public SanitaryMode sanitaryMode
        {
            get { return _sanitaryMode; }
            set
            {
                if (_sanitaryMode != value)
                {
                    _sanitaryMode = value;
                    INotifyPropertyChanged(nameof(sanitaryMode));
                }
            }
        }

        private DefrostState _defrostState;
        public DefrostState defrostState
        {
            get { return _defrostState; }
            set
            {
                if (_defrostState != value)
                {
                    _defrostState = value;
                    INotifyPropertyChanged(nameof(defrostState));
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


        public enum GeneralState
        {
            NA,
            OFF,
            ON,
            ONLYSANITARY,
            REMOTEONOFF,
        }

        public enum PlantMode
        {
            NA,
            COOL,
            HEAT,
        }

        public enum SanitaryMode
        {
            NA,
            OFF,
            RUNNING,
        }

        public enum DefrostState
        {
            NA,
            INACTIVE,
            STARTING,
            ACTIVE,
            DRIPPING,
            FINISHING,
        }

    }
}
