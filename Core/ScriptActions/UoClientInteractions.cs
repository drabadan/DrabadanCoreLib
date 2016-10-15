using StealthAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrabadanCoreLib.Core.ScriptActions
{
    public enum ClientInteractionTarget
    {
        UoObject,
        UoTile
    }

    public class UoClientInteractionsHandler : ScriptActionExecuter
    {
        private UoClientInteractionsHandler() { }

        public static async Task<TargetInfo> RequestTargetInfo(ClientInteractionTarget target)
        {
            bool callSuccess = false;
            switch (target)
            {
                case ClientInteractionTarget.UoObject:
                    callSuccess = await ScriptApiCallAsync(()=> StealthClient.ClientRequestObjectTarget());
                    if(callSuccess)
                        return await WaitClientTargetResponseAsync();
                    else
                        return default(TargetInfo);
                case ClientInteractionTarget.UoTile:
                    callSuccess = await ScriptApiCallAsync(() => StealthClient.ClientRequestTileTarget());
                    if (callSuccess)
                        return await WaitClientTargetResponseAsync();
                    else
                        return default(TargetInfo);
            }
            return default(TargetInfo);
        }

        private static async Task<TargetInfo> WaitClientTargetResponseAsync(int maxWaitCounter = 30000)
        {
            int counter = 0;
            bool response = false;
            do
            {
                counter++;
                await Task.Delay(1000);
                response = await ScriptApiCallAsync(()=> StealthClient.ClientTargetResponsePresent());
                if (response)
                    return await ScriptApiCallAsync(()=> StealthClient.ClientTargetResponse());
            } while (counter < (maxWaitCounter / 1000));

            return default(TargetInfo);
        }
    }
}
