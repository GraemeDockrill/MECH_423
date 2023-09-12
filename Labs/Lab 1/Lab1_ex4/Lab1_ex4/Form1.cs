using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab1_ex4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // When button is clicked, either disconnect or connect serial
        private void button1_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                serialPort1.Open();
                button1.Text = "Disconnect Serial";
            }
            else
            {
                serialPort1.Close();
                button1.Text = "Connect Serial";
            }
        }

        // When form1 is loaded, clear the dropdown box and load potential serial options. Also enable timer
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start(); // Enable timer1

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            if (comboBox1.Items.Count == 0)
                comboBox1.Text = "No COM ports!";
            else
                comboBox1.SelectedIndex = 0;
        }

        // When the dropdown box is changed, reconfigure the serial port name if not running
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!serialPort1.IsOpen)
                serialPort1.PortName = comboBox1.Text;
            else
            {
                MessageBox.Show("Current COM port is active, disconnect and retry!");
            }
        }

        // When data is received over serial...
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            int newByte = 0;
            int bytesToRead;

            bytesToRead = serialPort1.BytesToRead;

            // Loop  through data buffer and add bytes to a concurrent queue
            while (bytesToRead != 0)
            {
                newByte = serialPort1.ReadByte();
                serialDataString = serialDataString + newByte.ToString() + ", ";
                bytesToRead = serialPort1.BytesToRead;
            } 
        }

        // On tick of timer, update text boxes, and loop through concurrent queue and add to serialDataString
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                textBoxBytesToRead.Text = serialPort1.BytesToRead.ToString();
            }

            textBoxTempStringLength.Text = serialDataString.Length.ToString();
            textBoxSerialDataStream.AppendText(serialDataString);
            serialDataString = "";
        }
    }
}
