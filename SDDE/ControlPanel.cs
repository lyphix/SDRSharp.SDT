using SDRSharp.Common;
using System;
using System.Data;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using CsvHelper;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Linq;

namespace SDRSharp.SDDE
{
    public partial class ControlPanel : UserControl
    {
        private ISharpControl _control;

        public ControlPanel(ISharpControl control)
        {
            _control = control;
            InitializeComponent();
        }

        private void textBox_Longitude_Leave(object sender, System.EventArgs e)
        {
            double longitude;
            if (!double.TryParse(textBox_Longitude.Text, out longitude) || longitude < -180 || longitude > 180)
            {
                textBox_Longitude.Text = "0";
            }
        }

        private void textBox_Latitude_Leave(object sender, System.EventArgs e)
        {
            double latitude;
            if (!double.TryParse(textBox_Latitude.Text, out latitude) || latitude < -90 || latitude > 90)
            {
                textBox_Latitude.Text = "0";
            }
        }

        private void button_TLE_Click(object sender, EventArgs e)
        {
            TLE_list tLE_List = new TLE_list();
            tLE_List.Show();
        }

        private void ControlPanel_Load(object sender, EventArgs e)
        {
            LoadSatelliteTypes();
            Timer timer = new Timer();
            timer.Interval = 1000; // 设置定时器间隔为1秒
            timer.Tick += Timer_Tick;
            timer.Start();

            LoadSettings();

        }

        private void comboBox_Satelitetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBox_Satellites.Items.Clear(); // 清空列表

            string selectedFileName = comboBox_Satelitetype.SelectedItem.ToString();
            string directoryPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TLE");
            string filePath = Path.Combine(directoryPath, $"{selectedFileName}.csv");

            if (File.Exists(filePath))
            {
                DataTable csvData = ReadCsvFile(filePath);

                foreach (DataRow row in csvData.Rows)
                {
                    string objectName = row["OBJECT_NAME"].ToString();
                    checkedListBox_Satellites.Items.Add(objectName);
                }
            }
        }

        private void LoadSatelliteTypes()
        {
            string directoryPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TLE");
            if (Directory.Exists(directoryPath))
            {
                string[] csvFiles = Directory.GetFiles(directoryPath, "*.csv");
                foreach (string csvFile in csvFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(csvFile);
                    comboBox_Satelitetype.Items.Add(fileName);
                }
            }
        }
        private DataTable ReadCsvFile(string filePath)
        {
            DataTable csvData = new DataTable();

            using (var parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                bool isFirstRow = true;
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    if (isFirstRow)
                    {
                        foreach (string field in fields)
                        {
                            csvData.Columns.Add(field);
                        }
                        isFirstRow = false;
                    }
                    else
                    {
                        csvData.Rows.Add(fields);
                    }
                }
            }

            return csvData;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // 更新 Label 的文本为当前时间
            label_time.Text = DateTime.Now.ToString("HH:mm:ss");
            SaveSettings();
        }
        private void SaveSettings()
        {
            // 保存 SatelliteType 的选择
            if (comboBox_Satelitetype.SelectedItem != null)
            {
                Properties.Settings.Default.SatelliteType = comboBox_Satelitetype.SelectedItem.ToString();
            }

            // 保存 SelectedSatellites 的选择
            List<string> selectedSatellites = new List<string>();
            foreach (var item in checkedListBox_Satellites.CheckedItems)
            {
                selectedSatellites.Add(item.ToString());
            }

            if (selectedSatellites.Count > 0)
            {
                Properties.Settings.Default.SelectedSatellites = new System.Collections.Specialized.StringCollection();
                Properties.Settings.Default.SelectedSatellites.AddRange(selectedSatellites.ToArray());
            }

            // 保存经度和纬度的值
            if (!string.IsNullOrEmpty(textBox_Longitude.Text))
            {
                Properties.Settings.Default.Longitude = textBox_Longitude.Text;
            }

            if (!string.IsNullOrEmpty(textBox_Latitude.Text))
            {
                Properties.Settings.Default.Latitude = textBox_Latitude.Text;
            }

            // 保存设置
            Properties.Settings.Default.Save();
        }


        private void LoadSettings()
        {
            // 恢复 SatelliteType 的选择
            if (!string.IsNullOrEmpty(Properties.Settings.Default.SatelliteType))
            {
                comboBox_Satelitetype.SelectedItem = Properties.Settings.Default.SatelliteType;
            }

            // 恢复 SelectedSatellites 的选择
            if (Properties.Settings.Default.SelectedSatellites != null)
            {
                List<string> selectedSatellites = new List<string>(Properties.Settings.Default.SelectedSatellites.Cast<string>());
                foreach (string satellite in selectedSatellites)
                {
                    int index = checkedListBox_Satellites.FindStringExact(satellite);
                    if (index != ListBox.NoMatches)
                    {
                        checkedListBox_Satellites.SetItemChecked(index, true);
                    }
                }
            }

            // 恢复经度和纬度的值
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Longitude))
            {
                textBox_Longitude.Text = Properties.Settings.Default.Longitude;
            }
            if (!string.IsNullOrEmpty(Properties.Settings.Default.Latitude))
            {
                textBox_Latitude.Text = Properties.Settings.Default.Latitude;
            }
        }

    }
}
