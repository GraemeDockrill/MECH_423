
namespace Lab3_ex2
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
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.cmbComPorts = new System.Windows.Forms.ComboBox();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.txtBaudRate = new System.Windows.Forms.TextBox();
            this.btnComConnect = new System.Windows.Forms.Button();
            this.lblDCMotorControl = new System.Windows.Forms.Label();
            this.lblKeyboardInput = new System.Windows.Forms.Label();
            this.txtKeyboardInput = new System.Windows.Forms.TextBox();
            this.cbComResponse = new System.Windows.Forms.CheckBox();
            this.cbAlphNumOutput = new System.Windows.Forms.CheckBox();
            this.txtComOutput = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblDataRate = new System.Windows.Forms.Label();
            this.cbAutoReconnect = new System.Windows.Forms.CheckBox();
            this.tbDCDutyCycle = new System.Windows.Forms.TrackBar();
            this.tbStepperSpeed = new System.Windows.Forms.TrackBar();
            this.lblStepperControl = new System.Windows.Forms.Label();
            this.btnCWStepWhole = new System.Windows.Forms.Button();
            this.btnCCWStepWhole = new System.Windows.Forms.Button();
            this.btnStopDC = new System.Windows.Forms.Button();
            this.btnStopStep = new System.Windows.Forms.Button();
            this.btnCWStepHalf = new System.Windows.Forms.Button();
            this.btnCCWStepHalf = new System.Windows.Forms.Button();
            this.btnContWhole = new System.Windows.Forms.Button();
            this.btnContHalf = new System.Windows.Forms.Button();
            this.chartDCPosition = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartDCVelocity = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.tbDCDutyCycle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbStepperSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDCPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDCVelocity)).BeginInit();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // cmbComPorts
            // 
            this.cmbComPorts.FormattingEnabled = true;
            this.cmbComPorts.Location = new System.Drawing.Point(18, 18);
            this.cmbComPorts.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbComPorts.Name = "cmbComPorts";
            this.cmbComPorts.Size = new System.Drawing.Size(180, 28);
            this.cmbComPorts.TabIndex = 0;
            this.cmbComPorts.DropDown += new System.EventHandler(this.cmbComPorts_DropDown);
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Location = new System.Drawing.Point(208, 23);
            this.lblBaudRate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(90, 20);
            this.lblBaudRate.TabIndex = 1;
            this.lblBaudRate.Text = "Baud Rate:";
            // 
            // txtBaudRate
            // 
            this.txtBaudRate.Location = new System.Drawing.Point(309, 18);
            this.txtBaudRate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBaudRate.Name = "txtBaudRate";
            this.txtBaudRate.Size = new System.Drawing.Size(148, 26);
            this.txtBaudRate.TabIndex = 2;
            this.txtBaudRate.Text = "19200";
            // 
            // btnComConnect
            // 
            this.btnComConnect.Location = new System.Drawing.Point(468, 15);
            this.btnComConnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnComConnect.Name = "btnComConnect";
            this.btnComConnect.Size = new System.Drawing.Size(112, 35);
            this.btnComConnect.TabIndex = 3;
            this.btnComConnect.Text = "Connect";
            this.btnComConnect.UseVisualStyleBackColor = true;
            this.btnComConnect.Click += new System.EventHandler(this.btnComConnect_Click);
            // 
            // lblDCMotorControl
            // 
            this.lblDCMotorControl.AutoSize = true;
            this.lblDCMotorControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDCMotorControl.Location = new System.Drawing.Point(18, 55);
            this.lblDCMotorControl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDCMotorControl.Name = "lblDCMotorControl";
            this.lblDCMotorControl.Size = new System.Drawing.Size(283, 29);
            this.lblDCMotorControl.TabIndex = 4;
            this.lblDCMotorControl.Text = "DC Motor Duty Cycle %";
            // 
            // lblKeyboardInput
            // 
            this.lblKeyboardInput.AutoSize = true;
            this.lblKeyboardInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKeyboardInput.Location = new System.Drawing.Point(18, 431);
            this.lblKeyboardInput.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblKeyboardInput.Name = "lblKeyboardInput";
            this.lblKeyboardInput.Size = new System.Drawing.Size(196, 29);
            this.lblKeyboardInput.TabIndex = 16;
            this.lblKeyboardInput.Text = "Keyboard Input:";
            // 
            // txtKeyboardInput
            // 
            this.txtKeyboardInput.Location = new System.Drawing.Point(18, 466);
            this.txtKeyboardInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtKeyboardInput.Multiline = true;
            this.txtKeyboardInput.Name = "txtKeyboardInput";
            this.txtKeyboardInput.Size = new System.Drawing.Size(566, 79);
            this.txtKeyboardInput.TabIndex = 17;
            // 
            // cbComResponse
            // 
            this.cbComResponse.AutoSize = true;
            this.cbComResponse.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbComResponse.Location = new System.Drawing.Point(18, 557);
            this.cbComResponse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbComResponse.Name = "cbComResponse";
            this.cbComResponse.Size = new System.Drawing.Size(315, 33);
            this.cbComResponse.TabIndex = 18;
            this.cbComResponse.Text = "Response from COM Port";
            this.cbComResponse.UseVisualStyleBackColor = true;
            // 
            // cbAlphNumOutput
            // 
            this.cbAlphNumOutput.AutoSize = true;
            this.cbAlphNumOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAlphNumOutput.Location = new System.Drawing.Point(343, 559);
            this.cbAlphNumOutput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbAlphNumOutput.Name = "cbAlphNumOutput";
            this.cbAlphNumOutput.Size = new System.Drawing.Size(243, 30);
            this.cbAlphNumOutput.TabIndex = 19;
            this.cbAlphNumOutput.Text = "Alphanumeric Output";
            this.cbAlphNumOutput.UseVisualStyleBackColor = true;
            // 
            // txtComOutput
            // 
            this.txtComOutput.Location = new System.Drawing.Point(18, 600);
            this.txtComOutput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtComOutput.Multiline = true;
            this.txtComOutput.Name = "txtComOutput";
            this.txtComOutput.Size = new System.Drawing.Size(566, 261);
            this.txtComOutput.TabIndex = 20;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblDataRate
            // 
            this.lblDataRate.AutoSize = true;
            this.lblDataRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataRate.Location = new System.Drawing.Point(18, 868);
            this.lblDataRate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDataRate.Name = "lblDataRate";
            this.lblDataRate.Size = new System.Drawing.Size(451, 29);
            this.lblDataRate.TabIndex = 21;
            this.lblDataRate.Text = "Incoming Data Rate = 0 bytes per second";
            // 
            // cbAutoReconnect
            // 
            this.cbAutoReconnect.AutoSize = true;
            this.cbAutoReconnect.Location = new System.Drawing.Point(424, 55);
            this.cbAutoReconnect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbAutoReconnect.Name = "cbAutoReconnect";
            this.cbAutoReconnect.Size = new System.Drawing.Size(151, 24);
            this.cbAutoReconnect.TabIndex = 22;
            this.cbAutoReconnect.Text = "Auto Reconnect";
            this.cbAutoReconnect.UseVisualStyleBackColor = true;
            // 
            // tbDCDutyCycle
            // 
            this.tbDCDutyCycle.Location = new System.Drawing.Point(18, 94);
            this.tbDCDutyCycle.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbDCDutyCycle.Maximum = 200;
            this.tbDCDutyCycle.Name = "tbDCDutyCycle";
            this.tbDCDutyCycle.Size = new System.Drawing.Size(454, 69);
            this.tbDCDutyCycle.TabIndex = 23;
            this.tbDCDutyCycle.Value = 100;
            this.tbDCDutyCycle.ValueChanged += new System.EventHandler(this.tbDCDutyCycle_ValueChanged);
            // 
            // tbStepperSpeed
            // 
            this.tbStepperSpeed.Location = new System.Drawing.Point(24, 203);
            this.tbStepperSpeed.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbStepperSpeed.Maximum = 200;
            this.tbStepperSpeed.Name = "tbStepperSpeed";
            this.tbStepperSpeed.Size = new System.Drawing.Size(448, 69);
            this.tbStepperSpeed.TabIndex = 24;
            this.tbStepperSpeed.Value = 100;
            this.tbStepperSpeed.ValueChanged += new System.EventHandler(this.tbStepperSpeed_ValueChanged);
            // 
            // lblStepperControl
            // 
            this.lblStepperControl.AutoSize = true;
            this.lblStepperControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStepperControl.Location = new System.Drawing.Point(18, 168);
            this.lblStepperControl.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStepperControl.Name = "lblStepperControl";
            this.lblStepperControl.Size = new System.Drawing.Size(198, 29);
            this.lblStepperControl.TabIndex = 25;
            this.lblStepperControl.Text = "Stepper Control";
            // 
            // btnCWStepWhole
            // 
            this.btnCWStepWhole.Location = new System.Drawing.Point(24, 349);
            this.btnCWStepWhole.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCWStepWhole.Name = "btnCWStepWhole";
            this.btnCWStepWhole.Size = new System.Drawing.Size(112, 77);
            this.btnCWStepWhole.TabIndex = 26;
            this.btnCWStepWhole.Text = "CW Step (WHOLE)";
            this.btnCWStepWhole.UseVisualStyleBackColor = true;
            this.btnCWStepWhole.Click += new System.EventHandler(this.btnCWStepWhole_Click);
            // 
            // btnCCWStepWhole
            // 
            this.btnCCWStepWhole.Location = new System.Drawing.Point(146, 349);
            this.btnCCWStepWhole.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCCWStepWhole.Name = "btnCCWStepWhole";
            this.btnCCWStepWhole.Size = new System.Drawing.Size(112, 77);
            this.btnCCWStepWhole.TabIndex = 27;
            this.btnCCWStepWhole.Text = "CCW Step (WHOLE)";
            this.btnCCWStepWhole.UseVisualStyleBackColor = true;
            this.btnCCWStepWhole.Click += new System.EventHandler(this.btnCCWStepWhole_Click);
            // 
            // btnStopDC
            // 
            this.btnStopDC.Location = new System.Drawing.Point(474, 94);
            this.btnStopDC.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStopDC.Name = "btnStopDC";
            this.btnStopDC.Size = new System.Drawing.Size(112, 69);
            this.btnStopDC.TabIndex = 28;
            this.btnStopDC.Text = "STOP DC";
            this.btnStopDC.UseVisualStyleBackColor = true;
            this.btnStopDC.Click += new System.EventHandler(this.btnStopDC_Click);
            // 
            // btnStopStep
            // 
            this.btnStopStep.Location = new System.Drawing.Point(474, 203);
            this.btnStopStep.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnStopStep.Name = "btnStopStep";
            this.btnStopStep.Size = new System.Drawing.Size(112, 69);
            this.btnStopStep.TabIndex = 29;
            this.btnStopStep.Text = "STOP STEP";
            this.btnStopStep.UseVisualStyleBackColor = true;
            this.btnStopStep.Click += new System.EventHandler(this.btnStopStep_Click);
            // 
            // btnCWStepHalf
            // 
            this.btnCWStepHalf.Location = new System.Drawing.Point(267, 349);
            this.btnCWStepHalf.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCWStepHalf.Name = "btnCWStepHalf";
            this.btnCWStepHalf.Size = new System.Drawing.Size(112, 77);
            this.btnCWStepHalf.TabIndex = 30;
            this.btnCWStepHalf.Text = "CW Step (HALF)";
            this.btnCWStepHalf.UseVisualStyleBackColor = true;
            this.btnCWStepHalf.Click += new System.EventHandler(this.btnCWStepHalf_Click);
            // 
            // btnCCWStepHalf
            // 
            this.btnCCWStepHalf.Location = new System.Drawing.Point(388, 349);
            this.btnCCWStepHalf.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCCWStepHalf.Name = "btnCCWStepHalf";
            this.btnCCWStepHalf.Size = new System.Drawing.Size(112, 77);
            this.btnCCWStepHalf.TabIndex = 31;
            this.btnCCWStepHalf.Text = "CCW Step (HALF)";
            this.btnCCWStepHalf.UseVisualStyleBackColor = true;
            this.btnCCWStepHalf.Click += new System.EventHandler(this.btnCCWStepHalf_Click);
            // 
            // btnContWhole
            // 
            this.btnContWhole.Location = new System.Drawing.Point(24, 282);
            this.btnContWhole.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnContWhole.Name = "btnContWhole";
            this.btnContWhole.Size = new System.Drawing.Size(234, 58);
            this.btnContWhole.TabIndex = 32;
            this.btnContWhole.Text = "Continuous (WHOLE)";
            this.btnContWhole.UseVisualStyleBackColor = true;
            this.btnContWhole.Click += new System.EventHandler(this.btnContWhole_Click);
            // 
            // btnContHalf
            // 
            this.btnContHalf.Location = new System.Drawing.Point(267, 282);
            this.btnContHalf.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnContHalf.Name = "btnContHalf";
            this.btnContHalf.Size = new System.Drawing.Size(234, 58);
            this.btnContHalf.TabIndex = 33;
            this.btnContHalf.Text = "Continuous (HALF)";
            this.btnContHalf.UseVisualStyleBackColor = true;
            this.btnContHalf.Click += new System.EventHandler(this.btnContHalf_Click);
            // 
            // chartDCPosition
            // 
            this.chartDCPosition.Location = new System.Drawing.Point(587, 12);
            this.chartDCPosition.Name = "chartDCPosition";
            this.chartDCPosition.Size = new System.Drawing.Size(811, 442);
            this.chartDCPosition.TabIndex = 34;
            this.chartDCPosition.Text = "chart1";
            title1.Name = "DC Motor Position";
            title1.Text = "DC Motor Position";
            this.chartDCPosition.Titles.Add(title1);
            // 
            // chartDCVelocity
            // 
            this.chartDCVelocity.Location = new System.Drawing.Point(587, 460);
            this.chartDCVelocity.Name = "chartDCVelocity";
            this.chartDCVelocity.Size = new System.Drawing.Size(811, 442);
            this.chartDCVelocity.TabIndex = 35;
            this.chartDCVelocity.Text = "chart2";
            title2.Name = "DC Motor Velocity";
            title2.Text = "DC Motor Velocity";
            this.chartDCVelocity.Titles.Add(title2);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1416, 915);
            this.Controls.Add(this.chartDCVelocity);
            this.Controls.Add(this.chartDCPosition);
            this.Controls.Add(this.btnContHalf);
            this.Controls.Add(this.btnContWhole);
            this.Controls.Add(this.btnCCWStepHalf);
            this.Controls.Add(this.btnCWStepHalf);
            this.Controls.Add(this.btnStopStep);
            this.Controls.Add(this.btnStopDC);
            this.Controls.Add(this.btnCCWStepWhole);
            this.Controls.Add(this.btnCWStepWhole);
            this.Controls.Add(this.lblStepperControl);
            this.Controls.Add(this.tbStepperSpeed);
            this.Controls.Add(this.tbDCDutyCycle);
            this.Controls.Add(this.cbAutoReconnect);
            this.Controls.Add(this.lblDataRate);
            this.Controls.Add(this.txtComOutput);
            this.Controls.Add(this.cbAlphNumOutput);
            this.Controls.Add(this.cbComResponse);
            this.Controls.Add(this.txtKeyboardInput);
            this.Controls.Add(this.lblKeyboardInput);
            this.Controls.Add(this.lblDCMotorControl);
            this.Controls.Add(this.btnComConnect);
            this.Controls.Add(this.txtBaudRate);
            this.Controls.Add(this.lblBaudRate);
            this.Controls.Add(this.cmbComPorts);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tbDCDutyCycle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbStepperSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDCPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDCVelocity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ComboBox cmbComPorts;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.TextBox txtBaudRate;
        private System.Windows.Forms.Button btnComConnect;
        private System.Windows.Forms.Label lblDCMotorControl;
        private System.Windows.Forms.Label lblKeyboardInput;
        private System.Windows.Forms.TextBox txtKeyboardInput;
        private System.Windows.Forms.CheckBox cbComResponse;
        private System.Windows.Forms.CheckBox cbAlphNumOutput;
        private System.Windows.Forms.TextBox txtComOutput;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblDataRate;
        private System.Windows.Forms.CheckBox cbAutoReconnect;
        private System.Windows.Forms.TrackBar tbDCDutyCycle;
        private System.Windows.Forms.TrackBar tbStepperSpeed;
        private System.Windows.Forms.Label lblStepperControl;
        private System.Windows.Forms.Button btnCWStepWhole;
        private System.Windows.Forms.Button btnCCWStepWhole;
        private System.Windows.Forms.Button btnStopDC;
        private System.Windows.Forms.Button btnStopStep;
        private System.Windows.Forms.Button btnCWStepHalf;
        private System.Windows.Forms.Button btnCCWStepHalf;
        private System.Windows.Forms.Button btnContWhole;
        private System.Windows.Forms.Button btnContHalf;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDCPosition;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDCVelocity;
    }
}

