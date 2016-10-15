using DrabadanCoreLib.Common.WinapiHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DrabadanCoreLib.PreLaunchHandlings
{
    public class CharSelectorHandler_Dialog
    {
        public CharSelectorHandler_Dialog() { }

        private static async Task<string> TryToFindCharTitleAsync()
        {
            var process = await GetWindowsTitlesAndHWNDS.SearchForProcessByNameAsync("Stealth");
            if (process == null)
                return "";

            var listOfTitles = await GetWindowsTitlesAndHWNDS.GetChildWindowsTitlesAsync(process);
            if(listOfTitles.Count > 0)
            {
                int idx = listOfTitles.IndexOf("Main");
                if (idx > 0)
                    return listOfTitles[idx + 1];
            }

            return "";
        } 

        public async Task<DialogResult> CharacterSelectorResult()
        {
            var charTitle = await TryToFindCharTitleAsync();
            if (charTitle == "")
                return DialogResult.Abort;

            return MessageBox.Show($"Connecting to: {charTitle}", "Character informer", MessageBoxButtons.YesNo);
        }

    }
}
