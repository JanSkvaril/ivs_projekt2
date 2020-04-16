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
	/// <summary>
	/// Interakční logika pro MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private bool solved = false;
		private bool pressedEnter = false;
		private bool darkMode = true;
		public bool DarkMode
		{
			get { return darkMode; }
			set
			{
				darkMode = value;
				if (darkMode)
					Setmode(Mode.DarkMode);
				else
					Setmode(Mode.LightMode);
			}
		}


		public MainWindow()
		{
			InitializeComponent();
			Setmode(Mode.DarkMode);
			FocusManager.SetFocusedElement(this, numberTextBox);
		}

		private void settingsButton_Click(object sender, RoutedEventArgs e)
		{
			Settings settings = new Settings();
			settings.ShowDialog();
		}

		private void Setmode(Mode mode)
		{
			App.Current.Resources["MainColor"] = (Color)ColorConverter.ConvertFromString(mode.MainColor);
			App.Current.Resources["MinorColor"] = (Color)ColorConverter.ConvertFromString(mode.MinorColor);
			this.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(mode.Background);
			App.Current.Resources["Foreground"] = mode.Foreground;
			numberTextBox.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(mode.NumberBackground);
			numberTextBox.Foreground = mode.NumberForeground;
			topBar.Background = mode.Topbar;
		}

		private void closeButton_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void topBar_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.ChangedButton == MouseButton.Left)
				this.DragMove();
		}

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

		private bool Validate(string text)
		{
			Regex regex = new Regex(@"(?:[0-9])|(?:[\(\)\.\+\-\*\/\√\'ln'\^\!])");
			return regex.IsMatch(text) ? false : true;
		}
		private void numberSign_Click(object sender, RoutedEventArgs e)
		{
			if (solved && !pressedEnter)
			{
				numberTextBox.Clear();
				solved = false;
			}
			Insert((sender as Button).Content.ToString());
		}

		private void Insert(string insert)
		{
			int caretPosition = numberTextBox.CaretIndex + insert.Length;
			if (!Validate(numberTextBox.Text + insert))
				numberTextBox.Text = numberTextBox.Text.Insert(numberTextBox.CaretIndex, insert);
			FocusManager.SetFocusedElement(this, numberTextBox);
			numberTextBox.CaretIndex = caretPosition;
		}

		private void deleteAllButton_Click(object sender, RoutedEventArgs e)
		{
			numberTextBox.Clear();
			FocusManager.SetFocusedElement(this, numberTextBox);
		}

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

		private void equalsButton_Click(object sender, RoutedEventArgs e)
		{
			Solve();
		}

		private void Solve()
		{
			string problem = numberTextBox.Text;
			double result = Parser.Solve(problem);
			numberTextBox.Text = double.IsNaN(result) ? "error" : result.ToString();
			solved = true;
			FocusManager.SetFocusedElement(this, numberTextBox);
		}

		private void numberTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
                case Key.Enter:
					Solve();
					pressedEnter = true;
                    break;
				case Key.I:
					Settings settings = new Settings();
					settings.ShowDialog();
					break;
				case Key.C:
					numberTextBox.Clear();
					FocusManager.SetFocusedElement(this, numberTextBox);
					break;
			}
		}

		private void modeButton_Click(object sender, RoutedEventArgs e)
		{
			DarkMode = !DarkMode;
			modeButtonText.Text = darkMode ? "Dark" : "Light";
		}

		private void negationButton_Click(object sender, RoutedEventArgs e)
		{
			string s = numberTextBox.Text;
			if (numberTextBox.Text.StartsWith("-(") && numberTextBox.Text.EndsWith(")"))
			{
				s = s.Substring(0, s.Length - 1);
				s = s.Remove(0,2);
			} else
			{
				s = "-(" + s + ")";
			}
			numberTextBox.Text = s;
			FocusManager.SetFocusedElement(this, numberTextBox);
			numberTextBox.CaretIndex = s.Length;
		}
	}
}
