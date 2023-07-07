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
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Telerik.WinControls.Drawing;
using Path = System.IO.Path;

namespace SDRSharp.SDDE
{

    public partial class ControlPanel : UserControl
    {

        private List<SatelliteObservation> allObservations = new List<SatelliteObservation>();
        private ISharpControl _control;
        private Dictionary<string, Dictionary<int, Tle>> alltles = new();
        public Dictionary<int, Satellite> satellites = new();
        public Satellite SelectSatellite = null;

        public static Dictionary<string, string> SatelliteSourcesMap
        {
            get
            {
                return new Dictionary<string, string>
                {
                    {"Amateur", "https://celestrak.org/NORAD/elements/gp.php?GROUP=amateur&FORMAT=tle"},
                    {"Cubesat", "https://celestrak.org/NORAD/elements/gp.php?GROUP=cubesat&FORMAT=tle"},
                    {"Education", "https://celestrak.org/NORAD/elements/gp.php?GROUP=education&FORMAT=tle"},
                    {"Engineer", "https://celestrak.org/NORAD/elements/gp.php?GROUP=engineering&FORMAT=tle"},
                    {"Geostationary", "https://celestrak.org/NORAD/elements/gp.php?GROUP=geo&FORMAT=tle"},
                    {"Globalstar", "https://celestrak.org/NORAD/elements/gp.php?GROUP=globalstar&FORMAT=tle"},
                    {"GNSS", "https://celestrak.org/NORAD/elements/gp.php?GROUP=gnss&FORMAT=tle"},
                    {"Intelsat", "https://celestrak.org/NORAD/elements/gp.php?GROUP=intelsat&FORMAT=tle"},
                    {"Iridium", "https://celestrak.org/NORAD/elements/gp.php?GROUP=iridium-NEXT&FORMAT=tle"},
                    {"Military", "https://celestrak.org/NORAD/elements/gp.php?GROUP=military&FORMAT=tle"},
                    {"New", "https://celestrak.org/NORAD/elements/gp.php?GROUP=last-30-days&FORMAT=tle"},
                    {"OneWeb", "https://celestrak.org/NORAD/elements/gp.php?GROUP=oneweb&FORMAT=tle"},
                    {"Orbcomm", "https://celestrak.org/NORAD/elements/gp.php?GROUP=orbcomm&FORMAT=tle"},
                    {"Resource", "https://celestrak.org/NORAD/elements/gp.php?GROUP=resource&FORMAT=tle"},
                    {"SatNOGS", "https://celestrak.org/NORAD/elements/gp.php?GROUP=satnogs&FORMAT=tle"},
                    {"Science", "https://celestrak.org/NORAD/elements/gp.php?GROUP=science&FORMAT=tle"},
                    {"Spire", "https://celestrak.org/NORAD/elements/gp.php?GROUP=spire&FORMAT=tle"},
                    {"Starlink", "https://celestrak.org/NORAD/elements/gp.php?GROUP=starlink&FORMAT=tle"},
                    {"Swarm", "https://celestrak.org/NORAD/elements/gp.php?GROUP=swarm&FORMAT=tle"},
                    {"Weather", "https://celestrak.org/NORAD/elements/gp.php?GROUP=weather&FORMAT=tle" },
                    {"X-Comm", "https://celestrak.org/NORAD/elements/gp.php?GROUP=x-comm&FORMAT=tle" }
                };
            }
        }
        public static string Satnogs = "satnogs.json";
        public static string SatnogsURL = "https://db.satnogs.org/api/transmitters/?format=json";

        public class SatelliteInformations
        {
            public string uuid { get; set; }
            public string description { get; set; }
            public bool alive { get; set; }
            public string type { get; set; }
            public long? uplink_low { get; set; }
            public long? uplink_high { get; set; }
            public long? uplink_drift { get; set; }
            public long? downlink_low { get; set; }
            public long? downlink_high { get; set; }
            public long? downlink_drift { get; set; }
            public string mode { get; set; }
            public int? mode_id { get; set; }
            public string uplink_mode { get; set; }
            public bool invert { get; set; }
            public double? baud { get; set; }
            public string sat_id { get; set; }
            public int norad_cat_id { get; set; }
            public int? norad_follow_id { get; set; }
            public string status { get; set; }
            public DateTime updated { get; set; }
            public string citation { get; set; }
            public string service { get; set; }
            public string iaru_coordination { get; set; }
            public string iaru_coordination_url { get; set; }
            public ItuNotification itu_notification { get; set; }
            public bool frequency_violation { get; set; }
            public bool unconfirmed { get; set; }
        }
        public class ItuNotification
        {
            public List<string> urls { get; set; }
        }

        public List<SatelliteInformations> SatnogsJson = new();
        public ControlPanel(ISharpControl control)
        {
            _control = control;
            InitializeComponent();
        }
        bool DopplerisTracking = false;


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
            string path = Path.Combine(Directory.GetCurrentDirectory(), "TLE");
            System.Diagnostics.Process.Start("explorer.exe", path);
        }

