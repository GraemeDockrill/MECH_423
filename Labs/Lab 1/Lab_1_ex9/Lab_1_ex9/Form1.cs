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
using System.Diagnostics;

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

            // randomize car speed
            blueCarSpeed = carBaseSpeed * rnd.Next(1, 3);
            greenCarSpeed = carBaseSpeed * rnd.Next(1, 3);
            yellowCarSpeed = carBaseSpeed * rnd.Next(1, 3);
            purpleCarSpeed = carBaseSpeed * rnd.Next(1, 3);

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

                // parsing data stream = look for start byte of 255---------------------------
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
                    AxScaled = (-1.0 / 25) * item + 5.0; // scale orientation of X axis to -1 to +1
                    textBox1.Text = AxScaled.ToString();
                }
                else if (parsingState == parsingByte.Ay) // parsing Ay byte
                {
                    textBoxAy.Text = item.ToString();
                    parsingState = parsingByte.Az;
                    AyScaled = (1.0 / 25) * item - 5.0; // scale orientation of Y axis to -1 to +1
                    textBox2.Text = AyScaled.ToString();
                }
                else if (parsingState == parsingByte.Az) // parsing Az byte
                {
                    textBoxAz.Text = item.ToString();
                    parsingState = parsingByte.start;
                }

                // player movement with the MSP430 board-----------------------------------
                int x = pictureBoxPlayerCar.Location.X;
                int y = pictureBoxPlayerCar.Location.Y;
                
                // player collision with right edge
                if (x > pictureBoxRoad1.Location.X + pictureBoxGameBorder.Width - pictureBoxPlayerCar.Width)
                {
                    x = pictureBoxRoad1.Location.X + pictureBoxGameBorder.Width - pictureBoxPlayerCar.Width;

                    pictureBoxPlayerCar.Location = new Point(x, y);
                }
                // player collision with bottom edge
                if (y > pictureBoxGameBorder.Location.Y + pictureBoxGameBorder.Height - pictureBoxPlayerCar.Height)
                {
                    y = pictureBoxGameBorder.Location.Y + pictureBoxGameBorder.Height - pictureBoxPlayerCar.Height;

                    pictureBoxPlayerCar.Location = new Point(x, y);
                }
                // player collision with left edge
                if (x < pictureBoxGameBorder.Location.X)
                {
                    x = pictureBoxGameBorder.Location.X;

                    pictureBoxPlayerCar.Location = new Point(x, y);
                }
                // player collision with top edge
                if (y < pictureBoxGameBorder.Location.Y)
                {
                    y = pictureBoxGameBorder.Location.Y;

                    pictureBoxPlayerCar.Location = new Point(x, y);
                }
                
                // draw player location
                pictureBoxPlayerCar.Location = new Point(x + Convert.ToInt32(step * AxScaled), y + Convert.ToInt32(step * AyScaled));

                // scrolling background-----------------------------------------
                road1X = pictureBoxRoad1.Location.X;
                road1Y = pictureBoxRoad1.Location.Y;
                road2X = pictureBoxRoad2.Location.X;
                road2Y = pictureBoxRoad2.Location.Y;
                road3X = pictureBoxRoad3.Location.X;
                road3Y = pictureBoxRoad3.Location.Y;

                road1Y += roadSpeed;
                road2Y += roadSpeed;
                road3Y += roadSpeed;

                if (road1Y > pictureBoxGameBorder.Location.Y + pictureBoxGameBorder.Height)
                {
                    road1Y = road3Y - 630;
                }
                if (road2Y > pictureBoxGameBorder.Location.Y + pictureBoxGameBorder.Height)
                {
                    road2Y = road1Y - 630;
                }
                if (road3Y > pictureBoxGameBorder.Location.Y + pictureBoxGameBorder.Height)
                {
                    road3Y = road2Y - 630;
                }

                pictureBoxRoad1.Location = new Point(road1X, road1Y);
                pictureBoxRoad2.Location = new Point(road2X, road2Y);
                pictureBoxRoad3.Location = new Point(road3X, road3Y);

                // scrolling car movement------------------------------------------
                blueCarX = pictureBoxBlueCar.Location.X;
                blueCarY = pictureBoxBlueCar.Location.Y;
                greenCarX = pictureBoxGreenCar.Location.X;
                greenCarY = pictureBoxGreenCar.Location.Y;
                yellowCarX = pictureBoxYellowCar.Location.X;
                yellowCarY = pictureBoxYellowCar.Location.Y;
                purpleCarX = pictureBoxPurpleCar.Location.X;
                purpleCarY = pictureBoxPurpleCar.Location.Y;

                blueCarY += blueCarSpeed;
                greenCarY += greenCarSpeed;
                yellowCarY += yellowCarSpeed;
                purpleCarY += purpleCarSpeed;

                
                // respawning cars when they go off the screen
                if(blueCarY > pictureBoxGameBorder.Location.Y + pictureBoxGameBorder.Height)
                {
                    blueCarY = pictureBoxGameBorder.Location.Y - pictureBoxBlueCar.Height;
                    blueCarSpeed = carBaseSpeed * rnd.Next(2, 4);
                }
                if (greenCarY > pictureBoxGameBorder.Location.Y + pictureBoxGameBorder.Height)
                {
                    greenCarY = pictureBoxGameBorder.Location.Y - pictureBoxGreenCar.Height;
                    greenCarSpeed = carBaseSpeed * rnd.Next(2, 4);
                }
                if (yellowCarY > pictureBoxGameBorder.Location.Y + pictureBoxGameBorder.Height)
                {
                    yellowCarY = pictureBoxGameBorder.Location.Y - pictureBoxYellowCar.Height;
                    yellowCarSpeed = carBaseSpeed * rnd.Next(2, 4);
                }
                if (purpleCarY > pictureBoxGameBorder.Location.Y + pictureBoxGameBorder.Height)
                {
                    purpleCarY = pictureBoxGameBorder.Location.Y - pictureBoxPurpleCar.Height;
                    purpleCarSpeed = carBaseSpeed * rnd.Next(2, 4);
                }

                pictureBoxBlueCar.Location = new Point(blueCarX, blueCarY);
                pictureBoxGreenCar.Location = new Point(greenCarX, greenCarY);
                pictureBoxYellowCar.Location = new Point(yellowCarX, yellowCarY);
                pictureBoxPurpleCar.Location = new Point(purpleCarX, purpleCarY);

                // collision with cars----------------------------------------


            }
        }
    }
}
