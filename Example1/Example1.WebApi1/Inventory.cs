using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Inventory : IDescription
    {
        private Guid Id { get; set; }
        private bool IsActive { get; set; }
        private DateTime DateCreated { get; set; }
        private int CreatedByUserId { get; set; }
        private int UpdatedByUserId { get; set; }
        public List<Item> Items { get; set; }
        public Inventory()
        {
            Items = new List<Item>(6);
        }

        public string getDescription()
        {
            string output = string.Empty;
            for (int i = 0; i < Items.Count; i++)
            {
                output += $"{i} - {Items[i].Name}\n";
            }
            return output;
        }

    }
}
