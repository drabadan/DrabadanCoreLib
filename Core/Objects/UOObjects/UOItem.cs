using DrabadanCoreLib.Core.ScriptActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrabadanCoreLib.Core.Objects.UOObjects
{
    public class UoItem : IHasId
    {
        public UoItem() { }
        public UoItem(uint id)
        {
            Id = new UoObjectProperty<uint>(id, false, UoPropertyStateEnum.Initialized);
        }

        public UoObjectProperty<uint> Id { get; protected set; }
        public UoObjectProperty<ushort> Type { get; protected set; } = new UoObjectProperty<ushort>(0x0, false);
        public UoObjectProperty<string> Tooltip { get; protected set; } = new UoObjectProperty<string>("", true);

        public virtual async Task UpdateSelfAsync()
        {
            if (Type.CurrentState == UoPropertyStateEnum.NotInitialized)
            {
                Type = await UoObjectProperty<ushort>.SetPropertyValueAsync(async () =>
                {
                    var type = await FindTypeActions.GetItemTypeAsync(Id.Value);
                    return new UoObjectProperty<ushort>(type, false, UoPropertyStateEnum.Initialized);
                });
            }

            Tooltip = await UoObjectProperty<string>.SetPropertyValueAsync(async () =>
            {
                var tooltip = await TooltipHandler.GetTooltipAsync(Id.Value);
                return !string.IsNullOrEmpty(tooltip) ? new UoObjectProperty<string>(tooltip, true, UoPropertyStateEnum.Initialized) : default(UoObjectProperty<string>);
            });
        }
    }

    public static class UoItemExtensions
    {
        private static readonly List<Predicate<UoItem>> IsInsurableValidationPredicates = new List<Predicate<UoItem>> {
            (item) => item.Type.Value == 0x2006 || item.Type.Value == 0xeed,
            (item) => item.Tooltip.Value.Contains("Insured") || item.Tooltip.Value.Contains("Blessed")
        };

        public static async Task<bool> IsInsurable(this UoItem item)
        {
            return await Task.Run(() =>
            { return IsInsurableValidationPredicates.All(predicate => !predicate(item)); });
        }

        public static async Task InsureItemAsync(this UoItem item)
        {
            uint id = await Task.Run(() => item.Id.Value);
            await Insurer.InsureItemAsync(id);
        }

        public static async Task MoveToContainerAsync(this UoItem item, uint containerId)
        {
            uint id = await Task.Run(() => item.Id.Value);
            await MoveItemActions.MoveItemWithoutUsingAsync(id, containerId);
        }
    }
}
