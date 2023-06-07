using System;

namespace SDRSharp.SDDE
{
    partial class ControlPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox_Longitude = new System.Windows.Forms.TextBox();
            textBox_Latitude = new System.Windows.Forms.TextBox();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // textBox_Longitude
            // 
            textBox_Longitude.Location = new System.Drawing.Point(83, 3);
            textBox_Longitude.Name = "textBox_Longitude";
            textBox_Longitude.Size = new System.Drawing.Size(100, 23);
            textBox_Longitude.TabIndex = 0;
            textBox_Longitude.Leave += textBox_Longitude_Leave;
            // 
            // textBox_Latitude
            // 
            textBox_Latitude.Location = new System.Drawing.Point(83, 35);
            textBox_Latitude.Name = "textBox_Latitude";
            textBox_Latitude.Size = new System.Drawing.Size(100, 23);
            textBox_Latitude.TabIndex = 1;
            textBox_Latitude.Leave += textBox_Latitude_Leave;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(8, 6);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(69, 17);
            label1.TabIndex = 2;
            label1.Text = "Longitude:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(8, 38);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(57, 17);
            label2.TabIndex = 3;
            label2.Text = "Latitude:";
            // 
            // ControlPanel
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox_Latitude);
            Controls.Add(textBox_Longitude);
            Name = "ControlPanel";
            Size = new System.Drawing.Size(375, 420);
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        public System.Windows.Forms.TextBox textBox_Longitude;
        public System.Windows.Forms.TextBox textBox_Latitude;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
