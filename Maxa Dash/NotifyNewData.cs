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
        public event PropertyChangedEventHandler PropertyChanged;

        private void INotifyPropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // communication settings
        private string _comPortString;
        public string comPortString
        {
            get { return _comPortString; }
            set
            {
                if (_comPortString != value)
                {
                    _comPortString = value;
                    INotifyPropertyChanged(nameof(comPortString));
                }
            }
        }


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

        private float _RadiantPanelMixerTemp;
        public float RadiantPanelMixerTemp
        {
            get { return _RadiantPanelMixerTemp; }
            set
            {
                if (_RadiantPanelMixerTemp != (((float)value) / 10))
                {
                    _RadiantPanelMixerTemp = (((float)value) / 10);
                    INotifyPropertyChanged(nameof(RadiantPanelMixerTemp));
                }
            }
        }

        private float _DHWPrePRecirculationTemp;
        public float DHWPrePRecirculationTemp
        {
            get { return _DHWPrePRecirculationTemp; }
            set
            {
                if (_DHWPrePRecirculationTemp != (((float)value) / 10))
                {
                    _DHWPrePRecirculationTemp = (((float)value) / 10);
                    INotifyPropertyChanged(nameof(DHWPrePRecirculationTemp));
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

        private float _comp1SucTemp;
        public float comp1SucTemp
        {
            get { return _comp1SucTemp; }
            set
            {
                if (_comp1SucTemp != (((float)value) / 10))
                {
                    _comp1SucTemp = (((float)value) / 10);
                    INotifyPropertyChanged(nameof(comp1SucTemp));
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

        // Read-only setpoints
        
        private float _actualThermoragulationSP = 0f;
        public float actualThermoragulationSP
        {
            get { return _actualThermoragulationSP; }
            set
            {
                if (_actualThermoragulationSP != (((float)value) / 10))
                {
                    _actualThermoragulationSP = (((float)value) / 10);
                    INotifyPropertyChanged(nameof(actualThermoragulationSP));
                }
            }
        }

        private float _actualRefTemp4ThermoragulationSP = 0f;
        public float actualRefTemp4ThermoragulationSP
        {
            get { return _actualRefTemp4ThermoragulationSP; }
            set
            {
                if (_actualRefTemp4ThermoragulationSP != (((float)value) / 10))
                {
                    _actualRefTemp4ThermoragulationSP = (((float)value) / 10);
                    INotifyPropertyChanged(nameof(actualRefTemp4ThermoragulationSP));
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
        private MachinelState _generalState = MachinelState.NA;
        public MachinelState generalState
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

        /* from older version of register mapping - irrelevant
        private PlantMode _plantMode = PlantMode.NA;
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

        private PlantMode _machineMode = PlantMode.NA;
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

        private SanitaryMode _sanitaryMode = SanitaryMode.NA;
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

        private DefrostState _defrostState = DefrostState.NA;
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
        */

        // manufacturing data
        private string _firmwareVersion = "no data";
        public string firmwareVersion
        {
            get { return _firmwareVersion; }
            set
            {
                if (_firmwareVersion != value)
                {
                    _firmwareVersion = value;
                    INotifyPropertyChanged(nameof(firmwareVersion));
                }
            }
        }
        
        private string _firmwareRelease = "no data";
        public string firmwareRelease
        {
            get { return _firmwareRelease; }
            set
            {
                if (_firmwareRelease != value)
                {
                    _firmwareRelease = value;
                    INotifyPropertyChanged(nameof(firmwareRelease));
                }
            }
        }

        private string _firmwareCreationDate = "no data";
        public string firmwareCreationDate
        {
            get { return _firmwareCreationDate; }
            set
            {
                if (_firmwareCreationDate != value)
                {
                    _firmwareCreationDate = value;
                    INotifyPropertyChanged(nameof(firmwareCreationDate));
                }
            }
        }


        // error colors
        #region Error codes colors
        private Brush _E000 = Brushes.Gray;
        public Brush E000
        {
            get { return _E000; }
            set
            {
                if (_E000 != value)
                {
                    _E000 = value;
                    INotifyPropertyChanged(nameof(E000));
                }
            }
        }

        private Brush _E001 = Brushes.Gray;
        public Brush E001
        {
            get { return _E001; }
            set
            {
                if (_E001 != value)
                {
                    _E001 = value;
                    INotifyPropertyChanged(nameof(E001));
                }
            }
        }
        
        private Brush _E002 = Brushes.Gray;
        public Brush E002
        {
            get { return _E002; }
            set
            {
                if (_E002 != value)
                {
                    _E002 = value;
                    INotifyPropertyChanged(nameof(E002));
                }
            }
        }
        private Brush _E003 = Brushes.Gray;
        public Brush E003 
        {
            get { return _E003; }
            set
            {
                if (_E003 != value)
                {
                    _E003 = value;
                    INotifyPropertyChanged(nameof(E003));
                }
            }
        }

        private Brush _E005 = Brushes.Gray;
        public Brush E005
        {
            get { return _E005; }
            set
            {
                if (_E005 != value)
                {
                    _E005 = value;
                    INotifyPropertyChanged(nameof(E005));
                }
            }
        }

        private Brush _E006 = Brushes.Gray;
        public Brush E006
        {
            get { return _E006; }
            set
            {
                if (_E006 != value)
                {
                    _E006 = value;
                    INotifyPropertyChanged(nameof(E006));
                }
            }
        }

        private Brush _E008 = Brushes.Gray;
        public Brush E008
        {
            get { return _E008; }
            set
            {
                if (_E008 != value)
                {
                    _E008 = value;
                    INotifyPropertyChanged(nameof(E008));
                }
            }
        }

        private Brush _E009 = Brushes.Gray;
        public Brush E009
        {
            get { return _E009; }
            set
            {
                if (_E009 != value)
                {
                    _E009 = value;
                    INotifyPropertyChanged(nameof(E009));
                }
            }
        }

        private Brush _E010 = Brushes.Gray;
        public Brush E010
        {
            get { return _E010; }
            set
            {
                if (_E010 != value)
                {
                    _E010 = value;
                    INotifyPropertyChanged(nameof(E010));
                }
            }
        }
        private Brush _E018 = Brushes.Gray;
        public Brush E018
        {
            get { return _E018; }
            set
            {
                if (_E018 != value)
                {
                    _E018 = value;
                    INotifyPropertyChanged(nameof(E018));
                }
            }
        }
        private Brush _E041 = Brushes.Gray;
        public Brush E041
        {
            get { return _E041; }
            set
            {
                if (_E041 != value)
                {
                    _E041 = value;
                    INotifyPropertyChanged(nameof(E041));
                }
            }
        }
        private Brush _E042 = Brushes.Gray;
        public Brush E042
        {
            get { return _E042; }
            set
            {
                if (_E042 != value)
                {
                    _E042 = value;
                    INotifyPropertyChanged(nameof(E042));
                }
            }
        }
        private Brush _E050 = Brushes.Gray;
        public Brush E050
        {
            get { return _E050; }
            set
            {
                if (_E050 != value)
                {
                    _E050 = value;
                    INotifyPropertyChanged(nameof(E050));
                }
            }
        }
        private Brush _E101 = Brushes.Gray;
        public Brush E101
        {
            get { return _E101; }
            set
            {
                if (_E101 != value)
                {
                    _E101 = value;
                    INotifyPropertyChanged(nameof(E101));
                }
            }
        }
        private Brush _E611 = Brushes.Gray;
        public Brush E611
        {
            get { return _E611; }
            set
            {
                if (_E611 != value)
                {
                    _E611 = value;
                    INotifyPropertyChanged(nameof(E611));
                }
            }
        }
        private Brush _E612 = Brushes.Gray;
        public Brush E612
        {
            get { return _E612; }
            set
            {
                if (_E612 != value)
                {
                    _E612 = value;
                    INotifyPropertyChanged(nameof(E612));
                }
            }
        }
        private Brush _E631 = Brushes.Gray;
        public Brush E631
        {
            get { return _E631; }
            set
            {
                if (_E631 != value)
                {
                    _E631 = value;
                    INotifyPropertyChanged(nameof(E631));
                }
            }
        }
        private Brush _E641 = Brushes.Gray;
        public Brush E641
        {
            get { return _E641; }
            set
            {
                if (_E641 != value)
                {
                    _E641 = value;
                    INotifyPropertyChanged(nameof(E641));
                }
            }
        }
        private Brush _E651 = Brushes.Gray;
        public Brush E651
        {
            get { return _E651; }
            set
            {
                if (_E651 != value)
                {
                    _E651 = value;
                    INotifyPropertyChanged(nameof(E651));
                }
            }
        }
        private Brush _E652 = Brushes.Gray;
        public Brush E652
        {
            get { return _E652; }
            set
            {
                if (_E652 != value)
                {
                    _E652 = value;
                    INotifyPropertyChanged(nameof(E652));
                }
            }
        }
        private Brush _E661 = Brushes.Gray;
        public Brush E661
        {
            get { return _E661; }
            set
            {
                if (_E661 != value)
                {
                    _E661 = value;
                    INotifyPropertyChanged(nameof(E661));
                }
            }
        }
        private Brush _E662 = Brushes.Gray;
        public Brush E662
        {
            get { return _E662; }
            set
            {
                if (_E662 != value)
                {
                    _E662 = value;
                    INotifyPropertyChanged(nameof(E662));
                }
            }
        }
        private Brush _E671 = Brushes.Gray;
        public Brush E671
        {
            get { return _E671; }
            set
            {
                if (_E671 != value)
                {
                    _E671 = value;
                    INotifyPropertyChanged(nameof(E671));
                }
            }
        }
        private Brush _E672 = Brushes.Gray;
        public Brush E672
        {
            get { return _E672; }
            set
            {
                if (_E672 != value)
                {
                    _E672 = value;
                    INotifyPropertyChanged(nameof(E672));
                }
            }
        }
        private Brush _E691 = Brushes.Gray;
        public Brush E691
        {
            get { return _E691; }
            set
            {
                if (_E691 != value)
                {
                    _E691 = value;
                    INotifyPropertyChanged(nameof(E691));
                }
            }
        }
        private Brush _E701 = Brushes.Gray;
        public Brush E701
        {
            get { return _E701; }
            set
            {
                if (_E701 != value)
                {
                    _E701 = value;
                    INotifyPropertyChanged(nameof(E701));
                }
            }
        }
        private Brush _E711 = Brushes.Gray;
        public Brush E711
        {
            get { return _E711; }
            set
            {
                if (_E711 != value)
                {
                    _E711 = value;
                    INotifyPropertyChanged(nameof(E711));
                }
            }
        }
        private Brush _E801 = Brushes.Gray;
        public Brush E801
        {
            get { return _E801; }
            set
            {
                if (_E801 != value)
                {
                    _E801 = value;
                    INotifyPropertyChanged(nameof(E801));
                }
            }
        }
        private Brush _E851 = Brushes.Gray;
        public Brush E851
        {
            get { return _E851; }
            set
            {
                if (_E851 != value)
                {
                    _E851 = value;
                    INotifyPropertyChanged(nameof(E851));
                }
            }
        }
        private Brush _E861 = Brushes.Gray;
        public Brush E861
        {
            get { return _E861; }
            set
            {
                if (_E861 != value)
                {
                    _E861 = value;
                    INotifyPropertyChanged(nameof(E861));
                }
            }
        }
        private Brush _E871 = Brushes.Gray;
        public Brush E871
        {
            get { return _E871; }
            set
            {
                if (_E871 != value)
                {
                    _E871 = value;
                    INotifyPropertyChanged(nameof(E871));
                }
            }
        }
        private Brush _E881 = Brushes.Gray;
        public Brush E881
        {
            get { return _E881; }
            set
            {
                if (_E881 != value)
                {
                    _E881 = value;
                    INotifyPropertyChanged(nameof(E881));
                }
            }
        }
        private Brush _E891 = Brushes.Gray;
        public Brush E891
        {
            get { return _E891; }
            set
            {
                if (_E891 != value)
                {
                    _E891 = value;
                    INotifyPropertyChanged(nameof(E891));
                }
            }
        }
        private Brush _E901 = Brushes.Gray;
        public Brush E901
        {
            get { return _E901; }
            set
            {
                if (_E901 != value)
                {
                    _E901 = value;
                    INotifyPropertyChanged(nameof(E901));
                }
            }
        }
        private Brush _E911 = Brushes.Gray;
        public Brush E911
        {
            get { return _E911; }
            set
            {
                if (_E911 != value)
                {
                    _E911 = value;
                    INotifyPropertyChanged(nameof(E911));
                }
            }
        }
        private Brush _E921 = Brushes.Gray;
        public Brush E921
        {
            get { return _E921; }
            set
            {
                if (_E921 != value)
                {
                    _E921 = value;
                    INotifyPropertyChanged(nameof(E921));
                }
            }
        }
        private Brush _E931 = Brushes.Gray;
        public Brush E931
        {
            get { return _E931; }
            set
            {
                if (_E931 != value)
                {
                    _E931 = value;
                    INotifyPropertyChanged(nameof(E931));
                }
            }
        }
        private Brush _E941 = Brushes.Gray;
        public Brush E941
        {
            get { return _E941; }
            set
            {
                if (_E941 != value)
                {
                    _E941 = value;
                    INotifyPropertyChanged(nameof(E941));
                }
            }
        }
        private Brush _E951 = Brushes.Gray;
        public Brush E951
        {
            get { return _E951; }
            set
            {
                if (_E951 != value)
                {
                    _E951 = value;
                    INotifyPropertyChanged(nameof(E951));
                }
            }
        }
        private Brush _E961 = Brushes.Gray;
        public Brush E961
        {
            get { return _E961; }
            set
            {
                if (_E961 != value)
                {
                    _E961 = value;
                    INotifyPropertyChanged(nameof(E961));
                }
            }
        }
        private Brush _E971 = Brushes.Gray;
        public Brush E971
        {
            get { return _E971; }
            set
            {
                if (_E971 != value)
                {
                    _E971 = value;
                    INotifyPropertyChanged(nameof(E971));
                }
            }
        }

        
        #endregion



        public enum MachinelState
        {
            STANBY,
            COOL,
            HEAT,
            ONLY_SANITARY = 4,
            COOLnSANITARY = 5,
            HEATnSANITARY = 6,
            NA,
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
