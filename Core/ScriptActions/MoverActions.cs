using DrabadanCoreLib.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrabadanCoreLib.Core.ScriptActions
{
    public sealed class MoverActions : ScriptActionExecuter
    {
        private MoverActions() { }
        
        public static async Task<Point2D> GetItemLocationAsync(uint itemId)
        {
            bool itemExist = await FindTypeActions.ItemExistAsync(itemId);
            if (!itemExist)
                return default(Point2D);

            Point2D result = new Point2D();

            result.X = await ScriptApiCallAsync(() => { return (ushort)StealthClient.GetX(itemId); });
            result.Y = await ScriptApiCallAsync(() => { return (ushort)StealthClient.GetY(itemId); });

            return result;
        }

        public static async Task<int> GetDistanceToLocationAsync(Point2D location)
        {
            int distance = -1;

            Point2D selfLocation = await SelfActions.GetSelfLocation();
            distance = await ScriptApiCallAsync(()=> { return StealthClient.Dist((short)selfLocation.X, (short)selfLocation.Y, (short)location.X, (short)location.Y); });

            return distance;
        }

        public static async Task<int> GetDistanceToItemAsync(uint itemId)
        {
            int distance = -1;
            Point2D itemLocation = await GetItemLocationAsync(itemId);
            distance = await GetDistanceToLocationAsync(itemLocation);

            return distance;
        }

        public static async Task<bool> MoveToAsync(uint itemId)
        {
            Point2D location = await GetItemLocationAsync(itemId);
            if (location.X == 0 || location.Y == 0)
                return false;

            int distance = await GetDistanceToLocationAsync(location);
            if (distance == -1)
                return false;
            
            if(distance > 1)
                await ScriptApiCallAsync(() => 
                {
                    StealthClient.newMoveXY((ushort)location.X, (ushort)location.Y, true, 1, true);
                });

            int newDistance = await GetDistanceToItemAsync(itemId);

            if (newDistance <= 1)
                return true;

            return false;
        }

        
    }
}
