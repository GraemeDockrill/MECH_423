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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxDataAx = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDataAy = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxDataAz = new System.Windows.Forms.TextBox();
            this.buttonProcessNewDataPoint = new System.Windows.Forms.Button();
            this.textBoxCurrentState = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDataHistory = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ax";
            // 
            // textBoxDataAx
            // 
            this.textBoxDataAx.Location = new System.Drawing.Point(47, 13);
            this.textBoxDataAx.Name = "textBoxDataAx";
            this.textBoxDataAx.Size = new System.Drawing.Size(100, 26);
            this.textBoxDataAx.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(154, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ay";
            // 
            // textBoxDataAy
            // 
            this.textBoxDataAy.Location = new System.Drawing.Point(188, 13);
            this.textBoxDataAy.Name = "textBoxDataAy";
            this.textBoxDataAy.Size = new System.Drawing.Size(100, 26);
            this.textBoxDataAy.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(295, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Az";
            // 
            // textBoxDataAz
            // 
            this.textBoxDataAz.Location = new System.Drawing.Point(330, 13);
            this.textBoxDataAz.Name = "textBoxDataAz";
            this.textBoxDataAz.Size = new System.Drawing.Size(100, 26);
            this.textBoxDataAz.TabIndex = 5;
            // 
            // buttonProcessNewDataPoint
            // 
            this.buttonProcessNewDataPoint.Location = new System.Drawing.Point(12, 45);
            this.buttonProcessNewDataPoint.Name = "buttonProcessNewDataPoint";
            this.buttonProcessNewDataPoint.Size = new System.Drawing.Size(417, 34);
            this.buttonProcessNewDataPoint.TabIndex = 6;
            this.buttonProcessNewDataPoint.Text = "Process New Data Point";
            this.buttonProcessNewDataPoint.UseVisualStyleBackColor = true;
            // 
            // textBoxCurrentState
            // 
            this.textBoxCurrentState.Location = new System.Drawing.Point(328, 86);
            this.textBoxCurrentState.Name = "textBoxCurrentState";
            this.textBoxCurrentState.Size = new System.Drawing.Size(100, 26);
            this.textBoxCurrentState.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(217, 86);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Current State";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 114);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "Data History";
            // 
            // textBoxDataHistory
            // 
            this.textBoxDataHistory.Location = new System.Drawing.Point(12, 138);
            this.textBoxDataHistory.Multiline = true;
            this.textBoxDataHistory.Name = "textBoxDataHistory";
            this.textBoxDataHistory.Size = new System.Drawing.Size(418, 300);
            this.textBoxDataHistory.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 450);
            this.Controls.Add(this.textBoxDataHistory);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxCurrentState);
            this.Controls.Add(this.buttonProcessNewDataPoint);
            this.Controls.Add(this.textBoxDataAz);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxDataAy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxDataAx);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxDataAx;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDataAy;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxDataAz;
        private System.Windows.Forms.Button buttonProcessNewDataPoint;
        private System.Windows.Forms.TextBox textBoxCurrentState;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDataHistory;
    }
}

