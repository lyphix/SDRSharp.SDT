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
using System.Text;
using System.Drawing;

namespace SDRSharp.SDDE
{
    public static class SatKey
    {
        public static List<int> CheckedTlesKey { get; set; }

        static SatKey()
        {
            CheckedTlesKey = new List<int>();
        }
    }
    public partial class ControlPanel : UserControl
    {

        private List<SatelliteObservation> allObservations = new List<SatelliteObservation>();
        private ISharpControl _control;
        private Dictionary<string, Dictionary<int, Tle>> alltles = new();
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
            TLE_list tLE_List = new();
            tLE_List.Show();
            tLE_List.FormClosing += TLE_List_FormClosing;
            button_TLE.Enabled = false;
        }

        private void TLE_List_FormClosing(object sender, FormClosingEventArgs e)
        {
            button_TLE.Enabled = true;
        }

        private void ControlPanel_Load(object sender, EventArgs e)
        {

            Timer timer = new Timer();
            timer.Interval = 1000; // 设置定时器间隔为1秒
            timer.Tick += Timer_Tick;
            timer.Start();


            LoadSettings();
            alltles = ReadAlltles();


        }



        private void Timer_Tick(object sender, EventArgs e)
        {
            // 更新 Label 的文本为当前时间
            int selectedRowIndex = dataGridView_Satellitepass.CurrentCell?.RowIndex ?? -1;
            int selectedColumnIndex = dataGridView_Satellitepass.CurrentCell?.ColumnIndex ?? -1;
            int scrollPosition = dataGridView_Satellitepass.FirstDisplayedScrollingRowIndex;

            label_time.Text = DateTime.Now.ToString("HH:mm:ss");
            SaveSettings();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SatelliteName");
            dataTable.Columns.Add("Countdown");

            foreach (var observation in allObservations)
            {
                DataRow row = dataTable.NewRow();
                row["SatelliteName"] = observation.SatelliteName;

                DateTime start = observation.VisibilityPeriod.Start;
                TimeSpan countdown = start - DateTime.UtcNow;
                string formattedCountdown = countdown.ToString(@"hh\:mm\:ss");

                row["Countdown"] = formattedCountdown;

                dataTable.Rows.Add(row);
            }

            dataGridView_Satellitepass.DataSource = dataTable;

            if (selectedRowIndex >= 0 && selectedRowIndex < dataGridView_Satellitepass.Rows.Count &&
                selectedColumnIndex >= 0 && selectedColumnIndex < dataGridView_Satellitepass.Columns.Count)
            {
                dataGridView_Satellitepass.CurrentCell = dataGridView_Satellitepass[selectedColumnIndex, selectedRowIndex];
            }
            if (scrollPosition >= 0 && scrollPosition < dataGridView_Satellitepass.Rows.Count)
            {
                dataGridView_Satellitepass.FirstDisplayedScrollingRowIndex = scrollPosition;
            }


        }
        private void SaveSettings()
        {


            // 保存 Satellites 的选择


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

            // 恢复 SelectedSatellites 的选择

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

        public class SatelliteObservation
        {
            public string SatelliteName { get; set; }
            public SatelliteVisibilityPeriod VisibilityPeriod { get; set; }
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            alltles = ReadAlltles();
            List<int> keys = SatKey.CheckedTlesKey;
            allObservations.Clear();
            if (keys != null && alltles != null)
            {
                // Get latitude and longitude from text boxes
                double latitude = double.Parse(textBox_Latitude.Text);
                double longitude = double.Parse(textBox_Longitude.Text);

                // Set up our ground station location
                var location = new GeodeticCoordinate(Angle.FromDegrees(latitude), Angle.FromDegrees(longitude), 0);

                // Create a ground station
                var groundStation = new GroundStation(location);
                foreach (int key in keys)
                {
                    foreach (Dictionary<int, Tle> tles in alltles.Values)
                    {
                        if (tles.ContainsKey(key))
                        {
                            Tle tle = tles[key];
                            var sat = new Satellite(tle.Name, tle.Line1, tle.Line2);
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

                            var topocentricObservation = groundStation.Observe(sat, DateTime.UtcNow);
                            //var DopplerShift = topocentricObservation.GetDopplerShift(inputFrequency);


                            foreach (var observation in observations)
                            {

                                var satelliteObservation = new SatelliteObservation
                                {
                                    SatelliteName = tle.Name,
                                    VisibilityPeriod = observation
                                };
                                allObservations.Add(satelliteObservation);
                            }
                        }
                    }
                }
                allObservations.Sort((a, b) => a.VisibilityPeriod.Start.CompareTo(b.VisibilityPeriod.Start));

            }
        }

        private void button_Satellites_Click(object sender, EventArgs e)
        {
            SatellitesForm satellitesForm = new SatellitesForm(alltles);
            satellitesForm.Show();
            satellitesForm.FormClosing += SatellitesForm_FormClosing;
            button_Satellites.Enabled = false;
        }

        private void SatellitesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            button_Satellites.Enabled = true;
        }
        private Dictionary<string, Dictionary<int, Tle>> ReadAlltles()
        {
            string directoryPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "TLE");
            Dictionary<string, Dictionary<int, Tle>> alltles = new();
            if (Directory.Exists(directoryPath))
            {
                string[] txtFiles = Directory.GetFiles(directoryPath, "*.txt");
                if (txtFiles.Length == 0)
                {
                    return null;
                }
                foreach (string txtFile in txtFiles)
                {
                    //把所有TLE目录下的卫星都存到alltles
                    string filePath = Path.Combine(directoryPath, txtFile);
                    LocalTleProvider provider = new LocalTleProvider(true, filePath);
                    Dictionary<int, Tle> tles = provider.GetTles();
                    alltles.Add(filePath, tles);
                }
                return alltles;
            }
            else
            {
                return null;
            }
        }

    }



}
