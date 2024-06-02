using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class AmuletOfLost : Item
    {
        public override void OnEquip(Champion champion)
        {
            Console.WriteLine($"{champion.Name} se okitila!");
        }
        public override void OnUnEquip(Champion champion)
        {
            Console.WriteLine($"{champion.Name} se otkitila!");
        }
        public override string getDescription()
        {
            return "Sa ovom ogrlicom izgledam veoma eleKantno.";
        }
        public AmuletOfLost() : base("AmuletOfLost")
        {
        }
    }
}
