namespace SDRSharp.SDT
{
    partial class SatellitesForm
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
            comboBox_SatellitesType = new System.Windows.Forms.ComboBox();
            label_Satellites_Type = new System.Windows.Forms.Label();
            checkedListBox_Satellites = new System.Windows.Forms.CheckedListBox();
            SuspendLayout();
            // 
            // comboBox_SatellitesType
            // 
            comboBox_SatellitesType.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            comboBox_SatellitesType.FormattingEnabled = true;
            comboBox_SatellitesType.Location = new System.Drawing.Point(12, 29);
            comboBox_SatellitesType.Name = "comboBox_SatellitesType";
            comboBox_SatellitesType.Size = new System.Drawing.Size(325, 25);
            comboBox_SatellitesType.TabIndex = 0;
            comboBox_SatellitesType.DropDown += comboBox_SatellitesType_DropDown;
            comboBox_SatellitesType.SelectedIndexChanged += comboBox_SatellitesType_SelectedIndexChanged;
            // 
            // label_Satellites_Type
            // 
            label_Satellites_Type.AutoSize = true;
            label_Satellites_Type.Location = new System.Drawing.Point(12, 9);
            label_Satellites_Type.Name = "label_Satellites_Type";
            label_Satellites_Type.Size = new System.Drawing.Size(94, 17);
            label_Satellites_Type.TabIndex = 1;
            label_Satellites_Type.Text = "Satellites Type:";
            // 
            // checkedListBox_Satellites
            // 
            checkedListBox_Satellites.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            checkedListBox_Satellites.CheckOnClick = true;
            checkedListBox_Satellites.FormattingEnabled = true;
            checkedListBox_Satellites.Location = new System.Drawing.Point(12, 60);
            checkedListBox_Satellites.Name = "checkedListBox_Satellites";
            checkedListBox_Satellites.Size = new System.Drawing.Size(325, 328);
            checkedListBox_Satellites.Sorted = true;
            checkedListBox_Satellites.TabIndex = 2;
            checkedListBox_Satellites.ItemCheck += checkedListBox_Satellites_ItemCheck;
            // 
            // SatellitesForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(349, 402);
            Controls.Add(checkedListBox_Satellites);
            Controls.Add(label_Satellites_Type);
            Controls.Add(comboBox_SatellitesType);
            Name = "SatellitesForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "SatellitesForm";
            TopMost = true;
            FormClosing += SatellitesForm_FormClosing;
            Load += SatellitesForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox_SatellitesType;
        private System.Windows.Forms.Label label_Satellites_Type;
        private System.Windows.Forms.CheckedListBox checkedListBox_Satellites;
    }
}