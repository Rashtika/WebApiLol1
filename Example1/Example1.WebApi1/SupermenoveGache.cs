using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class SupermenoveGache : Item
    {
        public override void OnEquip(Champion champion)
        {
            Console.WriteLine($"{champion.Name} upravo si obukao gachiche!");
        }
        public override void OnUnEquip(Champion champion)
        {
            Console.WriteLine($"{champion.Name} upravo si skinuo gachiche!");
        }
        public override string getDescription()
        {
            return "Ove gache nose se preko hlača kao da si izgubio okladu.";
        }
        public SupermenoveGache() : base("SupermenoveGache")
        {
        }
    }
}
