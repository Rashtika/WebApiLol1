using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public abstract class Item : IDescription
    {
        private Guid Id {  get; set; }
        private bool IsActive { get; set; }
        private DateOnly DateCreated { get; set; }
        private int CreatedByUserId { get; set; }
        private int UpdatedByUserId { get; set; }
        public string Name { get; set; }

        public abstract string getDescription();

        public Item() {}
        public Item(string name)
        {
            Name = name;
        }
    }

}
