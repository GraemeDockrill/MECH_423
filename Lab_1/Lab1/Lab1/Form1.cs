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

        // When user mouses over pictureBox1, the X and Y coordinates are displayed in text boxes 1 and 2
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            textBox1.Text = e.X.ToString();
            textBox2.Text = e.Y.ToString();
        }

        // When user clicks in the picture box, the coordinates of the click are listed in textBox3
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            string XYstring = "(" + e.X.ToString() + ", " + e.Y.ToString() + ")" + Environment.NewLine;
            textBox3.AppendText(XYstring);
        }
    }
}
