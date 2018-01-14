//---------------------------------------------------------------------------------
// <copyright file="BasicTests.cs" company="Sample">
//     Copyright (c) 2018 All Rights Reserved
// </copyright>
// <author> Sriram Narayanan </author>
// <date> 01/14/2018 11:39:58 AM </date>
// <summary> Class representing a Unit Test Cases for Bakery Coding Challenge Program </summary>
//---------------------------------------------------------------------------------
namespace BakeryCodingChallenge.Tests
{
    using BakeryCodingChallenge.Core;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Collections.Generic;

    /// <summary>
    /// Basic Unit Test Cases
    /// </summary>
    [TestClass]
    public class BasicTests
    {
        /// <summary>
        /// Check "10 VS5" gives the following output
        /// 2 X Pack of 5
        /// </summary>
        [TestMethod]
        public void Test_VS5_10()
        {
            // Setup
            bool isValidInput = true;
            int inputQuantity = 0;
            string strInput = "10 VS5";
            string[] arrInputParts = null;
            Dictionary<string, Dictionary<int, double>> dicPacksWithRates = null;
            SortedDictionary<int, int> dicFinalPackSplitActual = null;
            SortedDictionary<int, int> dicFinalPackSplitExpected = new SortedDictionary<int, int>();
            dicFinalPackSplitExpected.Add(3, 0);
            dicFinalPackSplitExpected.Add(5, 2);

            dicPacksWithRates = Bakery.DataSetup();

            // Execute Validation
            // Validate the input given by the user.
            Bakery.ValidateInput(ref strInput, ref isValidInput, ref inputQuantity, ref arrInputParts, ref dicPacksWithRates);

            // Assert Validation
            Assert.IsTrue(isValidInput);

            // Execute Order
            if (isValidInput)
            {
                // If input stays valid, then try processing the order.
                Bakery.ProcessOrder(ref inputQuantity, ref arrInputParts, ref dicPacksWithRates, out dicFinalPackSplitActual);
            }

            // Assert Order
            try
            {
                CollectionAssert.AreEqual(dicFinalPackSplitExpected, dicFinalPackSplitActual);
            }
            catch (AssertFailedException)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Check "14 MB11" gives the following output
        /// 1 X Pack of 8
        /// 3 X Pack of 2
        /// </summary>
        [TestMethod]
        public void Test_MB11_14()
        {
            // Setup
            bool isValidInput = true;
            int inputQuantity = 0;
            string strInput = "14 MB11";
            string[] arrInputParts = null;
            Dictionary<string, Dictionary<int, double>> dicPacksWithRates = null;
            SortedDictionary<int, int> dicFinalPackSplitActual = null;
            SortedDictionary<int, int> dicFinalPackSplitExpected = new SortedDictionary<int, int>();
            dicFinalPackSplitExpected.Add(2, 3);
            dicFinalPackSplitExpected.Add(5, 0);
            dicFinalPackSplitExpected.Add(8, 1);

            dicPacksWithRates = Bakery.DataSetup();

            // Execute Validation
            // Validate the input given by the user.
            Bakery.ValidateInput(ref strInput, ref isValidInput, ref inputQuantity, ref arrInputParts, ref dicPacksWithRates);

            // Assert Validation
            Assert.IsTrue(isValidInput);

            // Execute Order
            if (isValidInput)
            {
                // If input stays valid, then try processing the order.
                Bakery.ProcessOrder(ref inputQuantity, ref arrInputParts, ref dicPacksWithRates, out dicFinalPackSplitActual);
            }

            // Assert Order
            try
            {
                CollectionAssert.AreEqual(dicFinalPackSplitExpected, dicFinalPackSplitActual);
            }
            catch (AssertFailedException)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// Check "13 CF" gives the following output
        /// 1 X Pack of 9
        /// 2 X Pack of 5
        /// </summary>
        [TestMethod]
        public void Test_CF_13()
        {
            // Setup
            bool isValidInput = true;
            int inputQuantity = 0;
            string strInput = "13 CF";
            string[] arrInputParts = null;
            Dictionary<string, Dictionary<int, double>> dicPacksWithRates = null;
            SortedDictionary<int, int> dicFinalPackSplitActual = null;
            SortedDictionary<int, int> dicFinalPackSplitExpected = new SortedDictionary<int, int>();
            dicFinalPackSplitExpected.Add(3, 1);
            dicFinalPackSplitExpected.Add(5, 2);
            dicFinalPackSplitExpected.Add(9, 0);

            dicPacksWithRates = Bakery.DataSetup();

            // Execute Validation
            // Validate the input given by the user.
            Bakery.ValidateInput(ref strInput, ref isValidInput, ref inputQuantity, ref arrInputParts, ref dicPacksWithRates);

            // Assert Validation
            Assert.IsTrue(isValidInput);

            // Execute Order
            if (isValidInput)
            {
                // If input stays valid, then try processing the order.
                Bakery.ProcessOrder(ref inputQuantity, ref arrInputParts, ref dicPacksWithRates, out dicFinalPackSplitActual);
            }

            // Assert Order
            try
            {
                CollectionAssert.AreEqual(dicFinalPackSplitExpected, dicFinalPackSplitActual);
            }
            catch (AssertFailedException)
            {
                Assert.Fail("Oops. Outputs Dont Match.");
            }
        }
    }
}
