using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Champion
    {
        public Guid Id;
        public List<Item> Items { get; set; }
        public string? Name { set; get; }
        public Champion(string name, List<Item> items)
        {
            Name = name;
            Items = items;
        }
        public Champion() { }
        public void Equip(Item item)
        {
            if (Items != null)
            {

                if (Items.Count < 6)
                {
                    Items.Add(item);
                }
            }
        }
        public void UnEquip(Item item)
        {
            if (Items != null) {
                if (Items.Count == 0) {
                    Console.WriteLine("Inventory is empty.");
                } else {
                    if (Items.Contains(item))
                    {
                        Items.Remove(item);
                    }
                }
            }
        }
    }
}
