using System;
using System.Collections.Generic;
using System.Text;

namespace CashMasterPos.Entities
{
    public class MessageResult<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; } = true;
    }
}
