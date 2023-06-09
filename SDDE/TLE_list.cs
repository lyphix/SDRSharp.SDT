using SGPdotNET.TLE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDRSharp.SDDE
{
    public partial class TLE_list : Form
    {
        public DataTable TLE_listdata;
        public TLE_list()
        {
            InitializeComponent();

        }

        private void TLE_list_Load(object sender, EventArgs e)
        {
            TLE_listdata = new DataTable();
            TLE_listdata.Columns.Add("Satellite");
            TLE_listdata.Columns.Add("URL");
            dataGridView1.DataSource = TLE_listdata;

            dataGridView1.Columns["Satellite"].Width = 150;
            dataGridView1.Columns["URL"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            ReadDataFromSettings();

        }

        private void TLE_list_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveDataToSettings();
        }

        private void ReadDataFromSettings()
        {
            string satelliteListString = Properties.Settings.Default.SatelliteList;
            if (!string.IsNullOrEmpty(satelliteListString))
            {
                string[] satelliteEntries = satelliteListString.Split(';');
                foreach (string entry in satelliteEntries)
                {
                    string[] parts = entry.Split(',');
                    if (parts.Length == 2)
                    {
                        string satellite = parts[0];
                        string url = parts[1];
                        TLE_listdata.Rows.Add(satellite, url);
                    }
                }
            }
        }
        private void SaveDataToSettings()
        {
            List<string> satelliteList = new List<string>();
            foreach (DataRow row in TLE_listdata.Rows)
            {
                string satellite = row["Satellite"].ToString();
                string url = row["URL"].ToString();
                string entry = $"{satellite},{url}";
                satelliteList.Add(entry);
            }

            Properties.Settings.Default.SatelliteList = string.Join(";", satelliteList);
            Properties.Settings.Default.Save();
        }

        private void button_UpdateTLElist_Click(object sender, EventArgs e)
        {
            string pluginDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string directoryPath = Path.Combine(pluginDirectory, "TLE"); //文件保存的目录路径
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath); // 如果目录不存在，创建目录
            }

            foreach (DataRow row in TLE_listdata.Rows)
            {
                string satellite = row["Satellite"].ToString();
                string url = row["URL"].ToString();

                if (!IsUrlValid(url))
                {
                    MessageBox.Show("Invalid URL: " + url);
                    continue; // 跳过当前行，继续下一行的处理
                }


                // 根据 Satellite 列的值生成 txt 文件名
                string fileName = $"{satellite}.txt";

                // 发送网络请求，获取数据
                string data = FetchDataFromURL(url);

                // 将数据保存为 txt 文件
                string filePath = Path.Combine(directoryPath, fileName);
                SaveDataToCsvFile(data, filePath);
            }

            MessageBox.Show("TLE list updated successfully.");
        }
        private string FetchDataFromURL(string url)
        {
            using (var client = new WebClient())
            {
                return client.DownloadString(url);
            }
        }

        private void SaveDataToCsvFile(string data, string filePath)
        {
            File.WriteAllText(filePath, data);
        }
        private bool IsUrlValid(string url)
        {
            // 检查 URL 是否为空
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }

            // 检查 URL 格式是否合法
            Uri uriResult;
            bool isValidUrl = Uri.TryCreate(url, UriKind.Absolute, out uriResult)
                             && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return isValidUrl;
        }
    }
}
