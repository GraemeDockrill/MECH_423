using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_1_ex7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonProcessNewDataPoint_Click(object sender, EventArgs e)
        {
            // check if text in boxes are valid ints
            if(int.TryParse(textBoxDataAx.Text, out int Ax) && int.TryParse(textBoxDataAy.Text, out int Ay) && int.TryParse(textBoxDataAz.Text, out int Az))
            {

                if (state == states.waitForData) // wait for new data
                {
                    if (Ax >= 180) // +X
                    {
                        state = states.punch;
                    }
                    else if (Az >= 180) // +Z
                    {
                        state = states.initiateHighPunch;
                    }
                }
                else if (state == states.punch) // user punches forward +X
                {
                    wait++; // wait for 10 data points

                    if (Ay >= 180) // +Y
                    {
                        state = states.initiateRightHook;
                    }
                    else if (wait >= waitCycles) // return to state 0
                    {
                        wait = 0;
                        state = states.waitForData;
                    }
                }
                else if (state == states.initiateRightHook) // user punches forward then left
                {
                    wait++;

                    if (Az >= 180) // +Z
                    {
                        state = states.RightHook;
                    }
                    else if (wait >= waitCycles) // return to state 0
                    {
                        wait = 0;
                        state = states.waitForData;
                    }
                }
                else if (state == states.RightHook) // user completes right hook
                {
                    wait++;

                    if (wait >= waitCycles) // return to state 0
                    {
                        wait = 0;
                        state = states.waitForData;
                    }
                }
                else if (state == states.initiateHighPunch) // user initiates high punch
                {
                    wait++;

                    if(Ax >= 180) // +X
                    {
                        state = states.HighPunch;
                    }
                    else if (wait >= waitCycles) // return to state 0
                    {
                        wait = 0;
                        state = states.waitForData;
                    }
                }
                else if (state == states.HighPunch) // user completes high punch
                {
                    wait++;

                    if(wait >= waitCycles) // return to state 0
                    {
                        wait = 0;
                        state = states.waitForData;
                    }
                }

                // output current Ax, Ay, Az, state to data history and display current state
                textBoxDataHistory.AppendText("(" + Ax + ", " + Ay + ", " + Az + ", " + state + "),");
                textBoxCurrentState.Text = state.ToString();

            }
            else
            {
                MessageBox.Show("Invalid input for Ax, Ay, or Az!");
            }

        }
    }
}
