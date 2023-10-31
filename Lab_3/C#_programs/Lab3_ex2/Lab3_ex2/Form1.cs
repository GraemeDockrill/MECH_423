using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3_ex2
{
    public partial class Form1 : Form
    {

        public int numberOfDataPoints = 0;
        public bool sendData = false;
        public int direction = 1;                   // 1 = CW, 0 = CCW
        public Form1()
        {
            InitializeComponent();
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
            tbDutyCycle.Value = 0;
            btnCCWdir.Enabled = true;
            btnCWdir.Enabled = false;
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

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
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
            }
        }

        private void cmbComPorts_DropDown(object sender, EventArgs e)
        {
            ComPortUpdate();
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
                        byte[] TxBytes = new Byte[3];
                        TxBytes[0] = Convert.ToByte(255);                   // start byte
                        serialPort1.Write(TxBytes, 0, 1);
                        TxBytes[1] = Convert.ToByte(tbDutyCycle.Value);     // duty cycle byte
                        serialPort1.Write(TxBytes, 1, 1);
                        TxBytes[2] = Convert.ToByte(direction);             // motor direction byte
                        serialPort1.Write(TxBytes, 2, 1);
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            // Auto reconnect functionality
            if(cbAutoReconnect.Checked == true && numberOfDataPoints == 0 && serialPort1.IsOpen == true)
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

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            sendData = true;
        }

        private void btnCCWdir_Click(object sender, EventArgs e)
        {
            // toggle which button is able to be pressed
            btnCCWdir.Enabled = false;
            btnCWdir.Enabled = true;
            sendData = true;
        }

        private void btnCWdir_Click(object sender, EventArgs e)
        {
            // toggle which button is able to be pressed
            btnCWdir.Enabled = false;
            btnCCWdir.Enabled = true;
            sendData = true;
        }
    }
}
