using SDRSharp.Common;
using System.Windows.Forms;

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




    }
}
