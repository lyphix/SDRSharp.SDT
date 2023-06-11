using SDRSharp.Common;
using SGPdotNET.TLE;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SDRSharp.SDDE
{
    public class SatelliteDDE : ISharpPlugin, ICanLazyLoadGui, ISupportStatus, IExtendedNameProvider
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
            }
        }

        public void Initialize(ISharpControl control)
        {
            _control = control;

        }

        public void Close()
        {
            
        }




    }
    public static class SatKey
    {
        public static List<int> CheckedTlesKey { get; set; }

        static SatKey()
        {
            CheckedTlesKey = new List<int>();
        }
    }
}
