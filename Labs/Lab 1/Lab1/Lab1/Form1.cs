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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            textBox1.Text = e.X.ToString();
            textBox2.Text = e.Y.ToString();
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            string XYstring = "(" + e.X.ToString() + ", " + e.Y.ToString() + ")" + Environment.NewLine;
            textBox3.AppendText(XYstring);
        }
    }
}
