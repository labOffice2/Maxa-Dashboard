using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EasyModbus;

namespace Maxa_Dash
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ModbusClient modbusClient;
        NotifyNewData notifyer = new NotifyNewData();

        System.Timers.Timer timer;

        bool isConnected = false;

        public float WaterInTemp;
        public float WaterOutTemp;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = notifyer;
            timer = new System.Timers.Timer();
            timer.AutoReset = true;
            timer.Interval = 1000;
            timer.Elapsed += elapsed;
            
        }

        private void elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateValues();
        }

        void SetModbus()
        {
            //int quantity = 1;

            try
            {
                modbusClient = new ModbusClient("COM4");
                //modbusClient.UnitIdentifier = 1; Not necessary since default slaveID = 1;
                //modbusClient.Baudrate = 9600;	// Not necessary since default baudrate = 9600
                modbusClient.Parity = System.IO.Ports.Parity.Even;
                modbusClient.StopBits = System.IO.Ports.StopBits.One;
                //modbusClient.ConnectionTimeout = 500;			
                modbusClient.Connect();
                isConnected = true;
                timer.Start();
            }catch
            {
                
                notifyer.waterInTemp = 100005;
                notifyer.waterOutTemp = 100000;
                
            }

        }

        private void UpdateValues()
        {

            if (isConnected)
            {
                try
                {

                    int[] data = modbusClient.ReadHoldingRegisters(Registers.InputWaterTempReg, 1);
                    notifyer.waterInTemp = data[0];
                    data = modbusClient.ReadHoldingRegisters(Registers.OutputWaterTempReg, 1);
                    notifyer.waterOutTemp = data[0];
                    data = modbusClient.ReadHoldingRegisters(Registers.OurdoorAirTempReg, 1);
                    notifyer.externalAirTemp = data[0];
                }
                catch
                {
                    notifyer.waterInTemp = 100005;
                    notifyer.waterOutTemp = 100000;
                }
            } 

        }

        private void OnClick_ConnectButton(object sender, RoutedEventArgs e)
        {
            SetModbus();
        }
    }
}
