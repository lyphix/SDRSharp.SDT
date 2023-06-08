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
            button_TLE = new System.Windows.Forms.Button();
            comboBox_Satelitetype = new System.Windows.Forms.ComboBox();
            checkedListBox_Satellites = new System.Windows.Forms.CheckedListBox();
            label_time = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // textBox_Longitude
            // 
            textBox_Longitude.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox_Longitude.Location = new System.Drawing.Point(83, 35);
            textBox_Longitude.Name = "textBox_Longitude";
            textBox_Longitude.Size = new System.Drawing.Size(210, 23);
            textBox_Longitude.TabIndex = 0;
            textBox_Longitude.Text = "0";
            textBox_Longitude.Leave += textBox_Longitude_Leave;
            // 
            // textBox_Latitude
            // 
            textBox_Latitude.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox_Latitude.Location = new System.Drawing.Point(83, 6);
            textBox_Latitude.Name = "textBox_Latitude";
            textBox_Latitude.Size = new System.Drawing.Size(210, 23);
            textBox_Latitude.TabIndex = 1;
            textBox_Latitude.Text = "0";
            textBox_Latitude.Leave += textBox_Latitude_Leave;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(8, 38);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(69, 17);
            label1.TabIndex = 2;
            label1.Text = "Longitude:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(8, 9);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(57, 17);
            label2.TabIndex = 3;
            label2.Text = "Latitude:";
            // 
            // button_TLE
            // 
            button_TLE.Location = new System.Drawing.Point(8, 64);
            button_TLE.Name = "button_TLE";
            button_TLE.Size = new System.Drawing.Size(75, 23);
            button_TLE.TabIndex = 4;
            button_TLE.Text = "TLE";
            button_TLE.UseVisualStyleBackColor = true;
            button_TLE.Click += button_TLE_Click;
            // 
            // comboBox_Satelitetype
            // 
            comboBox_Satelitetype.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_Satelitetype.FormattingEnabled = true;
            comboBox_Satelitetype.Location = new System.Drawing.Point(8, 93);
            comboBox_Satelitetype.Name = "comboBox_Satelitetype";
            comboBox_Satelitetype.Size = new System.Drawing.Size(285, 25);
            comboBox_Satelitetype.TabIndex = 5;
            comboBox_Satelitetype.SelectedIndexChanged += comboBox_Satelitetype_SelectedIndexChanged;
            // 
            // checkedListBox_Satellites
            // 
            checkedListBox_Satellites.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            checkedListBox_Satellites.CheckOnClick = true;
            checkedListBox_Satellites.FormattingEnabled = true;
            checkedListBox_Satellites.Location = new System.Drawing.Point(8, 124);
            checkedListBox_Satellites.Name = "checkedListBox_Satellites";
            checkedListBox_Satellites.Size = new System.Drawing.Size(285, 130);
            checkedListBox_Satellites.TabIndex = 7;
            // 
            // label_time
            // 
            label_time.AutoSize = true;
            label_time.Location = new System.Drawing.Point(89, 67);
            label_time.Name = "label_time";
            label_time.Size = new System.Drawing.Size(33, 17);
            label_time.TabIndex = 8;
            label_time.Text = "time";
            // 
            // ControlPanel
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(label_time);
            Controls.Add(checkedListBox_Satellites);
            Controls.Add(comboBox_Satelitetype);
            Controls.Add(button_TLE);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox_Latitude);
            Controls.Add(textBox_Longitude);
            Name = "ControlPanel";
            Size = new System.Drawing.Size(296, 420);
            Load += ControlPanel_Load;
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        public System.Windows.Forms.TextBox textBox_Longitude;
        public System.Windows.Forms.TextBox textBox_Latitude;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_TLE;
        private System.Windows.Forms.ComboBox comboBox_Satelitetype;
        private System.Windows.Forms.CheckedListBox checkedListBox_Satellites;
        private System.Windows.Forms.Label label_time;
    }
}
