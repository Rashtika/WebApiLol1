using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Shilterica : Item
    {
            public override void OnEquip(Champion champion)
            {
                Console.WriteLine($"{champion.Name} upravo si na glavu stavio shiltericu!");
            }
            public override void OnUnEquip(Champion champion)
            {
                Console.WriteLine($"{champion.Name} upravo si skinuo shiltericu!");
            }
            public override string getDescription()
            {
                return "Od sada te u napadu nece ometati sunceve zrake.";
            }
            public Shilterica() : base("Shilterica")
            {
            }
        }
}
