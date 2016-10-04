using DrabadanCoreLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrabadanCoreLib.Core.ScriptActions
{
    public sealed class FindTypeActions : ScriptActionExecuter
    {
        private FindTypeActions() { }

        public static async Task<uint> FindAtLocationAsync(Point2D location, ushort type = 0x0)
        {
            if(type == 0x0)
                return await ScriptApiCallAsync(()=> StealthClient.FindAtCoord((ushort)location.X, (ushort)location.Y));

            await ScriptApiCallAsync(() => StealthClient.FindAtCoord((ushort)location.X, (ushort)location.Y));
            var list = await GetFindedListAsync();
            if (list == null)
                return 0x0;
            foreach(var item in list)
            {
                var itemType = await GetItemTypeAsync(item);
                if (itemType == type)
                    return item;
            }

            return 0x0;
        }

        public static async Task<bool> IsContainerAsync(uint id)
        {
            bool isContainer = await ScriptApiCallAsync(()=> { return StealthClient.IsContainer(id); });
            return isContainer;
        }

        public static async Task<uint> FindTypeAsync(ushort type, uint containerId)
        {
            return await ScriptApiCallAsync(()=> { return StealthClient.FindType(type, containerId); });            
        }

        public static async Task<IList<uint>> GetFindedListAsync()
        {
            return await ScriptApiCallAsync(()=> { return StealthClient.GetFindList(); });
        }

        private static uint _alreadyValidated = 0;

        public static async Task<bool> ItemExistAsync(uint itemId)
        {
            if (_alreadyValidated == itemId)
                return true;

            _alreadyValidated = itemId;
            return await ScriptApiCallAsync(() => { return StealthClient.IsObjectExists(itemId); });
        }

        public static async Task<ushort> GetItemTypeAsync(uint itemId)
        {
            return await ScriptApiCallAsync(()=> { return StealthClient.GetType(itemId); });
        }
    }
}
