using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        //private List<UserMessages.MessageLabel> messageLabelsList = new List<UserMessages.MessageLabel>();

        private bool isConnected = false;
        private bool isUpdating = false;
        private bool isRecord = false;
        private bool isNewSetpointAvailable = false;
        private bool isResetErrors;
        private bool forceDefrostFlag;
        private bool antiLegionellaFlag;
        private bool plantVentingflag;

        private float setpointMaxCool = 23.0f;
        private float setpointMinCool = 5.0f;
        private float setpointMaxHeat = 55.0f;
        private float setpointMinHeat = 25.0f;

        private int opMode;
        private int[] activeErrors;

        private DateTime lastSuccesfullCommunication;
        private readonly double miscommunicationMinDuration = 0.5; // minimum duration of miscommunication to indicate an error (in minutes)

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

        /* moved to UserMessages class
        private void RemoveOldMessages()
        {
            foreach(var item in messageLabelsList.ToArray())
            {
                if (item == null) break;
                TimeSpan displayDuration = DateTime.Now - item.startTime;
                if(displayDuration > item.duration)
                {
                    if(MessagePanel.Children.Contains(item.Label))
                    {
                        MessagePanel.Children.Remove(item.Label);
                    }
                    messageLabelsList.Remove(item);
                }
            }
        }
        */

        /// <summary>
        /// This function fills the ComboBoxes in the UI with optios (ComboBoxItems).
        /// </summary>
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
            modeStandBy.Content = NotifyNewData.MachinelState.STANDBY;
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

        /// <summary>
        /// This function is called by the timer whenever it elapses
        /// It calls UpdateValues and RemoveOldMessages functions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                UpdateValues();
                //RemoveOldMessages();
                messagesPanel.RemoveOldMessages();
            });
            
        }

        /// <summary>
        /// This function handles the communication connection to the Maxa 
        /// It attempts to gets the communication setting from the gui and then establish connection.
        /// If connection cannot be established it will notify the user
        /// </summary>
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
                    lastSuccesfullCommunication = DateTime.Now;
                }
                else
                {
                    notifier.E000 = DataConverter.GetAlarmColor(true);
                    messagesPanel.AddMessage("couldn't connect", 0.1f, Brushes.Red);
                }
            }
            catch
            {
                notifier.E000 = DataConverter.GetAlarmColor(true);
                messagesPanel.AddMessage("couldn't connect", 0.1f, Brushes.Red);
                notifier.waterInTemp += 10;
                notifier.waterOutTemp = 100000;
                isConnected = false;
            }

        }


        /// <summary>
        /// This function is called periodically by the elapsed (every time the timer of 2 seconds elapses)
        /// This hanldes the communication with the Maxa machine according to flags set by used input.
        /// Fisrt It verifies that the last update proccess finished, otherwise it returns from the function.
        /// If the Maxa is connected it does the following tasks:
        ///     Resets error - if flag is set
        ///     sets new operation mode and setpoint - if flag is set
        ///     reads all relevant registers from Maxa and updates UI
        ///     records the data received to csv file - if flag is set
        ///     updates time of last successful communication
        /// 
        /// if there is a communication problem it checks when was the last successful communication and triggers an error after 30 seconds
        /// </summary>
        private void UpdateValues()
        {
            if (isUpdating) return;
            isUpdating = true;
            if (isConnected)
            {
                try
                {
                    if(isResetErrors)
                    {
                        Maxa.ResetErrors(modbusClient, activeErrors);
                    }

                    if (isNewSetpointAvailable)
                    {
                        Maxa.WriteSetPoints(notifier, modbusClient);
                        bool isSPWritten = isRecord? Maxa.VerifySetpoints(notifier, modbusClient,FileWriter) : Maxa.VerifySetpoints(notifier, modbusClient);
                        if (!isSPWritten) messagesPanel.AddMessage("new setpoint not applied", 0.1f, Brushes.Red);
                        else messagesPanel.AddMessage("new setpoint successfully applied", 0.1f, Brushes.Green);
                        Maxa.WriteOperatinMode(modbusClient, opMode);
                        isNewSetpointAvailable = false;
                    }

                    if (antiLegionellaFlag)
                    {
                        Maxa.RequestAntiLegionellaCycle(modbusClient);
                    }

                    if (forceDefrostFlag)
                    {
                        Maxa.ForceDefrost(modbusClient);
                    }

                    if(plantVentingflag || notifier.plantVentingState == NotifyNewData.PlantVentingState.DEACTIVATING)
                    {
                        if (plantVentingflag)
                            Maxa.ForcePlantVenting(modbusClient, true); // activate plant venting
                        else
                            Maxa.ForcePlantVenting(modbusClient, false); // deactivate plant venting
                    }

                    if (isRecord)
                    {
                        Maxa.VerifySetpoints(notifier, modbusClient, FileWriter);
                        Maxa.UpdateOperationMode(notifier, modbusClient,FileWriter);
                        Maxa.UpdateWaterSystemParameters(notifier, modbusClient, FileWriter);
                        Maxa.UpdateRefrigirationSystemParameters(notifier, modbusClient, FileWriter);
                        Maxa.UpdateReadOnlySetpoints(notifier, modbusClient, FileWriter);
                        activeErrors = Maxa.ReadErrors(notifier, modbusClient, FileWriter);
                        FileWriter.WriteToFile();
                    }
                    else
                    {
                        Maxa.UpdateOperationMode(notifier, modbusClient);
                        Maxa.UpdateWaterSystemParameters(notifier, modbusClient);
                        Maxa.UpdateRefrigirationSystemParameters(notifier, modbusClient);
                        Maxa.UpdateReadOnlySetpoints(notifier, modbusClient);
                        activeErrors = Maxa.ReadErrors(notifier, modbusClient);
                    }

                    ResetErrorsButton.IsEnabled = (activeErrors.Length > 0) ? true : false;

                    lastSuccesfullCommunication = DateTime.Now;
                    notifier.E000 = DataConverter.GetAlarmColor(false);
                }
                catch
                {
                    // Verify continuous communication with the Maxa
                    if (DateTime.Now - lastSuccesfullCommunication > TimeSpan.FromMinutes(miscommunicationMinDuration))
                    {
                        notifier.E000 = DataConverter.GetAlarmColor(true);
                        messagesPanel.AddMessage("Communication error", 0.1f, Brushes.Red);
                        ConnectButton.IsEnabled = true;
                    }
                }
            }
            else
            {
                //ConnectButton.IsEnabled = true;
                SetModbus();
            }
            isUpdating = false;

        }

        /// <summary>
        /// This function is called when the user presses the "connect" button
        /// It calls the SetModbus() function to attempt to connect to the Maxa.
        /// It also starts the timer responsible to periodically sample the Maxa.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClick_ConnectButton(object sender, RoutedEventArgs e)
        {
            SetModbus();
            timer.Start();
        }

        /// <summary>
        /// This function is triggered when the "set path" button is pressed.
        /// It opens a file browser to select a folder for CSV output file.
        /// It generates a FileWriter object with the path set by the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// This function is triggered when the used presses the "start recording" button
        /// It verifies that the user set a path for the file, if not - it notifies the user to do so.
        /// Otherwise it sets the isRecord flag to true to signal the program to start recording.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                messagesPanel.AddMessage("Select folder for scv file", 0.1f);
                MessageBox.Show("Please select a folder");
            }
        }

        // Handling setpoint settings ranges - auto fix
        #region setpoint range handling and auto fixing
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

        /// <summary>
        /// This function checks that a given setpoint (in the form for a testBox) set by the user is within the accepted range.
        /// If the setpoint in the TextBox is out of range - resets the TextBox value to the nearest setpoint limit.
        /// </summary>
        /// <param name="textBox">The TextBox that needs to be checked</param>
        /// <param name="isCoolSP">This flag is used to signal if the TextBox contains a cooling or heating setpoint (true for cool setpoint)</param>
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

        /// <summary>
        /// This function is called when the user presses the "send new settings" button
        /// It gets the selected operation mode from the comboBox and sets true the isNewSetpointsAvailable.
        /// If communication is not established yet - a message is displayed to the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplySetpoints_Click(object sender, RoutedEventArgs e)
        {
            if(modbusClient != null)
            {
                ComboBoxItem opModeItem = (ComboBoxItem)OpModeBox.SelectedItem;
                NotifyNewData.MachinelState machinelState = (NotifyNewData.MachinelState)opModeItem.Content;
                opMode = (int)machinelState;

                isNewSetpointAvailable = true;
            }
            else
            {
                messagesPanel.AddMessage("Connect to the maxa to set new setpoints", 0.1f);
            }
        }

        /// <summary>
        /// This function is called when the user presses the "reset active errors" button
        /// If there are any active errors it sets true the isResetErrors flag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetErrorsButton_Click(object sender, RoutedEventArgs e)
        {
            if(activeErrors.Length > 0)
            {
                isResetErrors = true;
            }
        }

        private void ForceDefrost_Click(object sender, RoutedEventArgs e)
        {
            if(opMode == 2 || opMode == 6) // must be on heating mode to activate defrost 
            {
                forceDefrostFlag = true;
                ForceDefrostButton.IsEnabled = false;
                notifier.PropertyChanged += DefrostEnded;
            }
            else
                messagesPanel.AddMessage("Must be on heating mode to activate defrost", 0.2f);

        }

        private void DefrostEnded(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(notifier.defrostState))
            {
                if (notifier.defrostState == NotifyNewData.DefrostState.CALL_ACTIVE)
                    forceDefrostFlag = false;

                if (notifier.defrostState == NotifyNewData.DefrostState.INACTIVE && forceDefrostFlag == false)
                {
                    notifier.PropertyChanged -= DefrostEnded;
                    ForceDefrostButton.IsEnabled = true;
                    messagesPanel.AddMessage("Defrost cycle ended", 0.2f);
                }
            }
        }

        private void ActivateAntiLegionella_Click(object sender, RoutedEventArgs e)
        {
            antiLegionellaFlag = true;
            ActivateAntiLegionellaButton.IsEnabled = false;
            notifier.PropertyChanged += AntilegionellaEnded;
        }

        private void AntilegionellaEnded(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(notifier.antiLegionellaState))
            {
                if (notifier.antiLegionellaState == NotifyNewData.AntilegionellaState.IN_PROGRESS)
                    antiLegionellaFlag = false;

                else if (antiLegionellaFlag == false)
                {
                    notifier.PropertyChanged -= AntilegionellaEnded;
                    ActivateAntiLegionellaButton.IsEnabled = true;
                    messagesPanel.AddMessage("Defrost cycle ended", 0.2f);
                }
            }
        }

        private void PlantVentingButton_Click(object sender, RoutedEventArgs e)
        {
            if(opMode == 0)     // only available on standby mode
            {
                switch(notifier.plantVentingState)
                {
                    case NotifyNewData.PlantVentingState.NA:
                    case NotifyNewData.PlantVentingState.INACTIVE:
                        PlantVentingButton.IsEnabled = false;
                        plantVentingflag = true;
                        messagesPanel.AddMessage("Activating plant venting", 0.2f);
                        break;

                    case NotifyNewData.PlantVentingState.ACTIVE:
                        plantVentingflag = false;
                        notifier.plantVentingState = NotifyNewData.PlantVentingState.DEACTIVATING;
                        messagesPanel.AddMessage("Deactivating plant venting", 0.2f);
                        break;
                }
            }
        }
    }
}
