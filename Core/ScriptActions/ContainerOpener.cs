using StealthAPI;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DrabadanCoreLib.Core.ScriptActions
{
    public sealed class ContainerOpener : ScriptActionExecuter
    {
        private ContainerOpener() { }

        private static uint _lastOpenedContainer { get; set; } = 0;

        public static async Task SimpleOpenContainerAsync(uint containerId)
        {
            bool itemExist = await FindTypeActions.ItemExistAsync(containerId);
            if (itemExist)
                await ScriptApiCallAsync(() => StealthClient.UseObject(containerId));
            else
                Messanger?.Invoke($"0x{containerId.ToString("X")} not found!");
        }

        public static async Task<bool> OpenParentContainerOfItemAsync(uint itemId)
        {
            uint parentId = await Task.Run(() => { return Stealth.Client.GetParent(itemId); });

            if (parentId > 0)
                return await OpenContainerAsync(parentId);
            else
            {
                Messanger?.Invoke($"ParentContainer of item 0x{itemId.ToString("X")} not found! Moving item dismissed.");
                return false;
            }
        }

        public static async Task<bool> OpenCorpseAsync(uint corpseId)
        {
            if (_lastOpenedContainer == corpseId)
                return true;

            bool itemExist = await FindTypeActions.ItemExistAsync(corpseId);
            if (!itemExist)
                return false;

            bool result = false;
            using (var cts = new CancellationTokenSource())
            {
                result = await ScriptApiCallAsync<DrawContainerEventArgs>(
                    () => StealthClient.UseObject(corpseId),
                        (sender, e) =>
                        {
                            if (e.ModelGump == 0x9)
                            {
                                cts?.Cancel();
                            }
                            //else
                            //    throw new ArgumentException();
                        }, cts);
            }

            if (result)
                _lastOpenedContainer = corpseId;

            return result;
        }

        public static async Task<bool> OpenContainerAsync(uint containerId)
        {
            if (_lastOpenedContainer == containerId)
                return true;

            bool result = false;
            using (var cts = new CancellationTokenSource())
            {
                result = await ScriptApiCallAsync<DrawContainerEventArgs>(
                    () => StealthClient.UseObject(containerId),
                        (sender, e) =>
                        {
                            long diff = Math.Abs(e.ContainerId - containerId);
                            if ((diff <= 2) && (diff >= 0))
                            {
                                cts?.Cancel();
                            }
                            //else
                            //    throw new ArgumentException();
                        }, cts);
            }

            if (result)
                _lastOpenedContainer = containerId;

            return result;
        }

        public static async Task<IList<uint>> OpenContainerGetContentsAsync(uint containerId)
        {
            bool opened = await OpenContainerAsync(containerId);
            if (!opened)
                return null;

            var value = await ScriptApiCallAsync(() => { return StealthClient.FindTypeEx(0xFFFF, 0xFFFF, containerId, true); }, 300);
            IList<uint> items = await ScriptApiCallAsync(() => { return StealthClient.GetFindList(); });
            return items;
        }

        public static async Task<IList<uint>> OpenCorpseGetContentsAsync(uint containerId)
        {
            bool opened = await OpenCorpseAsync(containerId);
            if (!opened)
                return null;

            var value = await ScriptApiCallAsync(() => { return StealthClient.FindTypeEx(0xFFFF, 0xFFFF, containerId, true); }, 300);
            IList<uint> items = await ScriptApiCallAsync(() => { return StealthClient.GetFindList(); }, 0);
            return items;
        }

    }

}
