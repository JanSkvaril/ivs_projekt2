/********************************************************************
 * Project name: ivs_projekt2
 * File: Help.xaml.cs
 * Author: Erik Báča xbacae00@fit.vutbr.cz
 * ******************************************************************/
/**
 * @file Help.xaml.cs
 * @brief Help window class
 * @author Erik Báča xbacae00@fit.vutbr.cz
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Calculator
{
	/**
     * Class containing interaction logic of the Help window
     */
	public partial class HelpWindow : Window
	{
		//dictionary of math functions with description
		Dictionary<string, HelpContent> helpDictionary = new Dictionary<string, HelpContent>();

		/**
		* @brief constructor for initializing components and filling dictionary
		*/
		public HelpWindow()
		{
			InitializeComponent();
			fillHelpDictionary();
		}

		/**
		* @brief filling the dictionary
		*/
		private void fillHelpDictionary()
		{
			helpDictionary.Add("√", HelpContent.Root);
			helpDictionary.Add("^", HelpContent.Exponentation);
			helpDictionary.Add("ln", HelpContent.Logarithm);
			helpDictionary.Add("!", HelpContent.Factorial);
			helpDictionary.Add("+/-", HelpContent.Negation);
		}

		/**
		* @brief event for ability to change the position of the Help window
		*/
		private void topBar_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
				this.DragMove();
		}

		/**
		* @brief event for starting of closing animation
		*/
		private void closeButton_Click(object sender, RoutedEventArgs e)
		{
			helpWindow.MinHeight = 0;
			DoubleAnimation closingAnimation = new DoubleAnimation()
			{
				From = helpWindow.ActualHeight,
				To = 0,
				Duration = new Duration(TimeSpan.FromSeconds(1))
			};
			closingAnimation.Completed += closingAnimation_Completed;
			this.BeginAnimation(Window.HeightProperty, closingAnimation);
		}

		/**
		* @brief event for closing the Help window
		*/
		private void closingAnimation_Completed(object sender, EventArgs e)
		{
			this.Close();
		}

		/**
		* @brief event for setting the contet of the Help window
		*/
		private void functionButton_Click(object sender, RoutedEventArgs e)
		{
			string caseText = (sender as Button).Content.ToString();
			HelpContent ActualHelpContent = helpDictionary[caseText];
			fillLabels(ActualHelpContent);
		}

		/**
		* @brief filling (setting the contents) the labels with needed content
		* @param mode needed content
		*/
		private void fillLabels(HelpContent ActualHelpContent)
		{
			titleLabel.Content = ActualHelpContent.Title;
			descriptionLabel.Content = ActualHelpContent.Description;
			caseOneLabel.Content = ActualHelpContent.Case[0];
			caseTwoLabel.Content = ActualHelpContent.Case[1];
			caseThreeLabel.Content = ActualHelpContent.Case[2];
			caseFourLabel.Content = ActualHelpContent.Case[3];
		}

		/**
		* @brief event for setting minimal window height after opening animation ends
		*/
		private void DoubleAnimation_Completed(object sender, EventArgs e)
		{
			helpWindow.MinHeight = 500;
		}
	}
}
