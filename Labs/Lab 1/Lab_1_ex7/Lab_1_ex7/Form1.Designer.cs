namespace Lab_1_ex7
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
            this.labelAx = new System.Windows.Forms.Label();
            this.textBoxDataAx = new System.Windows.Forms.TextBox();
            this.labelAy = new System.Windows.Forms.Label();
            this.textBoxDataAy = new System.Windows.Forms.TextBox();
            this.labelAz = new System.Windows.Forms.Label();
            this.textBoxDataAz = new System.Windows.Forms.TextBox();
            this.buttonProcessNewDataPoint = new System.Windows.Forms.Button();
            this.textBoxCurrentState = new System.Windows.Forms.TextBox();
            this.labelCurrentState = new System.Windows.Forms.Label();
            this.labelDataHistory = new System.Windows.Forms.Label();
            this.textBoxDataHistory = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelAx
            // 
            this.labelAx.AutoSize = true;
            this.labelAx.Location = new System.Drawing.Point(9, 8);
            this.labelAx.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAx.Name = "labelAx";
            this.labelAx.Size = new System.Drawing.Size(19, 13);
            this.labelAx.TabIndex = 0;
            this.labelAx.Text = "Ax";
            // 
            // textBoxDataAx
            // 
            this.textBoxDataAx.Location = new System.Drawing.Point(31, 8);
            this.textBoxDataAx.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxDataAx.Name = "textBoxDataAx";
            this.textBoxDataAx.Size = new System.Drawing.Size(68, 20);
            this.textBoxDataAx.TabIndex = 1;
            // 
            // labelAy
            // 
            this.labelAy.AutoSize = true;
            this.labelAy.Location = new System.Drawing.Point(103, 8);
            this.labelAy.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAy.Name = "labelAy";
            this.labelAy.Size = new System.Drawing.Size(19, 13);
            this.labelAy.TabIndex = 2;
            this.labelAy.Text = "Ay";
            // 
            // textBoxDataAy
            // 
            this.textBoxDataAy.Location = new System.Drawing.Point(125, 8);
            this.textBoxDataAy.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxDataAy.Name = "textBoxDataAy";
            this.textBoxDataAy.Size = new System.Drawing.Size(68, 20);
            this.textBoxDataAy.TabIndex = 3;
            // 
            // labelAz
            // 
            this.labelAz.AutoSize = true;
            this.labelAz.Location = new System.Drawing.Point(197, 8);
            this.labelAz.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAz.Name = "labelAz";
            this.labelAz.Size = new System.Drawing.Size(19, 13);
            this.labelAz.TabIndex = 4;
            this.labelAz.Text = "Az";
            // 
            // textBoxDataAz
            // 
            this.textBoxDataAz.Location = new System.Drawing.Point(220, 8);
            this.textBoxDataAz.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxDataAz.Name = "textBoxDataAz";
            this.textBoxDataAz.Size = new System.Drawing.Size(68, 20);
            this.textBoxDataAz.TabIndex = 5;
            // 
            // buttonProcessNewDataPoint
            // 
            this.buttonProcessNewDataPoint.Location = new System.Drawing.Point(8, 29);
            this.buttonProcessNewDataPoint.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonProcessNewDataPoint.Name = "buttonProcessNewDataPoint";
            this.buttonProcessNewDataPoint.Size = new System.Drawing.Size(278, 22);
            this.buttonProcessNewDataPoint.TabIndex = 6;
            this.buttonProcessNewDataPoint.Text = "Process New Data Point";
            this.buttonProcessNewDataPoint.UseVisualStyleBackColor = true;
            this.buttonProcessNewDataPoint.Click += new System.EventHandler(this.buttonProcessNewDataPoint_Click);
            // 
            // textBoxCurrentState
            // 
            this.textBoxCurrentState.Location = new System.Drawing.Point(219, 56);
            this.textBoxCurrentState.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxCurrentState.Name = "textBoxCurrentState";
            this.textBoxCurrentState.Size = new System.Drawing.Size(68, 20);
            this.textBoxCurrentState.TabIndex = 7;
            // 
            // labelCurrentState
            // 
            this.labelCurrentState.AutoSize = true;
            this.labelCurrentState.Location = new System.Drawing.Point(145, 56);
            this.labelCurrentState.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCurrentState.Name = "labelCurrentState";
            this.labelCurrentState.Size = new System.Drawing.Size(69, 13);
            this.labelCurrentState.TabIndex = 8;
            this.labelCurrentState.Text = "Current State";
            // 
            // labelDataHistory
            // 
            this.labelDataHistory.AutoSize = true;
            this.labelDataHistory.Location = new System.Drawing.Point(8, 74);
            this.labelDataHistory.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelDataHistory.Name = "labelDataHistory";
            this.labelDataHistory.Size = new System.Drawing.Size(65, 13);
            this.labelDataHistory.TabIndex = 9;
            this.labelDataHistory.Text = "Data History";
            // 
            // textBoxDataHistory
            // 
            this.textBoxDataHistory.Location = new System.Drawing.Point(8, 90);
            this.textBoxDataHistory.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBoxDataHistory.Multiline = true;
            this.textBoxDataHistory.Name = "textBoxDataHistory";
            this.textBoxDataHistory.Size = new System.Drawing.Size(280, 196);
            this.textBoxDataHistory.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 292);
            this.Controls.Add(this.textBoxDataHistory);
            this.Controls.Add(this.labelDataHistory);
            this.Controls.Add(this.labelCurrentState);
            this.Controls.Add(this.textBoxCurrentState);
            this.Controls.Add(this.buttonProcessNewDataPoint);
            this.Controls.Add(this.textBoxDataAz);
            this.Controls.Add(this.labelAz);
            this.Controls.Add(this.textBoxDataAy);
            this.Controls.Add(this.labelAy);
            this.Controls.Add(this.textBoxDataAx);
            this.Controls.Add(this.labelAx);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelAx;
        private System.Windows.Forms.TextBox textBoxDataAx;
        private System.Windows.Forms.Label labelAy;
        private System.Windows.Forms.TextBox textBoxDataAy;
        private System.Windows.Forms.Label labelAz;
        private System.Windows.Forms.TextBox textBoxDataAz;
        private System.Windows.Forms.Button buttonProcessNewDataPoint;
        private System.Windows.Forms.TextBox textBoxCurrentState;
        private System.Windows.Forms.Label labelCurrentState;
        private System.Windows.Forms.Label labelDataHistory;
        private System.Windows.Forms.TextBox textBoxDataHistory;

        int wait = 0;
        int waitCycles = 5;

        // enum for states
        enum states
        {
            waitForData,
            punch,
            initiateRightHook,
            RightHook,
            initiateHighPunch,
            HighPunch
            
        }

        states state = states.waitForData;

    }
}

