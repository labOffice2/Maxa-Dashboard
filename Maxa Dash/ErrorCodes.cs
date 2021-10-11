using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// This class is based on the Err class (see botton of class)
/// This class defines an Err object for each possible/relevnat error code.
/// The Err object contains:
///                         1) The address of the register of this error
///                         2) The bit representing the error
///                         3) Bit mask to get the specific error (calculated)
///                         4) A description of the error (currently not used)
///                         5) The error code
/// </summary>
namespace Maxa_Dash
{
    public static class ErrorCodes
    {
        // Register 950 Errors
        public static Err HighPressure                  = new Err(950, 0, "High Pressure", 01);
        public static Err lowPressure                   = new Err(950, 1, "Low Pressure", 02);
        public static Err DigBlockCom1                  = new Err(950, 2, "Digital block compressor 1", 03);
        public static Err DigBlockFan1                  = new Err(950, 3, "Digital block fan1", 04);
        public static Err IceError                      = new Err(950, 4, "Ice Error", 05);
        public static Err Flow                          = new Err(950, 5, "Flow", 06);
        public static Err LowTempDHWPreparer            = new Err(950, 6, "Low temperature DHW preparer", 07);
        public static Err LackOfLubrication             = new Err(950, 6, "Forced compressors OFF for lack of lubrication", 08);
        public static Err HighTempDischargeProtection   = new Err(950, 7, "High temperature discharge protection", 09);
        public static Err HighTempSolar                 = new Err(950, 8, "High temperature solar", 10);
        //public static Err DigBlockCom2                  = new Err(950, 12, "Digital block compressor 2", 013); // not needed for our models
        //public static Err DigBlockFan2                  = new Err(950, 13, "Digital block fan 2", 14);        // not needed for our models
        public static Err ThermalPump1                  = new Err(950, 15, "Thermal pump 1", 16);

        // Register 951 Errors
        public static Err HighTemp                      = new Err(951, 1, "High temperature", 18);
        //public static Err HighTempDischargeProtectionCp2= new Err(951, 2, "High temperature discharge protection Cp2", 19); // not needed for our models
        public static Err InvertedPressures             = new Err(951, 3, "Inverted pressures", 20);
        //public static Err ThermalCompressor3            = new Err(951, 6, "Thermal compressor 3", 23);                      // not needed for our models
        //public static Err ThermalFan3                   = new Err(951, 7, "Thermal fan 3", 24);                             // not needed for our models
        //public static Err Thermal2PumpUse               = new Err(951, 9, "Thermal 2 pump use", 26);                        // not needed for our models
        public static Err WrongTemp                     = new Err(951, 11, "Incongruous temperatures", 41);
        //public static Err InsufficientExchangeInSanitary= new Err(951, 12, "Incongruous temperatures", 42);
        public static Err HighTempSanitary              = new Err(951, 13, "High temperature sanitary", 50);
        //public static Err ModulGiDisconnected           = new Err(951, 14, "Modul Gi disconnected", 101);                   // not needed for our models

        // Register 952 Errors
        public static Err InputWaterProbe               = new Err(952, 0, "Input water probe", 611);
        public static Err OutputWaterProbe              = new Err(952, 1, "Output water probe", 621);
        public static Err SuctionProbe                  = new Err(952, 2, "Sunction probe", 631);
        public static Err DischargeProbe                = new Err(952, 3, "Discharge probe", 641);
        public static Err ExternProbe                   = new Err(952, 4, "Extern probe", 651);
        public static Err Probe6                        = new Err(952, 5, "Probe 6", 661);
        public static Err Probe7                        = new Err(952, 6, "Probe 7", 671);
        public static Err Probe8                        = new Err(952, 7, "Probe 8", 681);
        public static Err LowPressureTransducer          = new Err(952, 8, "Low pressure transducer", 691);
        public static Err HighPressureTransducer         = new Err(952, 9, "High pressure transducer", 701);
        public static Err Probe11                       = new Err(952, 10, "Probe 11", 711);
        //public static Err ModulGiProbe1                 = new Err(952, 11, "modul Gi - Probe 1", 612); // not needed for our models
        //public static Err ModulGiProbe2                 = new Err(952, 12, "modul Gi - Probe 2", 622); // not needed for our models
        //public static Err ModulGiProbe3                 = new Err(952, 13, "modul Gi - Probe 3", 632); // not needed for our models
        //public static Err ModulGiProbe4                 = new Err(952, 14, "modul Gi - Probe 4", 642); // not needed for our models
        //public static Err ModulGiProbe5                 = new Err(952, 15, "modul Gi - Probe 5", 652); // not needed for our models

        // Register 953 Errors
        //public static Err ModulGiProbe6                 = new Err(953, 0, "modul Gi - Probe 6", 662); // not needed for our models
        //public static Err ModulGiProbe7                 = new Err(953, 1, "modul Gi - Probe 7", 672); // not needed for our models
        //public static Err ModulGiProbe8                 = new Err(953, 2, "modul Gi - Probe 8", 682); // not needed for our models
        //public static Err ModulGiProbe9                 = new Err(953, 3, "modul Gi - Probe 9", 692); // not needed for our models
        //public static Err ModulGiProbe10                = new Err(953, 4, "modul Gi - Probe 10", 702); // not needed for our models
        //public static Err ModulGiProbe11                = new Err(953, 5, "modul Gi - Probe 11", 712); // not needed for our models

