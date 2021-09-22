using EasyModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxa_Dash
{
    public static class Maxa
    {
        public static void UpdateWaterSystemParameters(NotifyNewData notifier, ModbusClient modbusClient)
        {
            try
            {
                int[] data = modbusClient.ReadHoldingRegisters(Registers.OutputWaterTempReg, 1);
                notifier.waterOutTemp = data[0];
                data = modbusClient.ReadHoldingRegisters(Registers.InputWaterTempReg, 1);
                notifier.waterInTemp = data[0];
                data = modbusClient.ReadHoldingRegisters(Registers.DHWTempReg, 1);
                notifier.DHWTemp = data[0];
                data = modbusClient.ReadHoldingRegisters(Registers.StoragePlantReg, 1);
                notifier.storagePlantTemp = data[0];
                data = modbusClient.ReadHoldingRegisters(Registers.PumpAnalogOutReg, 1);
                notifier.pumpAnalogOut = data[0];
                data = modbusClient.ReadHoldingRegisters(Registers.OurdoorAirTempReg, 1);
                notifier.externalAirTemp = data[0];

                data = modbusClient.ReadHoldingRegisters(Registers.RadiantPanelMixerTempReg, 1);
                notifier.RadiantPanelMixerTemp = data[0];
                data = modbusClient.ReadHoldingRegisters(Registers.DHWPreparerRecirculationTempReg, 1);
                notifier.DHWPrePRecirculationTemp = data[0];
            }
            catch
            {
                // indicate problem in communication
            }
        }

        public static void UpdateWaterSystemParametersNRecord(NotifyNewData notifier, ModbusClient modbusClient, FileWriter fileWriter)
        {
            try
            {
                int[] data = modbusClient.ReadHoldingRegisters(Registers.OutputWaterTempReg, 1);
                notifier.waterOutTemp = data[0];
                fileWriter.dataDictionary["water output temp (°C)"] = notifier.waterOutTemp.ToString();
                data = modbusClient.ReadHoldingRegisters(Registers.InputWaterTempReg, 1);
                notifier.waterInTemp = data[0];
                fileWriter.dataDictionary["water input temp (°C)"] = notifier.waterInTemp.ToString();
                data = modbusClient.ReadHoldingRegisters(Registers.DHWTempReg, 1);
                notifier.DHWTemp = data[0];
                fileWriter.dataDictionary["DHW temp (°C)"] = notifier.DHWTemp.ToString();
                data = modbusClient.ReadHoldingRegisters(Registers.StoragePlantReg, 1);
                notifier.storagePlantTemp = data[0];
                fileWriter.dataDictionary["storage plant temp (°C)"] = notifier.storagePlantTemp.ToString();
                data = modbusClient.ReadHoldingRegisters(Registers.OurdoorAirTempReg, 1);
                notifier.externalAirTemp = data[0];
                fileWriter.dataDictionary["outdoor air temp (°C)"] = notifier.externalAirTemp.ToString();
                data = modbusClient.ReadHoldingRegisters(Registers.PumpAnalogOutReg, 1);
                notifier.pumpAnalogOut = data[0];
                fileWriter.dataDictionary["pump analog out"] = notifier.pumpAnalogOut.ToString();

                data = modbusClient.ReadHoldingRegisters(Registers.RadiantPanelMixerTempReg, 1);
                notifier.RadiantPanelMixerTemp = data[0];
                fileWriter.dataDictionary["radiant panel mixer temp (°C)"] = notifier.RadiantPanelMixerTemp.ToString();
                data = modbusClient.ReadHoldingRegisters(Registers.DHWPreparerRecirculationTempReg, 1);
                notifier.DHWPrePRecirculationTemp = data[0];
                fileWriter.dataDictionary["DHW preparer recirculation temp (°C)"] = notifier.RadiantPanelMixerTemp.ToString();
            }
            catch
            {
                // indicate problem in communication
            }
        }

        public static void UpdateRefrigirationSystemParameters(NotifyNewData notifier, ModbusClient modbusClient)
        {
            try
            {
                int[] data = modbusClient.ReadHoldingRegisters(Registers.HighPressureReg, 1);
                notifier.highPressure = data[0];
                data = modbusClient.ReadHoldingRegisters(Registers.LowPressureReg, 1);
                notifier.lowPressure = data[0];
                data = modbusClient.ReadHoldingRegisters(Registers.CompDis1TempReg, 1);
                notifier.comp1DisTemp = data[0];
                data = modbusClient.ReadHoldingRegisters(Registers.FanAnalogOutReg, 1);
                notifier.fanAnalogOut = data[0];
                data = modbusClient.ReadHoldingRegisters(Registers.EvaporationReg, 1);
                notifier.evaporationTemp = data[0];
                data = modbusClient.ReadHoldingRegisters(Registers.CondesationReg, 1);
                notifier.condendsationTemp = data[0];

                data = modbusClient.ReadHoldingRegisters(Registers.CompSuctionTempReg, 1);
                notifier.comp1SucTemp = data[0];
            }
            catch
            {
                // indicate problem in communication
            }
        }

        public static void UpdateRefrigirationSystemParametersNRecord(NotifyNewData notifier, ModbusClient modbusClient, FileWriter fileWriter)
        {
            try
            {
                int[] data = modbusClient.ReadHoldingRegisters(Registers.HighPressureReg, 1);
                notifier.highPressure = data[0];
                fileWriter.dataDictionary["high pressure (bar)"] = notifier.highPressure.ToString();
                data = modbusClient.ReadHoldingRegisters(Registers.LowPressureReg, 1);
                notifier.lowPressure = data[0];
                fileWriter.dataDictionary["low pressure (bar)"] = notifier.lowPressure.ToString();
                data = modbusClient.ReadHoldingRegisters(Registers.CompDis1TempReg, 1);
                notifier.comp1DisTemp = data[0];
                fileWriter.dataDictionary["compressor 1 discharge temp (°C)"] = notifier.comp1DisTemp.ToString();
                data = modbusClient.ReadHoldingRegisters(Registers.FanAnalogOutReg, 1);
                notifier.fanAnalogOut = data[0];
                fileWriter.dataDictionary["fan analog out"] = notifier.fanAnalogOut.ToString();
                data = modbusClient.ReadHoldingRegisters(Registers.EvaporationReg, 1);
                notifier.evaporationTemp = data[0];
                fileWriter.dataDictionary["evaporation temp (°C)"] = notifier.evaporationTemp.ToString();
                data = modbusClient.ReadHoldingRegisters(Registers.CondesationReg, 1);
                notifier.condendsationTemp = data[0];
                fileWriter.dataDictionary["condensation temp (°C)"] = notifier.condendsationTemp.ToString();

                data = modbusClient.ReadHoldingRegisters(Registers.CompSuctionTempReg, 1);
                notifier.comp1SucTemp = data[0];
                fileWriter.dataDictionary["compressor suction temp (°C)"] = notifier.comp1SucTemp.ToString();
            }
            catch
            {
                // indicate problem in communication
            }
        }

        public static void UpdateOperationMode(NotifyNewData notifier, ModbusClient modbusClient)
        {
            try
            {
                int[] data = modbusClient.ReadHoldingRegisters(Registers.MachineStateReadReg, 1);
                //notifier.generalState = (NotifyNewData.GeneralState)data[0];
            }
            catch (ArgumentException)
            {
                notifier.generalState = NotifyNewData.GeneralState.NA;
                // indicate problem in communication
            }
        }

        public static void ReadErrors(NotifyNewData notifier, ModbusClient modbusClient)
        {
            // get errors from registers 950
            try
            {
                int[] data = modbusClient.ReadHoldingRegisters(Registers.Alarm01_16Reg, 1);
                if (data[0] == 0) return;
                notifier.E001 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.HighPressure.bitMask) > 0);
                notifier.E002 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.lowPressure.bitMask) > 0);
                notifier.E003 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.DigBlockCom1.bitMask) > 0);
                //notifier.E004 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.DigBlockFan1.bitMask) > 0);
                notifier.E005 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.IceError.bitMask) > 0);
                notifier.E006 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.Flow.bitMask) > 0);
                //notifier.E007 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.LowTempDHWPreparer.bitMask) > 0);
                notifier.E008 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.LackOfLubrication.bitMask) > 0);
                notifier.E009 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.HighTempDischargeProtection.bitMask) > 0);
                notifier.E010 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.HighTempSolar.bitMask) > 0);
                //notifier.E013 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.DigBlockCom2.bitMask) > 0);
                //notifier.E014 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.DigBlockFan2.bitMask) > 0);
                //notifier.E016 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.ThermalPump1.bitMask) > 0);
            }
            catch
            {
                // indicate problem in communication
            }

            // get errors from registers 951
            try
            {
                int[] data = modbusClient.ReadHoldingRegisters(Registers.Alarm18_101Reg, 1);
                if (data[0] == 0) return;
                notifier.E018 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.HighTemp.bitMask) > 0);
                //notifier.E019 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes..bitMask) > 0);
                //notifier.E020 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes..bitMask) > 0);
                //notifier.E023 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes..bitMask) > 0);
                //notifier.E024 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes..bitMask) > 0);
                //notifier.E026 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.Thermal2PumpUse.bitMask) > 0);
                notifier.E041 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.WrongTemp.bitMask) > 0);
                notifier.E042 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.InsufficientExchangeInSanitary.bitMask) > 0); // said to be irelevant by Maxa team member Davide Mocellin
                notifier.E050 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.HighTempSanitary.bitMask) > 0);
                notifier.E101 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.ModulGiDisconnected.bitMask) > 0);
                //notifier.E102 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes..bitMask) > 0);
               
            }
            catch
            {
                // indicate problem in communication
            }

            // get errors from registers 952
            try
            {
                int[] data = modbusClient.ReadHoldingRegisters(Registers.Alarm611_652Reg, 1);
                if (data[0] == 0) return;
                notifier.E611 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.InputWaterProbe.bitMask) > 0);
                //notifier.E621 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.OutputWaterProbe.bitMask) > 0);
                notifier.E631 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.SuctionProbe.bitMask) > 0);
                notifier.E641 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.DischargeProbe.bitMask) > 0);
                notifier.E651 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.ExternProbe.bitMask) > 0);
                notifier.E661 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.Probe6.bitMask) > 0);
                notifier.E671 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.Probe7.bitMask) > 0);
                //notifier.E681 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.Probe8.bitMask) > 0); 
                notifier.E691 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.LowPressureTransducer.bitMask) > 0);
                notifier.E701 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.HighPressureTransducer.bitMask) > 0);
                notifier.E711 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.Probe11.bitMask) > 0);
                notifier.E612 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.ModulGiProbe1.bitMask) > 0);
                //notifier.E622 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.ModulGiProbe2.bitMask) > 0);
                //notifier.E632 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.ModulGiProbe3.bitMask) > 0);
                //notifier.E642 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.ModulGiProbe4.bitMask) > 0);
                notifier.E652 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.ModulGiProbe5.bitMask) > 0);
            }
            catch
            {
                // indicate problem in communication
            }

            // get errors from registers 953
            try
            {
                int[] data = modbusClient.ReadHoldingRegisters(Registers.Alarm662_712Reg, 1);
                if (data[0] == 0) return;
                notifier.E662 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.ModulGiProbe6.bitMask) > 0);
                notifier.E672 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.ModulGiProbe7.bitMask) > 0);
                //notifier.E682 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.ModulGiProbe8.bitMask) > 0);
                //notifier.E692 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.ModulGiProbe9.bitMask) > 0);
                //notifier.E702 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.ModulGiProbe10.bitMask) > 0);
                //notifier.E712 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.ModulGiProbe11.bitMask) > 0);
            }
            catch
            {
                // indicate problem in communication
            }

            // get errors from registers 954
            try
            {
                int[] data = modbusClient.ReadHoldingRegisters(Registers.Alarm801_882Reg, 1);
                if (data[0] == 0) return;
                notifier.E801 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.LinkInverter1.bitMask) > 0);
                //notifier.E802 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.LinkInverter2.bitMask) > 0);
                notifier.E851 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.HWFaultInverter1.bitMask) > 0);
                //notifier.E852 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.HWFaultInverter2.bitMask) > 0);
                notifier.E861 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.OverCurrentInverter1.bitMask) > 0);
                //notifier.E862 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.OverCurrentInverter2.bitMask) > 0);
                notifier.E871 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.HighTempInverter1.bitMask) > 0);
                //notifier.E872 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.HighTempInverter2.bitMask) > 0); 
                notifier.E881 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.BadVoltInverter1.bitMask) > 0);
                //notifier.E882 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.BadVoltInverter2.bitMask) > 0);
            }
            catch
            {
                // indicate problem in communication
            }

            // get errors from registers 955
            try
            {
                int[] data = modbusClient.ReadHoldingRegisters(Registers.Alarm891_941Reg, 1);
                if (data[0] == 0) return;
                notifier.E891 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.PhSequenceInverter1.bitMask) > 0);
                //notifier.E892 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.PhSequenceInverter2.bitMask) > 0);
                notifier.E901 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.ModelErrInverter1.bitMask) > 0);
                //notifier.E902 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.ModelErrInverter2.bitMask) > 0);
                notifier.E911 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.OLErrInverter1.bitMask) > 0);
                //notifier.E912 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.OLErrInverter2.bitMask) > 0);
                notifier.E921 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.OverCurrentPFCInverter1.bitMask) > 0);
                //notifier.E922 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.OverCurrentPFCInverter2.bitMask) > 0);
                notifier.E931 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.InternalComErrInverter1.bitMask) > 0);
                //notifier.E932 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.InternalComErrInverter2.bitMask) > 0);
                notifier.E941 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.FaultPFCInverter1.bitMask) > 0);
            }
            catch
            {
                // indicate problem in communication
            }

            // get errors from registers 956
            try
            {
                int[] data = modbusClient.ReadHoldingRegisters(Registers.Alarm942_972Reg, 1);
                if (data[0] == 0) return;
                //notifier.E942 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.FaultPFCInverter2.bitMask) > 0);
                notifier.E951 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.ProbeErrInverter1.bitMask) > 0);
                //notifier.E952 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.ProbeErrInverter2.bitMask) > 0);
                notifier.E961 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.AbnormalConditionInverter1.bitMask) > 0);
                //notifier.E962 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.AbnormalConditionInverter2.bitMask) > 0);
                notifier.E971 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.EEPROMInverter1.bitMask) > 0);
                //notifier.E972 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.EEPROMInverter2.bitMask) > 0);
            }
            catch
            {
                // indicate problem in communication
            }
        }

        public static void ReadManufacturingInfo(NotifyNewData notifier, ModbusClient modbusClient)
        {
            byte[] firmwareBytes;
            try
            {
                int[] firmwareInfo = modbusClient.ReadHoldingRegisters(Registers.FirmwareVersionReg, 4);
                int FirmwareVersion = firmwareInfo[0];
                int FirmwareRelease = firmwareInfo[1];
                firmwareBytes = BitConverter.GetBytes(firmwareInfo[2]);
                byte FirmwareSubRelease = firmwareBytes[1];
                byte FirmwareCreationDay = firmwareBytes[0];
                firmwareBytes = BitConverter.GetBytes(firmwareInfo[3]);
                byte FirmwareCreationMonth = firmwareBytes[1];
                byte FirmwareCreationYear = firmwareBytes[0];

                notifier.firmwareVersion = $"V.{FirmwareVersion}";
                notifier.firmwareRelease = $"{FirmwareRelease}.{FirmwareSubRelease}";
                notifier.firmwareCreationDate = $"{FirmwareCreationDay}/{FirmwareCreationMonth}/{FirmwareCreationYear}";
            }
            catch
            {
                notifier.firmwareVersion = "couldn't receive data";
                notifier.firmwareRelease = "couldn't receive data";
                notifier.firmwareCreationDate = "couldn't receive data";
            }
        }

    }
}
