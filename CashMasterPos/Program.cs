using CashMasterPos.Entities;
using System;
using System.Collections.Generic;
namespace CashMasterPos
{
    class Program
    {
        static void Main(string[] args)
        {
            //Initial instances
            var Item = new Item("Item");
            var utilities = new CashMasterPos.Utilities.Utilities();
            var errorHandler = new CashMasterPos.ErrorHandler.ErrorHandler();
            bool repeat = false;
            var validateInput = new MessageResult<string>();
            var validateDenominations = new MessageResult<bool>();
            //High level loop starts here, and it will loop if the repeat variable is set to true
            do {
                Console.Clear();
                //The List that will store all inputted amounts for the customer
                AmountManager amountManager = new AmountManager();
                //Loop to check whether the item price input amount is a number
                do
                {
                    repeat = false;
                    Console.Write("\nEnter item price: ");
                    var itemPrice = Console.ReadLine();
                    validateInput = errorHandler.ValidNumericInputs(itemPrice);
                    //if the input amount is not a number, it will loop until you enter a valid number
                    //Hitting the enter key will count as entering a 0
                    if (!validateInput.Status)
                    {
                        repeat = true;
                        Console.WriteLine(validateInput.Message);
                    }
                    else
                    {
                        //It parses the input string number to double precision
                        Item.Price = double.Parse(validateInput.Data);
                        utilities.Item = Item;
                    }
                 } while (repeat);
                var Customer = new Customer();
                Customer.FullName = "John Doe";
                var CurrentDenomination = new Denomitations();
                //Get the global array of denominations for this POS
                var currentDenomination = CurrentDenomination.GetDenominationByCountry(Enums.Denominations.US.ToString());
                double billCounter = 0;
                double dimeCounter = 0;
                //Loop to check whether customer input amount is greater than the item price.
                do
                {
                    var currentBill = string.Empty;
                    var currentDimes = string.Empty;
                    //Loop to check whether the customer input bills matches with the current array of denominations.
                    do
                    {
                        repeat = false;
                        //Loop to check whether the customer input bills are a valid number
                        do
                        {
                            repeat = false;
                            Console.Write("\nEnter bills: ");
                            currentBill = Console.ReadLine();
                            validateInput = errorHandler.ValidNumericInputs(currentBill);
                            if (!validateInput.Status)
                            {
                                repeat = true;
                                Console.WriteLine(validateInput.Message);
                            }
                        } while (repeat);
                        validateDenominations = errorHandler.ValidDenomination(double.Parse(validateInput.Data), true, currentDenomination);
                        if (!validateDenominations.Data)
                        {
                            Console.WriteLine(validateDenominations.Message);
                            repeat = true;
                        }
                        else
                            billCounter += double.Parse(validateInput.Data);

                    } while (repeat);
                    //if input bills over passes the item price, the dimes loop is being ignored
                    if (billCounter < utilities.Item.Price)
                    {
                        //Loop to check whether the customer input dimes matches with the current array of denominations.
                        do
                        {
                            //Loop to check whether the customer input dimes are a valid number
                            do
                            {                                
                                repeat = false;
                            Console.Write("\nEnter dimes: ");
                            currentDimes = Console.ReadLine();
                                validateInput = errorHandler.ValidNumericInputs(currentDimes);
                                if (!validateInput.Status)
                                {
                                    repeat = true;
                                    Console.WriteLine(validateInput.Message);
                                }
                            } while (repeat);
                            validateDenominations = errorHandler.ValidDenomination(double.Parse(validateInput.Data), false, currentDenomination);
                            if (!validateDenominations.Data)
                            {
                                Console.WriteLine(validateDenominations.Message);
                                repeat = true;
                            }
                            else
                                dimeCounter += double.Parse(validateInput.Data);

                        } while (repeat);
                        if(utilities.Item.Price - (billCounter + dimeCounter)>0)
                          Console.WriteLine(string.Format("\nReminder: {0}", utilities.RoundUp((utilities.Item.Price - (billCounter + dimeCounter)),2)));
                        }
                   
                } while ((billCounter + dimeCounter) < utilities.Item.Price);
                amountManager.Bill = billCounter;
                amountManager.Dimes = dimeCounter;
             
                Customer.InputAmount = amountManager;


                var sumAmount = utilities.Sum(Customer.InputAmount);
                utilities.InputAmount = sumAmount;
                var res = utilities.CalculateChange();
                var distributedBills = utilities.DistributeBills((double)res.Data, currentDenomination);
                var distributedDimes = utilities.DistributeDimes((double)res.Data, currentDenomination);
                Console.WriteLine(string.Format("\n********************\nTicket info\n********************\n Item: {0}\n Total: ${1}\n Paid: ${2}\n Your change: ${3}\n\nWe thank you for your purchase!", utilities.Item.Name, utilities.Item.Price, sumAmount, res.Data));
                if (distributedBills.Status)
                    Console.WriteLine(string.Format("\n\n Bills returned: {0} \n Amount of bills returned: {1}", distributedBills.Message, distributedBills.Data.Count));
                if (distributedDimes.Status)
                    Console.WriteLine(string.Format("\n\n Dimes returned: {0} \n Amount of coins returned: {1}", distributedDimes.Message, distributedDimes.Data.Count));
                Console.Write("\n\nWould you like to purchase another item?\nY/N\n");
                var carryOn = Console.ReadLine();
                switch (carryOn.ToUpper())
                {
                    case "Y": repeat = true; break; 
                    case "N": repeat = false; break; 
                    default: repeat = false; break;
                }

            } while (repeat);
        }
    }
}
