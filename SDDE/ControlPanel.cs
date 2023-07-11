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
        private ISharpControl _control;
        //插件dll路径
        public string pluginpath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private Dictionary<string, Dictionary<int, Tle>> alltles = new();
        private List<SatelliteObservation> allObservations = new List<SatelliteObservation>();
        public Dictionary<int, Satellite> selected_satellites = new();
        public Satellite SelectSatellite = null;
        //TLE网址
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
        //卫星信息文件名 网址
        public static string Satnogs = "satnogs.json";
        public static string SatnogsURL = "https://db.satnogs.org/api/transmitters/?format=json";
        //settings
        public static class SettingsManager
        {
            public static string SettingsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Settings.json");

            public static Settings LoadSettings()
            {
                Settings settings = new Settings();
                if (File.Exists(SettingsPath))
                {
                    try
                    {
                        string json = File.ReadAllText(SettingsPath);
                        settings = JsonSerializer.Deserialize<Settings>(json);
                    }
                    catch
                    {
                        // 文件可能无法读取或解析，这时我们会忽略错误并返回新的设置对象。
                        settings = new Settings();
                    }
                }
                return settings;
            }

            public static void SaveSettings(Settings settings)
            {
                string json = JsonSerializer.Serialize(settings);
                File.WriteAllText(SettingsPath, json);
            }

            public class Settings
            {
                public double latitude { get; set; }
                public double longitude { get; set; }
                public double degree { get; set; }
                public List<int> selected_key { get; set; }
            }
        }
        SettingsManager.Settings settings = new SettingsManager.Settings
        {
            latitude = 0.0,
            longitude = 0.0,
            degree = 0.0,
            selected_key = new()
        };

        //卫星Json文件格式
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
        
        public class SatelliteObservation
        {
            public Satellite Satellite { get; set; }
            public int SatelliteId { get; set; }
            public SatelliteVisibilityPeriod VisibilityPeriod { get; set; }
        }


        //追踪开关
        bool DopplerisTracking = false;

        //选取的卫星列表
        private List<ListViewItem> listViewItems;

        public ControlPanel(ISharpControl control)
        {
            _control = control;
            InitializeComponent();
            typeof(ListView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, listView_Satellitepass, new object[] { true });
        }
        private void ControlPanel_Load(object sender, EventArgs e)
        {
            //定时器
            Timer timer = new Timer();
            timer.Interval = 1000; // 设置定时器间隔为1秒
            timer.Tick += Timer_Tick;
            timer.Start();

            //读取设置 经纬度 角度 key
            settings = SettingsManager.LoadSettings();
            textBox_Latitude.Text = settings.latitude.ToString();
            textBox_Longitude.Text = settings.longitude.ToString();
            textBox_Degree.Text = settings.degree.ToString();
            SatKey.CheckedTlesKey = settings.selected_key;
            //读取卫星TLE
            alltles = ReadAlltles(pluginpath);

            //读取卫星信息
            string pathsatnogs = Path.Combine(pluginpath, $"{Satnogs}");
            if (File.Exists(pathsatnogs))
            {
                string json = File.ReadAllText(pathsatnogs);
                SatnogsJson = JsonSerializer.Deserialize<List<SatelliteInformations>>(json);
            }

            //初始化卫星过境表
            listView_Satellitepass.CheckBoxes = true;
            listView_Satellitepass.View = View.Details;
            listView_Satellitepass.Columns.Add("Satellite Name", listView_Satellitepass.Width * 40 / 100, HorizontalAlignment.Center);
            listView_Satellitepass.Columns.Add("ID", listView_Satellitepass.Width * 30 / 100, HorizontalAlignment.Center);
            listView_Satellitepass.Columns.Add("Time", listView_Satellitepass.Width * 30 / 100, HorizontalAlignment.Center);

            //初始化卫星频率表
            listView_SatelliteF.CheckBoxes = true;
            listView_SatelliteF.View = View.Details;
            listView_SatelliteF.Columns.Add("Description", listView_SatelliteF.Width * 40 / 100, HorizontalAlignment.Center);
            listView_SatelliteF.Columns.Add("Uplink", listView_SatelliteF.Width * 20 / 100, HorizontalAlignment.Center);
            listView_SatelliteF.Columns.Add("Mode", listView_SatelliteF.Width * 20 / 100, HorizontalAlignment.Center);
            listView_SatelliteF.Columns.Add("Downlink", listView_SatelliteF.Width * 20 / 100, HorizontalAlignment.Center);

            button_Refresh_Click(sender, e);
        }

        //经纬度 角度 输入
        private void textBox_Longitude_Leave(object sender, System.EventArgs e)
        {
            if (!double.TryParse(textBox_Longitude.Text, out double longitude) || longitude < -180 || longitude > 180)
            {
                textBox_Longitude.Text = "0";
                longitude = 0;
                MessageBox.Show("Wrong Longitude, Range:(-180,180)");
            }
            settings.longitude = longitude;
            button_Refresh_Click(sender, e);
        }

        private void textBox_Latitude_Leave(object sender, System.EventArgs e)
        {
            if (!double.TryParse(textBox_Latitude.Text, out double latitude) || latitude < -90 || latitude > 90)
            {
                textBox_Latitude.Text = "0";
                latitude = 0;
                MessageBox.Show("Wrong Latitude, Range:(-90,90)");
            }
            settings.latitude = latitude;
            button_Refresh_Click(sender, e);
        }

        private void textBox_Degree_Leave(object sender, EventArgs e)
        {
            if (!double.TryParse(textBox_Degree.Text, out double degree) || degree < 0 || degree > 90)
            {
                textBox_Degree.Text = "0";
                degree = 0;
                MessageBox.Show("Wrong Degree, Range:(0,90)");
            }
            settings.degree = degree;
            button_Refresh_Click(sender, e);
        }

        //打开TLE目录
        private void button_TLE_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(pluginpath, "TLE");
            System.Diagnostics.Process.Start("explorer.exe", path);
        }

        //定时器
        private void Timer_Tick(object sender, EventArgs e)
        {
            //保存
            SettingsManager.SaveSettings(settings);
            // 更新 Label 的文本为当前时间
            label_time.Text = DateTime.Now.ToString("HH:mm:ss");

            //计算多普勒
            var location = new GeodeticCoordinate(Angle.FromDegrees(settings.latitude), Angle.FromDegrees(settings.longitude), 0);
            // 创建地面站
            var groundStation = new GroundStation(location);
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
                        _control.SetFrequency(DopplerFrq, true);
                    }
                }
            }
            UpdateCountdowns();
        }

        private void UpdateCountdowns()
        {
            // 如果 listViewItems 未初始化，直接返回
            if (listViewItems == null)
            {
                return;
            }

            for (int i = 0; i < listViewItems.Count; i++)
            {
                var observation = allObservations[i];
                var listItem = listViewItems[i];

                // 计算倒计时
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
                    continue;
                }
                else
                {
                    countdown = end - DateTime.UtcNow;
                    formattedCountdown = "P " + countdown.ToString(@"hh\:mm\:ss");
                }

                // 更新倒计时
                listItem.SubItems[2].Text = formattedCountdown;
            }
        }

        private void button_Update_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Update TLE & Satellites data from Web?", "Update", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //下载TLE
                foreach (var pair in SatelliteSourcesMap)
                {
                    string TLEPath = Path.Combine(pluginpath, "TLE");
                    string path = Path.Combine(TLEPath, $"{pair.Key}.txt");
                    // 创建文件所在的目录，如果目录已存在则此方法不会执行任何操作
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                    _ = FetchAndSaveToFile(pair.Value, path);
                }
                //下载卫星数据
                string pathsatnogs = Path.Combine(pluginpath, $"{Satnogs}");

                FetchAndSaveToFile(SatnogsURL, pathsatnogs).ContinueWith(t =>
                {
                    if (t.Result == 1)
                    {
                        string json = File.ReadAllText(pathsatnogs);
                        SatnogsJson = JsonSerializer.Deserialize<List<SatelliteInformations>>(json);
                        MessageBox.Show("Success");
                    }
                    else
                    {
                        MessageBox.Show("Fail");
                    }
                });
            }
        }

        private void button_Satellites_Click(object sender, EventArgs e)
        {
            alltles = ReadAlltles(pluginpath);
            if (alltles != null)
            {
                SatellitesForm satellitesForm = new SatellitesForm(alltles);
                satellitesForm.Show();
                satellitesForm.FormClosing += SatellitesForm_FormClosing;
                button_Satellites.Enabled = false;
            }
            else
            {
                MessageBox.Show("Can't find TLE Files!");
            }

        }

        private void SatellitesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            button_Satellites.Enabled = true;
            button_Refresh_Click(sender, e);
        }
        private Dictionary<string, Dictionary<int, Tle>> ReadAlltles(string path)
        {
            //合并路径
            string directoryPath = Path.Combine(path, "TLE");
            Dictionary<string, Dictionary<int, Tle>> alltles = new();
            //读取全部txt
            if (Directory.Exists(directoryPath))
            {
                string[] txtFiles = Directory.GetFiles(directoryPath, "*.txt");
                if (txtFiles.Length == 0)
                {
                    return null;
                }
                foreach (string txtFile in txtFiles)
                {
                    //把所有TLE都存到alltles
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

        static async Task<int> FetchAndSaveToFile(string url, string path)
        {
            HttpClient client = new HttpClient();
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
            alltles = ReadAlltles(pluginpath);
            if (settings.selected_key != null && alltles != null)
            {
                // 保存选择的卫星到selected_satellites
                selected_satellites.Clear();
                foreach (int key in settings.selected_key)
                {
                    foreach (Dictionary<int, Tle> tles in alltles.Values)
                    {
                        if (tles.TryGetValue(key, out Tle value))
                        {
                            Tle tle = value;
                            var sat = new Satellite(tle.Name, tle.Line1, tle.Line2);
                            if (!selected_satellites.ContainsKey(key))
                            {
                                selected_satellites.Add(key, sat);
                            }

                        }
                    }
                }

                //读取选择的卫星 预测过境
                allObservations.Clear();

                // 设置地面位置
                var location = new GeodeticCoordinate(Angle.FromDegrees(settings.latitude), Angle.FromDegrees(settings.longitude), 0);
                // 创建地面站
                var groundStation = new GroundStation(location);
                // 最小角度
                double degree = double.Parse(textBox_Degree.Text);

                foreach (KeyValuePair<int, Satellite> entry in selected_satellites)
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

                //添加到列表中
                listView_Satellitepass.Items.Clear();
                listViewItems = new List<ListViewItem>();
                foreach (var observation in allObservations)
                {
                    ListViewItem listItem = new ListViewItem(observation.Satellite.Name);
                    listItem.SubItems.Add(observation.SatelliteId.ToString());

                    // 初始化倒计时为 00:00:00
                    listItem.SubItems.Add("00:00:00");

                    listViewItems.Add(listItem);
                    listView_Satellitepass.Items.Add(listItem);
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
            }
        }

        private void button_Doppler_Click(object sender, EventArgs e)
        {
            // 切换变量值
            DopplerisTracking = !DopplerisTracking;

            // 更新按钮文本
            button_Doppler.Text = DopplerisTracking ? "Stop" : "Start";
        }

        private void listView_Satellitepass_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                // 先取消选中其他所有项
                foreach (ListViewItem item in listView_Satellitepass.Items)
                {
                    if (item.Index != e.Index)
                    {
                        item.Checked = false;
                    }
                }

                var Name = listView_Satellitepass.Items[e.Index].SubItems[0].Text;

                var id = listView_Satellitepass.Items[e.Index].SubItems[1].Text;

                // 将名字赋给label_SatelliteName的Text
                label_SatelliteName.Text = Name + " " + id;
                SelectSatellite = selected_satellites[int.Parse(id)];

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
    }



}
