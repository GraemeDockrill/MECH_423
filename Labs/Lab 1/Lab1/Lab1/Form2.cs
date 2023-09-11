using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        // When the user clicks button1 and theres text in textBox1, the number is added to a queue
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Error: no text in box to enqueue!");
            }
            else
            {
                dataQueue.Enqueue(Convert.ToInt32(textBox1.Text));
            }
        }

        // When the user clicks button2 and theres entries in queue, remove them
        private void button2_Click(object sender, EventArgs e)
        {
            
            if (dataQueue.Count == 0)
            {
                MessageBox.Show("Error: data queue is empty!");
            }
            else
            {
                dataQueue.Dequeue();
            }
        }

        // Update the queue shown in textBox6 and keep track of the number of queued items in textBox3
        private void UpdateQueue()
        {
            textBox3.Text = dataQueue.Count.ToString();
            textBox6.Text = "";

            foreach (var item in dataQueue)
            {
                textBox6.AppendText(item.ToString() + ", ");
            }
        }

        // when timer1 ticks every 100ms, UpdateQueue is called
        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateQueue();
        }

        // When button3 is clicked, average the data in queue, and clear queue
        private void button3_Click(object sender, EventArgs e)
        {
            if(dataQueue.Count == 0)
            {
                MessageBox.Show("Error: insufficient data in queue for average!");
            }
            else
            {
                int sum = 0;
                int average;
                int count = dataQueue.Count;

                for (int i = count; i > 0; i--)
                {
                    sum = sum + dataQueue.Dequeue();
                }
                
                average = sum / count;
                textBox4.Text = count.ToString();
                textBox5.Text = average.ToString();
            }
        }
    }
}
