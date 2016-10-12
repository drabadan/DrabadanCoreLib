using StealthAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DrabadanCoreLib.Core.ScriptActions
{
    public sealed class CastingSpellsHandler : ScriptActionExecuter
    {
        private CastingSpellsHandler() { }

        private static async Task<bool> CastSpellAsync(string spellName, uint target = 0)
        {
            var cts = new CancellationTokenSource();

            List<string> speechCancellerList = new List<string>() {
                "already casting spell",
                "More reagents are needed for this",
                "not enough mana",
                "concentration is disturbed, thus ruining"
            };
            
            bool success = await ScriptApiCallAsync<Buff_DebuffSystemEventArgs>(
                () => StealthClient.CastSpellToObj(spellName, target),
                (sender, e) =>
                {
                    if (e.AttributeId == 1036)
                    {
                        Console.WriteLine("Hidden");
                        cts.Cancel();
                    }
                }, speechCancellerList,
                cts);

            return success;
        }


        public static async Task CastInvisibiltyAsync(uint target)
        {
            await CastSpellAsync("Invisibility", target);
        }
    }
}
