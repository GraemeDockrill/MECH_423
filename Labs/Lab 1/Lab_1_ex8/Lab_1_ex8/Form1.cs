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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using System.IO.Ports;
using System.Runtime.Remoting.Lifetime;

namespace Lab_1_ex8
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
            if (!serialPort1.IsOpen)
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
                dataQueue.Enqueue(newByte); // queue the new byte
                //serialDataString = serialDataString + newByte.ToString() + ", ";
                bytesToRead = serialPort1.BytesToRead;
            }
        }

        // On tick of timer, update text boxes, and loop through concurrent queue and add to serialDataString
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen) // read bytes if port open
            {
                textBoxBytesToRead.Text = serialPort1.BytesToRead.ToString();
            }

            textBoxItemsInQueue.Text = dataQueue.Count.ToString(); // update items in queue text box

            // loop for dequeueing data one byte at a time and processing
            for (int queueSize = dataQueue.Count; queueSize > 0; queueSize--)
            {
                int item; // the dequeued byte

                // try to dequeue data, if fails then break
                if (dataQueue.TryDequeue(out item) == false)
                {
                    MessageBox.Show("Error dequeueing serial data!");
                    break;
                }

                serialDataString = serialDataString + item.ToString() + ", "; // add dequeued item to serialDataString


                // parsing data stream - look for start byte of 255
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
                    textBoxAccelerationX.Text = item.ToString();
                    dataQueueAx.Enqueue(item);

                    // updating Ax average
                    AxSum += item;
                    Ax = item;
                    if (dataQueueAx.Count > averagePeriod)
                    {
                        //int dequeuedAx;
                        if (dataQueueAx.TryDequeue(out dequeuedAx) == false)
                            MessageBox.Show("Error Dequeuing Ax serial data!");
                        AxSum -= dequeuedAx;
                        textBoxAverageAx.Text = (AxSum / averagePeriod).ToString();
                    }

                    // updating Ax orientation
                    if (item >= 150)
                        textBoxOrientation.Text = ("+X");
                    else if (item <= 110)
                        textBoxOrientation.Text = ("-X");

                    // write Ax to file
                    if (Checked == 1) outputFile.Write(item.ToString() + ", ");

                    parsingState = parsingByte.Ay;
                }
                else if (parsingState == parsingByte.Ay) // parsing Ay byte
                {
                    textBoxAccelerationY.Text = item.ToString();
                    dataQueueAy.Enqueue(item);

                    // updating Ay average
                    AySum += item;
                    Ay = item;
                    if (dataQueueAy.Count > averagePeriod)
                    {
                        //int dequeuedAy;
                        if (dataQueueAy.TryDequeue(out dequeuedAy) == false)
                            MessageBox.Show("Error Dequeuing Ax serial data!");
                        AySum -= dequeuedAy;
                        textBoxAverageAy.Text = (AySum / averagePeriod).ToString();
                    }

                    // updating Ay orientation
                    if (item >= 150)
                        textBoxOrientation.Text = ("+Y");
                    else if (item <= 110)
                        textBoxOrientation.Text = ("-Y");

                    // write Ay to file
                    if (Checked == 1) outputFile.Write(item.ToString() + ", ");

                    parsingState = parsingByte.Az;
                }
                else if (parsingState == parsingByte.Az) // parsing Az byte
                {
                    textBoxAccelerationZ.Text = item.ToString();
                    dataQueueAz.Enqueue(item);

                    // updating Az average
                    AzSum += item;
                    Az = item;
                    if (dataQueueAz.Count > averagePeriod)
                    {
                        //int dequeuedAz;
                        if (dataQueueAz.TryDequeue(out dequeuedAz) == false)
                            MessageBox.Show("Error Dequeuing Ax serial data!");
                        AzSum -= dequeuedAz;
                        textBoxAverageAz.Text = (AzSum / averagePeriod).ToString();
                    }

                    // updating Az orientation
                    if (item >= 150)
                        textBoxOrientation.Text = ("+Z");
                    else if (item <= 110)
                        textBoxOrientation.Text = ("-Z");

                    // write Az and time to file
                    if (Checked == 1) outputFile.Write(item.ToString() + ", " + DateTime.Now + "\n");

                    parsingState = parsingByte.start;
                }
            }


            // state machine for gestures - sensing the negative acceleration
            if (state == gestureState.waitForData) // wait for new data
            {
                textBoxGesture.Text = ("");
                textBoxGestureState.Text = ("wait for data");

                if (Ax < gestureAcceleration) // +X
                {
                    wait = 0;
                    state = gestureState.punch;
                }
                else if (Az < gestureAcceleration) // +Z
                {
                    wait = 0;
                    state = gestureState.initiateHighPunch;
                }
            }
            else if(state == gestureState.punch) // user punches forward +X
            {
                wait++; // wait for 10 data points
                textBoxGestureState.Text = ("punch");

                if (Ay < gestureAcceleration) // +Y
                {
                    wait = 0;
                    state = gestureState.initiateRightHook;
                }
                else if (wait >= waitCycles) // user only did a simple punch
                {
                    textBoxGesture.Text = ("Simple Punch");

                    if (wait >= waitCycles * 2) // return to state 0
                    {
                        wait = 0;
                        state = gestureState.waitForData;
                    }
                }
            }
            else if(state == gestureState.initiateRightHook) // user punches forward then left
            {
                wait++;
                textBoxGestureState.Text = ("initiate Right Hook");

                if (Az < gestureAcceleration) // +Z
                {
                    wait = 0;
                    state = gestureState.rightHook;
                }
                else if (wait >= waitCycles) // return to state 0
                {
                    wait = 0;
                    state = gestureState.waitForData;
                }
            }
            else if(state == gestureState.rightHook) // user completes right hook
            {
                wait++;
                textBoxGesture.Text = ("Right Hook");
                textBoxGestureState.Text = ("Right Hook");

                if (wait >= waitCycles) // return to state 0
                {
                    wait = 0;
                    state = gestureState.waitForData;
                }
            }
            else if(state == gestureState.initiateHighPunch) // user initiates high punch
            {
                wait++;
                textBoxGestureState.Text = ("Initiate High Punch");

                if (Ax < gestureAcceleration) // +X
                {
                    wait = 0;
                    state = gestureState.highPunch;
                }
                else if (wait >= waitCycles) // return to state 0
                {
                    wait = 0;
                    state = gestureState.waitForData;
                }              
            }
            else if(state == gestureState.highPunch) // user completes high punch
            {
                wait++;
                textBoxGesture.Text = ("High Punch");
                textBoxGestureState.Text = ("High Punch");

                if (wait >= waitCycles) // return to state 0
                {
                    wait = 0;
                    state = gestureState.waitForData;
                }
            }

            // serialDataString string to temporarily hold data before adding to serialDataStream text box
            textBoxTempStringLength.Text = serialDataString.Length.ToString();
            textBoxSerialDataStream.AppendText(serialDataString);
            serialDataString = "";
        }

        // Changes the file path name to save data to (checks if valid)
        private void buttonSelectFilename_Click(object sender, EventArgs e)
        {
            // prompt user for a filename and location
            SaveFileDialog myDialogBox = new SaveFileDialog();
            myDialogBox.InitialDirectory = @"C:\C# Code";
            myDialogBox.ShowDialog();
            textBoxFileName.Text = myDialogBox.FileName.ToString() + ".CSV";

            // create file with a valid file name
            if (checkBoxSavetoFile.Checked == true)
            {
                outputFile = new StreamWriter(textBoxFileName.Text);
                outputFile.Write("Ax, Ay, Az, Timestamp\n"); // print header in outputFile
                Checked = 1; // sets checked flag to record data
            }
        }

        private void checkBoxSavetoFile_CheckStateChanged(object sender, EventArgs e)
        {
            if (checkBoxSavetoFile.Checked == false)
            {
                if (outputFile != null)
                {
                    outputFile.Close();
                    outputFile = null;
                }
                Checked = 0;
            }
        }
    }
}
