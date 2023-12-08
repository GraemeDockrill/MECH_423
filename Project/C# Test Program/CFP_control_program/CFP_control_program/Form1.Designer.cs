﻿namespace CFP_control_program
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
            System.Windows.Forms.DataVisualization.Charting.Title title13 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title14 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.cmbComPorts = new System.Windows.Forms.ComboBox();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.txtBaudRate = new System.Windows.Forms.TextBox();
            this.btnComConnect = new System.Windows.Forms.Button();
            this.txtKeyboardInput = new System.Windows.Forms.TextBox();
            this.lblKeyboardInput = new System.Windows.Forms.Label();
            this.cbComResponse = new System.Windows.Forms.CheckBox();
            this.cbAlphNumOutput = new System.Windows.Forms.CheckBox();
            this.txtComOutput = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.lblDataRate = new System.Windows.Forms.Label();
            this.cbAutoReconnect = new System.Windows.Forms.CheckBox();
            this.txtByte1 = new System.Windows.Forms.TextBox();
            this.txtByte2 = new System.Windows.Forms.TextBox();
            this.txtByte3 = new System.Windows.Forms.TextBox();
            this.txtByte4 = new System.Windows.Forms.TextBox();
            this.txtByte5 = new System.Windows.Forms.TextBox();
            this.chkByte1 = new System.Windows.Forms.CheckBox();
            this.chkByte2 = new System.Windows.Forms.CheckBox();
            this.chkByte3 = new System.Windows.Forms.CheckBox();
            this.chkByte4 = new System.Windows.Forms.CheckBox();
            this.chkByte5 = new System.Windows.Forms.CheckBox();
            this.btnTransmitToComPort = new System.Windows.Forms.Button();
            this.btnZeroPosition = new System.Windows.Forms.Button();
            this.btnSTOP = new System.Windows.Forms.Button();
            this.tbManualMove = new System.Windows.Forms.TrackBar();
            this.lblManualMovement = new System.Windows.Forms.Label();
            this.lblSetParameters = new System.Windows.Forms.Label();
            this.txtMembraneSize = new System.Windows.Forms.TextBox();
            this.btnSetMembraneSize = new System.Windows.Forms.Button();
            this.txtStrainTarget = new System.Windows.Forms.TextBox();
            this.btnSetStrainTarget = new System.Windows.Forms.Button();
            this.txtStrainRate = new System.Windows.Forms.TextBox();
            this.btnSetStrainRate = new System.Windows.Forms.Button();
            this.txtStrainIncrement = new System.Windows.Forms.TextBox();
            this.btnSetStrainIncrement = new System.Windows.Forms.Button();
            this.btnLockParams = new System.Windows.Forms.Button();
            this.btnReturntoZero = new System.Windows.Forms.Button();
            this.btnStretchtoMaxStrain = new System.Windows.Forms.Button();
            this.txtStrainCycles = new System.Windows.Forms.TextBox();
            this.btnSetStrainCycles = new System.Windows.Forms.Button();
            this.lblCyclicStretching = new System.Windows.Forms.Label();
            this.btnCyclicStretching = new System.Windows.Forms.Button();
            this.lblPositiveMove = new System.Windows.Forms.Label();
            this.lblNegativeMove = new System.Windows.Forms.Label();
            this.chartLoadCellForce = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartCurrentStepPosition = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSelectFileName = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.cbSaveFile = new System.Windows.Forms.CheckBox();
            this.btnResetAllCharts = new System.Windows.Forms.Button();
            this.txtByte6 = new System.Windows.Forms.TextBox();
            this.chkByte6 = new System.Windows.Forms.CheckBox();
            this.txtByte7 = new System.Windows.Forms.TextBox();
            this.chkByte7 = new System.Windows.Forms.CheckBox();
            this.txtByte8 = new System.Windows.Forms.TextBox();
            this.chkByte8 = new System.Windows.Forms.CheckBox();
            this.txtByte9 = new System.Windows.Forms.TextBox();
            this.chkByte9 = new System.Windows.Forms.CheckBox();
            this.txtByte10 = new System.Windows.Forms.TextBox();
            this.txtByte11 = new System.Windows.Forms.TextBox();
            this.txtByte12 = new System.Windows.Forms.TextBox();
            this.chkByte10 = new System.Windows.Forms.CheckBox();
            this.chkByte11 = new System.Windows.Forms.CheckBox();
            this.chkByte12 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.tbManualMove)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartLoadCellForce)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCurrentStepPosition)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbComPorts
            // 
            this.cmbComPorts.FormattingEnabled = true;
            this.cmbComPorts.Location = new System.Drawing.Point(12, 12);
            this.cmbComPorts.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cmbComPorts.Name = "cmbComPorts";
            this.cmbComPorts.Size = new System.Drawing.Size(186, 28);
            this.cmbComPorts.TabIndex = 0;
            this.cmbComPorts.DropDown += new System.EventHandler(this.cmbComPorts_DropDown);
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBaudRate.Location = new System.Drawing.Point(204, 15);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(90, 20);
            this.lblBaudRate.TabIndex = 1;
            this.lblBaudRate.Text = "Baud Rate:";
            // 
            // txtBaudRate
            // 
            this.txtBaudRate.Location = new System.Drawing.Point(300, 12);
            this.txtBaudRate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBaudRate.Name = "txtBaudRate";
            this.txtBaudRate.Size = new System.Drawing.Size(146, 26);
            this.txtBaudRate.TabIndex = 2;
            this.txtBaudRate.Text = "19200";
            // 
            // btnComConnect
            // 
            this.btnComConnect.Location = new System.Drawing.Point(452, 12);
            this.btnComConnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnComConnect.Name = "btnComConnect";
            this.btnComConnect.Size = new System.Drawing.Size(132, 29);
            this.btnComConnect.TabIndex = 3;
            this.btnComConnect.Text = "Connect";
            this.btnComConnect.UseVisualStyleBackColor = true;
            this.btnComConnect.Click += new System.EventHandler(this.btnComConnect_Click);
            // 
            // txtKeyboardInput
            // 
            this.txtKeyboardInput.Location = new System.Drawing.Point(10, 738);
            this.txtKeyboardInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtKeyboardInput.Multiline = true;
            this.txtKeyboardInput.Name = "txtKeyboardInput";
            this.txtKeyboardInput.Size = new System.Drawing.Size(563, 80);
            this.txtKeyboardInput.TabIndex = 4;
            this.txtKeyboardInput.TextChanged += new System.EventHandler(this.txtKeyboardInput_TextChanged);
            // 
            // lblKeyboardInput
            // 
            this.lblKeyboardInput.AutoSize = true;
            this.lblKeyboardInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKeyboardInput.Location = new System.Drawing.Point(10, 706);
            this.lblKeyboardInput.Name = "lblKeyboardInput";
            this.lblKeyboardInput.Size = new System.Drawing.Size(196, 29);
            this.lblKeyboardInput.TabIndex = 5;
            this.lblKeyboardInput.Text = "Keyboard Input:";
            // 
            // cbComResponse
            // 
            this.cbComResponse.AutoSize = true;
            this.cbComResponse.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbComResponse.Location = new System.Drawing.Point(10, 824);
            this.cbComResponse.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbComResponse.Name = "cbComResponse";
            this.cbComResponse.Size = new System.Drawing.Size(289, 30);
            this.cbComResponse.TabIndex = 6;
            this.cbComResponse.Text = "Response from COM Port";
            this.cbComResponse.UseVisualStyleBackColor = true;
            // 
            // cbAlphNumOutput
            // 
            this.cbAlphNumOutput.AutoSize = true;
            this.cbAlphNumOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAlphNumOutput.Location = new System.Drawing.Point(305, 824);
            this.cbAlphNumOutput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbAlphNumOutput.Name = "cbAlphNumOutput";
            this.cbAlphNumOutput.Size = new System.Drawing.Size(243, 30);
            this.cbAlphNumOutput.TabIndex = 7;
            this.cbAlphNumOutput.Text = "Alphanumeric Output";
            this.cbAlphNumOutput.UseVisualStyleBackColor = true;
            // 
            // txtComOutput
            // 
            this.txtComOutput.Location = new System.Drawing.Point(12, 860);
            this.txtComOutput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtComOutput.Multiline = true;
            this.txtComOutput.Name = "txtComOutput";
            this.txtComOutput.Size = new System.Drawing.Size(563, 164);
            this.txtComOutput.TabIndex = 8;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // lblDataRate
            // 
            this.lblDataRate.AutoSize = true;
            this.lblDataRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataRate.Location = new System.Drawing.Point(12, 1028);
            this.lblDataRate.Name = "lblDataRate";
            this.lblDataRate.Size = new System.Drawing.Size(413, 26);
            this.lblDataRate.TabIndex = 9;
            this.lblDataRate.Text = "Incoming Data Rate = 0 bytes per second";
            // 
            // cbAutoReconnect
            // 
            this.cbAutoReconnect.AutoSize = true;
            this.cbAutoReconnect.Location = new System.Drawing.Point(424, 46);
            this.cbAutoReconnect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbAutoReconnect.Name = "cbAutoReconnect";
            this.cbAutoReconnect.Size = new System.Drawing.Size(151, 24);
            this.cbAutoReconnect.TabIndex = 10;
            this.cbAutoReconnect.Text = "Auto Reconnect";
            this.cbAutoReconnect.UseVisualStyleBackColor = true;
            // 
            // txtByte1
            // 
            this.txtByte1.Location = new System.Drawing.Point(12, 109);
            this.txtByte1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtByte1.Name = "txtByte1";
            this.txtByte1.Size = new System.Drawing.Size(100, 26);
            this.txtByte1.TabIndex = 11;
            this.txtByte1.Text = "255";
            this.txtByte1.TextChanged += new System.EventHandler(this.genericTextBoxEventHandler);
            // 
            // txtByte2
            // 
            this.txtByte2.Location = new System.Drawing.Point(118, 109);
            this.txtByte2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtByte2.Name = "txtByte2";
            this.txtByte2.Size = new System.Drawing.Size(100, 26);
            this.txtByte2.TabIndex = 12;
            this.txtByte2.Text = "1";
            this.txtByte2.TextChanged += new System.EventHandler(this.genericTextBoxEventHandler);
            // 
            // txtByte3
            // 
            this.txtByte3.Location = new System.Drawing.Point(224, 109);
            this.txtByte3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtByte3.Name = "txtByte3";
            this.txtByte3.Size = new System.Drawing.Size(100, 26);
            this.txtByte3.TabIndex = 13;
            this.txtByte3.Text = "0";
            this.txtByte3.TextChanged += new System.EventHandler(this.genericTextBoxEventHandler);
            // 
            // txtByte4
            // 
            this.txtByte4.Location = new System.Drawing.Point(330, 109);
            this.txtByte4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtByte4.Name = "txtByte4";
            this.txtByte4.Size = new System.Drawing.Size(100, 26);
            this.txtByte4.TabIndex = 14;
            this.txtByte4.Text = "4";
            this.txtByte4.TextChanged += new System.EventHandler(this.genericTextBoxEventHandler);
            // 
            // txtByte5
            // 
            this.txtByte5.Location = new System.Drawing.Point(436, 109);
            this.txtByte5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtByte5.Name = "txtByte5";
            this.txtByte5.Size = new System.Drawing.Size(100, 26);
            this.txtByte5.TabIndex = 15;
            this.txtByte5.Text = "200";
            this.txtByte5.TextChanged += new System.EventHandler(this.genericTextBoxEventHandler);
            // 
            // chkByte1
            // 
            this.chkByte1.AutoSize = true;
            this.chkByte1.Location = new System.Drawing.Point(14, 79);
            this.chkByte1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkByte1.Name = "chkByte1";
            this.chkByte1.Size = new System.Drawing.Size(89, 24);
            this.chkByte1.TabIndex = 16;
            this.chkByte1.Text = "Byte #1";
            this.chkByte1.UseVisualStyleBackColor = true;
            this.chkByte1.CheckedChanged += new System.EventHandler(this.chkByte1_CheckedChanged);
            // 
            // chkByte2
            // 
            this.chkByte2.AutoSize = true;
            this.chkByte2.Location = new System.Drawing.Point(118, 79);
            this.chkByte2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkByte2.Name = "chkByte2";
            this.chkByte2.Size = new System.Drawing.Size(89, 24);
            this.chkByte2.TabIndex = 17;
            this.chkByte2.Text = "Byte #2";
            this.chkByte2.UseVisualStyleBackColor = true;
            this.chkByte2.CheckedChanged += new System.EventHandler(this.chkByte2_CheckedChanged);
            // 
            // chkByte3
            // 
            this.chkByte3.AutoSize = true;
            this.chkByte3.Location = new System.Drawing.Point(224, 78);
            this.chkByte3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkByte3.Name = "chkByte3";
            this.chkByte3.Size = new System.Drawing.Size(89, 24);
            this.chkByte3.TabIndex = 18;
            this.chkByte3.Text = "Byte #3";
            this.chkByte3.UseVisualStyleBackColor = true;
            this.chkByte3.CheckedChanged += new System.EventHandler(this.chkByte3_CheckedChanged);
            // 
            // chkByte4
            // 
            this.chkByte4.AutoSize = true;
            this.chkByte4.Location = new System.Drawing.Point(330, 78);
            this.chkByte4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkByte4.Name = "chkByte4";
            this.chkByte4.Size = new System.Drawing.Size(89, 24);
            this.chkByte4.TabIndex = 19;
            this.chkByte4.Text = "Byte #4";
            this.chkByte4.UseVisualStyleBackColor = true;
            this.chkByte4.CheckedChanged += new System.EventHandler(this.chkByte4_CheckedChanged);
            // 
            // chkByte5
            // 
            this.chkByte5.AutoSize = true;
            this.chkByte5.Location = new System.Drawing.Point(436, 79);
            this.chkByte5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chkByte5.Name = "chkByte5";
            this.chkByte5.Size = new System.Drawing.Size(89, 24);
            this.chkByte5.TabIndex = 20;
            this.chkByte5.Text = "Byte #5";
            this.chkByte5.UseVisualStyleBackColor = true;
            this.chkByte5.CheckedChanged += new System.EventHandler(this.chkByte5_CheckedChanged);
            // 
            // btnTransmitToComPort
            // 
            this.btnTransmitToComPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransmitToComPort.Location = new System.Drawing.Point(12, 141);
            this.btnTransmitToComPort.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTransmitToComPort.Name = "btnTransmitToComPort";
            this.btnTransmitToComPort.Size = new System.Drawing.Size(562, 52);
            this.btnTransmitToComPort.TabIndex = 21;
            this.btnTransmitToComPort.Text = "Transmit to COM Port";
            this.btnTransmitToComPort.UseVisualStyleBackColor = true;
            this.btnTransmitToComPort.Click += new System.EventHandler(this.btnTransmitToComPort_Click);
            // 
            // btnZeroPosition
            // 
            this.btnZeroPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnZeroPosition.Location = new System.Drawing.Point(12, 201);
            this.btnZeroPosition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnZeroPosition.Name = "btnZeroPosition";
            this.btnZeroPosition.Size = new System.Drawing.Size(148, 50);
            this.btnZeroPosition.TabIndex = 22;
            this.btnZeroPosition.Text = "Zero Position";
            this.btnZeroPosition.UseVisualStyleBackColor = true;
            this.btnZeroPosition.Click += new System.EventHandler(this.btnZeroPosition_Click);
            // 
            // btnSTOP
            // 
            this.btnSTOP.BackColor = System.Drawing.Color.Firebrick;
            this.btnSTOP.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSTOP.ForeColor = System.Drawing.SystemColors.Control;
            this.btnSTOP.Location = new System.Drawing.Point(12, 258);
            this.btnSTOP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSTOP.Name = "btnSTOP";
            this.btnSTOP.Size = new System.Drawing.Size(148, 50);
            this.btnSTOP.TabIndex = 23;
            this.btnSTOP.Text = "STOP";
            this.btnSTOP.UseVisualStyleBackColor = false;
            this.btnSTOP.Click += new System.EventHandler(this.btnSTOP_Click);
            // 
            // tbManualMove
            // 
            this.tbManualMove.Location = new System.Drawing.Point(166, 238);
            this.tbManualMove.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbManualMove.Name = "tbManualMove";
            this.tbManualMove.Size = new System.Drawing.Size(408, 69);
            this.tbManualMove.TabIndex = 24;
            this.tbManualMove.Value = 5;
            this.tbManualMove.ValueChanged += new System.EventHandler(this.tbManualMove_ValueChanged);
            // 
            // lblManualMovement
            // 
            this.lblManualMovement.AutoSize = true;
            this.lblManualMovement.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblManualMovement.Location = new System.Drawing.Point(162, 201);
            this.lblManualMovement.Name = "lblManualMovement";
            this.lblManualMovement.Size = new System.Drawing.Size(233, 25);
            this.lblManualMovement.TabIndex = 25;
            this.lblManualMovement.Text = "MANUAL MOVEMENT";
            this.lblManualMovement.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSetParameters
            // 
            this.lblSetParameters.AutoSize = true;
            this.lblSetParameters.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSetParameters.Location = new System.Drawing.Point(12, 310);
            this.lblSetParameters.Name = "lblSetParameters";
            this.lblSetParameters.Size = new System.Drawing.Size(208, 25);
            this.lblSetParameters.TabIndex = 26;
            this.lblSetParameters.Text = "SET PARAMETERS";
            // 
            // txtMembraneSize
            // 
            this.txtMembraneSize.Location = new System.Drawing.Point(12, 338);
            this.txtMembraneSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMembraneSize.Multiline = true;
            this.txtMembraneSize.Name = "txtMembraneSize";
            this.txtMembraneSize.Size = new System.Drawing.Size(190, 39);
            this.txtMembraneSize.TabIndex = 27;
            this.txtMembraneSize.TextChanged += new System.EventHandler(this.genericTextBoxEventHandler);
            // 
            // btnSetMembraneSize
            // 
            this.btnSetMembraneSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetMembraneSize.Location = new System.Drawing.Point(207, 338);
            this.btnSetMembraneSize.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSetMembraneSize.Name = "btnSetMembraneSize";
            this.btnSetMembraneSize.Size = new System.Drawing.Size(238, 39);
            this.btnSetMembraneSize.TabIndex = 28;
            this.btnSetMembraneSize.Text = "Set Membrane Size [mm]";
            this.btnSetMembraneSize.UseVisualStyleBackColor = true;
            this.btnSetMembraneSize.Click += new System.EventHandler(this.btnSetMembraneSize_Click);
            // 
            // txtStrainTarget
            // 
            this.txtStrainTarget.Location = new System.Drawing.Point(12, 382);
            this.txtStrainTarget.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtStrainTarget.Multiline = true;
            this.txtStrainTarget.Name = "txtStrainTarget";
            this.txtStrainTarget.Size = new System.Drawing.Size(190, 39);
            this.txtStrainTarget.TabIndex = 29;
            this.txtStrainTarget.TextChanged += new System.EventHandler(this.genericTextBoxEventHandler);
            // 
            // btnSetStrainTarget
            // 
            this.btnSetStrainTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetStrainTarget.Location = new System.Drawing.Point(206, 382);
            this.btnSetStrainTarget.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSetStrainTarget.Name = "btnSetStrainTarget";
            this.btnSetStrainTarget.Size = new System.Drawing.Size(238, 39);
            this.btnSetStrainTarget.TabIndex = 30;
            this.btnSetStrainTarget.Text = "Set Strain Target [%]";
            this.btnSetStrainTarget.UseVisualStyleBackColor = true;
            this.btnSetStrainTarget.Click += new System.EventHandler(this.btnSetStrainTarget_Click);
            // 
            // txtStrainRate
            // 
            this.txtStrainRate.Location = new System.Drawing.Point(12, 428);
            this.txtStrainRate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtStrainRate.Multiline = true;
            this.txtStrainRate.Name = "txtStrainRate";
            this.txtStrainRate.Size = new System.Drawing.Size(188, 39);
            this.txtStrainRate.TabIndex = 31;
            this.txtStrainRate.TextChanged += new System.EventHandler(this.genericTextBoxEventHandler);
            // 
            // btnSetStrainRate
            // 
            this.btnSetStrainRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetStrainRate.Location = new System.Drawing.Point(207, 428);
            this.btnSetStrainRate.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSetStrainRate.Name = "btnSetStrainRate";
            this.btnSetStrainRate.Size = new System.Drawing.Size(238, 39);
            this.btnSetStrainRate.TabIndex = 32;
            this.btnSetStrainRate.Text = "Set Strain Rate [%/s]";
            this.btnSetStrainRate.UseVisualStyleBackColor = true;
            this.btnSetStrainRate.Click += new System.EventHandler(this.btnSetStrainRate_Click);
            // 
            // txtStrainIncrement
            // 
            this.txtStrainIncrement.Location = new System.Drawing.Point(11, 472);
            this.txtStrainIncrement.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtStrainIncrement.Multiline = true;
            this.txtStrainIncrement.Name = "txtStrainIncrement";
            this.txtStrainIncrement.Size = new System.Drawing.Size(190, 39);
            this.txtStrainIncrement.TabIndex = 33;
            this.txtStrainIncrement.TextChanged += new System.EventHandler(this.genericTextBoxEventHandler);
            // 
            // btnSetStrainIncrement
            // 
            this.btnSetStrainIncrement.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetStrainIncrement.Location = new System.Drawing.Point(206, 472);
            this.btnSetStrainIncrement.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSetStrainIncrement.Name = "btnSetStrainIncrement";
            this.btnSetStrainIncrement.Size = new System.Drawing.Size(238, 39);
            this.btnSetStrainIncrement.TabIndex = 34;
            this.btnSetStrainIncrement.Text = "Set Strain Increment [%]";
            this.btnSetStrainIncrement.UseVisualStyleBackColor = true;
            this.btnSetStrainIncrement.Click += new System.EventHandler(this.btnSetStrainIncrement_Click);
            // 
            // btnLockParams
            // 
            this.btnLockParams.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLockParams.Location = new System.Drawing.Point(450, 338);
            this.btnLockParams.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLockParams.Name = "btnLockParams";
            this.btnLockParams.Size = new System.Drawing.Size(123, 174);
            this.btnLockParams.TabIndex = 35;
            this.btnLockParams.Text = "Lock Params";
            this.btnLockParams.UseVisualStyleBackColor = true;
            this.btnLockParams.Click += new System.EventHandler(this.btnLockParams_Click);
            // 
            // btnReturntoZero
            // 
            this.btnReturntoZero.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturntoZero.Location = new System.Drawing.Point(11, 518);
            this.btnReturntoZero.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReturntoZero.Name = "btnReturntoZero";
            this.btnReturntoZero.Size = new System.Drawing.Size(282, 48);
            this.btnReturntoZero.TabIndex = 36;
            this.btnReturntoZero.Text = "RETURN TO 0";
            this.btnReturntoZero.UseVisualStyleBackColor = true;
            this.btnReturntoZero.Click += new System.EventHandler(this.btnReturntoZero_Click);
            // 
            // btnStretchtoMaxStrain
            // 
            this.btnStretchtoMaxStrain.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStretchtoMaxStrain.Location = new System.Drawing.Point(299, 518);
            this.btnStretchtoMaxStrain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStretchtoMaxStrain.Name = "btnStretchtoMaxStrain";
            this.btnStretchtoMaxStrain.Size = new System.Drawing.Size(274, 48);
            this.btnStretchtoMaxStrain.TabIndex = 37;
            this.btnStretchtoMaxStrain.Text = "STRETCH TO X%";
            this.btnStretchtoMaxStrain.UseVisualStyleBackColor = true;
            this.btnStretchtoMaxStrain.Click += new System.EventHandler(this.btnStretchtoMaxStrain_Click);
            // 
            // txtStrainCycles
            // 
            this.txtStrainCycles.Location = new System.Drawing.Point(10, 600);
            this.txtStrainCycles.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtStrainCycles.Multiline = true;
            this.txtStrainCycles.Name = "txtStrainCycles";
            this.txtStrainCycles.Size = new System.Drawing.Size(281, 36);
            this.txtStrainCycles.TabIndex = 38;
            this.txtStrainCycles.TextChanged += new System.EventHandler(this.txtStrainCycles_TextChanged);
            // 
            // btnSetStrainCycles
            // 
            this.btnSetStrainCycles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetStrainCycles.Location = new System.Drawing.Point(297, 600);
            this.btnSetStrainCycles.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSetStrainCycles.Name = "btnSetStrainCycles";
            this.btnSetStrainCycles.Size = new System.Drawing.Size(276, 38);
            this.btnSetStrainCycles.TabIndex = 39;
            this.btnSetStrainCycles.Text = "Set Number of Strain Cycles";
            this.btnSetStrainCycles.UseVisualStyleBackColor = true;
            this.btnSetStrainCycles.Click += new System.EventHandler(this.btnSetStrainCycles_Click);
            // 
            // lblCyclicStretching
            // 
            this.lblCyclicStretching.AutoSize = true;
            this.lblCyclicStretching.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCyclicStretching.Location = new System.Drawing.Point(10, 568);
            this.lblCyclicStretching.Name = "lblCyclicStretching";
            this.lblCyclicStretching.Size = new System.Drawing.Size(279, 29);
            this.lblCyclicStretching.TabIndex = 40;
            this.lblCyclicStretching.Text = "CYCLIC STRETCHING";
            // 
            // btnCyclicStretching
            // 
            this.btnCyclicStretching.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCyclicStretching.Location = new System.Drawing.Point(10, 642);
            this.btnCyclicStretching.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCyclicStretching.Name = "btnCyclicStretching";
            this.btnCyclicStretching.Size = new System.Drawing.Size(562, 60);
            this.btnCyclicStretching.TabIndex = 41;
            this.btnCyclicStretching.Text = "CYCLIC STRETCHING";
            this.btnCyclicStretching.UseVisualStyleBackColor = true;
            this.btnCyclicStretching.Click += new System.EventHandler(this.btnCyclicStretching_Click);
            // 
            // lblPositiveMove
            // 
            this.lblPositiveMove.AutoSize = true;
            this.lblPositiveMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPositiveMove.Location = new System.Drawing.Point(531, 261);
            this.lblPositiveMove.Name = "lblPositiveMove";
            this.lblPositiveMove.Size = new System.Drawing.Size(44, 46);
            this.lblPositiveMove.TabIndex = 42;
            this.lblPositiveMove.Text = "+";
            // 
            // lblNegativeMove
            // 
            this.lblNegativeMove.AutoSize = true;
            this.lblNegativeMove.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNegativeMove.Location = new System.Drawing.Point(173, 261);
            this.lblNegativeMove.Name = "lblNegativeMove";
            this.lblNegativeMove.Size = new System.Drawing.Size(34, 46);
            this.lblNegativeMove.TabIndex = 43;
            this.lblNegativeMove.Text = "-";
            // 
            // chartLoadCellForce
            // 
            this.chartLoadCellForce.Location = new System.Drawing.Point(590, 176);
            this.chartLoadCellForce.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chartLoadCellForce.Name = "chartLoadCellForce";
            this.chartLoadCellForce.Size = new System.Drawing.Size(1009, 351);
            this.chartLoadCellForce.TabIndex = 44;
            this.chartLoadCellForce.Text = "chart1";
            title13.Name = "Load Cell Force";
            title13.Text = "Load Cell Force";
            this.chartLoadCellForce.Titles.Add(title13);
            // 
            // chartCurrentStepPosition
            // 
            this.chartCurrentStepPosition.Location = new System.Drawing.Point(590, 532);
            this.chartCurrentStepPosition.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chartCurrentStepPosition.Name = "chartCurrentStepPosition";
            this.chartCurrentStepPosition.Size = new System.Drawing.Size(1009, 491);
            this.chartCurrentStepPosition.TabIndex = 45;
            this.chartCurrentStepPosition.Text = "chart2";
            title14.Name = "Current Position";
            title14.Text = "Current Position";
            this.chartCurrentStepPosition.Titles.Add(title14);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(1083, 1030);
            this.txtFileName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(336, 26);
            this.txtFileName.TabIndex = 46;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(939, 1034);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 20);
            this.label1.TabIndex = 47;
            this.label1.Text = "Output File Name:";
            // 
            // btnSelectFileName
            // 
            this.btnSelectFileName.Location = new System.Drawing.Point(1426, 1030);
            this.btnSelectFileName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelectFileName.Name = "btnSelectFileName";
            this.btnSelectFileName.Size = new System.Drawing.Size(172, 29);
            this.btnSelectFileName.TabIndex = 48;
            this.btnSelectFileName.Text = "Select File Name";
            this.btnSelectFileName.UseVisualStyleBackColor = true;
            this.btnSelectFileName.Click += new System.EventHandler(this.btnSelectFileName_Click);
            // 
            // cbSaveFile
            // 
            this.cbSaveFile.AutoSize = true;
            this.cbSaveFile.Location = new System.Drawing.Point(810, 1032);
            this.cbSaveFile.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbSaveFile.Name = "cbSaveFile";
            this.cbSaveFile.Size = new System.Drawing.Size(122, 24);
            this.cbSaveFile.TabIndex = 49;
            this.cbSaveFile.Text = "Save To File";
            this.cbSaveFile.UseVisualStyleBackColor = true;
            this.cbSaveFile.CheckedChanged += new System.EventHandler(this.cbSaveFile_CheckedChanged);
            // 
            // btnResetAllCharts
            // 
            this.btnResetAllCharts.Location = new System.Drawing.Point(1324, 12);
            this.btnResetAllCharts.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnResetAllCharts.Name = "btnResetAllCharts";
            this.btnResetAllCharts.Size = new System.Drawing.Size(273, 29);
            this.btnResetAllCharts.TabIndex = 52;
            this.btnResetAllCharts.Text = "Reset All Charts";
            this.btnResetAllCharts.UseVisualStyleBackColor = true;
            this.btnResetAllCharts.Click += new System.EventHandler(this.btnResetAllCharts_Click);
            // 
            // txtByte6
            // 
            this.txtByte6.Location = new System.Drawing.Point(542, 110);
            this.txtByte6.Name = "txtByte6";
            this.txtByte6.Size = new System.Drawing.Size(100, 26);
            this.txtByte6.TabIndex = 53;
            this.txtByte6.Text = "4";
            this.txtByte6.TextChanged += new System.EventHandler(this.genericTextBoxEventHandler);
            // 
            // chkByte6
            // 
            this.chkByte6.AutoSize = true;
            this.chkByte6.Location = new System.Drawing.Point(542, 80);
            this.chkByte6.Name = "chkByte6";
            this.chkByte6.Size = new System.Drawing.Size(89, 24);
            this.chkByte6.TabIndex = 54;
            this.chkByte6.Text = "Byte #6";
            this.chkByte6.UseVisualStyleBackColor = true;
            this.chkByte6.CheckedChanged += new System.EventHandler(this.chkByte6_CheckedChanged);
            // 
            // txtByte7
            // 
            this.txtByte7.Location = new System.Drawing.Point(648, 109);
            this.txtByte7.Name = "txtByte7";
            this.txtByte7.Size = new System.Drawing.Size(100, 26);
            this.txtByte7.TabIndex = 55;
            this.txtByte7.Text = "200";
            this.txtByte7.TextChanged += new System.EventHandler(this.genericTextBoxEventHandler);
            // 
            // chkByte7
            // 
            this.chkByte7.AutoSize = true;
            this.chkByte7.Location = new System.Drawing.Point(648, 80);
            this.chkByte7.Name = "chkByte7";
            this.chkByte7.Size = new System.Drawing.Size(89, 24);
            this.chkByte7.TabIndex = 56;
            this.chkByte7.Text = "Byte #7";
            this.chkByte7.UseVisualStyleBackColor = true;
            this.chkByte7.CheckedChanged += new System.EventHandler(this.chkByte7_CheckedChanged);
            // 
            // txtByte8
            // 
            this.txtByte8.Location = new System.Drawing.Point(754, 110);
            this.txtByte8.Name = "txtByte8";
            this.txtByte8.Size = new System.Drawing.Size(100, 26);
            this.txtByte8.TabIndex = 57;
            this.txtByte8.Text = "20";
            this.txtByte8.TextChanged += new System.EventHandler(this.genericTextBoxEventHandler);
            // 
            // chkByte8
            // 
            this.chkByte8.AutoSize = true;
            this.chkByte8.Location = new System.Drawing.Point(754, 80);
            this.chkByte8.Name = "chkByte8";
            this.chkByte8.Size = new System.Drawing.Size(89, 24);
            this.chkByte8.TabIndex = 58;
            this.chkByte8.Text = "Byte #8";
            this.chkByte8.UseVisualStyleBackColor = true;
            this.chkByte8.CheckedChanged += new System.EventHandler(this.chkByte8_CheckedChanged);
            // 
            // txtByte9
            // 
            this.txtByte9.Location = new System.Drawing.Point(860, 109);
            this.txtByte9.Name = "txtByte9";
            this.txtByte9.Size = new System.Drawing.Size(100, 26);
            this.txtByte9.TabIndex = 59;
            this.txtByte9.Text = "0";
            this.txtByte9.TextChanged += new System.EventHandler(this.genericTextBoxEventHandler);
            // 
            // chkByte9
            // 
            this.chkByte9.AutoSize = true;
            this.chkByte9.Location = new System.Drawing.Point(860, 80);
            this.chkByte9.Name = "chkByte9";
            this.chkByte9.Size = new System.Drawing.Size(89, 24);
            this.chkByte9.TabIndex = 60;
            this.chkByte9.Text = "Byte #9";
            this.chkByte9.UseVisualStyleBackColor = true;
            this.chkByte9.CheckedChanged += new System.EventHandler(this.chkByte9_CheckedChanged);
            // 
            // txtByte10
            // 
            this.txtByte10.Location = new System.Drawing.Point(966, 110);
            this.txtByte10.Name = "txtByte10";
            this.txtByte10.Size = new System.Drawing.Size(100, 26);
            this.txtByte10.TabIndex = 61;
            this.txtByte10.Text = "20";
            this.txtByte10.TextChanged += new System.EventHandler(this.genericTextBoxEventHandler);
            // 
            // txtByte11
            // 
            this.txtByte11.Location = new System.Drawing.Point(1072, 110);
            this.txtByte11.Name = "txtByte11";
            this.txtByte11.Size = new System.Drawing.Size(100, 26);
            this.txtByte11.TabIndex = 62;
            this.txtByte11.Text = "0";
            this.txtByte11.TextChanged += new System.EventHandler(this.genericTextBoxEventHandler);
            // 
            // txtByte12
            // 
            this.txtByte12.Location = new System.Drawing.Point(1178, 110);
            this.txtByte12.Name = "txtByte12";
            this.txtByte12.Size = new System.Drawing.Size(100, 26);
            this.txtByte12.TabIndex = 63;
            this.txtByte12.Text = "0";
            this.txtByte12.TextChanged += new System.EventHandler(this.genericTextBoxEventHandler);
            // 
            // chkByte10
            // 
            this.chkByte10.AutoSize = true;
            this.chkByte10.Location = new System.Drawing.Point(966, 80);
            this.chkByte10.Name = "chkByte10";
            this.chkByte10.Size = new System.Drawing.Size(98, 24);
            this.chkByte10.TabIndex = 64;
            this.chkByte10.Text = "Byte #10";
            this.chkByte10.UseVisualStyleBackColor = true;
            this.chkByte10.CheckedChanged += new System.EventHandler(this.chkByte10_CheckedChanged);
            // 
            // chkByte11
            // 
            this.chkByte11.AutoSize = true;
            this.chkByte11.Location = new System.Drawing.Point(1072, 80);
            this.chkByte11.Name = "chkByte11";
            this.chkByte11.Size = new System.Drawing.Size(98, 24);
            this.chkByte11.TabIndex = 65;
            this.chkByte11.Text = "Byte #11";
            this.chkByte11.UseVisualStyleBackColor = true;
            this.chkByte11.CheckedChanged += new System.EventHandler(this.chkByte11_CheckedChanged);
            // 
            // chkByte12
            // 
            this.chkByte12.AutoSize = true;
            this.chkByte12.Location = new System.Drawing.Point(1178, 80);
            this.chkByte12.Name = "chkByte12";
            this.chkByte12.Size = new System.Drawing.Size(98, 24);
            this.chkByte12.TabIndex = 66;
            this.chkByte12.Text = "Byte #12";
            this.chkByte12.UseVisualStyleBackColor = true;
            this.chkByte12.CheckedChanged += new System.EventHandler(this.chkByte12_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1611, 1121);
            this.Controls.Add(this.chkByte12);
            this.Controls.Add(this.chkByte11);
            this.Controls.Add(this.chkByte10);
            this.Controls.Add(this.txtByte12);
            this.Controls.Add(this.txtByte11);
            this.Controls.Add(this.txtByte10);
            this.Controls.Add(this.chkByte9);
            this.Controls.Add(this.txtByte9);
            this.Controls.Add(this.chkByte8);
            this.Controls.Add(this.txtByte8);
            this.Controls.Add(this.chkByte7);
            this.Controls.Add(this.txtByte7);
            this.Controls.Add(this.chkByte6);
            this.Controls.Add(this.txtByte6);
            this.Controls.Add(this.btnResetAllCharts);
            this.Controls.Add(this.cbSaveFile);
            this.Controls.Add(this.btnSelectFileName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.chartCurrentStepPosition);
            this.Controls.Add(this.chartLoadCellForce);
            this.Controls.Add(this.lblNegativeMove);
            this.Controls.Add(this.lblPositiveMove);
            this.Controls.Add(this.btnCyclicStretching);
            this.Controls.Add(this.lblCyclicStretching);
            this.Controls.Add(this.btnSetStrainCycles);
            this.Controls.Add(this.txtStrainCycles);
            this.Controls.Add(this.btnStretchtoMaxStrain);
            this.Controls.Add(this.btnReturntoZero);
            this.Controls.Add(this.btnLockParams);
            this.Controls.Add(this.btnSetStrainIncrement);
            this.Controls.Add(this.txtStrainIncrement);
            this.Controls.Add(this.btnSetStrainRate);
            this.Controls.Add(this.txtStrainRate);
            this.Controls.Add(this.btnSetStrainTarget);
            this.Controls.Add(this.txtStrainTarget);
            this.Controls.Add(this.btnSetMembraneSize);
            this.Controls.Add(this.txtMembraneSize);
            this.Controls.Add(this.lblSetParameters);
            this.Controls.Add(this.lblManualMovement);
            this.Controls.Add(this.tbManualMove);
            this.Controls.Add(this.btnSTOP);
            this.Controls.Add(this.btnZeroPosition);
            this.Controls.Add(this.btnTransmitToComPort);
            this.Controls.Add(this.chkByte5);
            this.Controls.Add(this.chkByte4);
            this.Controls.Add(this.chkByte3);
            this.Controls.Add(this.chkByte2);
            this.Controls.Add(this.chkByte1);
            this.Controls.Add(this.txtByte5);
            this.Controls.Add(this.txtByte4);
            this.Controls.Add(this.txtByte3);
            this.Controls.Add(this.txtByte2);
            this.Controls.Add(this.txtByte1);
            this.Controls.Add(this.cbAutoReconnect);
            this.Controls.Add(this.lblDataRate);
            this.Controls.Add(this.txtComOutput);
            this.Controls.Add(this.cbAlphNumOutput);
            this.Controls.Add(this.cbComResponse);
            this.Controls.Add(this.lblKeyboardInput);
            this.Controls.Add(this.txtKeyboardInput);
            this.Controls.Add(this.btnComConnect);
            this.Controls.Add(this.txtBaudRate);
            this.Controls.Add(this.lblBaudRate);
            this.Controls.Add(this.cmbComPorts);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.tbManualMove)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartLoadCellForce)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartCurrentStepPosition)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbComPorts;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.TextBox txtBaudRate;
        private System.Windows.Forms.Button btnComConnect;
        private System.Windows.Forms.TextBox txtKeyboardInput;
        private System.Windows.Forms.Label lblKeyboardInput;
        private System.Windows.Forms.CheckBox cbComResponse;
        private System.Windows.Forms.CheckBox cbAlphNumOutput;
        private System.Windows.Forms.TextBox txtComOutput;
        private System.Windows.Forms.Timer timer1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Label lblDataRate;
        private System.Windows.Forms.CheckBox cbAutoReconnect;
        private System.Windows.Forms.TextBox txtByte1;
        private System.Windows.Forms.TextBox txtByte2;
        private System.Windows.Forms.TextBox txtByte3;
        private System.Windows.Forms.TextBox txtByte4;
        private System.Windows.Forms.TextBox txtByte5;
        private System.Windows.Forms.CheckBox chkByte1;
        private System.Windows.Forms.CheckBox chkByte2;
        private System.Windows.Forms.CheckBox chkByte3;
        private System.Windows.Forms.CheckBox chkByte4;
        private System.Windows.Forms.CheckBox chkByte5;
        private System.Windows.Forms.Button btnTransmitToComPort;
        private System.Windows.Forms.Button btnZeroPosition;
        private System.Windows.Forms.Button btnSTOP;
        private System.Windows.Forms.TrackBar tbManualMove;
        private System.Windows.Forms.Label lblManualMovement;
        private System.Windows.Forms.Label lblSetParameters;
        private System.Windows.Forms.TextBox txtMembraneSize;
        private System.Windows.Forms.Button btnSetMembraneSize;
        private System.Windows.Forms.TextBox txtStrainTarget;
        private System.Windows.Forms.Button btnSetStrainTarget;
        private System.Windows.Forms.TextBox txtStrainRate;
        private System.Windows.Forms.Button btnSetStrainRate;
        private System.Windows.Forms.TextBox txtStrainIncrement;
        private System.Windows.Forms.Button btnSetStrainIncrement;
        private System.Windows.Forms.Button btnLockParams;
        private System.Windows.Forms.Button btnReturntoZero;
        private System.Windows.Forms.Button btnStretchtoMaxStrain;
        private System.Windows.Forms.TextBox txtStrainCycles;
        private System.Windows.Forms.Button btnSetStrainCycles;
        private System.Windows.Forms.Label lblCyclicStretching;
        private System.Windows.Forms.Button btnCyclicStretching;
        private System.Windows.Forms.Label lblPositiveMove;
        private System.Windows.Forms.Label lblNegativeMove;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartLoadCellForce;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartCurrentStepPosition;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSelectFileName;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.CheckBox cbSaveFile;
        private System.Windows.Forms.Button btnResetAllCharts;
        private System.Windows.Forms.TextBox txtByte6;
        private System.Windows.Forms.CheckBox chkByte6;
        private System.Windows.Forms.TextBox txtByte7;
        private System.Windows.Forms.CheckBox chkByte7;
        private System.Windows.Forms.TextBox txtByte8;
        private System.Windows.Forms.CheckBox chkByte8;
        private System.Windows.Forms.TextBox txtByte9;
        private System.Windows.Forms.CheckBox chkByte9;
        private System.Windows.Forms.TextBox txtByte10;
        private System.Windows.Forms.TextBox txtByte11;
        private System.Windows.Forms.TextBox txtByte12;
        private System.Windows.Forms.CheckBox chkByte10;
        private System.Windows.Forms.CheckBox chkByte11;
        private System.Windows.Forms.CheckBox chkByte12;
    }
}

