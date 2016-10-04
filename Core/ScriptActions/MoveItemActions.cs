using StealthAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DrabadanCoreLib.Core.ScriptActions
{
    public sealed class MoveItemActions : ScriptActionExecuter
    {
        private static uint _backpackId { get; set; } = Stealth.Client.GetBackpackID();

        private MoveItemActions()
        {
        }
        
        public static async Task MoveItemAsync(uint itemId, uint containerTarget = 0)
        {
            uint targetContainer = (containerTarget == 0) ? _backpackId : containerTarget;

            bool itemValidation = await ValidateMoveItemAsync(itemId);
            if (!itemValidation)
                return;

            //not usefull here! deprecated
            //bool openContainer = await ContainerOpener.OpenParentContainerOfItemAsync(itemId);
            //if (!openContainer)
            //    return;
            
            using (var cts = new CancellationTokenSource())
            {
                await ScriptApiCallAsync<AddItemToContainerEventArgs>(() =>
                {
                    Stealth.Client.MoveItem(itemId, 0, targetContainer, 0, 0, 0);
                }, (sender, e) =>
                {
                    //if (e.ContainerId == targetContainer)// && e.ItemId == itemId)
                        cts?.Cancel();
                }
                , cts);
            }
            //Messanger?.Invoke("Moving item finished!");
        }


        private static CancellationTokenSource _cts = null;
        public static async Task MoveItemWithoutUsingAsync(uint itemId, uint containerTarget = 0)
        {
            uint targetContainer = (containerTarget == 0) ? _backpackId : containerTarget;

            bool itemValidation = await ValidateMoveItemAsync(itemId);
            if (!itemValidation)
                return;

            _cts = new CancellationTokenSource();
            await ScriptApiCallAsync<AddItemToContainerEventArgs>(
                () => { StealthClient.MoveItem(itemId, 0, targetContainer, 0, 0, 0); }, 
                AddItemToContainerEventHandler, 
                _cts);
            _cts = null;
        }

        private static void AddItemToContainerEventHandler(object sender, AddItemToContainerEventArgs args)
        {
            _cts?.Cancel();
        }

        private static async Task<bool> ValidateMoveItemAsync(uint itemId)
        {
            bool result = true;
            //is not corpse validation
            ushort type = await FindTypeActions.GetItemTypeAsync(itemId);
            result = !type.Equals(0x2006);

            return result;
        }

        public static async Task MoveItemAsync(ushort itemType, uint containerFrom, uint containerTarget = 0)
        {
            containerTarget = (containerTarget == 0) ? _backpackId : containerTarget;

            bool opened = await ContainerOpener.OpenContainerAsync(containerFrom);
            if (!opened)
                return;

            uint itemId = await Task.Run(()=> { return Stealth.Client.FindTypeEx(itemType, 0xFFFF, containerFrom, true); });
            if (itemId > 0)
                await MoveItemAsync(itemId, containerTarget);
        }

        public static async Task MoveItemsAsync(IList<uint> items, uint containerTarget = 0)
        {
            foreach (var item in items)
                await MoveItemAsync(item, containerTarget);
        }        
    }
}
