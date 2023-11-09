using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab3_ex2
{
    public partial class Form1 : Form
    {

        public int numberOfDataPoints = 0;
        public bool sendData = false;
        public int cmdByte0 = 0;                    // 0 = WS CW, 1 = WS CCW, 2 = HS CW, 3 = HS CCW, 4 = WS CW Cont., 5 = WS CCW Cont., 6 = HS CW Cont., 7 = HW CCW Cont. for STEPPER MOTOR
        public int cmdByte1 = 1;                    // 0 = CW, 1 = CCW for DC MOTOR
        public int stepSpeed = 0;                   // stepping speed for STEPPER MOTOR
        public int dutyCycle = 0;                   // duty cycle for the DC MOTOR
        public bool wholeStepping = true;
        public bool halfStepping = false;

        // variables for encoder parsing
        public double timeSinceCOMConnect = 0;
        public int encoderPulsesCW = 0;
        public int encoderPulsesCCW = 0;
        public double encoderPulseTotal = 0.0;
        public int encoderPPR = 400;
        public double DCPulleyRadius = 7.5;

        public int maxXValueDCPositionTime;
        public int maxXValueDCVelocityTime;

        public double DCshaftPosition = 0;
        public double DCPreviousShaftPosition = 0;
        public double DCshaftVelocity = 0;

        private Series seriesDCPosition, seriesDCVelocity;

        public enum encoderByte
        {
            startByte,
            TA0RLO,
            TA1RLO
        }

        public encoderByte encoderState = encoderByte.startByte;

        public Form1()
        {
            InitializeComponent();
            InitializeDCPositionChart();
            InitializeDCVelocityChart();
        }

        // function for creating position chart
        public void InitializeDCPositionChart()
        {
            ChartArea chartAreaDCPosition = new ChartArea();
            chartAreaDCPosition.AxisX.Title = "Time [s]";
            chartAreaDCPosition.AxisY.Title = "DC Motor Position [mm]";
            chartDCPosition.ChartAreas.Add(chartAreaDCPosition);

            seriesDCPosition = new Series();
            seriesDCPosition.ChartType = SeriesChartType.Line;

            chartDCPosition.Series.Add(seriesDCPosition);

            chartDCPosition.ChartAreas[0].AxisX.Interval = 10;      // set X-axis interval to 10
        }

        // function for creating velocity chart
        public void InitializeDCVelocityChart()
        {
            ChartArea chartAreaDCVelocity = new ChartArea();
            chartAreaDCVelocity.AxisX.Title = "Time [s]";
            chartAreaDCVelocity.AxisY.Title = "DC Motor Velocity [mm/s]";
            chartDCVelocity.ChartAreas.Add(chartAreaDCVelocity);

            seriesDCVelocity = new Series();
            seriesDCVelocity.ChartType = SeriesChartType.Line;

            chartDCVelocity.Series.Add(seriesDCVelocity);

            chartDCVelocity.ChartAreas[0].AxisX.Interval = 10;      // set X-axis interval to 10
        }

        public void ComPortUpdate()
        {
            cmbComPorts.Items.Clear();
            string[] comPortArray = System.IO.Ports.SerialPort.GetPortNames();
            Array.Reverse(comPortArray);
            cmbComPorts.Items.AddRange(comPortArray);
            if (cmbComPorts.Items.Count != 0)
                cmbComPorts.SelectedIndex = 0;
            else
                cmbComPorts.Text = "No Ports Found!";
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            lblDataRate.Visible = false;
            cbComResponse.Checked = true;
            ComPortUpdate();
        }

        private void btnComConnect_Click(object sender, EventArgs e)
        {
            if (btnComConnect.Text == "Connect")
            {
                if(cmbComPorts.Items.Count > 0)
                {
                    try
                    {
                        serialPort1.BaudRate = Convert.ToInt16(txtBaudRate.Text);
                        serialPort1.PortName = cmbComPorts.SelectedItem.ToString();
                        serialPort1.Open();
                        btnComConnect.Text = "Disconnect";
                        timer1.Enabled = true;
                        lblDataRate.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                try
                {
                    serialPort1.Close();
                    btnComConnect.Text = "Connect";
                    timer1.Enabled = false;
                    lblDataRate.Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            while (serialPort1.IsOpen && serialPort1.BytesToRead != 0)
            {
                int currentByte = serialPort1.ReadByte();
                numberOfDataPoints++;
                if (cbComResponse.Checked)
                {
                    this.BeginInvoke(new EventHandler(delegate
                    {
                        txtComOutput.AppendText(currentByte.ToString() + ", ");
                    }));
                }

                if (currentByte == 255)
                    encoderState = encoderByte.startByte;

                // figuring out which state to read data from
                if (encoderState == encoderByte.startByte)
                {
                    encoderState = encoderByte.TA0RLO;
                    timeSinceCOMConnect += 0.0655;
                }
                else if (encoderState == encoderByte.TA0RLO)
                {
                    encoderState = encoderByte.TA1RLO;
                    encoderPulsesCW += currentByte;
                }
                else if (encoderState == encoderByte.TA1RLO)
                {
                    encoderState = encoderByte.startByte;
                    encoderPulsesCCW += currentByte;

                    encoderPulseTotal = encoderPulsesCW - encoderPulsesCCW;                                 // total encoder pulses including the received message

                    DCshaftPosition = encoderPulseTotal * 4 * DCPulleyRadius / encoderPPR;                  // calculate the DC motor shaft position [pulses * (4 encoder slits per pulse) * (DC pulley radius) / (encoder PPR)]
                    DCshaftVelocity = (DCshaftPosition - DCPreviousShaftPosition) / 0.0655;                 // calculate the DC motor shaft speed

                    // store current DC shaft position
                    DCPreviousShaftPosition = DCshaftPosition;

                    // plot the motor position graph
                    chartDCPosition.Invoke((MethodInvoker)delegate
                    {
                        // plot new data
                        seriesDCPosition.Points.AddXY(timeSinceCOMConnect, DCshaftPosition);

                        // scroll the data window
                        int minXValue = ((int) timeSinceCOMConnect - 50 > 0) ? (int) timeSinceCOMConnect - 50 - (int) timeSinceCOMConnect % 10 : 0;
                        maxXValueDCPositionTime = ((int) timeSinceCOMConnect + 10) % 10 != 0 ? maxXValueDCPositionTime : (int) timeSinceCOMConnect + 10;
                        chartDCPosition.ChartAreas[0].AxisX.Minimum = minXValue;
                        chartDCPosition.ChartAreas[0].AxisX.Maximum = maxXValueDCPositionTime;

                        // redraw graph
                        chartDCPosition.Invalidate();
                    });

                    // plot the motor velocity graph
                    chartDCVelocity.Invoke((MethodInvoker)delegate
                    {
                        // plot new data
                        seriesDCVelocity.Points.AddXY(timeSinceCOMConnect, DCshaftVelocity);

                        // scroll the data window
                        int minXValue = ((int) timeSinceCOMConnect - 50 > 0) ? (int) timeSinceCOMConnect - 50 - (int)timeSinceCOMConnect % 10 : 0;
                        maxXValueDCVelocityTime = ((int) timeSinceCOMConnect + 10) % 10 != 0 ? maxXValueDCVelocityTime : (int) timeSinceCOMConnect + 10;
                        chartDCVelocity.ChartAreas[0].AxisX.Minimum = minXValue;
                        chartDCVelocity.ChartAreas[0].AxisX.Maximum = maxXValueDCVelocityTime;

                        // redraw graph
                        chartDCVelocity.Invalidate();
                    });
                }
            }
        }

        private void cmbComPorts_DropDown(object sender, EventArgs e)
        {
            ComPortUpdate();
        }

        private void tbDCDutyCycle_ValueChanged(object sender, EventArgs e)
        {
            if (tbDCDutyCycle.Value < tbDCDutyCycle.Maximum / 2)
            {
                // setting speed and direction for CW travel
                cmdByte1 = 0;
                dutyCycle = -(100 / (tbDCDutyCycle.Maximum / 2)) * tbDCDutyCycle.Value + 100;
            }
            else if (tbDCDutyCycle.Value > tbDCDutyCycle.Maximum / 2)
            {
                // setting speed and direction for CCW travel
                cmdByte1 = 1;
                dutyCycle = (100 / (tbDCDutyCycle.Maximum / 2)) * tbDCDutyCycle.Value - 100;
            }
            else
                dutyCycle = 0;
            sendData = true;
        }

        private void btnStopDC_Click(object sender, EventArgs e)
        {
            // set 0 DC motor speed (middle of slider) and send the command
            tbDCDutyCycle.Value = tbDCDutyCycle.Maximum / 2;
            sendData = true;
        }

        private void btnStopStep_Click(object sender, EventArgs e)
        {
            // setting stepper motor speed to 0 (middle of slider) & sending command
            tbStepperSpeed.Value = tbStepperSpeed.Maximum / 2;
            stepSpeed = 0;
            sendData = true;
        }

        private void btnContWhole_Click(object sender, EventArgs e)
        {
            // swtiching to stepper whole stepping
            halfStepping = false;
            wholeStepping = true;
            sendData = true;
        }

        private void btnContHalf_Click(object sender, EventArgs e)
        {
            // switching to stepper half stepping
            wholeStepping = false;
            halfStepping = true;
            sendData = true;
        }

        private void tbStepperSpeed_ValueChanged(object sender, EventArgs e)
        {
            if (tbStepperSpeed.Value < tbStepperSpeed.Maximum / 2)
            {
                // setting speed and direction for CW travel

                // check if half stepping or whole stepping
                if (wholeStepping)
                    cmdByte0 = 4;
                else if (halfStepping)
                    cmdByte0 = 6;

                stepSpeed = -(100 / (tbStepperSpeed.Maximum / 2)) * tbStepperSpeed.Value + 100;
            }
            else if (tbStepperSpeed.Value > tbStepperSpeed.Maximum / 2)
            {
                // setting speed and direction for CCW travel

                // check if half stepping or whole stepping
                if (wholeStepping)
                    cmdByte0 = 5;
                else if (halfStepping)
                    cmdByte0 = 7;
                
                stepSpeed = (100 / (tbStepperSpeed.Maximum / 2)) * tbStepperSpeed.Value - 100;
            }
            else
                stepSpeed = 0;
            sendData = true;
        }

        private void btnCWStepWhole_Click(object sender, EventArgs e)
        {
            cmdByte0 = 0;
            sendData = true;
        }

        private void btnCCWStepWhole_Click(object sender, EventArgs e)
        {
            cmdByte0 = 1;
            sendData = true;
        }

        private void btnCWStepHalf_Click(object sender, EventArgs e)
        {
            cmdByte0 = 2;
            sendData = true;
        }

        private void btnCCWStepHalf_Click(object sender, EventArgs e)
        {
            cmdByte0 = 3;
            sendData = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //lblDataRate.Text = "Incoming data rate = " + numberOfDataPoints.ToString() + " bytes per second";

            // sending UART data to MCU
            if (sendData)
            {
                try
                {
                    if (serialPort1.IsOpen)
                    {
                        byte[] TxBytes = new Byte[5];
                        TxBytes[0] = Convert.ToByte(255);                   // start byte
                        serialPort1.Write(TxBytes, 0, 1);
                        TxBytes[1] = Convert.ToByte(cmdByte0);              // command byte for stepper control
                        serialPort1.Write(TxBytes, 1, 1);
                        TxBytes[2] = Convert.ToByte(cmdByte1);              // command byte for DC motor control
                        serialPort1.Write(TxBytes, 2, 1);
                        TxBytes[3] = Convert.ToByte(stepSpeed);             // stepper speed byte
                        serialPort1.Write(TxBytes, 3, 1);
                        TxBytes[4] = Convert.ToByte(dutyCycle);             // DC motor duty byte
                        serialPort1.Write(TxBytes, 4, 1);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                sendData = false;                                           // after sending message, disable sending messages
            }

            // Auto reconnect functionality
            if (cbAutoReconnect.Checked == true && numberOfDataPoints == 0 && serialPort1.IsOpen == true)
            {
                try
                {
                    serialPort1.Close();
                    serialPort1.BaudRate = Convert.ToInt16(txtBaudRate.Text);
                    serialPort1.PortName = cmbComPorts.SelectedItem.ToString();
                    serialPort1.Open();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
