using System;
using System.Collections.Concurrent;

namespace Lab_1_ex9
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.comboBoxCOMPorts = new System.Windows.Forms.ComboBox();
            this.buttonSerialConnection = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAx = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAy = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxAz = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.pictureBoxPlayerCar = new System.Windows.Forms.PictureBox();
            this.pictureBoxBlueCar = new System.Windows.Forms.PictureBox();
            this.pictureBoxGreenCar = new System.Windows.Forms.PictureBox();
            this.pictureBoxYellowCar = new System.Windows.Forms.PictureBox();
            this.pictureBoxRoad1 = new System.Windows.Forms.PictureBox();
            this.pictureBoxRoad2 = new System.Windows.Forms.PictureBox();
            this.pictureBoxGameBorder = new System.Windows.Forms.PictureBox();
            this.pictureBoxRoad3 = new System.Windows.Forms.PictureBox();
            this.pictureBoxPurpleCar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayerCar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlueCar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGreenCar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxYellowCar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRoad1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRoad2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameBorder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRoad3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPurpleCar)).BeginInit();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // comboBoxCOMPorts
            // 
            this.comboBoxCOMPorts.FormattingEnabled = true;
            this.comboBoxCOMPorts.Location = new System.Drawing.Point(13, 13);
            this.comboBoxCOMPorts.Name = "comboBoxCOMPorts";
            this.comboBoxCOMPorts.Size = new System.Drawing.Size(121, 21);
            this.comboBoxCOMPorts.TabIndex = 0;
            this.comboBoxCOMPorts.SelectedIndexChanged += new System.EventHandler(this.comboBoxCOMPorts_SelectedIndexChanged);
            // 
            // buttonSerialConnection
            // 
            this.buttonSerialConnection.Location = new System.Drawing.Point(141, 13);
            this.buttonSerialConnection.Name = "buttonSerialConnection";
            this.buttonSerialConnection.Size = new System.Drawing.Size(150, 23);
            this.buttonSerialConnection.TabIndex = 1;
            this.buttonSerialConnection.Text = "Connect Serial";
            this.buttonSerialConnection.UseVisualStyleBackColor = true;
            this.buttonSerialConnection.Click += new System.EventHandler(this.buttonSerialConnection_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(298, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Ax:";
            // 
            // textBoxAx
            // 
            this.textBoxAx.Location = new System.Drawing.Point(327, 12);
            this.textBoxAx.Name = "textBoxAx";
            this.textBoxAx.Size = new System.Drawing.Size(100, 20);
            this.textBoxAx.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(433, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Ay:";
            // 
            // textBoxAy
            // 
            this.textBoxAy.Location = new System.Drawing.Point(461, 12);
            this.textBoxAy.Name = "textBoxAy";
            this.textBoxAy.Size = new System.Drawing.Size(100, 20);
            this.textBoxAy.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(568, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(22, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Az:";
            // 
            // textBoxAz
            // 
            this.textBoxAz.Location = new System.Drawing.Point(596, 13);
            this.textBoxAz.Name = "textBoxAz";
            this.textBoxAz.Size = new System.Drawing.Size(100, 20);
            this.textBoxAz.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Input X";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(69, 59);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Input Y";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(69, 96);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 12;
            // 
            // pictureBoxPlayerCar
            // 
            this.pictureBoxPlayerCar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pictureBoxPlayerCar.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxPlayerCar.Image")));
            this.pictureBoxPlayerCar.Location = new System.Drawing.Point(370, 128);
            this.pictureBoxPlayerCar.MaximumSize = new System.Drawing.Size(100, 180);
            this.pictureBoxPlayerCar.MinimumSize = new System.Drawing.Size(100, 180);
            this.pictureBoxPlayerCar.Name = "pictureBoxPlayerCar";
            this.pictureBoxPlayerCar.Size = new System.Drawing.Size(100, 180);
            this.pictureBoxPlayerCar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxPlayerCar.TabIndex = 13;
            this.pictureBoxPlayerCar.TabStop = false;
            // 
            // pictureBoxBlueCar
            // 
            this.pictureBoxBlueCar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pictureBoxBlueCar.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxBlueCar.Image")));
            this.pictureBoxBlueCar.Location = new System.Drawing.Point(179, 526);
            this.pictureBoxBlueCar.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxBlueCar.Name = "pictureBoxBlueCar";
            this.pictureBoxBlueCar.Size = new System.Drawing.Size(101, 182);
            this.pictureBoxBlueCar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxBlueCar.TabIndex = 14;
            this.pictureBoxBlueCar.TabStop = false;
            // 
            // pictureBoxGreenCar
            // 
            this.pictureBoxGreenCar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pictureBoxGreenCar.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxGreenCar.Image")));
            this.pictureBoxGreenCar.Location = new System.Drawing.Point(436, 526);
            this.pictureBoxGreenCar.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxGreenCar.Name = "pictureBoxGreenCar";
            this.pictureBoxGreenCar.Size = new System.Drawing.Size(100, 180);
            this.pictureBoxGreenCar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxGreenCar.TabIndex = 15;
            this.pictureBoxGreenCar.TabStop = false;
            // 
            // pictureBoxYellowCar
            // 
            this.pictureBoxYellowCar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pictureBoxYellowCar.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxYellowCar.Image")));
            this.pictureBoxYellowCar.Location = new System.Drawing.Point(301, 526);
            this.pictureBoxYellowCar.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxYellowCar.Name = "pictureBoxYellowCar";
            this.pictureBoxYellowCar.Size = new System.Drawing.Size(101, 182);
            this.pictureBoxYellowCar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxYellowCar.TabIndex = 16;
            this.pictureBoxYellowCar.TabStop = false;
            // 
            // pictureBoxRoad1
            // 
            this.pictureBoxRoad1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxRoad1.Image")));
            this.pictureBoxRoad1.Location = new System.Drawing.Point(0, 630);
            this.pictureBoxRoad1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxRoad1.Name = "pictureBoxRoad1";
            this.pictureBoxRoad1.Size = new System.Drawing.Size(840, 650);
            this.pictureBoxRoad1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxRoad1.TabIndex = 17;
            this.pictureBoxRoad1.TabStop = false;
            // 
            // pictureBoxRoad2
            // 
            this.pictureBoxRoad2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxRoad2.Image")));
            this.pictureBoxRoad2.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxRoad2.Name = "pictureBoxRoad2";
            this.pictureBoxRoad2.Size = new System.Drawing.Size(840, 650);
            this.pictureBoxRoad2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxRoad2.TabIndex = 19;
            this.pictureBoxRoad2.TabStop = false;
            // 
            // pictureBoxGameBorder
            // 
            this.pictureBoxGameBorder.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxGameBorder.Location = new System.Drawing.Point(140, 0);
            this.pictureBoxGameBorder.Name = "pictureBoxGameBorder";
            this.pictureBoxGameBorder.Size = new System.Drawing.Size(700, 1200);
            this.pictureBoxGameBorder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxGameBorder.TabIndex = 20;
            this.pictureBoxGameBorder.TabStop = false;
            // 
            // pictureBoxRoad3
            // 
            this.pictureBoxRoad3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxRoad3.Image")));
            this.pictureBoxRoad3.Location = new System.Drawing.Point(0, -630);
            this.pictureBoxRoad3.Name = "pictureBoxRoad3";
            this.pictureBoxRoad3.Size = new System.Drawing.Size(840, 650);
            this.pictureBoxRoad3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBoxRoad3.TabIndex = 21;
            this.pictureBoxRoad3.TabStop = false;
            // 
            // pictureBoxPurpleCar
            // 
            this.pictureBoxPurpleCar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pictureBoxPurpleCar.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxPurpleCar.Image")));
            this.pictureBoxPurpleCar.Location = new System.Drawing.Point(561, 526);
            this.pictureBoxPurpleCar.Name = "pictureBoxPurpleCar";
            this.pictureBoxPurpleCar.Size = new System.Drawing.Size(100, 180);
            this.pictureBoxPurpleCar.TabIndex = 22;
            this.pictureBoxPurpleCar.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 1161);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxAz);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxAy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxAx);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonSerialConnection);
            this.Controls.Add(this.comboBoxCOMPorts);
            this.Controls.Add(this.pictureBoxPlayerCar);
            this.Controls.Add(this.pictureBoxPurpleCar);
            this.Controls.Add(this.pictureBoxGreenCar);
            this.Controls.Add(this.pictureBoxYellowCar);
            this.Controls.Add(this.pictureBoxBlueCar);
            this.Controls.Add(this.pictureBoxRoad1);
            this.Controls.Add(this.pictureBoxRoad2);
            this.Controls.Add(this.pictureBoxRoad3);
            this.Controls.Add(this.pictureBoxGameBorder);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPlayerCar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlueCar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGreenCar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxYellowCar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRoad1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRoad2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameBorder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRoad3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPurpleCar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox comboBoxCOMPorts;
        private System.Windows.Forms.Button buttonSerialConnection;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxAz;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.PictureBox pictureBoxPlayerCar;
        private System.Windows.Forms.PictureBox pictureBoxBlueCar;
        private System.Windows.Forms.PictureBox pictureBoxGreenCar;
        private System.Windows.Forms.PictureBox pictureBoxYellowCar;
        private System.Windows.Forms.PictureBox pictureBoxRoad1;
        private System.Windows.Forms.PictureBox pictureBoxRoad2;
        private System.Windows.Forms.PictureBox pictureBoxGameBorder;
        private System.Windows.Forms.PictureBox pictureBoxRoad3;
        private System.Windows.Forms.PictureBox pictureBoxPurpleCar;

        ConcurrentQueue<Int32> dataQueue = new ConcurrentQueue<Int32>();

        Random rnd = new Random();

        double AxScaled;
        double AyScaled;
        int redCarX = 0;
        int redCarY = 0;
        int blueCarX = 0;
        int blueCarY = 0;
        int greenCarX = 0;
        int greenCarY = 0;
        int yellowCarX = 0;
        int yellowCarY = 0;
        int purpleCarX = 0;
        int purpleCarY = 0;
        int road1X = 0;
        int road1Y = 0;
        int road2X = 0;
        int road2Y = 0;
        int road3X = 0;
        int road3Y = 0;
        int roadSpeed = 5;
        int carBaseSpeed = 1;
        int blueCarSpeed;
        int greenCarSpeed;
        int yellowCarSpeed;
        int purpleCarSpeed;


        bool collision = false;

        int step = 5;

        enum parsingByte
        {
            start,
            Ax,
            Ay,
            Az
        }

        parsingByte parsingState = parsingByte.start;
        
    }
}

