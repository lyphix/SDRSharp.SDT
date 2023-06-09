using SDRSharp.Common;
using System;
using System.Data;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;
using System.Linq;
using SGPdotNET;
using SGPdotNET.CoordinateSystem;
using SGPdotNET.Util;
using SGPdotNET.TLE;
using SGPdotNET.Observation;
using Telerik.WinControls.VirtualKeyboard;

namespace SDRSharp.SDDE
{
    public partial class ControlPanel : UserControl
    {
        private LocalTleProvider provider;
        private Dictionary<int, Tle> tles;

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

            Timer timer = new Timer();
            timer.Interval = 1000; // 设置定时器间隔为1秒
            timer.Tick += Timer_Tick;
            timer.Start();

            LoadSatelliteTypes();

            LoadSettings();


        }

        private void comboBox_Satelitetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkedListBox_Satellites.Items.Clear(); // 清空列表

            string selectedFileName = comboBox_Satelitetype.SelectedItem.ToString();
            string directoryPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TLE");
            string filePath = Path.Combine(directoryPath, $"{selectedFileName}.txt");

            if (File.Exists(filePath))
            {
                provider = new LocalTleProvider(true, filePath);
                tles = provider.GetTles();
                foreach (var kvp in tles)
                {
                    checkedListBox_Satellites.Items.Add(kvp.Value.Name);
                }
            }
        }

        private void LoadSatelliteTypes()
        {
            string directoryPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TLE");
            if (Directory.Exists(directoryPath))
            {
                string[] txtFiles = Directory.GetFiles(directoryPath, "*.txt");
                foreach (string txtFile in txtFiles)
                {
                    string fileName = Path.GetFileNameWithoutExtension(txtFile);
                    comboBox_Satelitetype.Items.Add(fileName);
                }
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // 更新 Label 的文本为当前时间
            label_time.Text = DateTime.Now.ToString("HH:mm:ss");
            SaveSettings();

            if (provider != null && tles != null)
            {
                // Get latitude and longitude from text boxes
                double latitude = double.Parse(textBox_Latitude.Text);
                double longitude = double.Parse(textBox_Longitude.Text);

                // Set up our ground station location
                var location = new GeodeticCoordinate(Angle.FromDegrees(latitude), Angle.FromDegrees(longitude), 0);

                // Create a ground station
                var groundStation = new GroundStation(location);

                List<SatelliteVisibilityPeriod> allObservations = new List<SatelliteVisibilityPeriod>();

                foreach (var item in checkedListBox_Satellites.CheckedItems)
                {
                    foreach (var kvp in tles)
                    {
                        if (kvp.Value.Name == item.ToString())
                        {
                            // Create a satellite from the TLE
                            var sat = new Satellite(kvp.Value.Name, kvp.Value.Line1, kvp.Value.Line2);

                            // Observe the satellite
                            double degree = double.Parse(textBox_Degree.Text);

                            var observations = groundStation.Observe(
                                sat,
                                DateTime.UtcNow,
                                DateTime.UtcNow + TimeSpan.FromHours(24),
                                TimeSpan.FromSeconds(10),
                                minElevation: Angle.FromDegrees(degree),
                                clipToEndTime: true
                            );

                            // 将这个卫星的所有观测结果加入到allObservations列表中
                            allObservations.AddRange(observations);

                        }
                    }
                }

                allObservations.Sort((a, b) => a.Start.CompareTo(b.Start));
                dataGridView_Satellitepass.DataSource = allObservations;

            }


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

        private void comboBox_Satelitetype_DropDown(object sender, EventArgs e)
        {
            comboBox_Satelitetype.Items.Clear();
            LoadSatelliteTypes();
        }

        private void textBox_Degree_TextChanged(object sender, EventArgs e)
        {
            double degree;
            if (!double.TryParse(textBox_Degree.Text, out degree) || degree < 0 || degree > 90)
            {
                textBox_Longitude.Text = "10";
            }
        }

        private void textBox_Longitude_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
