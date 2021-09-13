﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
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
            SetComboBoxes();

            this.DataContext = notifyer;
            timer = new System.Timers.Timer();
            timer.AutoReset = true;
            timer.Interval = 1000;
            timer.Elapsed += elapsed;
            
        }

        private void SetComboBoxes()
        {
            string[] availablePorts = SerialPort.GetPortNames();
            foreach(string port in availablePorts)
            {
                ComboBoxItem portItem = new();
                portItem.Content = port;
                if (port == "COM1" || availablePorts.Length == 1) portItem.IsSelected = true;
                ComBox.Items.Add(portItem);
            }

            for(int i = 1; i<256; i++)
            {
                ComboBoxItem id = new();
                id.Content = i;
                if (i == 1) id.IsSelected = true;
                MachineIDBox.Items.Add(id);
            }

            ComboBoxItem ParityItemEven = new(), ParityItemOdd = new(), ParityItemMark = new(), ParityItemNone = new(), ParityItemSpace = new();
            ParityItemEven.Content = Parity.Even;
            ParityItemEven.IsSelected = true;
            ParityItemOdd.Content = Parity.Odd;
            ParityItemMark.Content = Parity.Mark;
            ParityItemNone.Content = Parity.None;
            ParityItemSpace.Content = Parity.Space;
            ParityBox.Items.Add(ParityItemEven);
            ParityBox.Items.Add(ParityItemOdd);
            ParityBox.Items.Add(ParityItemMark);
            ParityBox.Items.Add(ParityItemNone);
            ParityBox.Items.Add(ParityItemSpace);

            ComboBoxItem stopBitNone = new(), stopBitOne = new(), stopBitOneNHalf = new(), stopBitTwo = new();
            stopBitNone.Content = StopBits.None;
            stopBitOne.Content = StopBits.One;
            stopBitOne.IsSelected = true;
            stopBitOneNHalf.Content = StopBits.OnePointFive;
            stopBitTwo.Content = StopBits.Two;
            StopBitBox.Items.Add(stopBitNone);
            StopBitBox.Items.Add(stopBitOne);
            StopBitBox.Items.Add(stopBitOneNHalf);
            StopBitBox.Items.Add(stopBitTwo);

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
