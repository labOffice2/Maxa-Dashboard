using System;
using System.Collections.Generic;
using System.Text;


/// <summary>
/// This class contains the addresses of the registers in the Maxa machine
/// </summary>
namespace Maxa_Dash
{
    public static class Registers
    {
        // Firmware info
        public const int FirmwareVersionReg = 1;
        public const int FirmwareReleaseReg = 2;
        public const int FirmwareSubRelease_CreationDayReg = 3;
        public const int FirmwareCreationMonth_YearReg = 4;

        // Writing Bitmask Registers
        public const int EnableWritingBitMaskReg = 7201;
        public const int ForcingBitMaskReg = 7202;

        //Machine state
        public const int MachineStateWriteReg = 7200;
        public const int MachineStateReadReg = 200;
        public const int DefrostState = 7214;
        public const int AntiLegionellaState = 7216;
        public const int EnableMaxHzReg = 1611;     // parameter L02
        public const int MaxHzModeReg = 1612;       // parameter L03

        // Temperatures
        public const int InputWaterTempReg = 400;
        public const int OutputWaterTempReg = 401;
        public const int DHWTempReg = 405;
        public const int CompSuctionTempReg = 422;
        public const int OurdoorAirTempReg = 428;
        public const int CompDis1TempReg = 433;
        public const int CompDis2TempReg = 434;
        public const int CompDis3TempReg = 435;
        public const int SolarCollectorReg = 437;
        public const int SolarTankReg = 438;
        public const int StoragePlantReg = 440;
        public const int RadiantPanelMixerTempReg = 443;
        public const int DHWPreparerRecirculationTempReg = 447;
        public const int Comp2SuctionTempReg = 20422;
        public const int CompDis1TempCircuit2Reg = 20433;
        public const int CompDis2TempCircuit2Reg = 20434;
        public const int CompDis3TempCircuit2Reg = 20435;
        public const int EvaporationReg = 253;
        public const int CondesationReg = 254;

        //Pressures
        public const int HighPressureReg = 406;
        public const int LowPressureReg = 414;
        public const int HighPressureCircuit2Reg = 20406;
        public const int LowPressureCircuit2Reg = 20414;

        /*
        // Setpoints
        public const int CoolSPReg = 1001;
        public const int HeatSPReg = 1002;
        public const int SanitarySPReg = 1003;
        public const int SecondCoolSPReg = 1004;
        public const int SecondHeatSPReg = 1005;

        */

        // Setpoints
        public const int CoolSPReg = 7203;
        public const int HeatSPReg = 7204;
        public const int SanitarySPReg = 7205;
        public const int SecondCoolSPReg = 7206;
        public const int SecondHeatSPReg = 7207;
        public const int DHWPreparerSPReg = 7208;

        public const int ActualThermoregulationSPReg = 242;
        public const int ActualRefTempForThermoregulationSPReg = 247;

        // Digital inputs
        public const int DigitalInputsReg = 10; // missing from version 07 of registor map

        // Analog outputs
        public const int FanAnalogOutReg = 7000;
        public const int FanCircuit2AnalogOutReg = 628;
        public const int PumpAnalogOutReg = 7001;

        // Alarms
        public const int Alarm01_16Reg = 950;
        public const int Alarm18_101Reg = 951;
        public const int Alarm611_652Reg = 952;
        public const int Alarm662_712Reg = 953;
        public const int Alarm801_882Reg = 954;
        public const int Alarm891_941Reg = 955;
        public const int Alarm942_972Reg = 956;

        // Bit masks for register 7201
        public const int EnableMachineStateWriting = 0b00000001;
        public const int EnableSP_N_MechineStateWriting = 0b00000011;
        public const int EnablePassagetoSecondSP = 0b00000111;
        public const int EnableRemoteAmbientCall = 0b00001011;
        public const int EnableRemoteDHWCall = 0b00010000;
        public const int EnableAntiLegionellaCycle = 0b00100000;

        // Bit masks for register 7202
        public const int ActivateSecondSP = 0b00000001;
        public const int ForceRemoteAmbientCall = 0b00000010;
        public const int ForceRemoteDHWCalll = 0b00000100;
        public const int ActivateAntiLegionellaCycle = 0b00001000;
        public const int ForcePlantVenting = 0b00100000;            // only if the machine state is standby
        public const int SanitaryDisabling = 0b01000000;
        public const int ForceDefrost = 0b10000000;

        // Bit masks for register 7214 (Defrost status)
        public const int DefrostCall = 0b0010000000000000; // bit 13
        public const int DefrostInProgress = 0b0100000000000000; // bit 14

        // Bit masks for register 7216 (anti-legionella status)
        public const int AntilegionellaInProgress = 0b00100000;
        public const int AntilegionellaFailed = 0b01000000;

        // Enum for max hz mode, register 1612
        public enum MaxHzMode
        {
            NOT_ACTIVE = 0,
            ONLY_COOLING = 1,
            ONLY_HEATING = 2,
            ONLY_DHW = 3,
            COOLING_n_DHW = 4,
            HEATING_n_DHW = 5,
            COOLING_n_HEATING = 6,
            ALWAYS_ACTIVE = 7
        }
    }
}
