using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CashMasterPos.Entities;
using CashMasterPos.ErrorHandler;
using System.Linq;

namespace CashMasterPos.Utilities
{
   public class Utilities: IUtilities
    {
        public double InputAmount { get; set; }
        public Item Item { get; set; }
        public double  Change { get; set; }
        /// <summary>
        /// This method is used to calculate the difference between the input amount and the item price.
        /// </summary>
        /// <returns>MessageResult<T> object</returns>
        public MessageResult<double> CalculateChange()
        {
            return new MessageResult<double>
            {
                Data = RoundUp((InputAmount - Item.Price), 2),
                Message = "Ok",
                Status = true
        };
        }

        /// <summary>
        ///This method is used to distribute the smallest number of bills according to the customer's change.
        ///The bills returned are the ones available for the machine's current denomination of bills.
        /// </summary>
        /// <param name="change">A double precision number.</param>
        /// <param name="currentDenomination">A List of double precision number.</param>
        /// <returns>MessageResult<T> object.</returns>
        public MessageResult<List<AmountManager>> DistributeBills(double change, List<double> currentDenomination)
        {
            var denominationBills = GetBillsDenomination(currentDenomination).ToList();
            var distributedAmount = new List<AmountManager>();
            var bills = Math.Truncate(change);
            double billCounter = 0.0;
            var closest = 0.0;
            if (bills != 0)
            {
                do
                {
                    var temp = bills - billCounter;
                    if(temp!=0)
                         closest = denominationBills.Where(x => x <= temp).OrderBy(item => Math.Abs((temp) - item)).First();
                    distributedAmount.Add(new AmountManager
                    {
                        Bill = closest
                    });
                    billCounter += closest;
                } while (billCounter < bills);
            }
            if (distributedAmount.Count() > 0)
                return new MessageResult<List<AmountManager>>
                {
                    Data = distributedAmount,
                    Message = string.Join(",", distributedAmount.Select(x=>"$"+x.Bill)),
                    Status = true
                };
            else
                return new MessageResult<List<AmountManager>>
                {
                    Data = null,
                    Message = null,
                    Status = false
                };
        }
        /// <summary>
        ///This method is used to distribute the smallest number of coins according to the customer's change.
        ///The coins returned are the ones available for the machine's current denomination of dimes.
        /// </summary>
        /// <param name="change">A double precision number.</param>
        /// <param name="currentDenomination">A List of double precision number.</param>
        /// <returns>MessageResult<T> object.</returns>
        public MessageResult<List<AmountManager>> DistributeDimes(double change, List<double> currentDenomination)
        {
            var denominationdimes = GetDimesDenomination(currentDenomination).ToList();
            var distributedAmount = new List<AmountManager>();
            var dimes = change - Math.Truncate(change);
            double dimeCounter = 0.0;
            var closest = 0.0;
            if (dimes != 0)
            {
                do
                {
                    var temp = RoundUp((dimes - dimeCounter), 2);
                    if(temp!=0)
                         closest = denominationdimes.Where(x => x <= temp).OrderBy(item => Math.Abs((temp) - item)).First();
                    distributedAmount.Add(new AmountManager
                    {
                        Dimes = closest
                    });
                    dimeCounter += closest;
                } while (dimeCounter < dimes);
            }
            if (distributedAmount.Count() > 0)
                return new MessageResult<List<AmountManager>>
                {
                    Data = distributedAmount,
                    Message = string.Join(",", distributedAmount.Select(x=>"$"+x.Dimes)),
                    Status = true
                };
            else
                return new MessageResult<List<AmountManager>>
                {
                    Data = null,
                    Message = null,
                    Status = false
                };
        }
        /// <summary>
        ///This method is used to round up a number to x places possible.
        /// </summary>
        /// <param name="input">A double precision number.</param>
        /// /// <param name="places">A int precision number.</param>
        /// /// See <see cref="Math.Pow(double, double)"/> to pow doubles.
        /// See <see cref="Math.Ceiling(double)"/> to round values.
        /// /// See <see cref="Convert.ToDouble(int)"/> to convert values.
        /// <returns>A double precision number.</returns>
        public double RoundUp(double input, int places)
        {
            double multiplier = Math.Pow(10, Convert.ToDouble(places));
            return Math.Ceiling(input * multiplier) / multiplier;
        }
        /// <summary>
        ///This method is used to sum customer bills and dimes inputs.
        /// </summary>
        /// <param name="amount">An AmountManager object.</param>
        /// <returns>A double precision number.</returns>

        public double Sum(AmountManager amount)
        {
            return amount.Bill + amount.Dimes;
        }

        /// <summary>
        ///This method is used to retrieve all the bills values on the current denomination array for this machine.
        /// </summary>
        /// <param name="denominationList">A List of double precision number.</param>
        /// <returns>A List of double precision number.</returns>
        public IEnumerable<double> GetBillsDenomination(List<double> denominationList)
        {
            foreach(var el in denominationList)
            {
                if(el>=1)
                    yield return el;
            }
        }

        /// <summary>
        ///This method is used to retrieve all the dimes values on the current denomination array for this machine.
        /// </summary>
        /// <param name="denominationList">A List of double precision number.</param>
        /// <returns>A List of double precision number.</returns>
        public IEnumerable<double> GetDimesDenomination(List<double> denominationList)
        {
            foreach (var el in denominationList)
            {
                if (el < 1)
                    yield return el;
            }
        }


    }
}
