/********************************************************************
 * 
 * Project name: ivs_projekt2
 * File: Program.cs
 * Author: Jakub Novotný xnovot2a@fit.vutbr.cz
 * 
 * ******************************************************************/

using System;
using System.Linq;
using MatbLibrary;


namespace StandardDeviation
{
    class Program
    {

        public static double[] numbers = NumberRead();


        public static double[] NumberRead()
        {

            string line;
            double[] num;
            string[] split;
            string stringNum = "";


            try
            {
                while ((line = Console.ReadLine()) != null && line != "")
                {
                    stringNum = stringNum + line;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }


            try
            {
                split = stringNum.Split();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }


            try
            {
                num = split.Select(double.Parse).ToArray();

                return num;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

        }


        public static double StandardDeviation(double[] vari)
        {
            // variables, numbers - count
            double arrayCount = vari.GetLength(0) == 1 ? throw new ArgumentException("Error: count 1") : vari.GetLength(0);

            double average = 0;
            double add = 0;
            double var = 0;
            double summary = 0;

            try
            {
                // input values - sum up
                foreach (double item in vari)
                {
                    double add2 = Functions.Add(add, item);
                    add = add2;
                }

                // average
                average = Functions.Div(add, arrayCount);

                double variable = 0;
                foreach (double items in vari)
                {
                    var = Functions.Sub(items, average);
                    var = Functions.Mul(var, var);
                    variable = variable + var;
                }
                var = variable;
                var = Functions.Div(var, (arrayCount - 1));
                summary = Math.Sqrt(var);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return summary;
        }



        static void Main(string[] args)
        {
            Console.WriteLine("Result: " + StandardDeviation(numbers));
        }
    }
}
