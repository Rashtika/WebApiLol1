using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Champion
    {
        public Inventory? Inventory { get; set; }
        public string? Name { set; get; }
        public Champion(string name)
        {
            Name = name;
            Inventory = new Inventory();
        }
        public Champion() { }
        public void Equip(Item item)
        {
            if (Inventory != null)
            {

                if (Inventory.Items.Count < 6)
                {
                    Inventory.Items.Add(item);
                    item.OnEquip(this);
                }
            }
        }
        public void UnEquip(Item item)
        {
            if (Inventory != null) {
                if (Inventory.Items.Count == 0) {
                    Console.WriteLine("Inventory is empty.");
                } else {
                    if (Inventory.Items.Contains(item))
                    {
                        Inventory.Items.Remove(item);
                        item.OnUnEquip(this);
                    }
                }
            }
        }
    }
}
