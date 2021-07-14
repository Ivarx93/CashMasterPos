using System;
using System.Collections.Generic;
using System.Text;

namespace CashMasterPos.Entities
{
   public class Customer
    {
        public string FullName { get; set; }
        public AmountManager InputAmount{ get; set; }
    }
}
