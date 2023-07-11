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
            textBox_Degree = new System.Windows.Forms.TextBox();
            label3 = new System.Windows.Forms.Label();
            button_Update = new System.Windows.Forms.Button();
            button_Satellites = new System.Windows.Forms.Button();
            button_Refresh = new System.Windows.Forms.Button();
            label_SatelliteName = new System.Windows.Forms.Label();
            listView_SatelliteF = new System.Windows.Forms.ListView();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            listView_Satellitepass = new System.Windows.Forms.ListView();
            textBoxDopl = new System.Windows.Forms.TextBox();
            textBoxFreq = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            button_Doppler = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox_Longitude
            // 
            textBox_Longitude.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox_Longitude.Location = new System.Drawing.Point(83, 35);
            textBox_Longitude.Name = "textBox_Longitude";
            textBox_Longitude.Size = new System.Drawing.Size(377, 23);
            textBox_Longitude.TabIndex = 0;
            textBox_Longitude.Text = "0";
            textBox_Longitude.Leave += textBox_Longitude_Leave;
            // 
            // textBox_Latitude
            // 
            textBox_Latitude.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox_Latitude.Location = new System.Drawing.Point(83, 6);
            textBox_Latitude.Name = "textBox_Latitude";
            textBox_Latitude.Size = new System.Drawing.Size(377, 23);
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
            button_TLE.Location = new System.Drawing.Point(89, 93);
            button_TLE.Name = "button_TLE";
            button_TLE.Size = new System.Drawing.Size(57, 23);
            button_TLE.TabIndex = 4;
            button_TLE.Text = "TLE";
            button_TLE.UseVisualStyleBackColor = true;
            button_TLE.Click += button_TLE_Click;
            // 
            // label_time
            // 
            label_time.AutoSize = true;
            label_time.Location = new System.Drawing.Point(233, 96);
            label_time.Name = "label_time";
            label_time.Size = new System.Drawing.Size(56, 17);
            label_time.TabIndex = 8;
            label_time.Text = "00:00:00";
            // 
            // textBox_Degree
            // 
            textBox_Degree.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBox_Degree.Location = new System.Drawing.Point(83, 64);
            textBox_Degree.Name = "textBox_Degree";
            textBox_Degree.Size = new System.Drawing.Size(377, 23);
            textBox_Degree.TabIndex = 10;
            textBox_Degree.Text = "10";
            textBox_Degree.Leave += textBox_Degree_Leave;
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
            button_Update.Location = new System.Drawing.Point(295, 93);
            button_Update.Name = "button_Update";
            button_Update.Size = new System.Drawing.Size(62, 23);
            button_Update.TabIndex = 12;
            button_Update.Text = "Update";
            button_Update.UseVisualStyleBackColor = true;
            button_Update.Click += button_Update_Click;
            // 
            // button_Satellites
            // 
            button_Satellites.Location = new System.Drawing.Point(8, 93);
            button_Satellites.Name = "button_Satellites";
            button_Satellites.Size = new System.Drawing.Size(75, 23);
            button_Satellites.TabIndex = 13;
            button_Satellites.Text = "Satellites";
            button_Satellites.UseVisualStyleBackColor = true;
            button_Satellites.Click += button_Satellites_Click;
            // 
            // button_Refresh
            // 
            button_Refresh.Location = new System.Drawing.Point(152, 93);
            button_Refresh.Name = "button_Refresh";
            button_Refresh.Size = new System.Drawing.Size(75, 23);
            button_Refresh.TabIndex = 17;
            button_Refresh.Text = "Refresh";
            button_Refresh.UseVisualStyleBackColor = true;
            button_Refresh.Click += button_Refresh_Click;
            // 
            // label_SatelliteName
            // 
            label_SatelliteName.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label_SatelliteName.Location = new System.Drawing.Point(3, 119);
            label_SatelliteName.Name = "label_SatelliteName";
            label_SatelliteName.Size = new System.Drawing.Size(457, 27);
            label_SatelliteName.TabIndex = 18;
            label_SatelliteName.Text = "N/A";
            label_SatelliteName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listView_SatelliteF
            // 
            listView_SatelliteF.Dock = System.Windows.Forms.DockStyle.Fill;
            listView_SatelliteF.Location = new System.Drawing.Point(0, 0);
            listView_SatelliteF.Name = "listView_SatelliteF";
            listView_SatelliteF.Size = new System.Drawing.Size(463, 322);
            listView_SatelliteF.TabIndex = 21;
            listView_SatelliteF.UseCompatibleStateImageBehavior = false;
            listView_SatelliteF.ItemCheck += listView_SatelliteF_ItemCheck;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            splitContainer1.Location = new System.Drawing.Point(0, 239);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(listView_Satellitepass);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(listView_SatelliteF);
            splitContainer1.Size = new System.Drawing.Size(463, 598);
            splitContainer1.SplitterDistance = 272;
            splitContainer1.TabIndex = 26;
            // 
            // listView_Satellitepass
            // 
            listView_Satellitepass.Dock = System.Windows.Forms.DockStyle.Fill;
            listView_Satellitepass.Location = new System.Drawing.Point(0, 0);
            listView_Satellitepass.Name = "listView_Satellitepass";
            listView_Satellitepass.Size = new System.Drawing.Size(463, 272);
            listView_Satellitepass.TabIndex = 10;
            listView_Satellitepass.UseCompatibleStateImageBehavior = false;
            listView_Satellitepass.ItemCheck += listView_Satellitepass_ItemCheck;
            // 
            // textBoxDopl
            // 
            textBoxDopl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxDopl.Location = new System.Drawing.Point(83, 178);
            textBoxDopl.Name = "textBoxDopl";
            textBoxDopl.ReadOnly = true;
            textBoxDopl.Size = new System.Drawing.Size(377, 23);
            textBoxDopl.TabIndex = 22;
            // 
            // textBoxFreq
            // 
            textBoxFreq.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxFreq.Location = new System.Drawing.Point(83, 149);
            textBoxFreq.Name = "textBoxFreq";
            textBoxFreq.Size = new System.Drawing.Size(377, 23);
            textBoxFreq.TabIndex = 21;
            // 
            // label4
            // 
            label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(8, 152);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(70, 17);
            label4.TabIndex = 23;
            label4.Text = "Frequency:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(8, 181);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(59, 17);
            label5.TabIndex = 24;
            label5.Text = "Doppler:";
            // 
            // button_Doppler
            // 
            button_Doppler.Location = new System.Drawing.Point(8, 207);
            button_Doppler.Name = "button_Doppler";
            button_Doppler.Size = new System.Drawing.Size(75, 23);
            button_Doppler.TabIndex = 25;
            button_Doppler.Text = "Start";
            button_Doppler.UseVisualStyleBackColor = true;
            button_Doppler.Click += button_Doppler_Click;
            // 
            // ControlPanel
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(button_Doppler);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(splitContainer1);
            Controls.Add(label_SatelliteName);
            Controls.Add(textBoxDopl);
            Controls.Add(button_Refresh);
            Controls.Add(textBoxFreq);
            Controls.Add(button_Satellites);
            Controls.Add(button_Update);
            Controls.Add(label3);
            Controls.Add(textBox_Degree);
            Controls.Add(label_time);
            Controls.Add(button_TLE);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox_Latitude);
            Controls.Add(textBox_Longitude);
            Name = "ControlPanel";
            Size = new System.Drawing.Size(463, 776);
            Load += ControlPanel_Load;
            Resize += ControlPanel_Resize;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.TextBox textBox_Degree;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_Update;
        private System.Windows.Forms.Button button_Satellites;
        private System.Windows.Forms.Button button_Refresh;
        private System.Windows.Forms.Label label_SatelliteName;
        private System.Windows.Forms.ListView listView_SatelliteF;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView listView_Satellitepass;
        private System.Windows.Forms.TextBox textBoxDopl;
        private System.Windows.Forms.TextBox textBoxFreq;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_Doppler;
    }
}
