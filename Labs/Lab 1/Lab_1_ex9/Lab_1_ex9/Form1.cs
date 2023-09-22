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

namespace Lab_1_ex9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // when button clicked, either connect or disconnect serial
        private void buttonSerialConnection_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                serialPort1.Open();
                buttonSerialConnection.Text = "Disconnect Serial";
            }
            else
            {
                serialPort1.Close();
                buttonSerialConnection.Text = "Connect Serial";
            }
        }

        // when form1 loaded, start the timer and list available COM ports
        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();

            comboBoxCOMPorts.Items.Clear();
            comboBoxCOMPorts.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
            if (comboBoxCOMPorts.Items.Count == 0 )
            {
                comboBoxCOMPorts.Text = "No COM Ports!";
            }
            else
            {
                comboBoxCOMPorts.SelectedIndex = 0;
            }
        }

        // when dropdown changed, reconfigure serial port if running
        private void comboBoxCOMPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                serialPort1.PortName = comboBoxCOMPorts.Text;
            }
            else
            {
                MessageBox.Show("Current COM port is active, please disconnect and retry!");
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            int newByte = 0;
            int bytestoRead;

            bytestoRead = serialPort1.BytesToRead;

            // loop throyugh data buffer and add bytes to concurrent queue
            while (bytestoRead != 0)
            {
                newByte = serialPort1.ReadByte();
                dataQueue.Enqueue(newByte); // queue new byte
                bytestoRead = serialPort1.BytesToRead;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for(int queueSize = dataQueue.Count; queueSize > 0; queueSize--)
            {
                int item;

                // try to dequeue data, if fails then break
                if (dataQueue.TryDequeue(out item) == false)
                {
                    MessageBox.Show("Error dequeuing serial data!");
                    break;
                }

                // parsing data stream = look for start byte of 255
                if (item == 255)
                {
                    parsingState = parsingByte.start;
                }

                // based on which byte is received, update the Ax, Ay, Az text boxes
                if (parsingState == parsingByte.start) // byte of 255 read
                {
                    parsingState = parsingByte.Ax;
                }
                else if (parsingState == parsingByte.Ax) // parsing Ax byte
                {
                    textBoxAx.Text = item.ToString();
                    parsingState = parsingByte.Ay;
                }
                else if (parsingState == parsingByte.Ay) // parsing Ay byte
                {
                    textBoxAy.Text = item.ToString();
                    parsingState = parsingByte.Az;
                }
                else if (parsingState == parsingByte.Az) // parsing Az byte
                {
                    textBoxAz.Text = item.ToString();
                    parsingState = parsingByte.start;
                }
            }
        }
    }
}
