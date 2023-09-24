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
using IrrKlang;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab_1_ex9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public void titleScreenShow()
        {
            pictureBoxTitleScreen.Enabled = true;
            pictureBoxTitleScreen.Visible = true;
            textBoxFinalScore.Enabled = true;
            textBoxFinalScore.Visible = true;
            labelFinalScore.Enabled = true;
            labelFinalScore.Visible = true;
            textBoxBombsUsed.Enabled = true;
            textBoxBombsUsed.Visible = true;
            labelBombsUsed.Enabled = true;
            labelBombsUsed.Visible = true;
            labelTitle.Enabled = true;
            labelTitle.Visible = true;
            buttonStartGame.Enabled = true;
            buttonStartGame.Visible = true;
            comboBoxCOMPorts.Enabled = true;
            comboBoxCOMPorts.Visible = true;
            buttonSerialConnection.Enabled = true;
            buttonSerialConnection.Visible = true;
            labelAx.Enabled = true;
            labelAx.Visible = true;
            textBoxAx.Enabled = true;
            textBoxAx.Visible = true;
            labelAy.Enabled = true;
            labelAy.Visible = true;
            textBoxAy.Enabled = true;
            textBoxAy.Visible = true;
            labelAz.Enabled = true;
            labelAz.Visible = true;
            textBoxAz.Enabled = true;
            textBoxAz.Visible = true;

            textBoxFinalScore.Text = score.ToString();
            textBoxBombsUsed.Text = bombsUsed.ToString();
        }

        public void titleScreenHide() 
        {
            pictureBoxTitleScreen.Enabled = false;
            pictureBoxTitleScreen.Visible = false;
            textBoxFinalScore.Enabled = false;
            textBoxFinalScore.Visible = false;
            labelFinalScore.Enabled = false;
            labelFinalScore.Visible = false;
            textBoxBombsUsed.Enabled = false;
            textBoxBombsUsed.Visible = false;
            labelBombsUsed.Enabled = false;
            labelBombsUsed.Visible = false;
            labelTitle.Enabled = false;
            labelTitle.Visible = false;
            buttonStartGame.Enabled = false;
            buttonStartGame.Visible = false;
            comboBoxCOMPorts.Enabled = false;
            comboBoxCOMPorts.Visible = false;
            buttonSerialConnection.Enabled = false;
            buttonSerialConnection.Visible = false;
            labelAx.Enabled = false;
            labelAx.Visible = false;
            textBoxAx.Enabled = false;
            textBoxAx.Visible = false;
            labelAy.Enabled = false;
            labelAy.Visible = false;
            textBoxAy.Enabled = false;
            textBoxAy.Visible = false;
            labelAz.Enabled = false;
            labelAz.Visible = false;
            textBoxAz.Enabled = false;
            textBoxAz.Visible = false;
        }

        public void checkPlayerWallCollision()
        {
            // player collision with right edge
            if (playerCarX > pictureBoxRoad1.Location.X + pictureBoxGameBorder.Width - pictureBoxPlayerCar.Width)
            {
                playerCarX = pictureBoxRoad1.Location.X + pictureBoxGameBorder.Width - pictureBoxPlayerCar.Width;

                pictureBoxPlayerCar.Location = new Point(playerCarX, playerCarY);
            }
            // player collision with bottom edge
            if (playerCarY > pictureBoxGameBorder.Location.Y + pictureBoxGameBorder.Height - pictureBoxPlayerCar.Height)
            {
                playerCarY = pictureBoxGameBorder.Location.Y + pictureBoxGameBorder.Height - pictureBoxPlayerCar.Height;

                pictureBoxPlayerCar.Location = new Point(playerCarX, playerCarY);
            }
            // player collision with left edge
            if (playerCarX < pictureBoxGameBorder.Location.X)
            {
                playerCarX = pictureBoxGameBorder.Location.X;

                pictureBoxPlayerCar.Location = new Point(playerCarX, playerCarY);
            }
            // player collision with top edge
            if (playerCarY < pictureBoxGameBorder.Location.Y)
            {
                playerCarY = pictureBoxGameBorder.Location.Y;

                pictureBoxPlayerCar.Location = new Point(playerCarX, playerCarY);
            }
        }

        public void scrollRoadBackground()
        {
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
        }

        public void NPCCarMovement()
        {
            

            // get car positions
            blueCarX = pictureBoxBlueCar.Location.X;
            blueCarY = pictureBoxBlueCar.Location.Y;
            greenCarX = pictureBoxGreenCar.Location.X;
            greenCarY = pictureBoxGreenCar.Location.Y;
            yellowCarX = pictureBoxYellowCar.Location.X;
            yellowCarY = pictureBoxYellowCar.Location.Y;
            purpleCarX = pictureBoxPurpleCar.Location.X;
            purpleCarY = pictureBoxPurpleCar.Location.Y;

            // move cars by speed step
            blueCarY += blueCarSpeed;
            greenCarY += greenCarSpeed;
            yellowCarY += yellowCarSpeed;
            purpleCarY += purpleCarSpeed;


            // respawning cars when they go off the screen
            if (blueCarY > pictureBoxGameBorder.Location.Y + pictureBoxGameBorder.Height)
            {
                blueCarY = pictureBoxGameBorder.Location.Y - pictureBoxBlueCar.Height;
                blueCarSpeed = carBaseSpeed * rnd.Next(2, 5);
                pictureBoxBlueCar.Enabled = true;
                pictureBoxBlueCar.Visible = true;
            }
            if (greenCarY > pictureBoxGameBorder.Location.Y + pictureBoxGameBorder.Height)
            {
                greenCarY = pictureBoxGameBorder.Location.Y - pictureBoxGreenCar.Height;
                greenCarSpeed = carBaseSpeed * rnd.Next(2, 5);
                pictureBoxGreenCar.Enabled = true;
                pictureBoxGreenCar.Visible = true;
            }
            if (yellowCarY > pictureBoxGameBorder.Location.Y + pictureBoxGameBorder.Height)
            {
                yellowCarY = pictureBoxGameBorder.Location.Y - pictureBoxYellowCar.Height;
                yellowCarSpeed = carBaseSpeed * rnd.Next(2, 5);
                pictureBoxYellowCar.Enabled = true;
                pictureBoxYellowCar.Visible = true;
            }
            if (purpleCarY > pictureBoxGameBorder.Location.Y + pictureBoxGameBorder.Height)
            {
                purpleCarY = pictureBoxGameBorder.Location.Y - pictureBoxPurpleCar.Height;
                purpleCarSpeed = carBaseSpeed * rnd.Next(2, 5);
                pictureBoxPurpleCar.Enabled = true;
                pictureBoxPurpleCar.Visible = true;
            }

            pictureBoxBlueCar.Location = new Point(blueCarX, blueCarY);
            pictureBoxGreenCar.Location = new Point(greenCarX, greenCarY);
            pictureBoxYellowCar.Location = new Point(yellowCarX, yellowCarY);
            pictureBoxPurpleCar.Location = new Point(purpleCarX, purpleCarY);
        }

        public bool checkPlayerCarCollision()
        {
            // first check if cars are exploded
            if (pictureBoxBlueCar.Enabled && pictureBoxGreenCar.Enabled && pictureBoxYellowCar.Enabled && pictureBoxPurpleCar.Enabled)
            {
                if (playerCarX + pictureBoxPlayerCar.Width - collisionTolerance >= blueCarX + collisionTolerance && playerCarX + collisionTolerance <= blueCarX + pictureBoxBlueCar.Width - collisionTolerance && playerCarY + pictureBoxPlayerCar.Height - collisionTolerance >= blueCarY + collisionTolerance && playerCarY + collisionTolerance <= blueCarY + pictureBoxBlueCar.Height - collisionTolerance) // blue car collision
                {
                    return true;
                }
                if (playerCarX + pictureBoxPlayerCar.Width - collisionTolerance >= greenCarX + collisionTolerance && playerCarX + collisionTolerance <= greenCarX + pictureBoxGreenCar.Width - collisionTolerance && playerCarY + pictureBoxPlayerCar.Height - collisionTolerance >= greenCarY + collisionTolerance && playerCarY + collisionTolerance <= greenCarY + pictureBoxGreenCar.Height - collisionTolerance) // green car collision
                {
                    return true;
                }
                if (playerCarX + pictureBoxPlayerCar.Width - collisionTolerance >= yellowCarX + collisionTolerance && playerCarX + collisionTolerance <= yellowCarX + pictureBoxYellowCar.Width - collisionTolerance && playerCarY + pictureBoxPlayerCar.Height - collisionTolerance >= yellowCarY + collisionTolerance && playerCarY + collisionTolerance <= yellowCarY + pictureBoxYellowCar.Height - collisionTolerance) // yellow car collision
                {
                    return true;
                }
                if (playerCarX + pictureBoxPlayerCar.Width - collisionTolerance >= purpleCarX + collisionTolerance && playerCarX + collisionTolerance <= purpleCarX + pictureBoxPurpleCar.Width - collisionTolerance && playerCarY + pictureBoxPlayerCar.Height - collisionTolerance >= purpleCarY + collisionTolerance && playerCarY + collisionTolerance <= purpleCarY + pictureBoxPurpleCar.Height - collisionTolerance) // purple car collision
                {
                    return true;
                }
            }
            return false; // if player hasn't collided
        }

        public void resetGame()
        {
            pictureBoxPlayerCar.Location = new Point(370, 128);
            pictureBoxBlueCar.Location = new Point(179, 526);
            pictureBoxGreenCar.Location = new Point(436, 526);
            pictureBoxYellowCar.Location = new Point(301, 526);
            pictureBoxPurpleCar.Location = new Point(561, 526);
        }

        public void checkBombCars()
        {
            // if player uses bomb, clear screen
            if (Az < AzBombAcceleration && hasBomb)
            {
                // player used bomb
                hasBomb = false;
                bombsUsed++;
                pictureBoxBomb.BackColor = Color.Red;
                bombProgress = 0;

                // draw bomb to cars
                pictureBoxBlueCarExplosion.Location = new Point(blueCarX, blueCarY);
                pictureBoxGreenCarExplosion.Location = new Point(greenCarX, greenCarY);
                pictureBoxYellowCarExplosion.Location = new Point(yellowCarX, yellowCarY);
                pictureBoxPurpleCarExplosion.Location = new Point(purpleCarX, purpleCarY);

                // enable explosion image
                pictureBoxBlueCarExplosion.Enabled = true;
                pictureBoxBlueCarExplosion.Visible = true;
                pictureBoxGreenCarExplosion.Enabled = true;
                pictureBoxGreenCarExplosion.Visible = true;
                pictureBoxYellowCarExplosion.Enabled = true;
                pictureBoxYellowCarExplosion.Visible = true;
                pictureBoxPurpleCarExplosion.Enabled = true;
                pictureBoxPurpleCarExplosion.Visible = true;

                // disable cars
                carsHide();
            }

            if (bombShowTimer >= 20)
            {
                // disable explosion image
                pictureBoxBlueCarExplosion.Enabled = false;
                pictureBoxBlueCarExplosion.Visible = false;
                pictureBoxGreenCarExplosion.Enabled = false;
                pictureBoxGreenCarExplosion.Visible = false;
                pictureBoxYellowCarExplosion.Enabled = false;
                pictureBoxYellowCarExplosion.Visible = false;
                pictureBoxPurpleCarExplosion.Enabled = false;
                pictureBoxPurpleCarExplosion.Visible = false;

                bombShowTimer = 0;
            }

            bombShowTimer++;
        }

        public void carsShow()
        {
            pictureBoxBlueCar.Enabled = true;
            pictureBoxBlueCar.Visible = true;
            pictureBoxGreenCar.Enabled = true;
            pictureBoxGreenCar.Visible = true;
            pictureBoxYellowCar.Enabled = true;
            pictureBoxYellowCar.Visible = true;
            pictureBoxPurpleCar.Enabled = true;
            pictureBoxPurpleCar.Visible = true;
        }

        public void carsHide()
        {
            pictureBoxBlueCar.Enabled = false;
            pictureBoxBlueCar.Visible = false;
            pictureBoxGreenCar.Enabled = false;
            pictureBoxGreenCar.Visible = false;
            pictureBoxYellowCar.Enabled = false;
            pictureBoxYellowCar.Visible = false;
            pictureBoxPurpleCar.Enabled = false;
            pictureBoxPurpleCar.Visible = false;
        }

        public void checkBombProgress()
        {
            if (bombProgress / 10 >= 100)
            {
                hasBomb = true;
                pictureBoxBomb.BackColor = Color.LightGreen;
            }
            else
            {
                bombProgress++;
                textBoxBombProgress.Text = (bombProgress / 10).ToString();
            }
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

            titleScreenShow();

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

            // loop through data buffer and add bytes to concurrent queue
            while (bytestoRead != 0)
            {
                newByte = serialPort1.ReadByte();
                dataQueue.Enqueue(newByte); // queue new byte
                bytestoRead = serialPort1.BytesToRead;
            }
        }

        private void buttonStartGame_Click(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                MessageBox.Show("Connect COM device before starting!");
            }
            else
            {
                titleScreenHide();
                serialPort1.DiscardInBuffer(); // when start clicked, discard buffer
                resetGame();
                play = true;
                score = 0;
                bombsUsed = 0;
                bombProgress = 0;
                bombShowTimer = 0;
                hasBomb = false;
                pictureBoxBomb.BackColor = Color.Red;
                engine.Play2D("../../../media/mariojump.wav", true);

                // randomize car speeds
                blueCarSpeed = carBaseSpeed * rnd.Next(1, 3);
                greenCarSpeed = carBaseSpeed * rnd.Next(1, 3);
                yellowCarSpeed = carBaseSpeed * rnd.Next(1, 3);
                purpleCarSpeed = carBaseSpeed * rnd.Next(1, 3);

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
                    textBoxInputX.Text = AxScaled.ToString();
                }
                else if (parsingState == parsingByte.Ay) // parsing Ay byte
                {
                    textBoxAy.Text = item.ToString();
                    parsingState = parsingByte.Az;
                    AyScaled = (1.0 / 25) * item - 5.0; // scale orientation of Y axis to -1 to +1
                    textBoxInputY.Text = AyScaled.ToString();
                }
                else if (parsingState == parsingByte.Az) // parsing Az byte
                {
                    textBoxAz.Text = item.ToString();
                    parsingState = parsingByte.start;
                    Az = item;
                }

                // if game enabled
                if (play == true)
                {
                    // update score
                    score++;
                    textBoxScore.Text = score.ToString();

                    // player movement with the MSP430 board-----------------------------------
                    playerCarX = pictureBoxPlayerCar.Location.X;
                    playerCarY = pictureBoxPlayerCar.Location.Y;

                    checkPlayerWallCollision();

                    // draw player location
                    pictureBoxPlayerCar.Location = new Point(playerCarX + Convert.ToInt32(step * AxScaled), playerCarY + Convert.ToInt32(step * AyScaled));

                    // scrolling background-----------------------------------------
                    scrollRoadBackground();

                    // scrolling car movement------------------------------------------
                    NPCCarMovement();

                    // bomb progress----------------------------------------------
                    checkBombProgress();

                    // check if player bombed cars--------------------------------
                    checkBombCars();

                    // collision with cars----------------------------------------
                    if (checkPlayerCarCollision())
                    {
                        play = false;
                        titleScreenShow();
                    }
                }
            }
        }
    }
}
