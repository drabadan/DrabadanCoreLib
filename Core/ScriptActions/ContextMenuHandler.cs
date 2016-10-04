using StealthAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrabadanCoreLib.Core.ScriptActions
{
    public sealed class ContextMenuHandler : ScriptActionExecuter
    {
        private ContextMenuHandler() { }

        public static async Task<string> GetContextMenuAsync(uint itemId)
        {
            return await ScriptApiCallAsync(()=> StealthClient.GetContextMenu(), 1000);
        }
    }
}
