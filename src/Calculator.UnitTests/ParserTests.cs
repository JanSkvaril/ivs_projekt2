/********************************************************************
 * Project name: ivs_projekt2
 * File: ParserTests.cs
 * Author: Jan Skvaril xskvar09@fit.vutbr.cz
 * ******************************************************************/
/**
 * @file ParserTests.cs
 * @brief Contains tests for Parser class
 * @author Jan Skvaril xskvar09@fit.vutbr.cz
 */



using System;
using MatbLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Calculator.UnitTests
{
    [TestClass]
    public class ParserTests
    {
        /**
        * Tests Parser.Validate with strings that should be valid
        */
        [TestMethod]
        public void Validation_RightInput()
        {
            Assert.IsTrue(Parser.Validate("(5/3)*2"));
            Assert.IsTrue(Parser.Validate("-5+3*-10-2"));
            Assert.IsTrue(Parser.Validate("(-5+3)*-10-2"));

            Assert.IsTrue(Parser.Validate("(-5+3)*+10√2"));
            Assert.IsTrue(Parser.Validate("(-5+3)*-10^2"));

            Assert.IsTrue(Parser.Validate("(-5+3)*-(2!)"));


        }
        /**
        * Tests Parser.Validate with strings that should not be valid
        */
        [TestMethod]
        public void Validation_WrongInput()
        {

            Assert.IsFalse(Parser.Validate("(-5)!"));
            Assert.IsFalse(Parser.Validate("2√-2"));
            Assert.IsFalse(Parser.Validate("5*!"));
            Assert.IsFalse(Parser.Validate("5-"));
            Assert.IsFalse(Parser.Validate("5^"));
            Assert.IsFalse(Parser.Validate("^5"));
            Assert.IsFalse(Parser.Validate("5-^5"));
            Assert.IsFalse(Parser.Validate("(-5+3)*-2!"));
            Assert.IsFalse(Parser.Validate("5.2!"));
        }
        /**
        * Tests for simple operations
        */
        [TestMethod]
        public void SolveOperations()
        {
            Assert.AreEqual(Parser.Solve("5.2"), 5.2);

            Assert.AreEqual(Parser.Solve("5!"), 120);
            Assert.AreEqual(Parser.Solve("5+3"), 8);
            Assert.AreEqual(Parser.Solve("5-3"), 2);
            Assert.AreEqual(Parser.Solve("-5-3"), -8);
            //Assert.AreEqual(Parser.Solve("--5+3"), 8); - this will work, but it is not recomended to use


            Assert.AreEqual(Parser.Solve("5*3"), 15);
            Assert.AreEqual(Parser.Solve("5*-3"), -15);
            Assert.AreEqual(Parser.Solve("-5*-3"), 15);
            Assert.AreEqual(Parser.Solve("10/2"), 5);
            Assert.AreEqual(Parser.Solve("10/-2"), -5);

            Assert.AreEqual(Parser.Solve("5.2+3"), 8.2);
            Assert.AreEqual(Parser.Solve("1/2"), 0.5);

            Assert.AreEqual(Parser.Solve("2^2"), 4);
            Assert.AreEqual(Parser.Solve("-(2^2)"), -4);

            Assert.AreEqual(Parser.Solve("2√9"), 3);
            Assert.AreEqual((int)Parser.Solve("ln2.8"), 1);

        }
        /**
        * Tests for operation priorities
        */
        [TestMethod]
        public void SolveOperationOrder()
        {
            Assert.AreEqual(Parser.Solve("5!*2"), 240);

            Assert.AreEqual(Parser.Solve("3-5*2"), -7);
            Assert.AreEqual(Parser.Solve("3-5*-2"), 13);
            Assert.AreEqual(Parser.Solve("(3-5)*2"), -4);
            Assert.AreEqual(Parser.Solve("-5+3*-10-2"), -37);


            Assert.AreEqual(Parser.Solve("3-5^2*2"), 53);

        }
        /**
        * Tests for solving brackets
        */
        [TestMethod]
        public void SolveBrackets()
        {
            Assert.AreEqual(Parser.Solve("-((3-5)*-2)*2"), -8);
            Assert.AreEqual(Parser.Solve("-(3-5*-2)*(2-5)"), 39);
            Assert.AreEqual(Parser.Solve("-((3-5*-2)*(2-5))*(5*3+2)"), 663);
        }
    }
}
