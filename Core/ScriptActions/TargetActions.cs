using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrabadanCoreLib.Core.ScriptActions
{
    public sealed class TargetActions : ScriptActionExecuter
    {
        private TargetActions() { }

        public static async Task<bool> TargetPresentAsync()
        {
            return await ScriptApiCallAsync(() => { return StealthClient.GetTargetStatus(); });
        }

        public static async Task CancelTargetAsync()
        {
            bool present = await TargetPresentAsync();
            if (present)
                await ScriptApiCallAsync(() => StealthClient.CancelTarget());
        }
    }
}
