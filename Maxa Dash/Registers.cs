using System;
using System.Collections.Generic;
using System.Text;

namespace Maxa_Dash
{
    public static class Registers
    {
		public const int MachineSettings = 200;
		
        // Temperatures
        public const int HighPressureReg = 406;
        public const int LowPressureReg = 414;
        public const int InputWaterTempReg = 400;
        public const int OutputWaterTempReg = 401;
        public const int DHWTempReg = 405;
        public const int OurdoorAirTempReg = 428;
        public const int CompDis1TempReg = 433;
        public const int CompDis2TempReg = 434;

        // Setpoints
        public const int CoolSPReg = 1001;
        public const int HeatSPReg = 1002;
        public const int SanitarySPReg = 1003;
        public const int SecondCoolSPReg = 1004;
        public const int SecondHeatSPReg = 1005;

        public const int DigitalInputsReg = 10;

        // Analog outputs
        public const int FanAnalogOutReg = 7000;
        public const int PumpAnalogOutReg = 7000;

        // Alarms
        public const int Alarm01_16Reg = 950;
        public const int Alarm18_101Reg = 951;
        public const int Alarm611_652Reg = 952;
        public const int Alarm662_712Reg = 953;
        public const int Alarm801_882Reg = 954;
        public const int Alarm891_941Reg = 955;
        public const int Alarm942_972Reg = 956;
    }
}