        // Register 954 Errors
        public static Err LinkInverter1                 = new Err(954, 1, "Link inverter 1", 801);
        //public static Err LinkInverter2                 = new Err(954, 2, "Link inverter 2", 802);               // not needed for our models
        public static Err HWFaultInverter1              = new Err(954, 4, "Hardware fault inverter 1", 851);
        //public static Err HWFaultInverter2              = new Err(954, 5, "Hardware fault inverter 2", 852);     // not needed for our models
        public static Err OverCurrentInverter1          = new Err(954, 7, "Overcurrent inverter 1", 861);
        //public static Err OverCurrentInverter2          = new Err(954, 8, "Overcurrent inverter 2", 862);        // not needed for our models
        public static Err HighTempInverter1             = new Err(954, 10, "High temperature inverter 1", 871);
        //public static Err HighTempInverter2             = new Err(954, 11, "High temperature inverter 2", 872);  // not needed for our models
        public static Err BadVoltInverter1              = new Err(954, 13, "Bad voltage inverter 1", 881);
        //public static Err BadVoltInverter2              = new Err(954, 14, "Bad voltage inverter 2", 882);       // not needed for our models

        // Register 955 Errors
        public static Err PhSequenceInverter1           = new Err(955, 0, "Phase sequence inverter 1", 891);
        //public static Err PhSequenceInverter2           = new Err(955, 1, "Phase sequence inverter 2", 892);// not needed for our models
        public static Err ModelErrInverter1             = new Err(955, 3, "Model error inverter 1", 901);
        //public static Err ModelErrInverter2             = new Err(955, 4, "Model error inverter 2", 902);// not needed for our models
        public static Err OLErrInverter1                = new Err(955, 6, "Overload error inverter 1", 911);
        //public static Err OLErrInverter2                = new Err(955, 7, "Overload error inverter 2", 912);// not needed for our models
        public static Err OverCurrentPFCInverter1       = new Err(955, 9, "Overcurrent PFC inverter 1", 921);
        //public static Err OverCurrentPFCInverter2       = new Err(955, 10, "Overcurrent PFC inverter 2", 922);// not needed for our models
        public static Err InternalComErrInverter1       = new Err(955, 12, "Internal communication error inverter 1", 931);
        //public static Err InternalComErrInverter2       = new Err(955, 13, "Internal communication error inverter 2", 932);// not needed for our models
        public static Err FaultPFCInverter1             = new Err(955, 15, "Fault PFC inverter 1", 941);

        // Register 956 Errors
        //public static Err FaultPFCInverter2             = new Err(956, 0, "Fault PFC inverter 2", 942);     // not needed for our models
        public static Err ProbeErrInverter1             = new Err(956, 2, "Probe error inverter 1", 951);
        //public static Err ProbeErrInverter2             = new Err(956, 3, "Probe error inverter 2", 952);   // not needed for our models
        public static Err AbnormalConditionInverter1    = new Err(956, 5, "Abnormal condition inverter 1", 961);
        //public static Err AbnormalConditionInverter2    = new Err(956, 6, "Abnormal condition inverter 2", 962);// not needed for our models
        public static Err EEPROMInverter1               = new Err(956, 8, "EEPROM inverter 1", 971);
        //public static Err EEPROMInverter2               = new Err(956, 9, "EEPROM inverter 2", 972);        // not needed for our models
        public static Err AntiLegionellaDone            = new Err(956, 12, "Anti-legionella done", 60);
        public static Err AntiLegionellaFailure         = new Err(956, 13, "Anti-legionella failure", 61);  

        /*
        private static Err[] AllErrs =
        {
            HighPressure,lowPressure,DigBlockCom1,DigBlockCom2 DigBlockFan1,DigBlockFan2 IceError,Flow,LowTempDHWPreparer,LackOfLubrication,HighTempDischargeProtection,HighTempSolar,ThermalPump1,
            //HighTemp,Thermal2PumpUse,WrongTemp,InsufficientExchangeInSanitary,HighTempSanitary,ModulGiDisconnected,
            HighTemp,Thermal2PumpUseWrongTemp,HighTempSanitary,//ModulGiDisconnected,
            InputWaterProbe,OutputWaterProbe,SuctionProbe,DischargeProbe,ExternProbe,Probe6,Probe7,Probe8,LowPressureTransducer,HighPressureTransducer,Probe11,//ModulGiProbe1,ModulGiProbe2,ModulGiProbe3,ModulGiProbe4,ModulGiProbe5,
            //ModulGiProbe6,ModulGiProbe7,ModulGiProbe8,ModulGiProbe9,ModulGiProbe10,ModulGiProbe11,
            LinkInverter1,LinkInverter2HWFaultInverter1,HWFaultInverter2, OverCurrentInverter1,OverCurrentInverter2,HighTempInverter1,HighTempInverter2,BadVoltInverter1,BadVoltInverter2,
            PhSequenceInverter1,PhSequenceInverter2,ModelErrInverter1,ModelErrInverter2,OLErrInverter1,OLErrInverter2,OverCurrentPFCInverter1,OverCurrentPFCInverter2,InternalComErrInverter1,InternalComErrInverter2,FaultPFCInverter1,
            FaultPFCInverter2,ProbeErrInverter1,ProbeErrInverter2,AbnormalConditionInverter1,AbnormalConditionInverter2,EEPROMInverter1,EEPROMInverter2,AntiLegionellaDone,AntiLegionellaFailure,
        };
        */
        private static Err[] Register950Errs =
        {
            HighPressure,lowPressure,DigBlockCom1,/*DigBlockCom2*/ DigBlockFan1,/*DigBlockFan2*/ IceError,Flow,LowTempDHWPreparer,LackOfLubrication,HighTempDischargeProtection,HighTempSolar,ThermalPump1
        };

