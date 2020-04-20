/********************************************************************
 * Project name: ivs_projekt2
 * File: MainWindow.xaml.cs
 * Author: Erik Báča xbacae00@fit.vutbr.cz
 * ******************************************************************/
/**
 * @file MainWindow.xaml.cs
 * @brief Main window class
 * @author Erik Báča xbacae00@fit.vutbr.cz
 */

using MatbLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
	/**
     * Class containing interaction logic of the Main window
     */
	public partial class MainWindow : Window
	{
		private bool solved = false; // for clearing the textbox before next math problem is entered
		private bool pressedEnter = false; // for not cleaning the textbox right after enter click
		private bool darkMode = true;
		public bool DarkMode // property for changing of color mode
		{
			get { return darkMode; }
			set
			{
				darkMode = value;
				if (darkMode)
					SetMode(Mode.DarkMode);
				else
					SetMode(Mode.LightMode);
			}
		}

		/**
		* @brief constructor for initializing components, setting the default mode and focused element
		*/
		public MainWindow()
		{
			InitializeComponent();
			SetMode(Mode.DarkMode); //default mode
			FocusManager.SetFocusedElement(this, numberTextBox);
		}

		/**
		* @brief event for opening the Help window
		*/
		private void helpButton_Click(object sender, RoutedEventArgs e)
		{
			HelpWindow help = new HelpWindow();
			help.ShowDialog();
		}

		/**
		* @brief setting of the color mode of calculator
		* @param mode to set
		*/
		private void SetMode(Mode mode)
		{
			App.Current.Resources["MainColor"] = (Color)ColorConverter.ConvertFromString(mode.MainColor); 
			App.Current.Resources["MinorColor"] = (Color)ColorConverter.ConvertFromString(mode.MinorColor);
			this.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(mode.Background);
			App.Current.Resources["Foreground"] = mode.Foreground;
			numberTextBox.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(mode.NumberBackground);
			numberTextBox.Foreground = mode.NumberForeground;
			topBar.Background = mode.Topbar;
		}

		/**
		* @brief event for closing the calculator
		*/
		private void closeButton_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
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
		* @brief event for validating text input from user before inserting to textbox
		*/
		private void numberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if (solved && !pressedEnter)
			{
				numberTextBox.Clear();
				solved = false;
			}
			e.Handled = Validate(e.Text);
			pressedEnter = false;
		}

		/**
		* @brief simple validating of user input
		* @param text input from user
		* @return true if is text valid false if not
		*/
		private bool Validate(string text)
		{
			Regex regex = new Regex(@"(?:[0-9])|(?:[\(\)\.\+\-\*\/\√\'ln'\^\!])");
			return regex.IsMatch(text) ? false : true;
		}

		/**
		* @brief event for inserting the string into the main textbox
		*/
		private void numberSign_Click(object sender, RoutedEventArgs e)
		{
			if (solved && !pressedEnter)
			{
				numberTextBox.Clear();
				solved = false;
			}
			Insert((sender as Button).Content.ToString());
		}

		/**
		* @brief inserting of the string into the main textbox
		* @param text to insert
		*/
		private void Insert(string insert)
		{
			int caretPosition = numberTextBox.CaretIndex + insert.Length;
			if (!Validate(numberTextBox.Text + insert))
				numberTextBox.Text = numberTextBox.Text.Insert(numberTextBox.CaretIndex, insert);
			FocusManager.SetFocusedElement(this, numberTextBox);
			numberTextBox.CaretIndex = caretPosition;
		}

		/**
		* @brief event for clearing the textbox
		*/
		private void deleteAllButton_Click(object sender, RoutedEventArgs e)
		{
			numberTextBox.Clear();
			FocusManager.SetFocusedElement(this, numberTextBox);
		}

		/**
		* @brief event for deleting char in the main textbox
		*/
		private void deleteButton_Click(object sender, RoutedEventArgs e)
		{
			int caretPosition = numberTextBox.CaretIndex - 1;
			if (caretPosition >= 0)
				numberTextBox.Text = numberTextBox.Text.Remove(caretPosition, 1);
			else
				caretPosition = 0;
			FocusManager.SetFocusedElement(this, numberTextBox);
			numberTextBox.CaretIndex = caretPosition;

		}

		/**
		* @brief event for solving the math problem
		*/
		private void equalsButton_Click(object sender, RoutedEventArgs e)
		{
			Solve();
		}

		/**
		* @brief solving the math problem	
		*/
		private void Solve()
		{
			string problem = numberTextBox.Text;
			double result = Parser.Solve(problem);
			numberTextBox.Text = double.IsNaN(result) ? "error" : result.ToString().Replace(',','.');
			solved = true;
			FocusManager.SetFocusedElement(this, numberTextBox);
		}

		/**
		* @brief event for managing the shortcuts
		*/
		private void numberTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Enter:
					Solve();
					pressedEnter = true;
					break;
				case Key.I:
					HelpWindow settings = new HelpWindow();
					settings.ShowDialog();
					break;
				case Key.C:
					numberTextBox.Clear();
					FocusManager.SetFocusedElement(this, numberTextBox);
					break;
			}
		}

		/**
		* @brief event for changing the color mode
		*/
		private void modeButton_Click(object sender, RoutedEventArgs e)
		{
			DarkMode = !DarkMode;
			modeButtonText.Text = darkMode ? "Dark" : "Light";
		}

		/**
		* @brief event for negation of the math problem
		*/
		private void negationButton_Click(object sender, RoutedEventArgs e)
		{
			string s = numberTextBox.Text;
			if (numberTextBox.Text.StartsWith("-(") && numberTextBox.Text.EndsWith(")"))
			{
				s = s.Substring(0, s.Length - 1);
				s = s.Remove(0, 2);
			}
			else
			{
				s = "-(" + s + ")";
			}
			numberTextBox.Text = s;
			FocusManager.SetFocusedElement(this, numberTextBox);
			numberTextBox.CaretIndex = s.Length;
		}
	}
}
