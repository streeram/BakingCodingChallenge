//---------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Sample">
//     Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author> Sriram Narayanan </author>
// <date> 01/14/2018 11:39:58 AM </date>
// <summary> Class representing an entry point for Bakery Coding Challenge Program execution.</summary>
//---------------------------------------------------------------------------------
namespace BakeryCodingChallenge
{
    using System;
    using System.Collections.Generic;

    using BakeryCodingChallenge.Core;

    /// <summary>
    /// Bakery Coding Challenge Program Class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main Program - Entry Point
        /// </summary>
        /// <param name="args">Custom arguments passed while executing the exe.</param>
        public static void Main(string[] args)
        {
            // Initialise variables
            bool isValidInput = true;
            int inputQuantity = 0;
            string strInput = null;
            string[] arrInputParts = null;
            Dictionary<string, Dictionary<int, double>> dicPacksWithRates = null;
            SortedDictionary<int, int> dicFinalPackSplit = null;
            try
            {
                // Get the pack rates from data source.
                dicPacksWithRates = Bakery.DataSetup();
            }
            catch (Exception ex)
            {
                // Log the exception that data is missing.
                throw ex;
            }

            try
            {
                do
                {
                    // Reset the valid flag to true.
                    isValidInput = true;

                    // Get the user input.
                    Console.Write("Enter Order Item Required : ");
                    strInput = Console.ReadLine();
                   
                    // Validate the input given by the user.
                    Bakery.ValidateInput(ref strInput, ref isValidInput, ref inputQuantity, ref arrInputParts, ref dicPacksWithRates);

                    if (isValidInput)
                    {
                        // If input stays valid, then try processing the order.
                        Bakery.ProcessOrder(ref inputQuantity, ref arrInputParts, ref dicPacksWithRates, out dicFinalPackSplit);
                    }

                    // Give a lines space between two user inputs.
                    Console.WriteLine();
                }
                while (!HasProgramEnded(ref strInput));
            }
            catch (Exception ex)
            {
                // Log the runtime excepion and handle it.
                throw ex;
            }

            // Show exit message.
            Console.WriteLine($"Quitting...");
        }

        /// <summary>
        /// Checks if the user has provided instructions to quit the program
        /// </summary>
        /// <param name="input">user input</param>
        /// <returns>a boolean indicating whether user asked to quit or not.</returns>
        private static bool HasProgramEnded(ref string input)
        {
            bool isEnd = false;

            if (input.ToLowerInvariant().StartsWith("x") || input.ToLowerInvariant().StartsWith("q") 
                || input.ToLowerInvariant().StartsWith("exit") || input.ToLowerInvariant().StartsWith("quit"))
            {
                isEnd = true;
            }

            return isEnd;
        }
    }
}