        private void ControlPanel_Load(object sender, EventArgs e)
        {

            Timer timer = new Timer();
            timer.Interval = 1000; // 设置定时器间隔为1秒
            timer.Tick += Timer_Tick;
            timer.Start();


            LoadSettings();
            alltles = ReadAlltles();

            string pathsatnogs = Path.Combine(Directory.GetCurrentDirectory(), $"{Satnogs}");
            if (!File.Exists(pathsatnogs))
            {
                _ = FetchAndSaveToFile(SatnogsURL, pathsatnogs);
            }
            string json = File.ReadAllText(pathsatnogs);
            SatnogsJson = JsonSerializer.Deserialize<List<SatelliteInformations>>(json);
            //初始化卫星频率表
            listView_SatelliteF.CheckBoxes = true;
            listView_SatelliteF.View = View.Details;

            // 添加需要的列
            listView_SatelliteF.Columns.Add("Description", listView_SatelliteF.Width * 40 / 100, HorizontalAlignment.Center);
            listView_SatelliteF.Columns.Add("Uplink", listView_SatelliteF.Width * 20 / 100, HorizontalAlignment.Center);
            listView_SatelliteF.Columns.Add("Mode", listView_SatelliteF.Width * 20 / 100, HorizontalAlignment.Center);
            listView_SatelliteF.Columns.Add("Downlink", listView_SatelliteF.Width * 20 / 100, HorizontalAlignment.Center);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // 更新 Label 的文本为当前时间
            int selectedRowIndex = dataGridView_Satellitepass.CurrentCell?.RowIndex ?? -1;
            int selectedColumnIndex = dataGridView_Satellitepass.CurrentCell?.ColumnIndex ?? -1;
            int scrollPosition = dataGridView_Satellitepass.FirstDisplayedScrollingRowIndex;

            label_time.Text = DateTime.Now.ToString("HH:mm:ss");
            SaveSettings();

            //读取选择的卫星 预测过境
            allObservations.Clear();

            // 读取经纬度
            double latitude = double.Parse(textBox_Latitude.Text);
            double longitude = double.Parse(textBox_Longitude.Text);
            // 设置地面位置
            var location = new GeodeticCoordinate(Angle.FromDegrees(latitude), Angle.FromDegrees(longitude), 0);
            // 创建地面站
            var groundStation = new GroundStation(location);
            // 最小角度
            double degree = double.Parse(textBox_Degree.Text);

            foreach (KeyValuePair<int, Satellite> entry in satellites)
            {
                int satelliteId = entry.Key;
                Satellite sat = entry.Value;


                //预测卫星过境 从当前时间-10分钟到未来24小时
                var observations = groundStation.Observe(
                    sat,
                    DateTime.UtcNow - TimeSpan.FromMinutes(10),
                    DateTime.UtcNow + TimeSpan.FromHours(24),
                    TimeSpan.FromSeconds(10),
                    minElevation: Angle.FromDegrees(degree),
                    clipToStartTime: true
                );

                //保存观测结果
                foreach (var observation in observations)
                {

                    var satelliteObservation = new SatelliteObservation
                    {
                        Satellite = sat,
                        SatelliteId = satelliteId,
                        VisibilityPeriod = observation
                    };
                    allObservations.Add(satelliteObservation);
                }



            }
            //按时间排序
            allObservations.Sort((a, b) => a.VisibilityPeriod.Start.CompareTo(b.VisibilityPeriod.Start));

            //多普勒
            if (SelectSatellite != null)
            {
                if (double.TryParse(textBoxFreq.Text, out double inputFrequency))
                {
                    var topocentricObservation = groundStation.Observe(SelectSatellite, DateTime.UtcNow);
                    var DopplerShift = topocentricObservation.GetDopplerShift(inputFrequency) + inputFrequency;
                    long DopplerFrq = (long)(DopplerShift * 1000000);
                    textBoxDopl.Text = DopplerShift.ToString();
                    if (DopplerisTracking)
                    {
                        _control.SetFrequency(DopplerFrq,true);
                    }
                }

            }


            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SatelliteName");
            dataTable.Columns.Add("NORAD ID");
            dataTable.Columns.Add("Countdown");


            foreach (var observation in allObservations)
            {
                DataRow row = dataTable.NewRow();
                row["SatelliteName"] = observation.Satellite.Name;

                //计算倒计时
                DateTime start = observation.VisibilityPeriod.Start;
                DateTime end = observation.VisibilityPeriod.End;
                TimeSpan countdown;
                string formattedCountdown;
                if (end > DateTime.UtcNow && start > DateTime.UtcNow)
                {
                    countdown = start - DateTime.UtcNow;
                    formattedCountdown = countdown.ToString(@"hh\:mm\:ss");
                }
                else if (end < DateTime.UtcNow && start < DateTime.UtcNow)
                {
                    countdown = end - DateTime.UtcNow;
                    formattedCountdown = "* " + countdown.ToString(@"hh\:mm\:ss");
                    continue;
                }
                else
                {
                    countdown = end - DateTime.UtcNow;
                    formattedCountdown = "P " + countdown.ToString(@"hh\:mm\:ss");
                }
                row["NORAD ID"] = observation.SatelliteId;
                row["Countdown"] = formattedCountdown;
                dataTable.Rows.Add(row);
            }

            //保存表格位置
            dataGridView_Satellitepass.DataSource = dataTable;
            dataGridView_Satellitepass.Columns[dataGridView_Satellitepass.Columns.Count - 1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
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
            public Satellite Satellite { get; set; }
            public int SatelliteId { get; set; }
            public SatelliteVisibilityPeriod VisibilityPeriod { get; set; }
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            //下载TLE
            foreach (var pair in SatelliteSourcesMap)
            {
                string TLEPath = Path.Combine(Directory.GetCurrentDirectory(), "TLE");
                string path = Path.Combine(TLEPath, $"{pair.Key}.txt");
                // 创建文件所在的目录，如果目录已存在则此方法不会执行任何操作
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                _ = FetchAndSaveToFile(pair.Value, path);
            }
            //下载卫星数据
            string pathsatnogs = Path.Combine(Directory.GetCurrentDirectory(), $"{Satnogs}");
            _ = FetchAndSaveToFile(SatnogsURL, pathsatnogs);
            string json = File.ReadAllText(pathsatnogs);
            SatnogsJson = JsonSerializer.Deserialize<List<SatelliteInformations>>(json);
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

        private void dataGridView_Satellitepass_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView_Satellitepass.Rows[e.RowIndex];
                // 读取选中行的数据
                string satelliteName = selectedRow.Cells["SatelliteName"].Value.ToString();
                string countdown = selectedRow.Cells["Countdown"].Value.ToString();

            }
        }

