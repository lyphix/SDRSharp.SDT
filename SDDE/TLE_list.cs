using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            TLE_listdata.Columns.Add("URL(csv)");
            dataGridView1.DataSource = TLE_listdata;

            dataGridView1.Columns["Satellite"].Width = 150;
            dataGridView1.Columns["URL(csv)"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            ReadDataFromCsvFile();
        }

        private void TLE_list_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveDataToCsvFile();
        }

        private void ReadDataFromCsvFile()
        {
            if (File.Exists("TLE.csv"))
            {
                string[] lines = File.ReadAllLines("TLE.csv");
                foreach (string line in lines)
                {
                    string[] values = line.Split(',');
                    if (values.Length >= 2)
                    {
                        TLE_listdata.Rows.Add(values[0], values[1]);
                    }
                }
            }
        }
        private void SaveDataToCsvFile()
        {
            using (StreamWriter writer = new StreamWriter("TLE.csv"))
            {
                foreach (DataRow row in TLE_listdata.Rows)
                {
                    string line = string.Join(",", row.ItemArray);
                    writer.WriteLine(line);
                }
            }
        }

        private void button_UpdateTLElist_Click(object sender, EventArgs e)
        {
            string directoryPath = Path.Combine(Application.StartupPath, "TLE"); // CSV 文件保存的目录路径
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath); // 如果目录不存在，创建目录
            }

            foreach (DataRow row in TLE_listdata.Rows)
            {
                string satellite = row["Satellite"].ToString();
                string url = row["URL(csv)"].ToString();

                if (!IsUrlValid(url))
                {
                    MessageBox.Show("Invalid URL: " + url);
                    continue; // 跳过当前行，继续下一行的处理
                }


                // 根据 Satellite 列的值生成 CSV 文件名
                string fileName = $"{satellite}.csv";

                // 发送网络请求，获取数据
                string data = FetchDataFromURL(url);

                // 将数据保存为 CSV 文件
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
