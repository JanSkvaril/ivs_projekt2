using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace MatbLibrary
{

    public static class Parser
    {
        /**
        * Class for handling binary operations like plus, multiply, etc.
        */
        class Operation
        {
            //TODO: operations should have priority number, that would simbolize 

            /**
            * @brief Constructor for binary operator
            * @param _regex that matches given operator, should containt x in group 1 and y in group 2
            * @param _func function from Math library that will be called for given operator
            */
            public Operation(string _regex, Func<double, double, double> _func)
            {
                regex = new Regex(_regex);
                binFunc = _func;
                type = Operation_type.Binary;
            }
            /**
            * @brief Constructor for binary operator
            * @param _regex that matches given operator, should containt x in group 1
            * @param _func function from Math library that will be called for given operator
            */
            public Operation(string _regex, Func<double, double> _func)
            {
                regex = new Regex(_regex);
                unFunc = _func;
                type = Operation_type.Unary;
            }

            public Operation_type type;

            //regex patern of operation
            public Regex regex;
            //function for the operation from math library
            //binary function (two parameters)
            public Func<double, double, double> binFunc;
            //unary function (one parametr)
            public Func<double, double> unFunc;
        }
        //All operations supported
        enum Operations
        {
            Plus,
            Minus,
            Mul,
            Div,
            Pow,
            Sqrt,
            Fac,
            NLog,
        }
        //All types of operators
        enum Operation_type
        {
            Binary, //takes two parameters
            Unary, // takes only one paramer
        }
        //Dictionary of all binary operations and their matching object
        static Dictionary<Operations, Operation> operation_list;
        static Parser()
        {
            //Patter for double number
            string digit_regex = @"((?:\-|\+)?\d+(?:(?:\.|\,)?\d*)?)";

            operation_list = new Dictionary<Operations, Operation>();
            //unary operations
            operation_list[Operations.Fac] =
                new Operation(digit_regex + @"\!", Functions.Fact);
            operation_list[Operations.NLog] =
                new Operation(@"ln" + digit_regex, Functions.NatLog);

            //binary operations
            operation_list[Operations.Plus] =
                new Operation(digit_regex + @"\+" + digit_regex, Functions.Add);
            operation_list[Operations.Minus] =
                new Operation(digit_regex + @"\-" + digit_regex, Functions.Sub);
            operation_list[Operations.Mul] =
                new Operation(digit_regex + @"\*" + digit_regex, Functions.Mul);
            operation_list[Operations.Div] =
                new Operation(digit_regex + @"\/" + digit_regex, Functions.Div);
            operation_list[Operations.Pow] =
                new Operation(digit_regex + @"\^" + digit_regex, Functions.Exp);
            operation_list[Operations.Sqrt] =
                new Operation(digit_regex + @"\√" + digit_regex, Functions.Root);
        }

        /**
         * @brief Parses input string to separete calculations and solves them using the Math library
         * @param input Validated string to calculate, viz docs
         * @return Returns result of the given calculation or NaN if it fails
         */
        public static double Solve(string input)
        {
            input = CorrectFormat(input);
            try
            {
                input = SolveBrackets(input);
                input = SolveOperation(input, Operations.NLog);
                input = SolveOperation(input, Operations.Fac);
                input = SolveOperation(input, Operations.Pow);
                input = SolveOperation(input, Operations.Sqrt);
                input = SolveOperation(input, Operations.Mul);
                input = SolveOperation(input, Operations.Div);
                input = SolveOperation(input, Operations.Plus);
                input = SolveOperation(input, Operations.Minus);
            }
            catch (Exception)
            {
                return double.NaN;
            }

            input = CorrectFormat(input);
            input = input.Replace(',', '.'); //for correct parsing
            double result;
            if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
                return double.NaN;
            else return result;
        }
        /**
         * @brief Validates if input string is in corrent format
         * @param input String to validate
         * @return Retruns true if string is valide, false if not
         */
        public static bool Validate(string input)
        {
            double result;
            try
            {
                result = Solve(input);
            }
            catch (Exception)
            {

                return false;
            }
            if (double.IsNaN(result))
                return false;
            else return true;

        }

        /**
         * @brief Will handle multiple signs next to each other, e.g. -- will be replaced with +
         * @param input string to be correcterd
         * @return string with only singular signs
         */
        static string CorrectFormat(string input)
        {

            string replacement = input;
            do
            {
                input = replacement;
                replacement = replacement.Replace("--", "+");
                replacement = replacement.Replace("++", "+");
                replacement = replacement.Replace("-+", "-");
                replacement = replacement.Replace("+-", "-");
            } while (replacement != input);
            return replacement;
        }
        /**
         * @brief Finds all apearences of specified operation and replaces then with result
         * @param input Input string
         * @param oper Operation to be replaced
         * @return string with operation replaced with results
         */
        static string SolveOperation(string input, Operations oper)
        {

            MatchCollection matches;
            do
            {
                matches = operation_list[oper].regex.Matches(input);
                foreach (Match match in matches)
                {
                    double result = double.NaN;
                    if (operation_list[oper].type == Operation_type.Unary)
                    {
                        //regex pattern is written so x  group 1
                        double x = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
                        //uses function from the math library to calculate result
                        result = operation_list[oper].unFunc(x);
                    }
                    else if (operation_list[oper].type == Operation_type.Binary)
                    {
                        //regex pattern is written so x and y is always in group 1 and 2
                        double x = double.Parse(match.Groups[1].Value, CultureInfo.InvariantCulture);
                        double y = double.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);
                        //uses function from the math library to calculate result
                        result = operation_list[oper].binFunc(x, y);
                    }
                    string replacement = result.ToString();
                    //this is to fix issue with calculations like 
                    //3-5*-2, which would result into 300, instead of correct 3+10
                    if (result >= 0) replacement = "+" + replacement;

                    //replaces regex match with result of that operation
                    input = input.Replace(match.Value, replacement);
                }
                input = CorrectFormat(input);
            } while (matches.Count != 0);
            return input;
        }
        /**
         * @brief Solves all brackets using Solve() method and replaces them with result
         * @param Input string
         * @return string with all brackets replaced with Solve() output
         */
        static string SolveBrackets(string input)
        {
            //regex for matching brackets: (number|operation) 
            //it will match deeper brackets first, e.g. ((5+2)+6) would first match (5+2)
            //content of the bracket will be in group 1
            Regex regex = new Regex(@"\(((?:[0-9]|\+|ln|\!|\-|\√|\^|\*|\/|\.|\,)*)\)");
            MatchCollection matches;
            do
            {
                matches = regex.Matches(input);
                foreach (Match match in matches)
                {
                    //this will solve content of the bracket
                    double result = Solve(match.Groups[1].Value);
                    //replaces bracket with result
                    input = input.Replace(match.Value, result.ToString());
                }
                input = CorrectFormat(input);
            } while (matches.Count != 0);
            return input;
        }

    }
}
