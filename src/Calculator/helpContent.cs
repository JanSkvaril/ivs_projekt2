/********************************************************************
 * Project name: ivs_projekt2
 * File: HelpContent.cs
 * Author: Erik Báča xbacae00@fit.vutbr.cz
 * ******************************************************************/
/**
 * @file HelpContent.cs
 * @brief content of Help window object
 * @author Erik Báča xbacae00@fit.vutbr.cz
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
	/**
	* Class containing object for creating the the content of Help window
	*/
	class HelpContent
	{

		/**
		* @brief content properties
		*/
		public string Title { get; set; }
		public string Description { get; set; }
		public string[] Case { get; set; }

		/**
		* @brief static instance of the Root description
		*/
		public static HelpContent Root = new HelpContent()
		{
			Title = "Nth Root",
			Description = "Nth root of a number x: ",
			Case = new string[] { "n√y", "(n√y)", "n√(y)", "-(n√y)" }
		};

		/**
		* @brief static instance of the Exponentation description
		*/
		public static HelpContent Exponentation = new HelpContent()
		{
			Title = "Exponentation",
			Description = "Exponentation of a number x: ",
			Case = new string[] { "x^n", "(-x)^(-n)", "(x^y)", "(-x^y)" }
		};

		/**
		* @brief static instance of the Logarithm description
		*/
		public static HelpContent Logarithm = new HelpContent()
		{
			Title = "Natural Logarithm",
			Description = "Natural logarithm of a number x: ",
			Case = new string[] { "lnx", "ln(x)", "-(lnx)", "-ln(x)" }
		};

		/**
		* @brief static instance of the Factorial description
		*/
		public static HelpContent Factorial = new HelpContent()
		{
			Title = "Factorial",
			Description = "Factorial of a number n: ",
			Case = new string[] { "x!", "(x)!", "", "" }
		};

		/**
		* @brief static instance of the Negation description
		*/
		public static HelpContent Negation = new HelpContent()
		{
			Title = "Negation",
			Description = "Negation of a number x",
			Case = new string[] { "x=>-(x)", "-(x)=>x", "", "" }
		};
	}
}
