using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Champion
    {
<<<<<<< HEAD
        public Guid Id;
        public List<Item> Items { get; set; }
=======
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public Guid InventoryId { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedByUserId { get; set; }
        public int UpdatedByUserId { get; set; }
        public Inventory? Inventory { get; set; }
>>>>>>> 8772c826fd28bea845e2bbd07737bdc39f4fa5d9
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
