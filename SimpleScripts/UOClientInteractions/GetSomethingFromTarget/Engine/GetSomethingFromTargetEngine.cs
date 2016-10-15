using DrabadanCoreLib.Core.ScriptActions;
using StealthAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DrabadanCoreLib.UOClientInteractions.GetSomethingFromTarget.Engine
{
    public class GetSomethingFromTargetEngine
    {
        private readonly Action<string> _messanger;

        public GetSomethingFromTargetEngine(Action<string> messanger)
        {
            _messanger = messanger;
        }

        public async Task<TargetInfo> GetSomethingFromTargetAsync(ClientInteractionTarget target)
        {
            _messanger?.Invoke("Please go to client and target the item");

            return await UoClientInteractionsHandler.RequestTargetInfo(target);
        }
    }
}
