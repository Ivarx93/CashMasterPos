using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CashMasterPos.Entities
{
    public class Item
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public Item(string name)
        {
            this.Name = name;
        }
    }
    
}
