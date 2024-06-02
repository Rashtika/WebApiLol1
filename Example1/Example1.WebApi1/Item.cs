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
        public string Name { get; set; }
        public abstract void OnEquip (Champion champion);
        public abstract void OnUnEquip(Champion champion);

        public abstract string getDescription();

        public Item() {}
        public Item(string name)
        {
            Name = name;
        }
    }

}