        private static Err[] Register951Errs =
        {
            //HighTemp,Thermal2PumpUse,WrongTemp,InsufficientExchangeInSanitary,HighTempSanitary,ModulGiDisconnected,
            HighTemp,InvertedPressures,WrongTemp,HighTempSanitary,
        };

        private static Err[] Register952Errs =
        {
            InputWaterProbe,OutputWaterProbe,SuctionProbe,DischargeProbe,ExternProbe,Probe6,Probe7,Probe8,LowPressureTransducer,HighPressureTransducer,Probe11,//ModulGiProbe1,ModulGiProbe2,ModulGiProbe3,ModulGiProbe4,ModulGiProbe5,
        };

        private static Err[] Register953Errs =
        {
            //ModulGiProbe6,ModulGiProbe7,ModulGiProbe8,ModulGiProbe9,ModulGiProbe10,ModulGiProbe11,
        };

        private static Err[] Register954Errs =
        {
            LinkInverter1,/*LinkInverter2,*/HWFaultInverter1,/*HWFaultInverter2,*/ OverCurrentInverter1,/*OverCurrentInverter2,*/ HighTempInverter1,/*HighTempInverter2,*/ BadVoltInverter1,//BadVoltInverter2,
        };

        private static Err[] Register955Errs =
        {
            PhSequenceInverter1,/*PhSequenceInverter2,*/ ModelErrInverter1,/*ModelErrInverter2,*/ OLErrInverter1,/*OLErrInverter2,*/ OverCurrentPFCInverter1,/*OverCurrentPFCInverter2,*/ InternalComErrInverter1,/*InternalComErrInverter2,*/FaultPFCInverter1,
        };

        private static Err[] Register956Errs =
        {
            /*FaultPFCInverter2,*/ ProbeErrInverter1,/*ProbeErrInverter2,*/ AbnormalConditionInverter1,/*AbnormalConditionInverter2,*/ EEPROMInverter1,/*EEPROMInverter2,*/ AntiLegionellaDone,AntiLegionellaFailure,
        };

        public static Err[] GetErrors(int register, int value)
        {
            if (value == 0)
                return null;
            List<Err> errorList = new();
            switch (register)
            {
                case 950:
                    foreach(Err err in Register950Errs)
                    {
                        int e = value & err.bitMask;
                        if (e > 0)
                        {
                            errorList.Add(err);
                        }
                    }
                    break;

                case 951:
                    foreach (Err err in Register951Errs)
                    {
                        int e = value & err.bitMask;
                        if (e > 0)
                        {
                            errorList.Add(err);
                        }
                    }
                    break;

                case 952:
                    foreach (Err err in Register952Errs)
                    {
                        int e = value & err.bitMask;
                        if (e > 0)
                        {
                            errorList.Add(err);
                        }
                    }
                    break;

                case 953:
                    foreach (Err err in Register953Errs)
                    {
                        int e = value & err.bitMask;
                        if (e > 0)
                        {
                            errorList.Add(err);
                        }
                    }
                    break;

                case 954:
                    foreach (Err err in Register954Errs)
                    {
                        int e = value & err.bitMask;
                        if (e > 0)
                        {
                            errorList.Add(err);
                        }
                    }
                    break;

                case 955:
                    foreach (Err err in Register955Errs)
                    {
                        int e = value & err.bitMask;
                        if (e > 0)
                        {
                            errorList.Add(err);
                        }
                    }
                    break;

                case 956:
                    foreach (Err err in Register956Errs)
                    {
                        int e = value & err.bitMask;
                        if (e > 0)
                        {
                            errorList.Add(err);
                        }
                    }
                    break;

                default:
                    break;
            }

            return errorList.ToArray();
        }

        public class Err
        {
            public int registerAddress;
            public int bitLocation;
            public int bitMask;
            public string description;
            public int number;
            public Err(int registerAddress, int bitLocation, string description, int number)
            {
                this.registerAddress = registerAddress;
                this.bitLocation = bitLocation;
                this.bitMask = (int)Math.Pow(2, bitLocation);
                this.description = description;
                this.number = number;
            }
        }
    }
}
