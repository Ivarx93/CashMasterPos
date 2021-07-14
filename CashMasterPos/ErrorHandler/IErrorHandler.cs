using CashMasterPos.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CashMasterPos.ErrorHandler
{
    public interface IErrorHandler
    {
        public MessageResult<bool> ValidDenomination(double currentValue, bool isBill, List<double> currentDenomination);
        public MessageResult<string> ValidNumericInputs(string value);
    }
}
