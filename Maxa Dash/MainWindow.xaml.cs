using System;
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
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;


namespace Maxa_Dash
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ModbusClient modbusClient;

        NotifyNewData notifier = new NotifyNewData();

        private Timer timer;

        bool isConnected = false;
        bool isUpdating = false;

        public float WaterInTemp;
        public float WaterOutTemp;
        public MainWindow()
        {
            InitializeComponent();
            SetComboBoxes();

            this.DataContext = notifier;
            timer = new Timer
            {
                AutoReset = true,
                Interval = 300
            };
            timer.Elapsed += Elapsed;
            
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

            ComboBoxItem br9600 = new(), br19200 = new(), br38400 = new(), br57600 = new(), br115200 = new();
            br9600.Content = 9600;
            br9600.IsSelected = true;
            br19200.Content = 19200;
            br38400.Content = 38400;
            br57600.Content = 57600;
            br115200.Content = 115200;
            ComboBoxItem[] baudRateItems = { br9600, br19200, br38400, br57600, br115200 };
            foreach (ComboBoxItem br in baudRateItems)
                baudRateBox.Items.Add(br);

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

        private void Elapsed(object sender, ElapsedEventArgs e)
        {
            UpdateValues();
        }

        void SetModbus()
        {
            try
            {
                ComboBoxItem port = (ComboBoxItem)ComBox.SelectedItem;
                notifier.firmwareVersion = $"com port {port.Content}";
                modbusClient = new ModbusClient(port.Content.ToString());
                //modbusClient.UnitIdentifier = (byte)MachineIDBox.SelectedItem;  // Not necessary since default slaveID = 1;
                //modbusClient.Baudrate = (int)baudRateBox.SelectedItem;	        // Not necessary since default baudrate = 9600
                ComboBoxItem par = (ComboBoxItem)ParityBox.SelectedItem;
                modbusClient.Parity = (Parity)par.Content;
                ComboBoxItem stopB = (ComboBoxItem)StopBitBox.SelectedItem;
                modbusClient.StopBits = (StopBits)stopB.Content;
                //modbusClient.ConnectionTimeout = 500;
                modbusClient.Connect();
                isConnected = true;
                Maxa.ReadManufacturingInfo(notifier, modbusClient);
                ConnectButton.IsEnabled = false;
                notifier.E000 = GuiDataConverter.GetAlarmColor(false);
            }
            catch
            {
                notifier.E000 = GuiDataConverter.GetAlarmColor(true);
                notifier.waterInTemp += 10;
                notifier.waterOutTemp = 100000;
                isConnected = false;
            }

        }

        private void UpdateValues()
        {
            if (isUpdating) return;
            isUpdating = true;
            if (isConnected)
            {
                try
                {
                    //Maxa.UpdateOperationMode(notifier, modbusClient);
                    Maxa.UpdateWaterSystemParameters(notifier, modbusClient);
                    Maxa.UpdateRefrigirationSystemParameters(notifier, modbusClient);
                    Maxa.ReadErrors(notifier, modbusClient);
                }
                catch
                {
                    notifier.waterInTemp = 100005;
                    notifier.waterOutTemp = 100000;
                }
            }
            else
            {
                ConnectButton.IsEnabled = true;
                SetModbus();
            }
            isUpdating = false;

        }

        private void OnClick_ConnectButton(object sender, RoutedEventArgs e)
        {
            SetModbus();
            timer.Start();
        }

        private void PathSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog commonFileDialog = new CommonOpenFileDialog();
            commonFileDialog.IsFolderPicker = true;
            
            if(commonFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string path = commonFileDialog.FileName;
                PathSelectionButton.Content = path;
            }
        }
    }
}
