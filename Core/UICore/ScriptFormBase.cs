using DrabadanCoreLib.PreLaunchHandlings;
using StealthAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrabadanCoreLib.Core.UICore
{
    public partial class ScriptFormBase : Form
    {
        public ScriptFormBase()
        {
            InitializeComponent();
        }

        protected Panel MainPanel { get { return Main_panel; } }
        protected void AddControlToMainPanel(Control control)
        {   
            control.Dock = DockStyle.Fill;
            Main_panel.Controls.Add(control);
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

        protected void SendConsoleMessage(string message)
        {
            this?.Invoke((MethodInvoker)delegate
            {
                richTextBox1.SelectionStart = 0;
                richTextBox1.SelectionLength = 0;
                richTextBox1.SelectedText = DateTime.Now + ": " + message + Environment.NewLine;
            });
        }
        
        protected virtual async void ScriptForm_Load(object sender, EventArgs e)
        {
            CharSelectorHandler_Dialog dialog = new CharSelectorHandler_Dialog();
            var dialogResult = await dialog.CharacterSelectorResult();
            if (dialogResult != DialogResult.Yes)
            {
                MessageBox.Show("Stealth not found in the system... exiting!", "Stealth not found error");
                Application.Exit();
            }
        }
    }
}
