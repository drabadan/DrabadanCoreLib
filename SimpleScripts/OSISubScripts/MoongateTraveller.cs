using DrabadanCoreLib.Core.ScriptActions;
using DrabadanCoreLib.Data;
using DrabadanCoreLib.DataModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DrabadanCoreLib.OSISubScripts
{
    public class MoongateTraveller
    {
        public static BindingList<Moongate> Moongates = new BindingList<Moongate> {            
            #region Malas
            new Moongate("Luna", new Point2D(1015, 527), WorldEnum.Malas, 27),
            new Moongate("Umbra", new Point2D(1997, 1386), WorldEnum.Malas, 28),
            #endregion

            #region Trammel
            new Moongate("Moonglow", new Point2D(4467, 1283), WorldEnum.Trammel, 0),
            new Moongate("Britain", new Point2D(1336, 1997), WorldEnum.Trammel, 1),
            new Moongate("Jhelom", new Point2D(1499, 3771), WorldEnum.Trammel, 2),
            new Moongate("Yew", new Point2D(771, 752), WorldEnum.Trammel, 3),
            new Moongate("Minoc", new Point2D(2701, 692), WorldEnum.Trammel, 4),
            new Moongate("Trinsic", new Point2D(1828, 2948), WorldEnum.Trammel, 5),
            new Moongate("Skara brae", new Point2D(643, 2067), WorldEnum.Trammel, 6),
            new Moongate("New magincia", new Point2D(3563, 2139), WorldEnum.Trammel, 7),
            new Moongate("New heaven", new Point2D(3450, 2677), WorldEnum.Trammel, 8),
            #endregion

            #region Felucca
            new Moongate("Moonglow", new Point2D(4467, 1283), WorldEnum.Felucca, 9),
            new Moongate("Britain", new Point2D(1336, 1997), WorldEnum.Felucca, 10),
            new Moongate("Jhelom", new Point2D(1499, 3771), WorldEnum.Felucca, 11),
            new Moongate("Yew", new Point2D(771, 752), WorldEnum.Felucca, 12),
            new Moongate("Minoc", new Point2D(2701, 692), WorldEnum.Felucca, 13),
            new Moongate("Trinsic", new Point2D(1828, 2948), WorldEnum.Felucca, 14),
            new Moongate("Skara brae", new Point2D(643, 2067), WorldEnum.Felucca, 15),
            new Moongate("New magincia", new Point2D(3563, 2139), WorldEnum.Felucca, 16),
            new Moongate("Buccaneers den", new Point2D(2711, 2234), WorldEnum.Felucca, 17),
            #endregion

            #region Tokuno
            new Moongate("Isamu-jima", new Point2D(1169, 998), WorldEnum.Tokuno, 29),
            new Moongate("Makoto-jima", new Point2D(802, 1204), WorldEnum.Tokuno, 30),
            new Moongate("Homare-jima", new Point2D(270, 628), WorldEnum.Tokuno, 31),
            #endregion

            #region Ter mur
            new Moongate("Royal city", new Point2D(850, 3525), WorldEnum.TerMur, 32),
            //Valley of eodon, i'm not allowed to travel there...
            #endregion
        };

        public static async Task<Moongate> FindNearestMoongate()
        {
            int distance = 1000;
            Moongate mgate = null;
            WorldEnum currWorld = await SelfActions.GetCurrentWorldAsync();
            var locMoongates = Moongates.Where(w => w.World == currWorld).ToList();
            var selfLocation = await SelfActions.GetSelfLocation();
            foreach(var moongate in locMoongates)
            {
                int dist = await SelfActions.GetDistanceToLocationAsync(selfLocation, moongate.Location);
                if(dist < distance)
                {
                    distance = dist;
                    mgate = moongate;
                }
            }

            return mgate;
            
        }
        
        public MoongateTraveller() { }

       




    }
}
