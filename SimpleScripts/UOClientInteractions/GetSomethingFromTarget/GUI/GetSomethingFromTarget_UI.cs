using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DrabadanCoreLib.UOClientInteractions.GetSomethingFromTarget.Engine;
using StealthAPI;
using DrabadanCoreLib.Core.ScriptActions;

namespace DrabadanCoreLib.UOClientInteractions.GetSomethingFromTarget.GUI
{
    public partial class GetSomethingFromTarget_UI : UserControl
    {
        private readonly Action<string> _messanger;

        public GetSomethingFromTarget_UI(Action<string> messanger)
        {
            InitializeComponent();
            _messanger = messanger;
        }

        private async void GetId_button_Click(object sender, EventArgs e)
        {
            GetSomethingFromTargetEngine getter = new GetSomethingFromTargetEngine(_messanger);
            var result = await getter.GetSomethingFromTargetAsync(ClientInteractionTarget.UoObject);
            ID_label.Text = "ID: 0x" + result.ID.ToString("X");
            Type_label.Text = "Type: 0x" + Stealth.Client.GetType(result.ID).ToString("X");
            textBox1.Text = Stealth.Client.GetTooltip(result.ID);
        }
    }
}
