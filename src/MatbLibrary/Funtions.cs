/********************************************************************
 * Project name: ivs_projekt2
 * File: Functions.cs
 * Author: Michal Zavadil xzavad18@fit.vutbr.cz
 * ******************************************************************/
/**
 * @file Functions.cs
 * @brief class with math library functions
 * @author Michal Zavadil xzavad18@fit.vutbr.cz
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace MatbLibrary
{
    /**
     * Class containing math library functions
     */
    public static class Functions
    {
        /**
         * @brief addition
         * @param x first operand
         * @param y second operand
         * @return sum of 2 numbers
         */
        public static double Add(double x, double y)
        { 
            return x + y;
        }
        /**
         * @brief subtraction
         * @param x first operand
         * @param y second operand
         * @return difference of 2 numbers
         */
        public static double Sub(double x, double y)
        {
            return x - y;
        }
        /**
         * @brief multiplication
         * @param x first operand
         * @param y second operand
         * @return product of 2 numbers
         */
        public static double Mul(double x, double y)
        {
            return x * y;
        }
        /**
         * @brief division
         * @param x first operand
         * @param y second operand
         * @return quotient of 2 numbers
         * @throw DivideByZeroException if second operand is 0
         */
        public static double Div(double x, double y)
        {
            if (y == 0)
                throw new DivideByZeroException("Error: cannot divide by zero");
            return x / y;
        }
        /**
         * @brief factorial
         * @param x number whose factorial we want to find
         * @return factorial of x
         * @throw ArgumentException if x is negative or not a whole number
         */
        public static double Fact(double x)
        {
            if (x < 0 || x % 1 != 0)
                throw new ArgumentException("Error: invalid argument");
            if (x == 0)
                return 1;
            double sum;
            for (sum = 1; x > 0; x--)
            {
                sum *= x;
            }
            return sum;
        }
        /**
         * @brief x to the power of y
         * @param x base
         * @param y exponent
         * @return x to the power of y
         * @throw ArgumentException if y is not a whole number
         */
        public static double Exp(double x, double y)
        {
            if (y % 1 != 0)
                throw new ArgumentException("Error: invalid argument");
            if (y == 0)
                return 1;
            bool isNegative;
            if (y < 0)
            {
                isNegative = true;
                y = Math.Abs(y);
            }
            else
                isNegative = false;
            double sum = 1;
            for(; y > 0; y--)
            {
                sum *= x;
            }
            if (isNegative == true)
                sum = 1 / sum;
            return sum;
        }
        /**
         * Xth root of y using Newton's method.
         * @brief xth root of y
         * @param x root degree
         * @param y number whose root we want to find 
         * @return xth root of y
         * @throw ArgumentException if x is 0
         * @throw ArgumentException if x is not a whole number
         * @throw ArgumentException if y is negative and x is odd
         */
        public static double Root(double x, double y)
        {
            if (x == 0 || x % 1 != 0 || (y < 0 && x % 2 == 0))
                throw new ArgumentException("Error: invalid argument");

            Random rand = new Random();
            double xPrev = rand.Next(9) + 1;
            double diff = int.MaxValue;
            double xNext = 0.0;
            while (diff > 1e-12)
            {
                xNext = ((x - 1.0) * xPrev + y / Functions.Exp(xPrev, x - 1)) / x;
                diff = Math.Abs(xNext - xPrev);
                xPrev = xNext;
            }
            
            return xNext;
        }
        /**
         * Natural logarithm evaluation using Talor series.
         * @brief natural logarithm (base equals e)
         * @param x power, e^a=x
         * @return exponent 
         * @throw ArgumentException if x is not positive
         */
        public static double NatLog(double x)
        {
            if (x <= 0)
                throw new ArgumentException("Error: invalid argument");
            double sum = 0.0;
            double term_0 = (x - 1) / (x + 1);
            double term = term_0;
            double tmp = term;
            double divBy = 1.0;
            while (Math.Abs(tmp) > 1e-12)
            {
                sum += tmp;
                term *= term_0 * term_0;
                divBy += 2;
                tmp = term / divBy;
            }
            return (2 * sum);
        }

    }
}
/*** End of file Functions.cs ***/
