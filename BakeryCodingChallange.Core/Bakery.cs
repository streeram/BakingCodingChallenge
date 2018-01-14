//---------------------------------------------------------------------------------
// <copyright file="Bakery.cs" company="Sample">
//     Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author> Sriram Narayanan </author>
// <date> 01/14/2018 11:39:58 AM </date>
// <summary> Class representing the core logic for Bakery Coding Challenge Program </summary>
//---------------------------------------------------------------------------------
namespace BakeryCodingChallenge.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Core Class containing the Bakery Coding Logic
    /// </summary>
    public static class Bakery
    {
        /// <summary>
        /// Processes the order with the user provided order item.
        /// </summary>
        /// <param name="inputQuantity"> Order item quantity</param>
        /// <param name="arrInputParts"> User input string array split with whitespaces</param>
        /// <param name="dicPacksWithRates"> A Dictionary loaded with packs and rates on offer.</param>
        /// <param name="dicFinalPackSplit"> A SortedDictionary final calculation of packs.</param>
        public static void ProcessOrder(ref int inputQuantity, ref string[] arrInputParts, ref Dictionary<string, Dictionary<int, double>> dicPacksWithRates, out SortedDictionary<int, int> dicFinalPackSplit)
        {
            // Initialise variables.
            int qty = inputQuantity;
            int response = -1;
            string strProductCode = arrInputParts[1].ToUpper();
            Stack<int> stkPreviousReminder = new Stack<int>();

            // Push the user input quantity to the bottom of the stack
            stkPreviousReminder.Push(qty);

            // Get the pack details.
            int[] packs = dicPacksWithRates[strProductCode].Keys.ToArray();

            // Dictionary to hold the packs and the corresponding multipliers
            dicFinalPackSplit = new SortedDictionary<int, int>();

            foreach (var item in packs)
            {
                // Initialize Dictionary with zero multiplier for all packs
                dicFinalPackSplit.Add(item, -1);
            }

            // Sort the packs in descending order and loop through each pack
            foreach (var packSize in dicFinalPackSplit.Reverse())
            {
                // Pass the previous reminder stack as reference, user input quantity and the dictionary containing the pack multipliers
                response = CalculatePackSplit(ref stkPreviousReminder, qty, ref dicFinalPackSplit);

                // If result of the calculation is -1 proceed with the loop.
                if (response == -1)
                {
                    continue;
                }

                // If result of the calculation is 0 then it means we have finished with our calculation.
                if (response == 0)
                {
                    // Format the printing of the output to console.
                    DisplayCurrentDictionaryValues(strProductCode, ref qty, ref dicFinalPackSplit, ref dicPacksWithRates);

                    // End iteration.
                    break;
                }
                else
                {
                    // If the quantity user entered is not matching the split using the currently available packs, inform the user. 
                    Console.WriteLine($"We are unable to process the order for {strProductCode} with quantity {qty} . Please try another quantity.");
                }
            }

            // If the quantity user entered is not matching the split using the currently available packs, inform the user.
            if (response == -1)
            {
                Console.WriteLine($"We are unable to process the order for {strProductCode} with quantity {qty} . Please try another quantity.");
            }

            // End the process
            Console.WriteLine("---------");
        }

        /// <summary>
        /// Validates user input
        /// </summary>
        /// <param name="input">Input as entered by the user</param>
        /// <param name="isValidInput">Vaild Flag</param>
        /// <param name="inputQuantity">First Part of the input</param>
        /// <param name="inputParts">String array got from splitting user input by whitespaces</param>
        /// <param name="dicPacksWithRates">Dictionary containig the packs and rates</param>
        public static void ValidateInput(ref string input, ref bool isValidInput, ref int inputQuantity, ref string[] inputParts, ref Dictionary<string, Dictionary<int, double>> dicPacksWithRates)
        {
            inputParts = input.Split(' ');

            if (inputParts.Length == 1)
            {
                if (inputParts[0].ToLowerInvariant().Equals("x") || inputParts[0].ToLowerInvariant().Equals("q")
                    || inputParts[0].ToLowerInvariant().Equals("exit") || inputParts[0].ToLowerInvariant().Equals("quit"))
                {
                    Console.WriteLine($"Quitting...");
                    Environment.Exit(0);
                }
            }

            if (inputParts.Length != 2)
            {
                Console.WriteLine($"Invalid Input {input}. Please try again with a valid numeric quantity followed by product code. Eg: 20 BM1");
                isValidInput = false;
            }

            if (isValidInput)
            {
                isValidInput = int.TryParse(inputParts[0], out inputQuantity);
                if (inputParts.Length == 2 && !isValidInput)
                {
                    Console.WriteLine($"Invalid Input {input}. Please try again with a valid numeric quantity. Eg: 20");
                }
            }

            if (isValidInput && !Regex.IsMatch(inputParts[1], @"^[A-Za-z]{2}[0-9]{0,2}$"))
            {
                isValidInput = false;
                Console.WriteLine($"Invalid Input {input}. Please try again with a valid product code. " +
                    $"1st 2 characters alphabets followed by 0 - 2 digit number Eg: XX12, YY, ZZ1");
            }

            if (isValidInput)
            {   
                if (!dicPacksWithRates.ContainsKey(inputParts[1].ToUpper()))
                {
                    isValidInput = false;
                    Console.WriteLine($"Invalid Product Code {inputParts[1]}. Please try again with a valid product code.");
                }
            }
        }

        /// <summary>
        /// Setup the product packs and rates
        /// </summary>
        /// <returns> A Dictionary loaded with packs and rates on offer.</returns>
        public static Dictionary<string, Dictionary<int, double>> DataSetup()
        {
            // Get the pack details.
            int[] packs = { 5, 9, 3, 18 };
            Dictionary<string, Dictionary<int, double>> dicPacksWithRates = new Dictionary<string, Dictionary<int, double>>();
            Dictionary<int, double> dicPackwithRate = new Dictionary<int, double>
                {
                    { 3, 6.99D },
                    { 5, 8.99D }
                };

            dicPacksWithRates.Add("VS5", dicPackwithRate);

            dicPackwithRate = new Dictionary<int, double>
                {
                    { 2, 9.95D },
                    { 5, 16.95D },
                    { 8, 24.95D },
                };

            dicPacksWithRates.Add("MB11", dicPackwithRate);

            dicPackwithRate = new Dictionary<int, double>
                {
                    { 3, 5.95D },
                    { 5, 9.95D },
                    { 9, 16.99D }
                };

            dicPacksWithRates.Add("CF", dicPackwithRate);

            return dicPacksWithRates;
        }

        /// <summary>
        /// Update the packs after the calculation of pack split is completed to make displaying to user friendly.
        /// </summary>
        /// <param name="dicFinalPackSplit"> A SortedDictionary containing the final calculation result.</param>
        private static void UpdatePackDetails(ref SortedDictionary<int, int> dicFinalPackSplit)
        {
            foreach (var item in dicFinalPackSplit.Where(c => c.Value == -1).ToList())
            {
                dicFinalPackSplit[item.Key] = 0;
            }
        }

        /// <summary>
        /// Resets the SortedDictionary whenever a solution can't be arrived at. so that we can re-iterate with different pack combinations.
        /// </summary>
        /// <param name="dicFinalPackSplit"> A SortedDictionary containing the last calculation result.</param>
        /// <param name="prevPackKey"> Previous Pack number to identify the packs less than the previous pack number to reset it to original state.</param>
        private static void ResetPackDetails(ref SortedDictionary<int, int> dicFinalPackSplit, int prevPackKey)
        {
            foreach (var item in dicFinalPackSplit.Where(c => c.Value == 0 && c.Key <= prevPackKey).ToList())
            {
                dicFinalPackSplit[item.Key] = -1;
            }
        }

        /// <summary>
        /// Displays the final result in a user friendly manner
        /// </summary>
        /// <param name="strProductCode"> Product Code which the user picked.</param>
        /// <param name="qty"> Quantity of that product which user has requested.</param>
        /// <param name="dicFinalPackSplit"> A SortedDictionary containing the final calculation result.</param>
        /// <param name="dicPackRates"> A Dictionary loaded with packs and rates on offer.</param>
        private static void DisplayCurrentDictionaryValues(string strProductCode, ref int qty, ref SortedDictionary<int, int> dicFinalPackSplit, ref Dictionary<string, Dictionary<int, double>> dicPackRates)
        {
            Console.WriteLine("-----");
            Console.WriteLine("Purchase Order Items");
            StringBuilder strOutput = new StringBuilder();
            double total = 0D;

            foreach (var item in dicFinalPackSplit.Reverse().Where(a => a.Value != 0))
            {
                total += dicPackRates[strProductCode][item.Key] * item.Value;
                strOutput.Append($" {item.Value} X PACK of {item.Key}s ${dicPackRates[strProductCode][item.Key]}");
                strOutput.AppendLine();
            }

            Console.WriteLine($" {qty} {strProductCode} ${total}");
            Console.WriteLine(strOutput.ToString());
            Console.WriteLine("-----");
            Console.WriteLine($"----TASK COMPLETED----");
        }

        /// <summary>
        /// Method to calculate the number of packs required to satisfy user's need with minimal number of packs to save on shipping space.
        /// </summary>
        /// <param name="stkLastReminder"> A Stack contining all the quantities including the user entered and the multiple remainders during the calculation.</param>
        /// <param name="orderQuantity"> User entered item quantity.</param>
        /// <param name="dicFinalPackSplit"> A SortedDictionary containing the last calculation result.</param>
        /// <returns> An integer noting whether iteration should continue or calculation has ended.</returns>
        private static int CalculatePackSplit(ref Stack<int> stkLastReminder, int orderQuantity, ref SortedDictionary<int, int> dicFinalPackSplit)
        {
            // declare remainder variable and initialize it to -1
            int remainder = -1;
            int topPackKey, topPackQuotient;

            // get the top pack key and quotient
            try
            {
                topPackKey = dicFinalPackSplit.Reverse().FirstOrDefault(c => c.Value == -1).Key;
                topPackQuotient = dicFinalPackSplit.Reverse().FirstOrDefault(c => c.Value == -1).Value;
            }
            catch (NullReferenceException)
            {
                return -2;
            }

            if (topPackKey == 0 && topPackQuotient == 0)
            {
                // we reached a dead end. so try reducing 1 multiplier value from the previous top pack.
                var newMultiplier = dicFinalPackSplit.Reverse().LastOrDefault(c => c.Value != 0).Value;
                var prevPackKey = dicFinalPackSplit.Reverse().LastOrDefault(c => c.Value != 0).Key;

                if (dicFinalPackSplit.ContainsKey(prevPackKey))
                {
                    ResetPackDetails(ref dicFinalPackSplit, prevPackKey);
                    var newQuotent = newMultiplier - 1;

                    dicFinalPackSplit[prevPackKey] = newQuotent;
                    if (newQuotent == 0)
                    {
                        if (stkLastReminder.Count != 0)
                        {
                            orderQuantity = stkLastReminder.Pop();
                        }

                        return CalculatePackSplit(ref stkLastReminder, orderQuantity, ref dicFinalPackSplit);
                    }

                    if (newQuotent > 1)
                    {
                        if (stkLastReminder.Count != 0)
                        {
                            orderQuantity = stkLastReminder.Pop();
                        }

                        orderQuantity = orderQuantity - (prevPackKey * newQuotent);
                        return CalculatePackSplit(ref stkLastReminder, orderQuantity, ref dicFinalPackSplit);
                    }
                }
            }

            // Check if the order quantity is greater than or equal to the pack size
            // And we are dividing the quantity by that pack for the first time.
            if (orderQuantity >= topPackKey && topPackQuotient == -1)
            {
                // If yes, then divide the quantity by the pack size and get the quotient and remainder.
                int quotient = Math.DivRem(orderQuantity, topPackKey, out remainder);

                // Push the current remainder to the remainder stack for future reference
                if (remainder > dicFinalPackSplit.Keys.Min())
                {
                    stkLastReminder.Push(remainder);
                }

                // Update the dictionary with the new quotient for that pack.
                if (topPackKey > 0)
                {
                    dicFinalPackSplit[topPackKey] = quotient;
                }

                // If remainder is zero then finish calculating since the problem is solved.
                if (remainder == 0)
                {
                    // Update all other packs whose multiplier is still -1.
                    UpdatePackDetails(ref dicFinalPackSplit);

                    return remainder;
                }

                // If we already have divided the quantity by the given pack size, 
                // Then reduce the quotient in the dictionary by 1 and recurse the function call with new quantity as the current remainder
                if (dicFinalPackSplit[topPackKey] > -1 || remainder > 0)
                {
                    orderQuantity = remainder;
                    return CalculatePackSplit(ref stkLastReminder, orderQuantity, ref dicFinalPackSplit);
                }
            }
            else
            {
                if (topPackKey > 0)
                {
                    dicFinalPackSplit[topPackKey] = 0;
                    return CalculatePackSplit(ref stkLastReminder, orderQuantity, ref dicFinalPackSplit);
                }
            }

            return remainder;
        }
    }
}
