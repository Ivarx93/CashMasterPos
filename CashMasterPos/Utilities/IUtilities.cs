using CashMasterPos.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CashMasterPos.Utilities
{
    public interface IUtilities
    {
        public MessageResult<double> CalculateChange();
        public MessageResult<List<AmountManager>> DistributeBills(double amount, List<double> currentDenomination);
        public MessageResult<List<AmountManager>> DistributeDimes(double amount, List<double> currentDenomination);
        public double RoundUp(double input, int places);
        public double Sum(AmountManager amount);

        public  IEnumerable<double> GetBillsDenomination(List<double> denominationList);
        public  IEnumerable<double> GetDimesDenomination(List<double> denominationList);

    }
}
