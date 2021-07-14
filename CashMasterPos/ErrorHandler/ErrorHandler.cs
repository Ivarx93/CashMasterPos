using CashMasterPos.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace CashMasterPos.ErrorHandler
{
    public class ErrorHandler : IErrorHandler
    {
        /// <summary>
        ///This method checks wheater the introduced amount matches with the values on the denomination array for this machine.
        /// </summary>
        /// <param name="currentValue">A double precision number.</param>
        /// <param name="isBill">A boolean.</param>
        /// /// <param name="currentDenomination">A List of double precision number.</param>
        /// See <see cref="String.Format(string, object?[])"/> to format string.
        /// <returns>MessageResult<T> object.</returns>
        public MessageResult<bool> ValidDenomination(double currentValue, bool isBill, List<double> currentDenomination)
        {
            var utilities = new CashMasterPos.Utilities.Utilities();
            var bills = utilities.GetBillsDenomination(currentDenomination).ToList();
            var dimes = utilities.GetDimesDenomination(currentDenomination).ToList();
            MessageResult<bool> res = new MessageResult<bool>();

            if (!bills.Contains(currentValue) && (currentValue != 0) && isBill)
            {
                res.Data = false;
                res.Message = String.Format("The introduced amount is not accepted, accepted denominations are: {1}", currentValue, string.Join(",", bills));
                res.Status = false;
            }
            else if (!dimes.Contains(currentValue) && (currentValue != 0) && !isBill)
            {
                res.Data = false;
                res.Message = String.Format("The introduced amount is not accepted, accepted denominations are: {1}", currentValue, string.Join(",", dimes));
                res.Status = false;
            }
            else
            {
                res.Data = true;
                res.Message = "Ok";
                res.Status = true;
            }

            return res;
        }
        /// <summary>
        ///This method checks wheater the introduced amount is a number or not.
        /// </summary>
        /// <param name="value">A string object.</param>
        /// /// <param name="currentDenomination">A List of double precision number.</param>
        /// See <see cref="Regex.Match(string)"/> to match a string with a pattern.
        /// <returns>MessageResult<T> object.</returns>
        public MessageResult<string> ValidNumericInputs(string value)
        {
            Regex reg = new Regex(@"^\d*(\.\d+)?$");
            MessageResult<string> res = new MessageResult<string>();
            if (string.IsNullOrEmpty(value))
                value = "0";
            if (reg.Match(value).Success)
            {
                res.Data = value;
                res.Message = "Ok";
                res.Status = true;
            }
            else
            {
                res.Data = null;
                res.Message = "Numeric value is required";
                res.Status = false;
            }
            return res;
        }
    }
}
