using System;
using System.Collections.Generic;
using System.Text;

namespace Maxa_Dash
{
    public static class Registers
    {
        // Firmware info
        public const int FirmwareVersionReg = 1;
        public const int FirmwareReleaseReg = 2;
        public const int FirmwareSubRelease_CreationDayReg = 3;
        public const int FirmwareCreationMonth_YearReg = 4;

        //Machine state
        public const int MachineStateWriteReg = 7200;
        public const int MachineStateReadReg = 200;
        public const int MachineStateBitMask = 7201;

        // Temperatures
        public const int InputWaterTempReg = 400;
        public const int OutputWaterTempReg = 401;
        public const int DHWTempReg = 405;
        public const int OurdoorAirTempReg = 428;
        public const int CompDis1TempReg = 433;
        public const int CompDis2TempReg = 434;
        public const int SolarCollectorReg = 437;
        public const int SolarTankReg = 438;
        public const int StoragePlantReg = 440;
        public const int CompDis1TempCircuit2Reg = 20433;
        public const int CompDis2TempCircuit2Reg = 20434;
        public const int EvaporationReg = 234;
        public const int CondesationReg = 235;

        //Pressures
        public const int HighPressureReg = 406;
        public const int LowPressureReg = 414;
        public const int HighPressureCircuit2Reg = 20406;
        public const int LowPressureCircuit2Reg = 20414;

        // Setpoints
        public const int CoolSPReg = 1001;
        public const int HeatSPReg = 1002;
        public const int SanitarySPReg = 1003;
        public const int SecondCoolSPReg = 1004;
        public const int SecondHeatSPReg = 1005;

        // Digital inputs
        public const int DigitalInputsReg = 10;

        // Analog outputs
        public const int FanAnalogOutReg = 7000;
        public const int FanCircuit2AnalogOutReg = 627;
        public const int PumpAnalogOutReg = 7001;

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
