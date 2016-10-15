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
        }
        
        private async void Test_button_Click(object sender, EventArgs e)
        {
            //await GetGumpInfoTest();
            //await MoveAroundHouseTest();
            //await StaticsTileTest();            
        }

        protected static Stealth StealthClient
        {
            get
            {
                if (ScriptActionExecuter.ErrorMessageAction == null)
                    ScriptActionExecuter.ErrorMessageAction = RaiseErrorMessage;
                return ScriptActionExecuter.StealthClient;
            }
        }

        private static void RaiseErrorMessage(string message)
        {
            MessageBox.Show(message);
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


        private static async Task StaticsTileTest()
        {
            await Task.Run(() =>
            {
                byte worldNum = StealthClient.GetWorldNum();
                var statics = StealthClient.ReadStaticsXY(872, 509, worldNum);
            });
        }

        private static async Task MoveAroundHouseTest()
        {
            await MoverActions.MoveToLocation(new Point2D(672, 758));
        }

        private async Task GetGumpInfoTest()
        {
            StealthClient.IncomingGump += async (sender, e) =>
             {
                 SendConsoleMessage($"{e.GumpId}");
                 bool result = await Task.Run(() => StealthClient.NumGumpTextEntryTest());
             };

        }
    }
}
