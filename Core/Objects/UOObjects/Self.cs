using DrabadanCoreLib.Core.ScriptActions;
using DrabadanCoreLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrabadanCoreLib.Core.Objects.UOObjects
{
    public class Self : UoItem
    {
        private static Self _player;
        public static Self Player
        {
            get
            {
                if (_player == null)
                {
                    _player = new Self();
                }

                return _player;
            }
        }

        public static UoPropertyStateEnum SelfInitializedStatus { get; private set; }

        private Self()
        {
            SelfInitializedStatus = UoPropertyStateEnum.NotInitialized;
        }

        public async Task InitializeSelf()
        {
            var id = await SelfActions.GetSelfId();
            _player.Id = new UoObjectProperty<uint>(id, false, UoPropertyStateEnum.Initialized);
            await _player.UpdateSelfAsync();
            SelfInitializedStatus = UoPropertyStateEnum.Initialized;
        }

        public UoObjectProperty<UoContainer> Backpack { get; protected set; } = new UoObjectProperty<UoContainer>(new UoContainer(), false);
        public UoObjectProperty<Point2D> Location { get; protected set; } = new UoObjectProperty<Point2D>(new Point2D(0, 0), true);
        public UoObjectProperty<string> ContextMenu { get; protected set; } = new UoObjectProperty<string>("", false);

        public override async Task UpdateSelfAsync()
        {
            await base.UpdateSelfAsync();
            Backpack = await UoObjectProperty<UoContainer>.SetPropertyValueAsync(async () =>
            {
                var bpId = await SelfActions.GetBackpackIdAsync();
                var container = await UoContainer.ConstructorProxy(bpId);
                return new UoObjectProperty<UoContainer>(container, false, UoPropertyStateEnum.Initialized);
            });
            Location = await UoObjectProperty<Point2D>.SetPropertyValueAsync(async () =>
            {
                var loc = await SelfActions.GetSelfLocation();
                return new UoObjectProperty<Point2D>(loc, true, UoPropertyStateEnum.Initialized);
            });
            ContextMenu = await UoObjectProperty<string>.SetPropertyValueAsync(async () =>
            {
                var contextMenu = await ContextMenuHandler.GetContextMenuAsync(Id.Value);
                return new UoObjectProperty<string>(contextMenu, false, UoPropertyStateEnum.Initialized);
            });
        }
    }

    public static class SelfExtensions
    {
        public static async Task MoveTo(this Self self, uint id)
        {
            await Task.Delay(50);
        }
    }
}
