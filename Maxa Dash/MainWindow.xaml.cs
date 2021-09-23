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

        FileWriter FileWriter;

        private Timer timer;

        private UserMessages messagesPanel;
        private List<UserMessages.MessageLabel> messageLabels = new List<UserMessages.MessageLabel>();

        bool isConnected = false;
        bool isUpdating = false;
        bool isRecord = false;

        private float setpointMaxCool = 23.0f;
        private float setpointMinCool = 5.0f;
        private float setpointMaxHeat = 55.0f;
        private float setpointMinHeat = 25.0f;
        public MainWindow()
        {
            InitializeComponent();
            SetComboBoxes();

            this.DataContext = notifier;
            timer = new Timer
            {
                AutoReset = true,
                Interval = 2000,
            };
            timer.Elapsed += Elapsed;

            messagesPanel = new UserMessages(MessagePanel);
        }

        private void RemoveOldMessages()
        {
            foreach(var item in messageLabels.ToArray())
            {
                if (item == null) break;
                TimeSpan displayDuration = DateTime.Now - item.startTime;
                if(displayDuration > item.duration)
                {
                    if(MessagePanel.Children.Contains(item.Label))
                    {
                        MessagePanel.Children.Remove(item.Label);
                    }
                    messageLabels.Remove(item);
                }
            }
        }

        private void SetComboBoxes()
        {
            // fill communication port ComboBox
            string[] availablePorts = SerialPort.GetPortNames();
            foreach(string port in availablePorts)
            {
                ComboBoxItem portItem = new();
                portItem.Content = port;
                if (port == "COM1" || availablePorts.Length == 1) portItem.IsSelected = true;
                ComBox.Items.Add(portItem);
            }

            // fill machine ID ComboBox
            for (int i = 1; i<256; i++)
            {
                ComboBoxItem id = new();
                id.Content = i;
                if (i == 1) id.IsSelected = true;
                MachineIDBox.Items.Add(id);
            }

            // fill baud rate ComboBox
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

            // fill parity ComboBox
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

            // fill stop bit ComboBox
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

            // fill operation mode ComboBox
            ComboBoxItem modeStandBy = new(), modeCool = new(), modeHeat = new(), modeSanitary = new(), modeCoolNSanitary = new(), modeHeatNSanitary = new();
            modeStandBy.IsSelected = true;
            modeStandBy.Content = NotifyNewData.MachinelState.STANBY;
            modeCool.Content = NotifyNewData.MachinelState.COOL;
            modeHeat.Content = NotifyNewData.MachinelState.HEAT;
            modeSanitary.Content = NotifyNewData.MachinelState.ONLY_SANITARY;
            modeCoolNSanitary.Content = NotifyNewData.MachinelState.COOLnSANITARY;
            modeHeatNSanitary.Content = NotifyNewData.MachinelState.HEATnSANITARY;
            OpModeBox.Items.Add(modeStandBy);
            OpModeBox.Items.Add(modeCool);
            OpModeBox.Items.Add(modeHeat);
            OpModeBox.Items.Add(modeSanitary);
            OpModeBox.Items.Add(modeCoolNSanitary);
            OpModeBox.Items.Add(modeHeatNSanitary);

        }

        private void Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                UpdateValues();
                RemoveOldMessages();
            });
            
        }

        void SetModbus()
        {
            try
            {
                ComboBoxItem port = (ComboBoxItem)ComBox.SelectedItem;
                if(port != null)
                {

                    //notifier.firmwareVersion = $"com port {port.Content}";
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
                    notifier.E000 = DataConverter.GetAlarmColor(false);
                }
                else
                {
                    notifier.E000 = DataConverter.GetAlarmColor(true);
                    messageLabels = messagesPanel.AddMessage(messageLabels,"couldn't connect", 0.1f, Brushes.Red);
                }
            }
            catch
            {
                notifier.E000 = DataConverter.GetAlarmColor(true);
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
                    if(isRecord)
                    {
                        Maxa.UpdateOperationMode(notifier, modbusClient);
                        Maxa.UpdateWaterSystemParametersNRecord(notifier, modbusClient, FileWriter);
                        Maxa.UpdateRefrigirationSystemParametersNRecord(notifier, modbusClient, FileWriter);
                        Maxa.UpdateReadOnlySetpoints(notifier, modbusClient);
                        Maxa.ReadErrors(notifier, modbusClient);
                        FileWriter.WriteToFile();
                    }
                    else
                    {
                        Maxa.UpdateOperationMode(notifier, modbusClient);
                        Maxa.UpdateWaterSystemParameters(notifier, modbusClient);
                        Maxa.UpdateRefrigirationSystemParameters(notifier, modbusClient);
                        Maxa.UpdateReadOnlySetpoints(notifier, modbusClient);
                        Maxa.ReadErrors(notifier, modbusClient);
                    }
                }
                catch
                {
                    notifier.waterInTemp = 100005;
                    notifier.waterOutTemp = 100000;
                }
            }
            else
            {
                //ConnectButton.IsEnabled = true;
                SetModbus();
            }
            isUpdating = false;

        }

        private void OnClick_ConnectButton(object sender, RoutedEventArgs e)
        {
            messageLabels = messagesPanel.AddMessage(messageLabels, "pressed connect", 0.1f);
            SetModbus();
            timer.Start();
        }

        // Openning a file browser to select a folder for CSV output file
        private void PathSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog commonFileDialog = new CommonOpenFileDialog();
            commonFileDialog.IsFolderPicker = true;
            
            if(commonFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string path = commonFileDialog.FileName;
                PathSelectionButton.Content = path;
                FileWriter = new FileWriter(path);
            }
        }

        private void StartRecordingButton_Click(object sender, RoutedEventArgs e)
        {
            if(FileWriter != null)
            {
                if(isRecord)
                {
                    isRecord = false;
                    StartRecordingButton.Content = "Start Recording";
                }
                else
                {
                    isRecord = true;
                    StartRecordingButton.Content = "Stop Recording";
                }
            }
            else
            {
                messageLabels = messagesPanel.AddMessage(messageLabels,"Select folder for scv file", 0.1f);
                MessageBox.Show("Please select a folder");
            }
        }

        // Handling setpoint settings ranges - auto fix
        #region setpoint range handling
        private void CoolSP1_LostFocus(object sender, RoutedEventArgs e)
        {
            VerifySetpoint(CoolSP1, true);
        }

        private void HeatSP1_LostFocus(object sender, RoutedEventArgs e)
        {
            VerifySetpoint(HeatSP1, false);
        }

        private void DHWSP_LostFocus(object sender, RoutedEventArgs e)
        {
            VerifySetpoint(DHWSP, false);
        }

        private void CoolSP2_LostFocus(object sender, RoutedEventArgs e)
        {
            VerifySetpoint(CoolSP2, true);
        }

        private void HeatSP2_LostFocus(object sender, RoutedEventArgs e)
        {
            VerifySetpoint(HeatSP2, false);

        }

        private void DHWPrepSP_LostFocus(object sender, RoutedEventArgs e)
        {
            float maxSP = 80f;
            float minSP = 0f;
            if (float.Parse(DHWPrepSP.Text) > maxSP) DHWPrepSP.Text = maxSP.ToString();
            else if (float.Parse(DHWPrepSP.Text) < minSP) DHWPrepSP.Text = minSP.ToString();
        }

        private void VerifySetpoint(TextBox textBox,bool isCoolSP)
        {
            float maxSP;
            float minSP;
            try
            {
                if(isCoolSP)
                {
                    maxSP = setpointMaxCool;
                    minSP = setpointMinCool;
                }
                else
                {
                    maxSP = setpointMaxHeat;
                    minSP = setpointMinHeat;
                }
                if(float.Parse(textBox.Text) > maxSP) textBox.Text = maxSP.ToString();
                else if(float.Parse(textBox.Text) < minSP) textBox.Text = minSP.ToString();
            }
            catch
            { }
        }


        #endregion

        private void ApplySetpoints_Click(object sender, RoutedEventArgs e)
        {
            if(modbusClient != null)
            {
                Maxa.WriteSetPoints(notifier, modbusClient);
                bool isSPWritten = Maxa.VerifySetpoints(notifier, modbusClient);
                if(!isSPWritten) messageLabels = messagesPanel.AddMessage(messageLabels, "new setpoint not applied", 0.1f, Brushes.Red);
                else messageLabels = messagesPanel.AddMessage(messageLabels, "new setpoint successfully applied", 0.1f, Brushes.Green);
                Maxa.WriteOperatinMode(notifier, modbusClient);
            }
            else
            {
                messageLabels = messagesPanel.AddMessage(messageLabels, "Connect to the maxa to set new setpoints", 0.2f);
            }
        }
    }
}
