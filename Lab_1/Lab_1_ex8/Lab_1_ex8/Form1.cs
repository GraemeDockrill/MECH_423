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

        public double convertZToG(double input)
        {
            double g = input * 1.0 / 27.0 + (1.0 - 154.0 / 27.0);
            return g;
        }

        public double convertXYToG(double input)
        {
            double g = input * 1.0 / 25.0 - 5.0;
            return g;
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
                    maxdataQueueAx.Enqueue(item);
                    stddataQueueAx.Enqueue(item);

                    // updating Ax average
                    AxSum += item;
                    Ax = item;
                    if (dataQueueAx.Count > averagePeriod)
                    {
                        //int dequeuedAx;
                        if (dataQueueAx.TryDequeue(out dequeuedAx) == false)
                            MessageBox.Show("Error Dequeuing Ax serial data!");
                        AxSum -= dequeuedAx;
                        textBoxAverageAx.Text = convertXYToG((AxSum / averagePeriod)).ToString(); // display average accel in g
                    }

                    // finding max
                    if (maxdataQueueAx.Count > maxPeriod)
                    {
                        // remove first byte
                        if (maxdataQueueAx.TryDequeue(out dequeuedMaxAx) == false)
                            MessageBox.Show("Error Dequeuing Ax serial data!");

                        // loop through and find max
                        for(int maxAxcount = maxdataQueueAx.Count; maxAxcount > 0; maxAxcount--)
                        {
                            int item1;
                            if (maxdataQueueAx.TryDequeue(out item1) == false)
                                MessageBox.Show("Error Dequeuing Ax serial data!");

                            if(item1 > maxAx)
                            {
                                maxAx = item1;
                            }

                            maxdataQueueAx.Enqueue(item1);
                        }

                        textBoxAxMax.Text = convertXYToG(maxAx).ToString();
                    }


                    // finding std
                    if (stddataQueueAx.Count > averagePeriod)
                    {
                        stdSumAx = 0;

                        // remove first byte
                        if (maxdataQueueAx.TryDequeue(out dequeuedMaxAx) == false)
                            MessageBox.Show("Error Dequeuing Ax serial data!");

                        // loop through and find max
                        for (int stdAxcount = stddataQueueAx.Count; stdAxcount > 0; stdAxcount--)
                        {
                            int item1;
                            if (stddataQueueAx.TryDequeue(out item1) == false)
                                MessageBox.Show("Error Dequeuing Ax serial data!");

                            stdSumAx += Math.Pow(convertXYToG(item1) - convertXYToG((AxSum / averagePeriod)), 2);
                        }
                        
                        textBoxStdAx.Text = Math.Pow((stdSumAx/averagePeriod), 0.5).ToString();
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
                    maxdataQueueAy.Enqueue(item);
                    stddataQueueAy.Enqueue(item);

                    // updating Ay average
                    AySum += item;
                    Ay = item;
                    if (dataQueueAy.Count > averagePeriod)
                    {
                        //int dequeuedAy;
                        if (dataQueueAy.TryDequeue(out dequeuedAy) == false)
                            MessageBox.Show("Error Dequeuing Ax serial data!");
                        AySum -= dequeuedAy;
                        textBoxAverageAy.Text = convertXYToG((AySum / averagePeriod)).ToString(); // display average accel in g
                    }


                    // finding max
                    if (maxdataQueueAy.Count > maxPeriod)
                    {
                        // remove first byte
                        if (maxdataQueueAy.TryDequeue(out dequeuedMaxAy) == false)
                            MessageBox.Show("Error Dequeuing Ax serial data!");

                        // loop through and find max
                        for (int maxAycount = maxdataQueueAy.Count; maxAycount > 0; maxAycount--)
                        {
                            int item1;
                            if (maxdataQueueAy.TryDequeue(out item1) == false)
                                MessageBox.Show("Error Dequeuing Ax serial data!");

                            if (item1 > maxAy)
                            {
                                maxAy = item1;
                            }

                            maxdataQueueAy.Enqueue(item1);
                        }

                        textBoxAyMax.Text = convertXYToG(maxAy).ToString();
                    }


                    // finding std
                    if (stddataQueueAy.Count > averagePeriod)
                    {
                        stdSumAy = 0;

                        // remove first byte
                        if (maxdataQueueAy.TryDequeue(out dequeuedMaxAy) == false)
                            MessageBox.Show("Error Dequeuing Ax serial data!");

                        // loop through and find max
                        for (int stdAycount = stddataQueueAy.Count; stdAycount > 0; stdAycount--)
                        {
                            int item1;
                            if (stddataQueueAy.TryDequeue(out item1) == false)
                                MessageBox.Show("Error Dequeuing Ay serial data!");

                            stdSumAy += Math.Pow(convertXYToG(item1) - convertXYToG((AySum / averagePeriod)), 2);
                        }

                        textBoxStdAy.Text = Math.Pow((stdSumAy / averagePeriod), 0.5).ToString();
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
                    maxdataQueueAz.Enqueue(item);
                    stddataQueueAz.Enqueue(item);

                    // updating Az average
                    AzSum += item;
                    Az = item;
                    if (dataQueueAz.Count > averagePeriod)
                    {
                        //int dequeuedAz;
                        if (dataQueueAz.TryDequeue(out dequeuedAz) == false)
                            MessageBox.Show("Error Dequeuing Ax serial data!");
                        AzSum -= dequeuedAz;
                        textBoxAverageAz.Text = convertZToG((AzSum / averagePeriod)).ToString(); // display average accel in g
                    }


                    // finding max
                    if (maxdataQueueAz.Count > maxPeriod)
                    {
                        // remove first byte
                        if (maxdataQueueAz.TryDequeue(out dequeuedMaxAz) == false)
                            MessageBox.Show("Error Dequeuing Ax serial data!");

                        // loop through and find max
                        for (int maxAzcount = maxdataQueueAz.Count; maxAzcount > 0; maxAzcount--)
                        {
                            int item1;
                            if (maxdataQueueAz.TryDequeue(out item1) == false)
                                MessageBox.Show("Error Dequeuing Ax serial data!");

                            if (item1 > maxAz)
                            {
                                maxAz = item1;
                            }

                            maxdataQueueAz.Enqueue(item1);
                        }

                        textBoxAzMax.Text = convertXYToG(maxAz).ToString();
                    }


                    // finding std
                    if (stddataQueueAz.Count > averagePeriod)
                    {
                        stdSumAz = 0;

                        // remove first byte
                        if (maxdataQueueAz.TryDequeue(out dequeuedMaxAz) == false)
                            MessageBox.Show("Error Dequeuing Ax serial data!");

                        // loop through and find max
                        for (int stdAzcount = stddataQueueAz.Count; stdAzcount > 0; stdAzcount--)
                        {
                            int item1;
                            if (stddataQueueAz.TryDequeue(out item1) == false)
                                MessageBox.Show("Error Dequeuing Ay serial data!");

                            stdSumAz += Math.Pow(convertZToG(item1) - convertZToG((AzSum / averagePeriod)), 2);
                        }

                        textBoxStdAz.Text = Math.Pow((stdSumAz / averagePeriod), 0.5).ToString();
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


            //// state machine for gestures - sensing the negative acceleration
            //if (state == gestureState.waitForData) // wait for new data
            //{
            //    textBoxGesture.Text = ("");
            //    textBoxGestureState.Text = ("wait for data");

            //    if (Ax < gestureAcceleration) // +X
            //    {
            //        wait = 0;
            //        state = gestureState.punch;
            //    }
            //    else if( Ax > 200)
            //    {
            //        wait = 0;
            //        state = gestureState.backpunch;
            //    }
            //    else if (Az < gestureAcceleration) // +Z
            //    {
            //        wait = 0;
            //        state = gestureState.initiateHighPunch;
            //    }
            //}
            //else if(state == gestureState.punch) // user punches forward +X
            //{
            //    wait++; // wait for 10 data points
            //    textBoxGestureState.Text = ("punch");

            //    if (Ay < gestureAcceleration) // +Y
            //    {
            //        wait = 0;
            //        state = gestureState.initiateRightHook;
            //    }
            //    else if (wait >= waitCycles) // user only did a simple punch
            //    {
            //        textBoxGesture.Text = ("Simple Punch");

            //        if (wait >= waitCycles * 2) // return to state 0
            //        {
            //            wait = 0;
            //            state = gestureState.waitForData;
            //        }
            //    }
            //}
            //else if(state == gestureState.initiateRightHook) // user punches forward then left
            //{
            //    wait++;
            //    textBoxGestureState.Text = ("initiate Right Hook");

            //    if (Az < gestureAcceleration) // +Z
            //    {
            //        wait = 0;
            //        state = gestureState.rightHook;
            //    }
            //    else if (wait >= waitCycles) // return to state 0
            //    {
            //        wait = 0;
            //        state = gestureState.waitForData;
            //    }
            //}
            //else if(state == gestureState.rightHook) // user completes right hook
            //{
            //    wait++;
            //    textBoxGesture.Text = ("Right Hook");
            //    textBoxGestureState.Text = ("Right Hook");

            //    if (wait >= waitCycles) // return to state 0
            //    {
            //        wait = 0;
            //        state = gestureState.waitForData;
            //    }
            //}
            //else if(state == gestureState.initiateHighPunch) // user initiates high punch
            //{
            //    wait++;
            //    textBoxGestureState.Text = ("Initiate High Punch");

            //    if (Ax < gestureAcceleration) // +X
            //    {
            //        wait = 0;
            //        state = gestureState.highPunch;
            //    }
            //    else if (wait >= waitCycles) // return to state 0
            //    {
            //        wait = 0;
            //        state = gestureState.waitForData;
            //    }              
            //}
            //else if(state == gestureState.highPunch) // user completes high punch
            //{
            //    wait++;
            //    textBoxGesture.Text = ("High Punch");
            //    textBoxGestureState.Text = ("High Punch");

            //    if (wait >= waitCycles) // return to state 0
            //    {
            //        wait = 0;
            //        state = gestureState.waitForData;
            //    }
            //}
            //else if (state == gestureState.backpunch) // user completes backwards punch
            //{
            //    wait++;
            //    textBoxGesture.Text = ("Back Punch");
            //    textBoxGestureState.Text = ("Back Punch");

            //    if (wait >= waitCycles) // return to state 0
            //    {
            //        wait = 0;
            //        state = gestureState.waitForData;
            //    }
            //}






            // state machine for gestures - sensing the negative acceleration
            if (state == gestureState.waitForData) // wait for new data
            {
                textBoxGesture.Text = ("");
                textBoxGestureState.Text = ("wait for data");

                if (Az < positiveGestureAcceleration) // -Z
                {
                    wait = 0;
                    state = gestureState.freeFall;
                }
                else if (Az > negativeGestureAcceleration) // +Z
                {
                    wait = 0;
                    state = gestureState.initiateWave;
                }
            }
            else if (state == gestureState.freeFall) // user punches back -Z
            {
                wait++; // wait for 10 data points
                textBoxGestureState.Text = ("freeFall");

                if (Ax < positiveGestureAcceleration) // +X
                {
                    wait = 0;
                    state = gestureState.graveDigger;
                }
                else if (wait >= waitCycles) // user only did a simple punch
                {
                    textBoxGesture.Text = ("Free Fall");

                    if (wait >= waitCycles * 2) // return to state 0
                    {
                        wait = 0;
                        state = gestureState.waitForData;
                    }
                }
            }
            else if (state == gestureState.graveDigger) // user punches down then forward
            {
                wait++;
                textBoxGesture.Text = ("graveDigger");
                textBoxGestureState.Text = ("Grave Digger");

                if (wait >= waitCycles) // return to state 0
                {
                    wait = 0;
                    state = gestureState.waitForData;
                }
            }
            else if (state == gestureState.initiateWave) // user initiates high punch
            {
                wait++;
                textBoxGestureState.Text = ("Initiate Wave");
                
                if (Ay < positiveGestureAcceleration) // +Y
                {
                    wait = 0;
                    state = gestureState.midWave;
                }
                else if (wait >= waitCycles) // return to state 0
                {
                    wait = 0;
                    state = gestureState.waitForData;
                }           
            }
            else if (state == gestureState.midWave) // user mid wave
            {
                wait++;
                textBoxGestureState.Text = ("Mid Wave");

                if (wait >= waitCycles/4)
                {
                    if (Ay >= negativeGestureAcceleration) // -Y
                    {
                        wait = 0;
                        state = gestureState.wave;
                    }
                    else if (wait >= 2 * waitCycles) // return to state 0
                    {
                        wait = 0;
                        state = gestureState.waitForData;
                    }
                }
            }
            else if (state == gestureState.wave) // user completes wave
            {
                wait++;
                textBoxGesture.Text = ("Wave");
                textBoxGestureState.Text = ("Wave");

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
