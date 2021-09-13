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
            }catch
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
            }catch
            {
                // indicate problem in communication
            }
        }

        public static void UpdateOperationMode(NotifyNewData notifier, ModbusClient modbusClient)
        {
            try
            {
                int[] data = modbusClient.ReadHoldingRegisters(Registers.MachineStateReadReg, 1);
                notifier.generalState = (NotifyNewData.GeneralState)data[0];
            }
            catch
            {
                // indicate problem in communication
            }
        }

        public static void ReadErrors(NotifyNewData notifier, ModbusClient modbusClient)
        {
            try
            {
                int[] data = modbusClient.ReadHoldingRegisters(Registers.Alarm01_16Reg, 1);
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
