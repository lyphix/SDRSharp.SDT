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
            label_time = new System.Windows.Forms.Label();
            dataGridView_Satellitepass = new System.Windows.Forms.DataGridView();
            textBox_Degree = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            button_Update = new System.Windows.Forms.Button();
            button_Satellites = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView_Satellitepass).BeginInit();
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
            textBox_Longitude.TextChanged += textBox_Longitude_TextChanged;
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
            button_TLE.Location = new System.Drawing.Point(8, 93);
            button_TLE.Name = "button_TLE";
            button_TLE.Size = new System.Drawing.Size(57, 23);
            button_TLE.TabIndex = 4;
            button_TLE.Text = "TLE";
            button_TLE.UseVisualStyleBackColor = true;
            button_TLE.Click += button_TLE_Click;
            // 
            // label_time
            // 
            label_time.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label_time.AutoSize = true;
            label_time.Location = new System.Drawing.Point(245, 93);
            label_time.Name = "label_time";
            label_time.Size = new System.Drawing.Size(33, 17);
            label_time.TabIndex = 8;
            label_time.Text = "time";
            // 
            // dataGridView_Satellitepass
            // 
            dataGridView_Satellitepass.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dataGridView_Satellitepass.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView_Satellitepass.Location = new System.Drawing.Point(8, 122);
            dataGridView_Satellitepass.Name = "dataGridView_Satellitepass";
            dataGridView_Satellitepass.RowTemplate.Height = 25;
            dataGridView_Satellitepass.Size = new System.Drawing.Size(285, 320);
            dataGridView_Satellitepass.TabIndex = 9;
            // 
            // textBox_Degree
            // 
            textBox_Degree.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox_Degree.Location = new System.Drawing.Point(83, 64);
            textBox_Degree.Name = "textBox_Degree";
            textBox_Degree.Size = new System.Drawing.Size(210, 23);
            textBox_Degree.TabIndex = 10;
            textBox_Degree.Text = "10";
            textBox_Degree.TextChanged += textBox_Degree_TextChanged;
            // 
            // label3
            // 
            label3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(8, 67);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(54, 17);
            label3.TabIndex = 11;
            label3.Text = "Degree:";
            // 
            // button_Update
            // 
            button_Update.Location = new System.Drawing.Point(152, 93);
            button_Update.Name = "button_Update";
            button_Update.Size = new System.Drawing.Size(63, 23);
            button_Update.TabIndex = 12;
            button_Update.Text = "Update";
            button_Update.UseVisualStyleBackColor = true;
            button_Update.Click += button_Update_Click;
            // 
            // button_Satellites
            // 
            button_Satellites.Location = new System.Drawing.Point(71, 93);
            button_Satellites.Name = "button_Satellites";
            button_Satellites.Size = new System.Drawing.Size(75, 23);
            button_Satellites.TabIndex = 13;
            button_Satellites.Text = "Satellites";
            button_Satellites.UseVisualStyleBackColor = true;
            button_Satellites.Click += button_Satellites_Click;
            // 
            // ControlPanel
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(button_Satellites);
            Controls.Add(button_Update);
            Controls.Add(label3);
            Controls.Add(textBox_Degree);
            Controls.Add(dataGridView_Satellitepass);
            Controls.Add(label_time);
            Controls.Add(button_TLE);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox_Latitude);
            Controls.Add(textBox_Longitude);
            Name = "ControlPanel";
            Size = new System.Drawing.Size(296, 566);
            Load += ControlPanel_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView_Satellitepass).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }


        #endregion

        public System.Windows.Forms.TextBox textBox_Longitude;
        public System.Windows.Forms.TextBox textBox_Latitude;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_TLE;
        private System.Windows.Forms.Label label_time;
        private System.Windows.Forms.DataGridView dataGridView_Satellitepass;
        private System.Windows.Forms.TextBox textBox_Degree;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_Update;
        private System.Windows.Forms.Button button_Satellites;
    }
}