        static HttpClient client = new HttpClient();
        static async Task<int> FetchAndSaveToFile(string url, string path)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                File.WriteAllText(path, responseBody);

                return 1;
            }
            catch
            {
                return 0;
            }
        }

        private void button_Refresh_Click(object sender, EventArgs e)
        {
            alltles = ReadAlltles();
            List<int> keys = SatKey.CheckedTlesKey;

            if (keys != null && alltles != null)
            {
                // 保存选择的卫星到satellites
                satellites.Clear();
                foreach (int key in keys)
                {
                    foreach (Dictionary<int, Tle> tles in alltles.Values)
                    {
                        if (tles.TryGetValue(key, out Tle value))
                        {
                            Tle tle = value;
                            var sat = new Satellite(tle.Name, tle.Line1, tle.Line2);
                            if (!satellites.ContainsKey(key))
                            {
                                satellites.Add(key, sat);
                            }

                        }
                    }
                }
            }
        }

        private void dataGridView_Satellitepass_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 确保选中了有效的行
            if (e.RowIndex >= 0)
            {
                // 获取名字
                var firstColumnValue = dataGridView_Satellitepass.Rows[e.RowIndex].Cells[0].Value.ToString();

                var id = dataGridView_Satellitepass.Rows[e.RowIndex].Cells[1].Value.ToString();

                // 将名字赋给label_SatelliteName的Text
                label_SatelliteName.Text = firstColumnValue + " " + id;
                SelectSatellite = satellites[int.Parse(id)];

                List<SatelliteInformations> targetSatellites = SatnogsJson.Where(satellite => satellite.norad_cat_id.ToString() == id).ToList();

                listView_SatelliteF.Items.Clear();
                foreach (SatelliteInformations targetSatellite in targetSatellites)
                {
                    // 使用ListViewItem来创建新的行，并设置每一列的值
                    ListViewItem listItem = new ListViewItem(targetSatellite.description);
                    listItem.SubItems.Add((targetSatellite.uplink_low / 1000000.0).ToString());
                    listItem.SubItems.Add(targetSatellite.mode);
                    listItem.SubItems.Add((targetSatellite.downlink_low / 1000000.0).ToString());

                    // 将新的行添加到listView_SatelliteF中
                    listView_SatelliteF.Items.Add(listItem);
                }
            }
        }

        private void listView_SatelliteF_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                // 先取消选中其他所有项
                foreach (ListViewItem item in listView_SatelliteF.Items)
                {
                    if (item.Index != e.Index)
                    {
                        item.Checked = false;
                    }
                }

                // 获取当前被选中项的 "Downlink Low" 的值
                string downlinkLow = listView_SatelliteF.Items[e.Index].SubItems[3].Text;
                textBoxFreq.Text = downlinkLow;
                // 在这里你可以使用 downlinkLow 做其他操作
            }
        }

        private void button_Doppler_Click(object sender, EventArgs e)
        {
            // 切换变量值
            DopplerisTracking = !DopplerisTracking;

            // 更新按钮文本
            button_Doppler.Text = DopplerisTracking ? "Stop" : "Start";
        }
    }



}
