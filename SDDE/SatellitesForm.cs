using SGPdotNET.TLE;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.VirtualKeyboard;

namespace SDRSharp.SDDE
{
    public partial class SatellitesForm : Form
    {
        private Dictionary<string, Dictionary<int, Tle>> _alltles = new();

        public SatellitesForm(Dictionary<string, Dictionary<int, Tle>> alltles)
        {
            InitializeComponent();
            _alltles = alltles;
        }

        private void comboBox_SatellitesType_DropDown(object sender, EventArgs e)
        {
            comboBox_SatellitesType.Items.Clear();
            foreach (string filePath in _alltles.Keys)
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                comboBox_SatellitesType.Items.Add(fileName);
            }
            


        }

        private void comboBox_SatellitesType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //更新checkedListBox
            checkedListBox_Satellites.Items.Clear();
            foreach (string filePath in _alltles.Keys)
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                if (comboBox_SatellitesType.SelectedItem != null && comboBox_SatellitesType.SelectedItem.ToString() == fileName)
                {
                    Dictionary<int, Tle> Tles = _alltles[filePath];
                    foreach (int Key in Tles.Keys)
                    {
                        string Name = Tles[Key].Name;
                        checkedListBox_Satellites.Items.Add(Name + "," + Key);
                    }
                }
            }
            for (int i = 0; i < checkedListBox_Satellites.Items.Count; i++)
            {
                string[] item = checkedListBox_Satellites.Items[i].ToString().Split(',');
                int key = int.Parse(item[1]);
                foreach (int satkey in SatKey.CheckedTlesKey)
                {
                    if (satkey == key)
                    {
                        checkedListBox_Satellites.SetItemChecked(i, true);
                        break;
                    }
                }
            }
        }

        private void SatellitesForm_Load(object sender, EventArgs e)
        {

            comboBox_SatellitesType.Items.Clear();
            foreach (string filePath in _alltles.Keys)
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                comboBox_SatellitesType.Items.Add(fileName);
            }
            comboBox_SatellitesType.SelectedIndex = 0;
        }

        private void SatellitesForm_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void checkedListBox_Satellites_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string satelliteName = checkedListBox_Satellites.Items[e.Index].ToString();
            string[] parts = satelliteName.Split(',');
            int key = int.Parse(parts[1]);
            if (e.NewValue == CheckState.Checked)
            {
                List<int> temp = SatKey.CheckedTlesKey;
                if (!temp.Contains(key))
                {
                    temp.Add(key);
                }
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                List<int> temp = SatKey.CheckedTlesKey;
                temp.RemoveAll(x => x == key);
            }


        }

    }
}
