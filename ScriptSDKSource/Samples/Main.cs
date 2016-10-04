using System;
using System.Collections.Generic;
using DrabadanCoreLib.Engines;
using DrabadanCoreLib.Items;
using DrabadanCoreLib.Mobiles;
using DrabadanCoreLib.Targets;
using StealthAPI;

namespace DrabadanCoreLib
{
#pragma warning disable 1591
    public class Test
    {
        public Test3 data { get; set; }

        public Test()
        {
            data = new Test3();
        }
    }

    public class Test3
    {
        internal Test3()
        {
            
        }
    }

    class program
    {
        [STAThread]
        static void Main()
        {
            //var p = PlayerMobile.GetPlayer();
            //while (!p.Skills.Healing.Value.Equals(100.0))
            //    p.Salute();

            //List<Mobile> list = Scanner.Find<Mobile>(0x23E, 0xFFFF, 0x0, true);

            Console.WriteLine("Hello World");

            GetPathArrayTest();
            Console.ReadKey();
        }

        #region DrabadanTests

        static Stealth _stealth = Stealth.Client;

        private static void GetPathArrayTest()
        {
            var x = _stealth.GetX(_stealth.GetSelfID());

            var path = _stealth.GetPathArray(981, 538, true, 0);
            Console.WriteLine($"Path points {path}");
        }

        #endregion




    }
#pragma warning restore 1591
}

