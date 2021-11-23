using EasyModbus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// This static class is responsible for the communication with the Maxa machine - reading and writing.
/// It updates the UI variables in NotifyNewData, and the data to be writen in the csv file if relevant using the fileWriter.dataDictionary.
/// </summary>
namespace Maxa_Dash
{
    public static class Maxa
    {
        /// <summary>
        /// Reads water system related values from Maxa ,updates gui and adds values to fileWriter dictionary
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="modbusClient">Modbus client object currently connected to a Maxa unit</param>
        /// <returns>No return value</returns>
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
                notifier.waterTempDelta = (decimal)(notifier.waterOutTemp - notifier.waterInTemp);
            }
            catch
            {
                // indicate problem in communication
                throw;
            }
        }

        /// <summary>
        /// Reads water system related values from Maxa ,updates gui and adds values to fileWriter dictionary
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="modbusClient">Modbus client object currently connected to a Maxa unit</param>
        /// <param name="fileWriter">FileWriter object to add values to its dictionary for later to be written to csv file</param>
        /// <returns>No return value</returns>
        public static void UpdateWaterSystemParameters(NotifyNewData notifier, ModbusClient modbusClient, FileWriter fileWriter)
        {
            try
            {
                UpdateWaterSystemParameters(notifier, modbusClient);

                fileWriter.dataDictionary["water output temp (°C)"] = notifier.waterOutTemp.ToString();
                fileWriter.dataDictionary["water input temp (°C)"] = notifier.waterInTemp.ToString();
                fileWriter.dataDictionary["Water ∆T (°C)"] = notifier.waterTempDelta.ToString();
                fileWriter.dataDictionary["DHW temp (°C)"] = notifier.DHWTemp.ToString();
                fileWriter.dataDictionary["storage plant temp (°C)"] = notifier.storagePlantTemp.ToString();
                fileWriter.dataDictionary["outdoor air temp (°C)"] = notifier.externalAirTemp.ToString();
                fileWriter.dataDictionary["pump analog out"] = notifier.pumpAnalogOut.ToString();
                fileWriter.dataDictionary["radiant panel mixer temp (°C)"] = notifier.RadiantPanelMixerTemp.ToString();
                fileWriter.dataDictionary["DHW preparer recirculation temp (°C)"] = notifier.RadiantPanelMixerTemp.ToString();
                
            }
            catch
            {
                // indicate problem in communication
                throw;
            }
        }

        /// <summary>
        /// Reads refrigiration system related values from Maxa and updates gui
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="modbusClient">Modbus client object currently connected to a Maxa unit</param>
        /// <returns>No return value</returns>
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
                data = modbusClient.ReadHoldingRegisters(Registers.DefrostState, 1);
                notifier.defrostState = DataConverter.GetDefrostState(data[0]);
            }
            catch
            {
                // indicate problem in communication
                throw;
            }
        }

        /// <summary>
        /// Reads refrigiration system related values from Maxa ,updates gui and adds values to fileWriter dictionary
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="modbusClient">Modbus client object currently connected to a Maxa unit</param>
        /// <param name="fileWriter">FileWriter object to add values to its dictionary for later to be written to csv file</param>
        /// <returns>No return value</returns>
        public static void UpdateRefrigirationSystemParameters(NotifyNewData notifier, ModbusClient modbusClient, FileWriter fileWriter)
        {
            try
            {
                UpdateRefrigirationSystemParameters(notifier, modbusClient);

                fileWriter.dataDictionary["high pressure (bar)"] = notifier.highPressure.ToString();
                fileWriter.dataDictionary["low pressure (bar)"] = notifier.lowPressure.ToString();
                fileWriter.dataDictionary["compressor 1 discharge temp (°C)"] = notifier.comp1DisTemp.ToString();
                fileWriter.dataDictionary["fan analog out"] = notifier.fanAnalogOut.ToString();
                fileWriter.dataDictionary["evaporation temp (°C)"] = notifier.evaporationTemp.ToString();
                fileWriter.dataDictionary["condensation temp (°C)"] = notifier.condendsationTemp.ToString();
                fileWriter.dataDictionary["compressor suction temp (°C)"] = notifier.comp1SucTemp.ToString();
            }
            catch
            {
                // indicate problem in communication
                throw;
            }
        }

        /// <summary>
        /// Reads (read only) setpoint values from Maxa and update gui
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="modbusClient">Modbus client object currently connected to a Maxa unit</param>
        /// <returns>No return value</returns>
        public static void UpdateReadOnlySetpoints(NotifyNewData notifier, ModbusClient modbusClient)
        {
            try
            {
                int[] data = modbusClient.ReadHoldingRegisters(Registers.ActualThermoregulationSPReg, 1);
                notifier.actualThermoragulationSP = data[0];
                data = modbusClient.ReadHoldingRegisters(Registers.ActualRefTempForThermoregulationSPReg, 1);
                notifier.actualRefTemp4ThermoragulationSP = data[0];
            }
            catch
            {
                // indicate problem in communication
                throw;
            }
        }

        /// <summary>
        /// Reads (read only) setpoint values from Maxa and update gui
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="modbusClient">Modbus client object currently connected to a Maxa unit</param>
        /// <param name="fileWriter">FileWriter object to add values to its dictionary for later to be written to csv file</param>
        /// <returns>No return value</returns>
        public static void UpdateReadOnlySetpoints(NotifyNewData notifier, ModbusClient modbusClient, FileWriter fileWriter)
        {
            try
            {
                UpdateReadOnlySetpoints(notifier, modbusClient);
                //int[] data = modbusClient.ReadHoldingRegisters(Registers.ActualThermoregulationSPReg, 1);
                //notifier.actualThermoragulationSP = data[0];
                fileWriter.dataDictionary["actual Thermoragulation SP (°C)"] = notifier.actualThermoragulationSP.ToString();
                //data = modbusClient.ReadHoldingRegisters(Registers.ActualRefTempForThermoregulationSPReg, 1);
                //notifier.actualRefTemp4ThermoragulationSP = data[0];
                fileWriter.dataDictionary["actual Ref Temperature for Thermoragulation"] = notifier.actualRefTemp4ThermoragulationSP.ToString();
            }
            catch
            {
                // indicate problem in communication
                throw;
            }
        }

        /// <summary>
        /// Reads operation mode from Maxa and updates gui
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="modbusClient">Modbus client object currently connected to a Maxa unit</param>
        /// <returns>No return value</returns>
        public static void UpdateOperationMode(NotifyNewData notifier, ModbusClient modbusClient)
        {
            try
            {
                int[] data = modbusClient.ReadHoldingRegisters(Registers.MachineStateReadReg, 1);
                notifier.generalState = DataConverter.GetMachineState(data[0]);
                data = modbusClient.ReadHoldingRegisters(Registers.ForcingBitMaskReg, 1);
                notifier.plantVentingState = DataConverter.GetPlantVentingState(data[0]);
                notifier.ambientCallState = DataConverter.GetAmbientCallState(data[0]);

                int[] dataL02 = modbusClient.ReadHoldingRegisters(Registers.EnableMaxHzReg, 1);
                int[] dataL03 = modbusClient.ReadHoldingRegisters(Registers.MaxHzModeReg, 1);
                notifier.maxHzState = DataConverter.GetMaxHzState(dataL02[0],dataL03[0]);
            }
            catch
            {
                notifier.generalState = NotifyNewData.MachinelState.NA;
                // indicate problem in communication
                throw;
            }
        }

        /// <summary>
        /// Reads operation mode from Maxa and updates gui
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="modbusClient">Modbus client object currently connected to a Maxa unit</param>
        /// <param name="fileWriter">FileWriter object to add values to its dictionary for later to be written to csv file</param>
        /// <returns>No return value</returns>
        public static void UpdateOperationMode(NotifyNewData notifier, ModbusClient modbusClient, FileWriter fileWriter)
        {
            try
            {
                UpdateOperationMode(notifier, modbusClient);
                //int[] data = modbusClient.ReadHoldingRegisters(Registers.MachineStateReadReg, 1);
                //notifier.generalState = DataConverter.GetMachineState(data[0]);
                //data = modbusClient.ReadHoldingRegisters(Registers.ForcingBitMaskReg, 1);
                //notifier.plantVentingState = DataConverter.GetPlantVentingState(data[0]);
                fileWriter.dataDictionary["Machine state"] = notifier.generalState.ToString();
                fileWriter.dataDictionary["Defrost state"] = notifier.defrostState.ToString();
                fileWriter.dataDictionary["Anti-legionella state"] = notifier.antiLegionellaState.ToString();
                fileWriter.dataDictionary["Plant venting state"] = notifier.plantVentingState.ToString();
                fileWriter.dataDictionary["Ambient call state"] = notifier.ambientCallState.ToString();
                fileWriter.dataDictionary["Max Hz state"] = notifier.maxHzState.ToString();
            }
            catch
            {
                notifier.generalState = NotifyNewData.MachinelState.NA;
                fileWriter.dataDictionary["Machine state"] = notifier.generalState.ToString();
                // indicate problem in communication
                throw;
            }
        }

        /// <summary>
        /// Reads anti-legionella status and updates gui
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="modbusClient">Modbus client object currently connected to a Maxa unit</param>
        /// <returns>No return value</returns>
        public static void UpdateAntiLegionellaState(NotifyNewData notifier, ModbusClient modbusClient)
        {
            try
            {
                int[] data = modbusClient.ReadHoldingRegisters(Registers.AntiLegionellaState, 1);
                int[] alarmData = modbusClient.ReadHoldingRegisters(Registers.Alarm942_972Reg, 1);
                notifier.antiLegionellaState = DataConverter.GetAntiLegionellaState(data[0], alarmData[0]);

            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Reads all 7 error registers, and update active errors to gui 
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="modbusClient">Modbus client object currently connected to a Maxa unit</param>
        /// <returns>Returns an array of register's addresses that contain active errors</returns>
        public static int[] ReadErrors(NotifyNewData notifier, ModbusClient modbusClient, bool isResetErrors)
        {
            // get errors from registers 950
            List<int> ErrorContainingRegistorsList = new List<int>();
            try
            {
                int[] data = modbusClient.ReadHoldingRegisters(Registers.Alarm01_16Reg, 7);
                if (data[0] != 0 || isResetErrors)
                {
                    notifier.E001 = DataConverter.GetAlarmColor((data[0] & ErrorCodes.HighPressure.bitMask) > 0);
                    notifier.E002 = DataConverter.GetAlarmColor((data[0] & ErrorCodes.lowPressure.bitMask) > 0);
                    notifier.E003 = DataConverter.GetAlarmColor((data[0] & ErrorCodes.DigBlockCom1.bitMask) > 0);
                    notifier.E004 = DataConverter.GetAlarmColor((data[0] & ErrorCodes.DigBlockFan1.bitMask) > 0);
                    notifier.E005 = DataConverter.GetAlarmColor((data[0] & ErrorCodes.IceError.bitMask) > 0);
                    notifier.E006 = DataConverter.GetAlarmColor((data[0] & ErrorCodes.Flow.bitMask) > 0);
                    notifier.E007 = DataConverter.GetAlarmColor((data[0] & ErrorCodes.LowTempDHWPreparer.bitMask) > 0);
                    notifier.E008 = DataConverter.GetAlarmColor((data[0] & ErrorCodes.LackOfLubrication.bitMask) > 0);
                    notifier.E009 = DataConverter.GetAlarmColor((data[0] & ErrorCodes.HighTempDischargeProtection.bitMask) > 0);
                    notifier.E010 = DataConverter.GetAlarmColor((data[0] & ErrorCodes.HighTempSolar.bitMask) > 0);
                    //notifier.E013 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.DigBlockCom2.bitMask) > 0);
                    //notifier.E014 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.DigBlockFan2.bitMask) > 0);
                    notifier.E016 = DataConverter.GetAlarmColor((data[0] & ErrorCodes.ThermalPump1.bitMask) > 0);
                    ErrorContainingRegistorsList.Add(Registers.Alarm01_16Reg);
                }

            // get errors from registers 951
            
                //int[] data = modbusClient.ReadHoldingRegisters(Registers.Alarm18_101Reg, 1);
                if (data[1] != 0 || isResetErrors)
                {
                    notifier.E018 = DataConverter.GetAlarmColor((data[1] & ErrorCodes.HighTemp.bitMask) > 0);
                    //notifier.E019 = GuiDataConverter.GetAlarmColor((data[1] & ErrorCodes..bitMask) > 0);
                    notifier.E020 = DataConverter.GetAlarmColor((data[1] & ErrorCodes.InvertedPressures.bitMask) > 0);
                    //notifier.E023 = GuiDataConverter.GetAlarmColor((data[1] & ErrorCodes..bitMask) > 0);
                    //notifier.E024 = GuiDataConverter.GetAlarmColor((data[1] & ErrorCodes..bitMask) > 0);
                    //notifier.E026 = GuiDataConverter.GetAlarmColor((data[1] & ErrorCodes.Thermal2PumpUse.bitMask) > 0);
                    notifier.E041 = DataConverter.GetAlarmColor((data[1] & ErrorCodes.WrongTemp.bitMask) > 0);
                    //notifier.E042 = GuiDataConverter.GetAlarmColor((data[1] & ErrorCodes.InsufficientExchangeInSanitary.bitMask) > 0); // said to be irrelevant by Maxa team member Davide Mocellin
                    notifier.E050 = DataConverter.GetAlarmColor((data[1] & ErrorCodes.HighTempSanitary.bitMask) > 0);
                    //notifier.E101 = GuiDataConverter.GetAlarmColor((data[1] & ErrorCodes.ModulGiDisconnected.bitMask) > 0);
                    //notifier.E102 = GuiDataConverter.GetAlarmColor((data[1] & ErrorCodes..bitMask) > 0);
                    ErrorContainingRegistorsList.Add(Registers.Alarm18_101Reg);
                }


            

            // get errors from registers 952
            
                //int[] data = modbusClient.ReadHoldingRegisters(Registers.Alarm611_652Reg, 1);
                if (data[2] != 0 || isResetErrors)
                {
                    notifier.E611 = DataConverter.GetAlarmColor((data[2] & ErrorCodes.InputWaterProbe.bitMask) > 0);
                    //notifier.E621 = GuiDataConverter.GetAlarmColor((data[2] & ErrorCodes.OutputWaterProbe.bitMask) > 0);
                    notifier.E631 = DataConverter.GetAlarmColor((data[2] & ErrorCodes.SuctionProbe.bitMask) > 0);
                    notifier.E641 = DataConverter.GetAlarmColor((data[2] & ErrorCodes.DischargeProbe.bitMask) > 0);
                    notifier.E651 = DataConverter.GetAlarmColor((data[2] & ErrorCodes.ExternProbe.bitMask) > 0);
                    notifier.E661 = DataConverter.GetAlarmColor((data[2] & ErrorCodes.Probe6.bitMask) > 0);
                    notifier.E671 = DataConverter.GetAlarmColor((data[2] & ErrorCodes.Probe7.bitMask) > 0);
                    //notifier.E681 = GuiDataConverter.GetAlarmColor((data[2] & ErrorCodes.Probe8.bitMask) > 0); 
                    notifier.E691 = DataConverter.GetAlarmColor((data[2] & ErrorCodes.LowPressureTransducer.bitMask) > 0);
                    notifier.E701 = DataConverter.GetAlarmColor((data[2] & ErrorCodes.HighPressureTransducer.bitMask) > 0);
                    notifier.E711 = DataConverter.GetAlarmColor((data[2] & ErrorCodes.Probe11.bitMask) > 0);
                    //notifier.E612 = GuiDataConverter.GetAlarmColor((data[2] & ErrorCodes.ModulGiProbe1.bitMask) > 0);
                    //notifier.E622 = GuiDataConverter.GetAlarmColor((data[2] & ErrorCodes.ModulGiProbe2.bitMask) > 0);
                    //notifier.E632 = GuiDataConverter.GetAlarmColor((data[2] & ErrorCodes.ModulGiProbe3.bitMask) > 0);
                    //notifier.E642 = GuiDataConverter.GetAlarmColor((data[2] & ErrorCodes.ModulGiProbe4.bitMask) > 0);
                    //notifier.E652 = GuiDataConverter.GetAlarmColor((data[2] & ErrorCodes.ModulGiProbe5.bitMask) > 0);
                    ErrorContainingRegistorsList.Add(Registers.Alarm611_652Reg);
                }


                // get errors from registers 953

                //int[] data = modbusClient.ReadHoldingRegisters(Registers.Alarm662_712Reg, 1);
                //if (data[3] != 0 || isResetErrors)
                //{
                //notifier.E662 = GuiDataConverter.GetAlarmColor((data[3] & ErrorCodes.ModulGiProbe6.bitMask) > 0);
                //notifier.E672 = GuiDataConverter.GetAlarmColor((data[3] & ErrorCodes.ModulGiProbe7.bitMask) > 0);
                //notifier.E682 = GuiDataConverter.GetAlarmColor((data[3] & ErrorCodes.ModulGiProbe8.bitMask) > 0);
                //notifier.E692 = GuiDataConverter.GetAlarmColor((data[3] & ErrorCodes.ModulGiProbe9.bitMask) > 0);
                //notifier.E702 = GuiDataConverter.GetAlarmColor((data[3] & ErrorCodes.ModulGiProbe10.bitMask) > 0);
                //notifier.E712 = GuiDataConverter.GetAlarmColor((data[3] & ErrorCodes.ModulGiProbe11.bitMask) > 0);
                //ErrorContainingRegistorsList.Add(Registers.Alarm662_712Reg);
                //}


                // get errors from registers 954

                //int[] data = modbusClient.ReadHoldingRegisters(Registers.Alarm801_882Reg, 1);
                if (data[4] != 0 || isResetErrors)
                {
                    notifier.E801 = DataConverter.GetAlarmColor((data[4] & ErrorCodes.LinkInverter1.bitMask) > 0);
                    //notifier.E802 = GuiDataConverter.GetAlarmColor((data[4] & ErrorCodes.LinkInverter2.bitMask) > 0);
                    notifier.E851 = DataConverter.GetAlarmColor((data[4] & ErrorCodes.HWFaultInverter1.bitMask) > 0);
                    //notifier.E852 = GuiDataConverter.GetAlarmColor((data[4] & ErrorCodes.HWFaultInverter2.bitMask) > 0);
                    notifier.E861 = DataConverter.GetAlarmColor((data[4] & ErrorCodes.OverCurrentInverter1.bitMask) > 0);
                    //notifier.E862 = GuiDataConverter.GetAlarmColor((data[4] & ErrorCodes.OverCurrentInverter2.bitMask) > 0);
                    notifier.E871 = DataConverter.GetAlarmColor((data[4] & ErrorCodes.HighTempInverter1.bitMask) > 0);
                    //notifier.E872 = GuiDataConverter.GetAlarmColor((data[4] & ErrorCodes.HighTempInverter2.bitMask) > 0); 
                    notifier.E881 = DataConverter.GetAlarmColor((data[4] & ErrorCodes.BadVoltInverter1.bitMask) > 0);
                    //notifier.E882 = GuiDataConverter.GetAlarmColor((data[4] & ErrorCodes.BadVoltInverter2.bitMask) > 0);
                    ErrorContainingRegistorsList.Add(Registers.Alarm801_882Reg);
                }
            

            // get errors from registers 955
            
                //int[] data = modbusClient.ReadHoldingRegisters(Registers.Alarm891_941Reg, 1);
                if (data[5] != 0 || isResetErrors)
                {
                    notifier.E891 = DataConverter.GetAlarmColor((data[5] & ErrorCodes.PhSequenceInverter1.bitMask) > 0);
                    //notifier.E892 = GuiDataConverter.GetAlarmColor((data[5] & ErrorCodes.PhSequenceInverter2.bitMask) > 0);
                    notifier.E901 = DataConverter.GetAlarmColor((data[5] & ErrorCodes.ModelErrInverter1.bitMask) > 0);
                    //notifier.E902 = GuiDataConverter.GetAlarmColor((data[5] & ErrorCodes.ModelErrInverter2.bitMask) > 0);
                    notifier.E911 = DataConverter.GetAlarmColor((data[5] & ErrorCodes.OLErrInverter1.bitMask) > 0);
                    //notifier.E912 = GuiDataConverter.GetAlarmColor((data[5] & ErrorCodes.OLErrInverter2.bitMask) > 0);
                    notifier.E921 = DataConverter.GetAlarmColor((data[5] & ErrorCodes.OverCurrentPFCInverter1.bitMask) > 0);
                    //notifier.E922 = GuiDataConverter.GetAlarmColor((data[5] & ErrorCodes.OverCurrentPFCInverter2.bitMask) > 0);
                    notifier.E931 = DataConverter.GetAlarmColor((data[5] & ErrorCodes.InternalComErrInverter1.bitMask) > 0);
                    //notifier.E932 = GuiDataConverter.GetAlarmColor((data[5] & ErrorCodes.InternalComErrInverter2.bitMask) > 0);
                    notifier.E941 = DataConverter.GetAlarmColor((data[5] & ErrorCodes.FaultPFCInverter1.bitMask) > 0);
                    ErrorContainingRegistorsList.Add(Registers.Alarm891_941Reg);
                }
            

            // get errors from registers 956
            
                //int[] data = modbusClient.ReadHoldingRegisters(Registers.Alarm942_972Reg, 1);
                if (data[6] != 0 || isResetErrors)
                {
                    //notifier.E942 = GuiDataConverter.GetAlarmColor((data[6] & ErrorCodes.FaultPFCInverter2.bitMask) > 0);
                    notifier.E951 = DataConverter.GetAlarmColor((data[6] & ErrorCodes.ProbeErrInverter1.bitMask) > 0);
                    //notifier.E952 = GuiDataConverter.GetAlarmColor((data[6] & ErrorCodes.ProbeErrInverter2.bitMask) > 0);
                    notifier.E961 = DataConverter.GetAlarmColor((data[6] & ErrorCodes.AbnormalConditionInverter1.bitMask) > 0);
                    //notifier.E962 = GuiDataConverter.GetAlarmColor((data[6] & ErrorCodes.AbnormalConditionInverter2.bitMask) > 0);
                    notifier.E971 = DataConverter.GetAlarmColor((data[6] & ErrorCodes.EEPROMInverter1.bitMask) > 0);
                    //notifier.E972 = GuiDataConverter.GetAlarmColor((data[6] & ErrorCodes.EEPROMInverter2.bitMask) > 0);
                    notifier.E060 = DataConverter.GetAlarmColor((data[6] & ErrorCodes.AntiLegionellaDone.bitMask) > 0);
                    notifier.E061 = DataConverter.GetAlarmColor((data[6] & ErrorCodes.AntiLegionellaFailure.bitMask) > 0);
                    ErrorContainingRegistorsList.Add(Registers.Alarm942_972Reg);
                }
            }
            catch
            {
                // indicate problem in communication
                throw;
            }

            return ErrorContainingRegistorsList.ToArray();
        }

        /// <summary>
        /// Reads all 7 error registers, and update active errors to gui, also writes them to file 
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="modbusClient">Modbus client object currently connected to a Maxa unit</param>
        /// <param name="fileWriter">FileWriter object to add values to its dictionary for later to be written to csv file</param>
        /// <returns>Returns an array of register's addresses that contain active errors</returns>
        public static int[] ReadErrors(NotifyNewData notifier, ModbusClient modbusClient, FileWriter fileWriter, bool isResetErrors)
        {
            // get errors from registers 950
            List<int> ErrorContainingRegistorsList = new List<int>();
            try
            {
                int[] data = modbusClient.ReadHoldingRegisters(Registers.Alarm01_16Reg, 7);
                if (data[0] != 0 || isResetErrors)
                {
                    fileWriter.dataDictionary["alarm register 950"] = data[0].ToString();
                    notifier.E001 = DataConverter.GetAlarmColor((data[0] & ErrorCodes.HighPressure.bitMask) > 0);
                    notifier.E002 = DataConverter.GetAlarmColor((data[0] & ErrorCodes.lowPressure.bitMask) > 0);
                    notifier.E003 = DataConverter.GetAlarmColor((data[0] & ErrorCodes.DigBlockCom1.bitMask) > 0);
                    notifier.E004 = DataConverter.GetAlarmColor((data[0] & ErrorCodes.DigBlockFan1.bitMask) > 0);
                    notifier.E005 = DataConverter.GetAlarmColor((data[0] & ErrorCodes.IceError.bitMask) > 0);
                    notifier.E006 = DataConverter.GetAlarmColor((data[0] & ErrorCodes.Flow.bitMask) > 0);
                    notifier.E007 = DataConverter.GetAlarmColor((data[0] & ErrorCodes.LowTempDHWPreparer.bitMask) > 0);
                    notifier.E008 = DataConverter.GetAlarmColor((data[0] & ErrorCodes.LackOfLubrication.bitMask) > 0);
                    notifier.E009 = DataConverter.GetAlarmColor((data[0] & ErrorCodes.HighTempDischargeProtection.bitMask) > 0);
                    notifier.E010 = DataConverter.GetAlarmColor((data[0] & ErrorCodes.HighTempSolar.bitMask) > 0);
                    //notifier.E013 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.DigBlockCom2.bitMask) > 0);
                    //notifier.E014 = GuiDataConverter.GetAlarmColor((data[0] & ErrorCodes.DigBlockFan2.bitMask) > 0);
                    notifier.E016 = DataConverter.GetAlarmColor((data[0] & ErrorCodes.ThermalPump1.bitMask) > 0);
                    ErrorContainingRegistorsList.Add(Registers.Alarm01_16Reg);
                }
                else if (fileWriter.dataDictionary.ContainsKey("alarm register 950"))
                    fileWriter.dataDictionary.Remove("alarm register 950");

                // get errors from registers 951

                //int[] data = modbusClient.ReadHoldingRegisters(Registers.Alarm18_101Reg, 1);
                if (data[1] != 0 || isResetErrors)
                {
                    fileWriter.dataDictionary["alarm register 951"] = data[1].ToString();
                    notifier.E018 = DataConverter.GetAlarmColor((data[1] & ErrorCodes.HighTemp.bitMask) > 0);
                    //notifier.E019 = GuiDataConverter.GetAlarmColor((data[1] & ErrorCodes..bitMask) > 0);
                    notifier.E020 = DataConverter.GetAlarmColor((data[1] & ErrorCodes.InvertedPressures.bitMask) > 0);
                    //notifier.E023 = GuiDataConverter.GetAlarmColor((data[1] & ErrorCodes..bitMask) > 0);
                    //notifier.E024 = GuiDataConverter.GetAlarmColor((data[1] & ErrorCodes..bitMask) > 0);
                    //notifier.E026 = GuiDataConverter.GetAlarmColor((data[1] & ErrorCodes.Thermal2PumpUse.bitMask) > 0);
                    notifier.E041 = DataConverter.GetAlarmColor((data[1] & ErrorCodes.WrongTemp.bitMask) > 0);
                    //notifier.E042 = GuiDataConverter.GetAlarmColor((data[1] & ErrorCodes.InsufficientExchangeInSanitary.bitMask) > 0); // said to be irrelevant by Maxa team member Davide Mocellin
                    notifier.E050 = DataConverter.GetAlarmColor((data[1] & ErrorCodes.HighTempSanitary.bitMask) > 0);
                    //notifier.E101 = GuiDataConverter.GetAlarmColor((data[1] & ErrorCodes.ModulGiDisconnected.bitMask) > 0);
                    //notifier.E102 = GuiDataConverter.GetAlarmColor((data[1] & ErrorCodes..bitMask) > 0);
                    ErrorContainingRegistorsList.Add(Registers.Alarm18_101Reg);
                }
                else if(fileWriter.dataDictionary.ContainsKey("alarm register 951")) 
                    fileWriter.dataDictionary.Remove("alarm register 951");




                // get errors from registers 952

                //int[] data = modbusClient.ReadHoldingRegisters(Registers.Alarm611_652Reg, 1);
                if (data[2] != 0 || isResetErrors)
                {
                    fileWriter.dataDictionary["alarm register 952"] = data[2].ToString();
                    notifier.E611 = DataConverter.GetAlarmColor((data[2] & ErrorCodes.InputWaterProbe.bitMask) > 0);
                    //notifier.E621 = GuiDataConverter.GetAlarmColor((data[2] & ErrorCodes.OutputWaterProbe.bitMask) > 0);
                    notifier.E631 = DataConverter.GetAlarmColor((data[2] & ErrorCodes.SuctionProbe.bitMask) > 0);
                    notifier.E641 = DataConverter.GetAlarmColor((data[2] & ErrorCodes.DischargeProbe.bitMask) > 0);
                    notifier.E651 = DataConverter.GetAlarmColor((data[2] & ErrorCodes.ExternProbe.bitMask) > 0);
                    notifier.E661 = DataConverter.GetAlarmColor((data[2] & ErrorCodes.Probe6.bitMask) > 0);
                    notifier.E671 = DataConverter.GetAlarmColor((data[2] & ErrorCodes.Probe7.bitMask) > 0);
                    //notifier.E681 = GuiDataConverter.GetAlarmColor((data[2] & ErrorCodes.Probe8.bitMask) > 0); 
                    notifier.E691 = DataConverter.GetAlarmColor((data[2] & ErrorCodes.LowPressureTransducer.bitMask) > 0);
                    notifier.E701 = DataConverter.GetAlarmColor((data[2] & ErrorCodes.HighPressureTransducer.bitMask) > 0);
                    notifier.E711 = DataConverter.GetAlarmColor((data[2] & ErrorCodes.Probe11.bitMask) > 0);
                    //notifier.E612 = GuiDataConverter.GetAlarmColor((data[2] & ErrorCodes.ModulGiProbe1.bitMask) > 0);
                    //notifier.E622 = GuiDataConverter.GetAlarmColor((data[2] & ErrorCodes.ModulGiProbe2.bitMask) > 0);
                    //notifier.E632 = GuiDataConverter.GetAlarmColor((data[2] & ErrorCodes.ModulGiProbe3.bitMask) > 0);
                    //notifier.E642 = GuiDataConverter.GetAlarmColor((data[2] & ErrorCodes.ModulGiProbe4.bitMask) > 0);
                    //notifier.E652 = GuiDataConverter.GetAlarmColor((data[2] & ErrorCodes.ModulGiProbe5.bitMask) > 0);
                    ErrorContainingRegistorsList.Add(Registers.Alarm611_652Reg);
                }
                else if (fileWriter.dataDictionary.ContainsKey("alarm register 952"))
                    fileWriter.dataDictionary.Remove("alarm register 952");


                // get errors from registers 953

                //int[] data = modbusClient.ReadHoldingRegisters(Registers.Alarm662_712Reg, 1);
                //if (data[3] != 0 || isResetErrors)
                //{
                //notifier.E662 = GuiDataConverter.GetAlarmColor((data[3] & ErrorCodes.ModulGiProbe6.bitMask) > 0);
                //notifier.E672 = GuiDataConverter.GetAlarmColor((data[3] & ErrorCodes.ModulGiProbe7.bitMask) > 0);
                //notifier.E682 = GuiDataConverter.GetAlarmColor((data[3] & ErrorCodes.ModulGiProbe8.bitMask) > 0);
                //notifier.E692 = GuiDataConverter.GetAlarmColor((data[3] & ErrorCodes.ModulGiProbe9.bitMask) > 0);
                //notifier.E702 = GuiDataConverter.GetAlarmColor((data[3] & ErrorCodes.ModulGiProbe10.bitMask) > 0);
                //notifier.E712 = GuiDataConverter.GetAlarmColor((data[3] & ErrorCodes.ModulGiProbe11.bitMask) > 0);
                //ErrorContainingRegistorsList.Add(Registers.Alarm662_712Reg);
                //}


                // get errors from registers 954

                //int[] data = modbusClient.ReadHoldingRegisters(Registers.Alarm801_882Reg, 1);
                if (data[4] != 0 || isResetErrors)
                {
                    fileWriter.dataDictionary["alarm register 954"] = data[2].ToString();
                    notifier.E801 = DataConverter.GetAlarmColor((data[4] & ErrorCodes.LinkInverter1.bitMask) > 0);
                    //notifier.E802 = GuiDataConverter.GetAlarmColor((data[4] & ErrorCodes.LinkInverter2.bitMask) > 0);
                    notifier.E851 = DataConverter.GetAlarmColor((data[4] & ErrorCodes.HWFaultInverter1.bitMask) > 0);
                    //notifier.E852 = GuiDataConverter.GetAlarmColor((data[4] & ErrorCodes.HWFaultInverter2.bitMask) > 0);
                    notifier.E861 = DataConverter.GetAlarmColor((data[4] & ErrorCodes.OverCurrentInverter1.bitMask) > 0);
                    //notifier.E862 = GuiDataConverter.GetAlarmColor((data[4] & ErrorCodes.OverCurrentInverter2.bitMask) > 0);
                    notifier.E871 = DataConverter.GetAlarmColor((data[4] & ErrorCodes.HighTempInverter1.bitMask) > 0);
                    //notifier.E872 = GuiDataConverter.GetAlarmColor((data[4] & ErrorCodes.HighTempInverter2.bitMask) > 0); 
                    notifier.E881 = DataConverter.GetAlarmColor((data[4] & ErrorCodes.BadVoltInverter1.bitMask) > 0);
                    //notifier.E882 = GuiDataConverter.GetAlarmColor((data[4] & ErrorCodes.BadVoltInverter2.bitMask) > 0);
                    ErrorContainingRegistorsList.Add(Registers.Alarm801_882Reg);
                }
                else if (fileWriter.dataDictionary.ContainsKey("alarm register 954"))
                    fileWriter.dataDictionary.Remove("alarm register 954");


                // get errors from registers 955

                //int[] data = modbusClient.ReadHoldingRegisters(Registers.Alarm891_941Reg, 1);
                if (data[5] != 0 || isResetErrors)
                {
                    fileWriter.dataDictionary["alarm register 955"] = data[2].ToString();
                    notifier.E891 = DataConverter.GetAlarmColor((data[5] & ErrorCodes.PhSequenceInverter1.bitMask) > 0);
                    //notifier.E892 = GuiDataConverter.GetAlarmColor((data[5] & ErrorCodes.PhSequenceInverter2.bitMask) > 0);
                    notifier.E901 = DataConverter.GetAlarmColor((data[5] & ErrorCodes.ModelErrInverter1.bitMask) > 0);
                    //notifier.E902 = GuiDataConverter.GetAlarmColor((data[5] & ErrorCodes.ModelErrInverter2.bitMask) > 0);
                    notifier.E911 = DataConverter.GetAlarmColor((data[5] & ErrorCodes.OLErrInverter1.bitMask) > 0);
                    //notifier.E912 = GuiDataConverter.GetAlarmColor((data[5] & ErrorCodes.OLErrInverter2.bitMask) > 0);
                    notifier.E921 = DataConverter.GetAlarmColor((data[5] & ErrorCodes.OverCurrentPFCInverter1.bitMask) > 0);
                    //notifier.E922 = GuiDataConverter.GetAlarmColor((data[5] & ErrorCodes.OverCurrentPFCInverter2.bitMask) > 0);
                    notifier.E931 = DataConverter.GetAlarmColor((data[5] & ErrorCodes.InternalComErrInverter1.bitMask) > 0);
                    //notifier.E932 = GuiDataConverter.GetAlarmColor((data[5] & ErrorCodes.InternalComErrInverter2.bitMask) > 0);
                    notifier.E941 = DataConverter.GetAlarmColor((data[5] & ErrorCodes.FaultPFCInverter1.bitMask) > 0);
                    ErrorContainingRegistorsList.Add(Registers.Alarm891_941Reg);
                }
                else if (fileWriter.dataDictionary.ContainsKey("alarm register 955"))
                    fileWriter.dataDictionary.Remove("alarm register 955");


                // get errors from registers 956

                //int[] data = modbusClient.ReadHoldingRegisters(Registers.Alarm942_972Reg, 1);
                if (data[6] != 0 || isResetErrors)
                {
                    fileWriter.dataDictionary["alarm register 956"] = data[2].ToString();
                    //notifier.E942 = GuiDataConverter.GetAlarmColor((data[6] & ErrorCodes.FaultPFCInverter2.bitMask) > 0);
                    notifier.E951 = DataConverter.GetAlarmColor((data[6] & ErrorCodes.ProbeErrInverter1.bitMask) > 0);
                    //notifier.E952 = GuiDataConverter.GetAlarmColor((data[6] & ErrorCodes.ProbeErrInverter2.bitMask) > 0);
                    notifier.E961 = DataConverter.GetAlarmColor((data[6] & ErrorCodes.AbnormalConditionInverter1.bitMask) > 0);
                    //notifier.E962 = GuiDataConverter.GetAlarmColor((data[6] & ErrorCodes.AbnormalConditionInverter2.bitMask) > 0);
                    notifier.E971 = DataConverter.GetAlarmColor((data[6] & ErrorCodes.EEPROMInverter1.bitMask) > 0);
                    //notifier.E972 = GuiDataConverter.GetAlarmColor((data[6] & ErrorCodes.EEPROMInverter2.bitMask) > 0);
                    notifier.E060 = DataConverter.GetAlarmColor((data[6] & ErrorCodes.AntiLegionellaDone.bitMask) > 0);
                    notifier.E061 = DataConverter.GetAlarmColor((data[6] & ErrorCodes.AntiLegionellaFailure.bitMask) > 0);
                    ErrorContainingRegistorsList.Add(Registers.Alarm942_972Reg);
                }
                else if (fileWriter.dataDictionary.ContainsKey("alarm register 956"))
                    fileWriter.dataDictionary.Remove("alarm register 956");
            }
            catch
            {
                // indicate problem in communication
                throw;
            }

            return ErrorContainingRegistorsList.ToArray();
        }

        /// <summary>
        /// Reads the manufacturing data and updates to gui
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="modbusClient">Modbus client object currently connected to a Maxa unit</param>
        /// <returns>No return value</returns>
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
                throw;
            }
        }

        /// <summary>
        /// Writes new setpoints from GUI to the machine
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="modbusClient">Modbus client object currently connected to a Maxa unit</param>
        /// <returns>No return value</returns>
        public static void WriteSetPoints(NotifyNewData notifier, ModbusClient modbusClient)
        {
            try
            {
                modbusClient.WriteSingleRegister(Registers.EnableWritingBitMaskReg, Registers.EnableSP_N_MechineStateWriting);

                int[] setpointValues = {    DataConverter.GetSetPointFromGui(notifier.coolSP), DataConverter.GetSetPointFromGui(notifier.heatSP), DataConverter.GetSetPointFromGui(notifier.DHWSP),
                                            DataConverter.GetSetPointFromGui(notifier.coolSP2), DataConverter.GetSetPointFromGui(notifier.heatSP2), DataConverter.GetSetPointFromGui(notifier.DHWPreparerSP) };
                
                modbusClient.WriteMultipleRegisters(Registers.CoolSPReg, setpointValues);
                
                /* old method - less efficient to write setpoints one by one to the Maxa
                modbusClient.WriteSingleRegister(Registers.CoolSPReg, DataConverter.GetSetPointFromGui(notifier.coolSP));
                modbusClient.WriteSingleRegister(Registers.HeatSPReg, DataConverter.GetSetPointFromGui(notifier.heatSP));
                modbusClient.WriteSingleRegister(Registers.SanitarySPReg, DataConverter.GetSetPointFromGui(notifier.DHWSP));
                modbusClient.WriteSingleRegister(Registers.SecondCoolSPReg, DataConverter.GetSetPointFromGui(notifier.coolSP2));
                modbusClient.WriteSingleRegister(Registers.SecondHeatSPReg, DataConverter.GetSetPointFromGui(notifier.heatSP2));
                modbusClient.WriteSingleRegister(Registers.DHWPreparerSPReg, DataConverter.GetSetPointFromGui(notifier.DHWPreparerSP));
                */
            }
            catch
            {
                // indicate problem in communication
                throw;
            }
        }

        /// <summary>
        /// Verifies the setpoint were successfully written to the machine
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="modbusClient">Modbus client object currently connected to a Maxa unit</param>
        /// <returns>Returns true if the setpoints were written successfully</returns>
        public static bool VerifySetpoints(NotifyNewData notifier, ModbusClient modbusClient)
        {
            try
            {
                int[] data = modbusClient.ReadHoldingRegisters(Registers.CoolSPReg, 6);
                if (data[0] != DataConverter.GetSetPointFromGui(notifier.coolSP)) return false;
                //data = modbusClient.ReadHoldingRegisters(Registers.HeatSPReg, 1);
                if (data[1] != DataConverter.GetSetPointFromGui(notifier.heatSP)) return false;
                //data = modbusClient.ReadHoldingRegisters(Registers.SanitarySPReg, 1);
                if (data[2] != DataConverter.GetSetPointFromGui(notifier.DHWSP)) return false;
                //data = modbusClient.ReadHoldingRegisters(Registers.SecondCoolSPReg, 1);
                if (data[3] != DataConverter.GetSetPointFromGui(notifier.coolSP2)) return false;
                //data = modbusClient.ReadHoldingRegisters(Registers.SecondHeatSPReg, 1);
                if (data[4] != DataConverter.GetSetPointFromGui(notifier.heatSP2)) return false;
                //data = modbusClient.ReadHoldingRegisters(Registers.DHWPreparerSPReg, 1);
                if (data[5] != DataConverter.GetSetPointFromGui(notifier.DHWPreparerSP)) return false;

            }
            catch
            {
                // indicate problem in communication
                throw;
            }

            return true; 
        }

        /// <summary>
        /// Verifies the setpoint were successfully written to the machine
        /// </summary>
        /// <param name="notifier">Object holding all variables for data biniding</param>
        /// <param name="modbusClient">Modbus client object currently connected to a Maxa unit</param>
        /// <param name="fileWriter">FileWriter object to add values to its dictionary for later to be written to csv file</param>
        /// <returns>Returns true if the setpoints were written successfully</returns>
        public static bool VerifySetpoints(NotifyNewData notifier, ModbusClient modbusClient, FileWriter fileWriter)
        {
            try
            {
                int[] data = modbusClient.ReadHoldingRegisters(Registers.CoolSPReg, 6);

                decimal[] decimalData = new decimal[data.Length];
                int i = 0;
                foreach(int dat in data) 
                { 
                    decimalData[i] = (decimal)dat / 10;
                    i++;
                }

                fileWriter.dataDictionary["Cool SP"] = decimalData[0].ToString("0.0");
                fileWriter.dataDictionary["Heat SP"] = decimalData[1].ToString("0.0");
                fileWriter.dataDictionary["DHW SP"] = decimalData[2].ToString("0.0");
                fileWriter.dataDictionary["Cool SP2"] = decimalData[3].ToString("0.0");
                fileWriter.dataDictionary["Heat SP2"] = decimalData[4].ToString("0.0");
                fileWriter.dataDictionary["DHW preparer SP"] = decimalData[5].ToString("0.0");

                if (data[0] != DataConverter.GetSetPointFromGui(notifier.coolSP)) return false;
                //data = modbusClient.ReadHoldingRegisters(Registers.HeatSPReg, 1);
                if (data[1] != DataConverter.GetSetPointFromGui(notifier.heatSP)) return false;
                //data = modbusClient.ReadHoldingRegisters(Registers.SanitarySPReg, 1);
                if (data[2] != DataConverter.GetSetPointFromGui(notifier.DHWSP)) return false;
                //data = modbusClient.ReadHoldingRegisters(Registers.SecondCoolSPReg, 1);
                if (data[3] != DataConverter.GetSetPointFromGui(notifier.coolSP2)) return false;
                //data = modbusClient.ReadHoldingRegisters(Registers.SecondHeatSPReg, 1);
                if (data[4] != DataConverter.GetSetPointFromGui(notifier.heatSP2)) return false;
                //data = modbusClient.ReadHoldingRegisters(Registers.DHWPreparerSPReg, 1);
                if (data[5] != DataConverter.GetSetPointFromGui(notifier.DHWPreparerSP)) return false;

            }
            catch
            {
                // indicate problem in communication
                throw;
            }

            return true;
        }

        /// <summary>
        /// Writes new operation mode to the connected Maxa unit
        /// </summary>
        /// <param name="modbusClient">Modbus client object currently connected to a Maxa unit</param>
        /// <param name="opMode">The operation mode to set to the Maxa unit</param>
        /// <returns>no return value</returns>
        public static void WriteOperatinMode(ModbusClient modbusClient, int opMode)
        {
            try
            {
                modbusClient.WriteSingleRegister(Registers.EnableWritingBitMaskReg, Registers.EnableSP_N_MechineStateWriting);

                modbusClient.WriteSingleRegister(Registers.MachineStateWriteReg, opMode);
            }
            catch
            {
                // indicate problem in communication
                throw;
            }
        }

        /// <summary>
        /// Send a request to start anti-legionella cycle
        /// </summary>
        /// <param name="modbusClient">Modbus client object currently connected to a Maxa unit</param>
        /// <returns>no return value</returns>
        public static void RequestAntiLegionellaCycle(ModbusClient modbusClient)
        {
            try
            {
                modbusClient.WriteSingleRegister(Registers.EnableWritingBitMaskReg, Registers.EnableAntiLegionellaCycle);

                modbusClient.WriteSingleRegister(Registers.ForcingBitMaskReg, Registers.ActivateAntiLegionellaCycle);
            }
            catch
            {
                // indicate problem in communication
                throw;
            }
        }

        /// <summary>
        /// Force a defrost cycle
        /// </summary>
        /// <param name="modbusClient">Modbus client object currently connected to a Maxa unit</param>
        /// <returns>no return value</returns>
        public static void ForceDefrost(ModbusClient modbusClient)
        {
            try
            {
                modbusClient.WriteSingleRegister(Registers.ForcingBitMaskReg, Registers.ForceDefrost);
            }
            catch
            {
                // indicate problem in communication
                throw;
            }
        }

        /// <summary>
        /// Force a plant venting
        /// </summary>
        /// <param name="modbusClient">Modbus client object currently connected to a Maxa unit</param>
        /// <param name="activate">true to activate, false to stop plant venting</param>
        /// <returns>no return value</returns>
        public static void ForcePlantVenting(ModbusClient modbusClient, bool activate)
        {
            try
            {
                if(activate)
                    modbusClient.WriteSingleRegister(Registers.ForcingBitMaskReg, Registers.ForcePlantVenting);

                else
                    modbusClient.WriteSingleRegister(Registers.ForcingBitMaskReg, 0);
            }
            catch
            {
                // indicate problem in communication
                throw;
            }
        }

        /// <summary>
        /// Force ambient call
        /// </summary>
        /// <param name="modbusClient">Modbus client object currently connected to a Maxa unit</param>
        /// <param name="activate">true to activate, false to stop ambient call</param>
        /// <returns>no return value</returns>
        public static void ForceAmbientCall(ModbusClient modbusClient, bool activate)
        {
            try
            {
                if (activate)
                {
                    modbusClient.WriteSingleRegister(Registers.EnableWritingBitMaskReg, Registers.EnableRemoteAmbientCall);
                    modbusClient.WriteSingleRegister(Registers.ForcingBitMaskReg, Registers.ForceRemoteAmbientCall);
                }

                else
                {
                    modbusClient.WriteSingleRegister(Registers.EnableWritingBitMaskReg, Registers.EnableSP_N_MechineStateWriting);
                    modbusClient.WriteSingleRegister(Registers.ForcingBitMaskReg, 0);
                }
            }
            catch
            {
                // indicate problem in communication
                throw;
            }
        }

        /// <summary>
        /// Enable Max Hz function for all modes
        /// </summary>
        /// <param name="modbusClient">Modbus client object currently connected to a Maxa unit</param>
        /// <param name="activate">true to activate, false to stop Max Hz function</param>
        /// <returns>no return value</returns>
        public static void EnableMaxHz(ModbusClient modbusClient, bool activate)
        {
            try
            {
                if (activate)
                {
                    modbusClient.WriteSingleRegister(Registers.EnableMaxHzReg, 1);
                    modbusClient.WriteSingleRegister(Registers.MaxHzModeReg, (int)Registers.MaxHzMode.ALWAYS_ACTIVE);
                }

                else
                {
                    modbusClient.WriteSingleRegister(Registers.EnableMaxHzReg, 0);
                    modbusClient.WriteSingleRegister(Registers.ForcingBitMaskReg, (int)Registers.MaxHzMode.NOT_ACTIVE);
                }
            }
            catch
            {
                // indicate problem in communication
                throw;
            }
        }


        /// <summary>
        /// Attempts to reset active arrors by wrting '0' to the appropriate registers
        /// </summary>
        /// <param name="modbusClient">Modbus client object currently connected to a Maxa unit</param>
        /// <param name="errorsRegistersArray">An array of the registers with active errors</param>
        /// <returns>no return value</returns>
        public static void ResetErrors(ModbusClient modbusClient, int[] errorsRegistersArray)
        {
            try
            {
                foreach(int register in errorsRegistersArray)
                {
                    modbusClient.WriteSingleRegister(register, 0);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
