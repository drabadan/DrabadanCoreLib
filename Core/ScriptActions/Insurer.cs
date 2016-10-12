using DrabadanCoreLib.Core.Objects.UOObjects;
using DrabadanCoreLib.Core.ScriptActions;
using StealthAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DrabadanCoreLib.Core.Objects;
using System.Diagnostics.Contracts;

namespace DrabadanCoreLib.Core.ScriptActions
{
    public sealed class Insurer : ScriptActionExecuter
    {
        private Insurer() { }
        private static byte _insureEntry { get; set; } = 0;        
        private static byte _contextMenuEntry = 0;

        //private static uint _selfId = 0x0;
        private static bool _menuHookSet = false;

        private static async Task SetContextMenuEntry()
        {
            if (Self.SelfInitializedStatus == UoPropertyStateEnum.NotInitialized)
                await Self.Player.InitializeSelf();
            
            string context = Self.Player.ContextMenu.Value;
            var lines = context.Split(new string[] { Environment.NewLine }, StringSplitOptions.None).ToList();

            _contextMenuEntry = (byte)lines.FindIndex(l => l.Contains("Toggle Item Insurance"));
     }

        public static async Task InsureItemAsync(uint itemId)
        {
            ushort itemExist = await ScriptApiCallAsync(()=> StealthClient.GetType(itemId));
            if (itemExist == 0)
                return;

            if (_contextMenuEntry == 0)
                await SetContextMenuEntry();

            if (!_menuHookSet)
            {
                await ScriptApiCallAsync(() => { StealthClient.SetContextMenuHook(Self.Player.Id.Value, _contextMenuEntry); });
                Messanger?.Invoke("Hook set!");
                _menuHookSet = true;
            }            

            bool targetPresent = await TargetActions.TargetPresentAsync();
            bool success = true;
            if (!targetPresent)
            {
                var cts1 = new CancellationTokenSource();
                success= await ScriptApiCallAsync<ClilocSpeechEventArgs>(() => { StealthClient.RequestContextMenu(Self.Player.Id.Value); }, (sender, e) =>
                {
                    if (e.ClilocId == 1060868)
                        cts1.Cancel();
                }, cts1);
            }

            if (!success)
            {
                Messanger?.Invoke("Insurance failed due to target not received!");
                return;
            }

            var cts2 = new CancellationTokenSource();
            success = await ScriptApiCallAsync<ClilocSpeechEventArgs>(() => { StealthClient.TargetToObject(itemId); }, (sender, e) =>
            {
                if (e.ClilocId == 1060873 || e.ClilocId == 1060874)
                    cts2?.Cancel();
            }, cts2);

            await TargetActions.CancelTargetAsync();

            if (!success)
            {
                Messanger?.Invoke("Insurance failed due to targeting exception!");
                return;
            }

            Messanger?.Invoke("Insurance successfull");
        }
        
    }
}
