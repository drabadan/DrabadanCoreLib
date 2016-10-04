using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrabadanCoreLib.Core.ScriptActions
{
    public sealed class TooltipHandler : ScriptActionExecuter
    {
        private TooltipHandler() { }

        private const int Maxwaittooltipinit = 1000;

        public static async Task<string> GetTooltipAsync(uint itemId)
        {
            bool valid = await FindTypeActions.ItemExistAsync(itemId);
            if (!valid)
                return string.Empty;

            string tooltip = await ScriptApiCallAsync(() => StealthClient.GetTooltip(itemId, Maxwaittooltipinit));

            if (!string.IsNullOrEmpty(tooltip) || !string.IsNullOrWhiteSpace(tooltip))
                return tooltip;

            return string.Empty;

        }

    }
}
