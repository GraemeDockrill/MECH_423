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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Error: no text in box to enqueue!");
            }
            else
            {
                dataQueue.Enqueue(Convert.ToInt32(textBox1.Text));
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (dataQueue.Count == 0)
            {
                MessageBox.Show("Error: data queue is empty!");
            }
            else
            {
                int item;
                if(dataQueue.TryDequeue(out item) == false)
                {
                    MessageBox.Show("Error: data queue was not able to be dequeued!");
                }
            }
        }

        private void UpdateQueue()
        {
            textBox3.Text = dataQueue.Count.ToString();
            textBox6.Text = "";

            foreach (var item in dataQueue)
            {
                textBox6.AppendText(item.ToString() + ", ");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateQueue();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataQueue.Count == 0)
            {
                MessageBox.Show("Error: insufficient data in queue for average!");
            }
            else
            {
                int sum = 0;
                int average;
                int count = dataQueue.Count;
                int item;

                for (int i = count; i > 0; i--)
                {
                    if (dataQueue.TryDequeue(out item) == false)
                    {
                        MessageBox.Show("Error: data queue was not able to be dequeued!");
                        break;
                    }
                    sum = sum + item;
                }

                average = sum / count;
                textBox4.Text = count.ToString();
                textBox5.Text = average.ToString();
            }
        }
    }
}
