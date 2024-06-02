using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class BucketHelmet : Item
    {
        public override void OnEquip(Champion champion)
        {
            Console.WriteLine($"{champion.Name} upravo si na glavu stavio kantu!");
        }
        public override void OnUnEquip(Champion champion)
        {
            Console.WriteLine($"{champion.Name} upravo si skinuo kantu s glave!");
        }
        public override string getDescription()
        {
            return "Ništa ne vidiš.";
        }
        public BucketHelmet() : base("BucketHelmet")
        {
        }
    }
}
