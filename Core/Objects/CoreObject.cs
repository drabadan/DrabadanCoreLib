using DrabadanCoreLib.Core.Objects.UOObjects;
using DrabadanCoreLib.Core.ScriptActions;
using DrabadanCoreLib.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DrabadanCoreLib.Core.Objects
{
    public class CoreObject
    {
        public CoreObject() { }
        public CoreObject(uint id) { Id = id; }

        public uint Id { get; set; }

        private ushort _type = 0x0;
        public ushort Type
        {
            get
            {
                if (_type == 0x0)
                    GetItemType();
                return _type;
            }
        }

        private async void GetItemType()
        {
            _type = await FindTypeActions.GetItemTypeAsync(Id);
        }

        private string _tooltip;
        public string Tooltip
        {
            get
            {
                if (_tooltip == null)
                    GetTooltip();

                return _tooltip;
            }
        }

        private async void GetTooltip()
        {
            _tooltip = await TooltipHandler.GetTooltipAsync(Id);
        }
    }    

    public static class CoreObjectExtensions
    {
        public static async Task<CoreObject> ValidateItem(this CoreObject item, IList<Func<CoreObject, Task<bool>>> validationActions)
        {
            foreach(var action in validationActions)
            {
                bool valid = await action(item);
                if (!valid)
                    return null;
            }

            return item;
        }
        
        public static CoreObject IsInsurable(this CoreObject item)
        {
            if (item.Tooltip.ToLower().Contains("durability"))
                return item;

            return null;
        }

        public static async Task<CoreObject> MoveToContainerAsync(this CoreObject item, uint targetContainer)
        {
            await MoveItemActions.MoveItemWithoutUsingAsync(item.Id);
            return item;
        }

        public static async Task<CoreObject> InsureItem(this CoreObject item)
        {
            if (item.IsInsurable() == null)
                return null;

            await Insurer.InsureItemAsync(item.Id);
            return item;
        }
    }
}
