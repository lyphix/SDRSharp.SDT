using SDRSharp.Common;
using System.IO;
using System.Windows.Forms;

namespace SDRSharp.SDDE
{
    public class SDDE : ISharpPlugin, ICanLazyLoadGui, ISupportStatus, IExtendedNameProvider
    {
        private ControlPanel _gui;
        private ISharpControl _control;

        public string DisplayName => "SDDE";

        public string Category => "DDE";

        public string MenuItemName => DisplayName;

        public bool IsActive => _gui != null && _gui.Visible;

        public UserControl Gui
        {
            get
            {
                LoadGui();
                return _gui;
            }
        }

        public void LoadGui()
        {
            if (_gui == null)
            {
                _gui = new ControlPanel(_control);
                if (File.Exists("settings.txt"))
                {
                    var settings = File.ReadAllText("settings.txt").Split(',');
                    _gui.textBox_Longitude.Text = settings[0];
                    _gui.textBox_Latitude.Text = settings[1];
                }
            }
        }

        public void Initialize(ISharpControl control)
        {
            _control = control;
            
        }

        public void Close()
        {
            File.WriteAllText("settings.txt", $"" +
                $"{_gui.textBox_Longitude.Text}," +
                $"{_gui.textBox_Latitude.Text}");
        }
    }
}
