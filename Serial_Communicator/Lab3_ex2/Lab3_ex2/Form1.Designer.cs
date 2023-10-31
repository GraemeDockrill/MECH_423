
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
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.cmbComPorts = new System.Windows.Forms.ComboBox();
            this.lblBaudRate = new System.Windows.Forms.Label();
            this.txtBaudRate = new System.Windows.Forms.TextBox();
            this.btnComConnect = new System.Windows.Forms.Button();
            this.lblPackagedInput = new System.Windows.Forms.Label();
            this.cbByte1 = new System.Windows.Forms.CheckBox();
            this.cbByte2 = new System.Windows.Forms.CheckBox();
            this.cbByte3 = new System.Windows.Forms.CheckBox();
            this.cbByte4 = new System.Windows.Forms.CheckBox();
            this.cbByte5 = new System.Windows.Forms.CheckBox();
            this.txtByte1 = new System.Windows.Forms.TextBox();
            this.txtByte2 = new System.Windows.Forms.TextBox();
            this.txtByte3 = new System.Windows.Forms.TextBox();
            this.txtByte4 = new System.Windows.Forms.TextBox();
            this.txtByte5 = new System.Windows.Forms.TextBox();
            this.btnComTransmit = new System.Windows.Forms.Button();
            this.lblKeyboardInput = new System.Windows.Forms.Label();
            this.txtKeyboardInput = new System.Windows.Forms.TextBox();
            this.cbComResponse = new System.Windows.Forms.CheckBox();
            this.cbAlphNumOutput = new System.Windows.Forms.CheckBox();
            this.txtComOutput = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblDataRate = new System.Windows.Forms.Label();
            this.cbAutoReconnect = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // cmbComPorts
            // 
            this.cmbComPorts.FormattingEnabled = true;
            this.cmbComPorts.Location = new System.Drawing.Point(12, 12);
            this.cmbComPorts.Name = "cmbComPorts";
            this.cmbComPorts.Size = new System.Drawing.Size(121, 21);
            this.cmbComPorts.TabIndex = 0;
            this.cmbComPorts.DropDown += new System.EventHandler(this.cmbComPorts_DropDown);
            // 
            // lblBaudRate
            // 
            this.lblBaudRate.AutoSize = true;
            this.lblBaudRate.Location = new System.Drawing.Point(139, 15);
            this.lblBaudRate.Name = "lblBaudRate";
            this.lblBaudRate.Size = new System.Drawing.Size(61, 13);
            this.lblBaudRate.TabIndex = 1;
            this.lblBaudRate.Text = "Baud Rate:";
            // 
            // txtBaudRate
            // 
            this.txtBaudRate.Location = new System.Drawing.Point(206, 12);
            this.txtBaudRate.Name = "txtBaudRate";
            this.txtBaudRate.Size = new System.Drawing.Size(100, 20);
            this.txtBaudRate.TabIndex = 2;
            this.txtBaudRate.Text = "9600";
            // 
            // btnComConnect
            // 
            this.btnComConnect.Location = new System.Drawing.Point(312, 10);
            this.btnComConnect.Name = "btnComConnect";
            this.btnComConnect.Size = new System.Drawing.Size(75, 23);
            this.btnComConnect.TabIndex = 3;
            this.btnComConnect.Text = "Connect";
            this.btnComConnect.UseVisualStyleBackColor = true;
            this.btnComConnect.Click += new System.EventHandler(this.btnComConnect_Click);
            // 
            // lblPackagedInput
            // 
            this.lblPackagedInput.AutoSize = true;
            this.lblPackagedInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPackagedInput.Location = new System.Drawing.Point(12, 36);
            this.lblPackagedInput.Name = "lblPackagedInput";
            this.lblPackagedInput.Size = new System.Drawing.Size(140, 20);
            this.lblPackagedInput.TabIndex = 4;
            this.lblPackagedInput.Text = "Packaged Input:";
            // 
            // cbByte1
            // 
            this.cbByte1.AutoSize = true;
            this.cbByte1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbByte1.Location = new System.Drawing.Point(12, 59);
            this.cbByte1.Name = "cbByte1";
            this.cbByte1.Size = new System.Drawing.Size(71, 20);
            this.cbByte1.TabIndex = 5;
            this.cbByte1.Text = "Byte #1";
            this.cbByte1.UseVisualStyleBackColor = true;
            this.cbByte1.CheckedChanged += new System.EventHandler(this.cbByte1_CheckedChanged);
            // 
            // cbByte2
            // 
            this.cbByte2.AutoSize = true;
            this.cbByte2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbByte2.Location = new System.Drawing.Point(89, 59);
            this.cbByte2.Name = "cbByte2";
            this.cbByte2.Size = new System.Drawing.Size(71, 20);
            this.cbByte2.TabIndex = 6;
            this.cbByte2.Text = "Byte #2";
            this.cbByte2.UseVisualStyleBackColor = true;
            this.cbByte2.CheckedChanged += new System.EventHandler(this.cbByte2_CheckedChanged);
            // 
            // cbByte3
            // 
            this.cbByte3.AutoSize = true;
            this.cbByte3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbByte3.Location = new System.Drawing.Point(166, 59);
            this.cbByte3.Name = "cbByte3";
            this.cbByte3.Size = new System.Drawing.Size(71, 20);
            this.cbByte3.TabIndex = 7;
            this.cbByte3.Text = "Byte #3";
            this.cbByte3.UseVisualStyleBackColor = true;
            this.cbByte3.CheckedChanged += new System.EventHandler(this.cbByte3_CheckedChanged);
            // 
            // cbByte4
            // 
            this.cbByte4.AutoSize = true;
            this.cbByte4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbByte4.Location = new System.Drawing.Point(243, 59);
            this.cbByte4.Name = "cbByte4";
            this.cbByte4.Size = new System.Drawing.Size(71, 20);
            this.cbByte4.TabIndex = 8;
            this.cbByte4.Text = "Byte #4";
            this.cbByte4.UseVisualStyleBackColor = true;
            this.cbByte4.CheckedChanged += new System.EventHandler(this.cbByte4_CheckedChanged);
            // 
            // cbByte5
            // 
            this.cbByte5.AutoSize = true;
            this.cbByte5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbByte5.Location = new System.Drawing.Point(320, 59);
            this.cbByte5.Name = "cbByte5";
            this.cbByte5.Size = new System.Drawing.Size(71, 20);
            this.cbByte5.TabIndex = 9;
            this.cbByte5.Text = "Byte #5";
            this.cbByte5.UseVisualStyleBackColor = true;
            this.cbByte5.CheckedChanged += new System.EventHandler(this.cbByte5_CheckedChanged);
            // 
            // txtByte1
            // 
            this.txtByte1.Location = new System.Drawing.Point(12, 86);
            this.txtByte1.Name = "txtByte1";
            this.txtByte1.Size = new System.Drawing.Size(71, 20);
            this.txtByte1.TabIndex = 10;
            this.txtByte1.TextChanged += new System.EventHandler(this.Byte_TextChanged);
            // 
            // txtByte2
            // 
            this.txtByte2.Location = new System.Drawing.Point(89, 86);
            this.txtByte2.Name = "txtByte2";
            this.txtByte2.Size = new System.Drawing.Size(71, 20);
            this.txtByte2.TabIndex = 11;
            this.txtByte2.TextChanged += new System.EventHandler(this.Byte_TextChanged);
            // 
            // txtByte3
            // 
            this.txtByte3.Location = new System.Drawing.Point(166, 86);
            this.txtByte3.Name = "txtByte3";
            this.txtByte3.Size = new System.Drawing.Size(71, 20);
            this.txtByte3.TabIndex = 12;
            this.txtByte3.TextChanged += new System.EventHandler(this.Byte_TextChanged);
            // 
            // txtByte4
            // 
            this.txtByte4.Location = new System.Drawing.Point(243, 86);
            this.txtByte4.Name = "txtByte4";
            this.txtByte4.Size = new System.Drawing.Size(71, 20);
            this.txtByte4.TabIndex = 13;
            this.txtByte4.TextChanged += new System.EventHandler(this.Byte_TextChanged);
            // 
            // txtByte5
            // 
            this.txtByte5.Location = new System.Drawing.Point(320, 86);
            this.txtByte5.Name = "txtByte5";
            this.txtByte5.Size = new System.Drawing.Size(71, 20);
            this.txtByte5.TabIndex = 14;
            this.txtByte5.TextChanged += new System.EventHandler(this.Byte_TextChanged);
            // 
            // btnComTransmit
            // 
            this.btnComTransmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnComTransmit.Location = new System.Drawing.Point(12, 112);
            this.btnComTransmit.Name = "btnComTransmit";
            this.btnComTransmit.Size = new System.Drawing.Size(379, 42);
            this.btnComTransmit.TabIndex = 15;
            this.btnComTransmit.Text = "Transmit to COM Port";
            this.btnComTransmit.UseVisualStyleBackColor = true;
            this.btnComTransmit.Click += new System.EventHandler(this.btnComTransmit_Click);
            // 
            // lblKeyboardInput
            // 
            this.lblKeyboardInput.AutoSize = true;
            this.lblKeyboardInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKeyboardInput.Location = new System.Drawing.Point(12, 157);
            this.lblKeyboardInput.Name = "lblKeyboardInput";
            this.lblKeyboardInput.Size = new System.Drawing.Size(136, 20);
            this.lblKeyboardInput.TabIndex = 16;
            this.lblKeyboardInput.Text = "Keyboard Input:";
            // 
            // txtKeyboardInput
            // 
            this.txtKeyboardInput.Location = new System.Drawing.Point(12, 180);
            this.txtKeyboardInput.Multiline = true;
            this.txtKeyboardInput.Name = "txtKeyboardInput";
            this.txtKeyboardInput.Size = new System.Drawing.Size(379, 53);
            this.txtKeyboardInput.TabIndex = 17;
            // 
            // cbComResponse
            // 
            this.cbComResponse.AutoSize = true;
            this.cbComResponse.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbComResponse.Location = new System.Drawing.Point(12, 239);
            this.cbComResponse.Name = "cbComResponse";
            this.cbComResponse.Size = new System.Drawing.Size(202, 22);
            this.cbComResponse.TabIndex = 18;
            this.cbComResponse.Text = "Response from COM Port";
            this.cbComResponse.UseVisualStyleBackColor = true;
            // 
            // cbAlphNumOutput
            // 
            this.cbAlphNumOutput.AutoSize = true;
            this.cbAlphNumOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAlphNumOutput.Location = new System.Drawing.Point(227, 239);
            this.cbAlphNumOutput.Name = "cbAlphNumOutput";
            this.cbAlphNumOutput.Size = new System.Drawing.Size(164, 22);
            this.cbAlphNumOutput.TabIndex = 19;
            this.cbAlphNumOutput.Text = "Alphanumeric Output";
            this.cbAlphNumOutput.UseVisualStyleBackColor = true;
            // 
            // txtComOutput
            // 
            this.txtComOutput.Location = new System.Drawing.Point(12, 267);
            this.txtComOutput.Multiline = true;
            this.txtComOutput.Name = "txtComOutput";
            this.txtComOutput.Size = new System.Drawing.Size(379, 171);
            this.txtComOutput.TabIndex = 20;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblDataRate
            // 
            this.lblDataRate.AutoSize = true;
            this.lblDataRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataRate.Location = new System.Drawing.Point(12, 441);
            this.lblDataRate.Name = "lblDataRate";
            this.lblDataRate.Size = new System.Drawing.Size(303, 20);
            this.lblDataRate.TabIndex = 21;
            this.lblDataRate.Text = "Incoming Data Rate = 0 bytes per second";
            // 
            // cbAutoReconnect
            // 
            this.cbAutoReconnect.AutoSize = true;
            this.cbAutoReconnect.Location = new System.Drawing.Point(283, 36);
            this.cbAutoReconnect.Name = "cbAutoReconnect";
            this.cbAutoReconnect.Size = new System.Drawing.Size(104, 17);
            this.cbAutoReconnect.TabIndex = 22;
            this.cbAutoReconnect.Text = "Auto Reconnect";
            this.cbAutoReconnect.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 470);
            this.Controls.Add(this.cbAutoReconnect);
            this.Controls.Add(this.lblDataRate);
            this.Controls.Add(this.txtComOutput);
            this.Controls.Add(this.cbAlphNumOutput);
            this.Controls.Add(this.cbComResponse);
            this.Controls.Add(this.txtKeyboardInput);
            this.Controls.Add(this.lblKeyboardInput);
            this.Controls.Add(this.btnComTransmit);
            this.Controls.Add(this.txtByte5);
            this.Controls.Add(this.txtByte4);
            this.Controls.Add(this.txtByte3);
            this.Controls.Add(this.txtByte2);
            this.Controls.Add(this.txtByte1);
            this.Controls.Add(this.cbByte5);
            this.Controls.Add(this.cbByte4);
            this.Controls.Add(this.cbByte3);
            this.Controls.Add(this.cbByte2);
            this.Controls.Add(this.cbByte1);
            this.Controls.Add(this.lblPackagedInput);
            this.Controls.Add(this.btnComConnect);
            this.Controls.Add(this.txtBaudRate);
            this.Controls.Add(this.lblBaudRate);
            this.Controls.Add(this.cmbComPorts);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.ComboBox cmbComPorts;
        private System.Windows.Forms.Label lblBaudRate;
        private System.Windows.Forms.TextBox txtBaudRate;
        private System.Windows.Forms.Button btnComConnect;
        private System.Windows.Forms.Label lblPackagedInput;
        private System.Windows.Forms.CheckBox cbByte1;
        private System.Windows.Forms.CheckBox cbByte2;
        private System.Windows.Forms.CheckBox cbByte3;
        private System.Windows.Forms.CheckBox cbByte4;
        private System.Windows.Forms.CheckBox cbByte5;
        private System.Windows.Forms.TextBox txtByte1;
        private System.Windows.Forms.TextBox txtByte2;
        private System.Windows.Forms.TextBox txtByte3;
        private System.Windows.Forms.TextBox txtByte4;
        private System.Windows.Forms.TextBox txtByte5;
        private System.Windows.Forms.Button btnComTransmit;
        private System.Windows.Forms.Label lblKeyboardInput;
        private System.Windows.Forms.TextBox txtKeyboardInput;
        private System.Windows.Forms.CheckBox cbComResponse;
        private System.Windows.Forms.CheckBox cbAlphNumOutput;
        private System.Windows.Forms.TextBox txtComOutput;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblDataRate;
        private System.Windows.Forms.CheckBox cbAutoReconnect;
    }
}

