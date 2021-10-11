using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

/// <summary>
/// This is a general data conversion class
/// Used for converting data from and to the user interface
/// </summary>
namespace Maxa_Dash
{
    public class DataConverter
    {

        /// <summary>
        /// This function converts the received boolean value to a color for the alarm.
        /// </summary>
        /// <param name="status">True for activate alarms, False for inactive alarms</param>
        /// <returns>A brush with a color according to the received value </returns>
        public static Brush GetAlarmColor(bool status)
        {
            return status ? Brushes.Red : Brushes.Gray;
        }

        /// <summary>
        /// This function converts an int to a machine state type
        /// the int is received from the Maxa and converted for displaying on the UI 
        /// </summary>
        /// <param name="data">an integer representing the desired machine state / operation mode</param>
        /// <returns>A machine state</returns>
        public static NotifyNewData.MachinelState GetMachineState(int data)
        {
            if (data < 7)
            {
                return (NotifyNewData.MachinelState)data;
            }
            else
            {
                return GetMachineState(data & 0x07); // if the data is invalid - try again masking irrelevant bits
            }
        }

        /// <summary>
        /// This function convets setpoints from the UI to a format used by the Maxa
        /// </summary>
        /// <param name="value">The setpoint set by user in °C </param>
        /// <returns>The setpoint in the format to write to the maxa (°C/10)</returns>
        public static int GetSetPointFromGui(float value)
        {
            int data = (int)(value * 10);
            return data;
        }

    }
}
