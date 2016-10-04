using DrabadanCoreLib.Data;
using DrabadanCoreLib.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrabadanCoreLib.OSISubScripts
{
    public class Moongate
    {
        public Moongate(string title, Point2D location, WorldEnum world)
        {
            Title = title;
            Location = location;
            World = world;
        }

        public Moongate(string title, Point2D location, WorldEnum world, byte osiGumpButton) 
        {
            Title = title;
            Location = location;
            World = world;
            OSIGumpRadioButton = osiGumpButton;
        }

        public string Title { get; set; }
        public Point2D Location { get; set; }
        public WorldEnum World { get; set; }
        public byte OSIGumpRadioButton { get; set; }
    }
}
