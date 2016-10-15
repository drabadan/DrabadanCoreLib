using DrabadanCoreLib.Core.UICore;
using DrabadanCoreLib.UOClientInteractions.GetSomethingFromTarget.GUI;
using System;
using System.Windows.Forms;

namespace MyBasicWinFormsScriptExample
{
    public class MyBasicScriptExampleForm : ScriptFormBase
    {
        //For adding new controls we override the ScriptForm_Load eventhandler from base class;

        //Example of adding simple Control based element to main_panel container of base form;
        //protected override void ScriptForm_Load(object sender, EventArgs e)
        //{
        //    base.ScriptForm_Load(sender, e);

        //    GetSomethingFromTarget_UI ui = new GetSomethingFromTarget_UI(SendConsoleMessage);
        //    AddControlToMainPanel(ui);
        //}

        //More complex example of adding TabControl and filling it with diff ui's;
        protected override void ScriptForm_Load(object sender, EventArgs e)
        {
            base.ScriptForm_Load(sender, e);

            Control tabControl = new TabControl();

            //creating sequence of pages for tabControl
            //1 tab
            TabPage page1 = new TabPage("Get from client");
            GetSomethingFromTarget_UI ui = new GetSomethingFromTarget_UI(SendConsoleMessage);
            ui.Dock = DockStyle.Fill;
            page1.Controls.Add(ui);

            //2 tab
            TabPage page2 = new TabPage("Misc");
            Button sampleButton = new Button();
            sampleButton.Text = "Raise hello world";
            sampleButton.Click += (sender1, e1) => SendConsoleMessage("Hello World");
            page2.Controls.Add(sampleButton);
            //adding pages to tabControl
            tabControl.Controls.AddRange(new TabPage[] { page1, page2 });

            //adding tabControl to main_panel
            AddControlToMainPanel(tabControl);
        }
    }
}
