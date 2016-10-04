using DrabadanCoreLib.OSISubScripts;
using DrabadanCoreLib.Gumps;
using StealthAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DrabadanCoreLib.Core.ScriptActions
{
    public class UseObjectHandler : ScriptActionExecuter
    {
        private UseObjectHandler() { }

        public static async Task<Gump> UseOSIMoongate(uint id)
        {
            var cts = new CancellationTokenSource();

            Gump g = null;
            var result = await ScriptApiCallAsync<IncomingGumpEventArgs>(
                () => StealthClient.UseObject(id),
                (sender, e) =>
                {
                    if (e.GumpId == 0x258)
                    {
                        g = Gump.GetGump(e.GumpId);
                        cts.Cancel();
                    }
                },
                cts);

            return g;
        }

        public static async Task<bool> TravelToMoongateAsync(Gump gump, Moongate gate)
        {
            bool? result = await ScriptApiCallAsync(()=> gump.RadioButtons?[gate.OSIGumpRadioButton]?.Click(true));
            result = await ScriptApiCallAsync(()=> gump.Buttons?[0]?.Click());
            await Task.Delay(3000);
            return (bool)result;
        }

    }
}
