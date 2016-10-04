using DrabadanCoreLib.DataModel;
using DrabadanCoreLib.Data;
using StealthAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrabadanCoreLib.Core.ScriptActions
{
    public sealed class SelfActions : ScriptActionExecuter
    {
        private static uint _selfBackpackId;

        private SelfActions() { }

        private static uint _selfId { get; set; } = 0;

        public static async Task<bool> GetHiddenStatusAsync()
        {
            return await ScriptApiCallAsync(()=> StealthClient.GetHiddenStatus());
        }


        private static DateTime _lastTryToHide = DateTime.Now;
        private static TimeSpan GetLastHidingTimeExecuted()
        {
            return DateTime.Now - _lastTryToHide;
        }

        public static async Task HideIfNotHiddenAsync(int maxTriesCount = 5)
        {
            bool hidden = await GetHiddenStatusAsync();

            if (!hidden)
            {
                if (GetLastHidingTimeExecuted().Seconds > 10)
                {
                    _lastTryToHide = DateTime.Now;
                    await ScriptApiCallAsync(() => StealthClient.UseSkill("Hiding"));
                }
            }
        }

        public static async Task<uint> GetSelfId()
        {
            uint id = await ScriptApiCallAsync(()=> StealthClient.GetSelfID());
            _selfId = id;
            return id;
        }

        public static async Task<Point2D> GetSelfLocation()
        {
            if(_selfId == 0)            
                _selfId = await ScriptApiCallAsync(()=> StealthClient.GetSelfID());
            
            return await MoverActions.GetItemLocationAsync(_selfId);
        }

        public static async Task<uint> GetBackpackIdAsync()
        {
            if(_selfBackpackId == 0)
                _selfBackpackId = await ScriptApiCallAsync(() => StealthClient.GetBackpackID());
            return _selfBackpackId;
        }

        public static async Task<int> GetDistanceToLocationAsync(Point2D locationFrom, Point2D locationTo)
        {   
            return await ScriptApiCallAsync(() => StealthClient.Dist((short)locationFrom.X, (short)locationFrom.Y, (short)locationTo.X, (short)locationTo.Y));
        }

        public static async Task<int> GetDistanceToLocationAsync(Point2D location)
        {
            Point2D selfLocation = await GetSelfLocation();
            return await ScriptApiCallAsync(() => StealthClient.Dist((short)selfLocation.X, (short)selfLocation.Y, (short)location.X, (short)location.Y));
        }

        public static async Task<WorldEnum> GetCurrentWorldAsync()
        {
            return (WorldEnum)await ScriptApiCallAsync(()=> StealthClient.GetWorldNum());
        }

        public static async Task ApproachToLocationAsync(Point2D location)
        {
            await ScriptApiCallAsync(()=> StealthClient.newMoveXY((ushort)location.X, (ushort)location.Y, true, 1, true));
            await Task.Delay(2000);
        }
    }
}
