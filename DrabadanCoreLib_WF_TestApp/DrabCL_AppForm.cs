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
using DrabadanCoreLib.Data;
using DrabadanCoreLib.Core.ScriptActions;
using DrabadanCoreLib.UOClientInteractions.GetSomethingFromTarget.GUI;
using DrabadanCoreLib.Core;
using System.Diagnostics;
using System.Runtime.InteropServices;
using DrabadanCoreLib.PreLaunchHandlings;

namespace DrabadanCoreLib_WF_TestApp
{
    public partial class DrabCL_App : Form
    {
        public DrabCL_App()
        {
            InitializeComponent();
            
            ScriptActionExecuter.Messanger = SendConsoleMessage;
            GetSomethingFromTarget_UI ui = new GetSomethingFromTarget_UI(SendConsoleMessage);
            ui.Dock = DockStyle.Fill;
            tabControl1.TabPages[1].Controls.Add(ui);
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
            //await GetGumpInfoTest();
            //await MoveAroundHouseTest();
            //await StaticsTileTest();            
        }

        private static Stealth _stealth = Stealth.Client;

        private static async Task StaticsTileTest()
        {
            await Task.Run(() =>
            {
                byte worldNum = _stealth.GetWorldNum();
                var statics = _stealth.ReadStaticsXY(872, 509, worldNum);
            });
        }

        private static async Task MoveAroundHouseTest()
        {
            await MoverActions.MoveToLocation(new Point2D(672, 758));
        }

        private async Task GetGumpInfoTest()
        {
            _stealth.IncomingGump += async (sender, e) =>
             {
                 SendConsoleMessage($"{e.GumpId}");
                 bool result = await Task.Run(() => _stealth.NumGumpTextEntryTest());
             };

        }

        private async void DrabCL_App_Load(object sender, EventArgs e)
        {
            CharSelectorHandler_Dialog dialog = new CharSelectorHandler_Dialog();
            var dialogResult = await dialog.CharacterSelectorResult();
            if (dialogResult != DialogResult.Yes)
                Application.Exit();
        }
    }
}
