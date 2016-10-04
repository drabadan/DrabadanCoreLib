using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StealthAPI;
using DrabadanCoreLib.Gumps;

namespace DrabadanCoreLib_WF_TestApp
{
    public partial class DrabCL_App : Form
    {
        public DrabCL_App()
        {
            InitializeComponent();
        }

        private void SendConsoleMessage(string message)
        {
            this?.Invoke((MethodInvoker)delegate
            {
                richTextBox1.SelectionStart = 0;
                richTextBox1.SelectionLength = 0;
                richTextBox1.SelectedText = DateTime.Now + ": " + message + Environment.NewLine;
            });
        }

        private async void Test_button_Click(object sender, EventArgs e)
        {
            GetGumpInfoTest();
        }

        private static Stealth _stealth = Stealth.Client;

        private void GetGumpInfoTest()
        {
            _stealth.IncomingGump += (sender, e) =>
             {
                 SendConsoleMessage($"{e.GumpId}");
                 Gump g = Gump.GetGump(e.GumpId);
                 SendConsoleMessage($"{g.Buttons.Count}");
                 g.TextEdits.ForEach(ed => ed.Text = "Hello");
                 g.Buttons.First().Click();
             };
            
        }
    }
}
