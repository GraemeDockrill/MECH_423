using System;
using System.Collections.Concurrent;
using System.IO;

namespace Lab_1_ex8
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
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxBytesToRead = new System.Windows.Forms.TextBox();
            this.textBoxTempStringLength = new System.Windows.Forms.TextBox();
            this.textBoxItemsInQueue = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSerialDataStream = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxAccelerationX = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxAccelerationY = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxAccelerationZ = new System.Windows.Forms.TextBox();
            this.textBoxOrientation = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBoxSavetoFile = new System.Windows.Forms.CheckBox();
            this.buttonSelectFilename = new System.Windows.Forms.Button();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxAverageAz = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxAverageAy = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxAverageAx = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxGesture = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(9, 8);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(163, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(175, 8);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 19);
            this.button1.TabIndex = 1;
            this.button1.Text = "Connect Serial";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Serial Bytes to Read";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Temp String Length";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 75);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Items in Queue";
            // 
            // textBoxBytesToRead
            // 
            this.textBoxBytesToRead.Location = new System.Drawing.Point(137, 31);
            this.textBoxBytesToRead.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxBytesToRead.Name = "textBoxBytesToRead";
            this.textBoxBytesToRead.Size = new System.Drawing.Size(193, 20);
            this.textBoxBytesToRead.TabIndex = 5;
            // 
            // textBoxTempStringLength
            // 
            this.textBoxTempStringLength.Location = new System.Drawing.Point(137, 53);
            this.textBoxTempStringLength.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxTempStringLength.Name = "textBoxTempStringLength";
            this.textBoxTempStringLength.Size = new System.Drawing.Size(193, 20);
            this.textBoxTempStringLength.TabIndex = 6;
            // 
            // textBoxItemsInQueue
            // 
            this.textBoxItemsInQueue.Location = new System.Drawing.Point(137, 74);
            this.textBoxItemsInQueue.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxItemsInQueue.Name = "textBoxItemsInQueue";
            this.textBoxItemsInQueue.Size = new System.Drawing.Size(193, 20);
            this.textBoxItemsInQueue.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 105);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Serial Data Stream:";
            // 
            // textBoxSerialDataStream
            // 
            this.textBoxSerialDataStream.Location = new System.Drawing.Point(9, 123);
            this.textBoxSerialDataStream.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxSerialDataStream.Multiline = true;
            this.textBoxSerialDataStream.Name = "textBoxSerialDataStream";
            this.textBoxSerialDataStream.Size = new System.Drawing.Size(321, 163);
            this.textBoxSerialDataStream.TabIndex = 9;
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 289);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(22, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Ax:";
            // 
            // textBoxAccelerationX
            // 
            this.textBoxAccelerationX.Location = new System.Drawing.Point(34, 289);
            this.textBoxAccelerationX.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxAccelerationX.Name = "textBoxAccelerationX";
            this.textBoxAccelerationX.Size = new System.Drawing.Size(68, 20);
            this.textBoxAccelerationX.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(105, 289);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Ay:";
            // 
            // textBoxAccelerationY
            // 
            this.textBoxAccelerationY.Location = new System.Drawing.Point(131, 289);
            this.textBoxAccelerationY.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxAccelerationY.Name = "textBoxAccelerationY";
            this.textBoxAccelerationY.Size = new System.Drawing.Size(68, 20);
            this.textBoxAccelerationY.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(202, 289);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Az:";
            // 
            // textBoxAccelerationZ
            // 
            this.textBoxAccelerationZ.Location = new System.Drawing.Point(228, 289);
            this.textBoxAccelerationZ.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxAccelerationZ.Name = "textBoxAccelerationZ";
            this.textBoxAccelerationZ.Size = new System.Drawing.Size(68, 20);
            this.textBoxAccelerationZ.TabIndex = 15;
            // 
            // textBoxOrientation
            // 
            this.textBoxOrientation.Location = new System.Drawing.Point(71, 310);
            this.textBoxOrientation.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxOrientation.Name = "textBoxOrientation";
            this.textBoxOrientation.Size = new System.Drawing.Size(128, 20);
            this.textBoxOrientation.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 310);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "Orientation";
            // 
            // checkBoxSavetoFile
            // 
            this.checkBoxSavetoFile.AutoSize = true;
            this.checkBoxSavetoFile.Location = new System.Drawing.Point(6, 409);
            this.checkBoxSavetoFile.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxSavetoFile.Name = "checkBoxSavetoFile";
            this.checkBoxSavetoFile.Size = new System.Drawing.Size(82, 17);
            this.checkBoxSavetoFile.TabIndex = 18;
            this.checkBoxSavetoFile.Text = "Save to File";
            this.checkBoxSavetoFile.UseVisualStyleBackColor = true;
            this.checkBoxSavetoFile.CheckStateChanged += new System.EventHandler(this.checkBoxSavetoFile_CheckStateChanged);
            // 
            // buttonSelectFilename
            // 
            this.buttonSelectFilename.Location = new System.Drawing.Point(7, 429);
            this.buttonSelectFilename.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSelectFilename.Name = "buttonSelectFilename";
            this.buttonSelectFilename.Size = new System.Drawing.Size(92, 20);
            this.buttonSelectFilename.TabIndex = 19;
            this.buttonSelectFilename.Text = "Select Filename";
            this.buttonSelectFilename.UseVisualStyleBackColor = true;
            this.buttonSelectFilename.Click += new System.EventHandler(this.buttonSelectFilename_Click);
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Location = new System.Drawing.Point(103, 432);
            this.textBoxFileName.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(225, 20);
            this.textBoxFileName.TabIndex = 20;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(54, 332);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(205, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Running Average of Past 100 Data Points";
            // 
            // textBoxAverageAz
            // 
            this.textBoxAverageAz.Location = new System.Drawing.Point(228, 347);
            this.textBoxAverageAz.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxAverageAz.Name = "textBoxAverageAz";
            this.textBoxAverageAz.Size = new System.Drawing.Size(68, 20);
            this.textBoxAverageAz.TabIndex = 27;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(202, 347);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(22, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "Az:";
            // 
            // textBoxAverageAy
            // 
            this.textBoxAverageAy.Location = new System.Drawing.Point(131, 347);
            this.textBoxAverageAy.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxAverageAy.Name = "textBoxAverageAy";
            this.textBoxAverageAy.Size = new System.Drawing.Size(68, 20);
            this.textBoxAverageAy.TabIndex = 25;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(105, 347);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(22, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Ay:";
            // 
            // textBoxAverageAx
            // 
            this.textBoxAverageAx.Location = new System.Drawing.Point(34, 347);
            this.textBoxAverageAx.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxAverageAx.Name = "textBoxAverageAx";
            this.textBoxAverageAx.Size = new System.Drawing.Size(68, 20);
            this.textBoxAverageAx.TabIndex = 23;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(9, 347);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 13);
            this.label12.TabIndex = 22;
            this.label12.Text = "Ax:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(26, 376);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(47, 13);
            this.label13.TabIndex = 28;
            this.label13.Text = "Gesture:";
            // 
            // textBoxGesture
            // 
            this.textBoxGesture.Location = new System.Drawing.Point(79, 373);
            this.textBoxGesture.Name = "textBoxGesture";
            this.textBoxGesture.Size = new System.Drawing.Size(249, 20);
            this.textBoxGesture.TabIndex = 29;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(342, 463);
            this.Controls.Add(this.textBoxGesture);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.textBoxAverageAz);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBoxAverageAy);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBoxAverageAx);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxFileName);
            this.Controls.Add(this.buttonSelectFilename);
            this.Controls.Add(this.checkBoxSavetoFile);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBoxOrientation);
            this.Controls.Add(this.textBoxAccelerationZ);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBoxAccelerationY);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxAccelerationX);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxSerialDataStream);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxItemsInQueue);
            this.Controls.Add(this.textBoxTempStringLength);
            this.Controls.Add(this.textBoxBytesToRead);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.comboBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxBytesToRead;
        private System.Windows.Forms.TextBox textBoxTempStringLength;
        private System.Windows.Forms.TextBox textBoxItemsInQueue;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxSerialDataStream;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxAccelerationX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxAccelerationY;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxAccelerationZ;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxOrientation;
        private System.Windows.Forms.CheckBox checkBoxSavetoFile;
        private System.Windows.Forms.Button buttonSelectFilename;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxAverageAz;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxAverageAy;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBoxAverageAx;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox textBoxGesture;

        string serialDataString = "";
        int caseState;
        int Checked = 0;
        int AxSum = 0;
        int AySum = 0;
        int AzSum = 0;
        int averagePeriod = 100;
        int wait = 0;
        int waitCycles = 20;
        int gestureAcceleration = 180;

        int dequeuedAx;
        int dequeuedAy;
        int dequeuedAz;

        ConcurrentQueue<Int32> dataQueue = new ConcurrentQueue<Int32>();
        ConcurrentQueue<Int32> dataQueueAx = new ConcurrentQueue<Int32>();
        ConcurrentQueue<Int32> dataQueueAy = new ConcurrentQueue<Int32>();
        ConcurrentQueue<Int32> dataQueueAz = new ConcurrentQueue<Int32>();
        StreamWriter outputFile;

        enum parsingByte
        {
            start,
            Ax,
            Ay,
            Az
        }

        parsingByte parsingState = parsingByte.start;

        enum gestureState
        {
            waitForData,
            punch,
            initiateRightHook,
            rightHook,
            initiateHighPunch,
            highPunch
        }

        gestureState state = gestureState.waitForData;
    }
}

