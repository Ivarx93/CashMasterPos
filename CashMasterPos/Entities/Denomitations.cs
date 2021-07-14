using System;
using System.Collections.Generic;
using System.Text;
using CashMasterPos.Entities;

namespace CashMasterPos.Entities
{
    public class Denomitations
    {

         private readonly List<double> UsDenomination = new List<double> { 0.01, 0.05, 0.10, 0.25, 0.50, 1.00, 2.00, 5.00, 10.00, 20.00, 50.00, 100.00 };
         private readonly List<double> MxDenomination =new  List<double>{ 0.05, 0.10, 0.20, 0.50, 1.00, 2.00, 5.00, 10.00, 20.00, 50.00, 100 };
       
        public List<double> GetDenominationByCountry(string type)
        {
            List<double> denominations = new List<double>();
            if (type == Enums.Denominations.US.ToString())
                denominations = UsDenomination;
            else if (type == Enums.Denominations.MX.ToString())
                denominations =  MxDenomination;
            return denominations;
        }
    }
}
