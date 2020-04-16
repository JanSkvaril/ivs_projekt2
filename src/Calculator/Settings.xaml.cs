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
	/// <summary>
	/// Interakční logika pro Settings.xaml
	/// </summary>
	public partial class Settings : Window
	{
		Dictionary<string, HelpContent> helpDictionary = new Dictionary<string, HelpContent>();
		public Settings()
		{
			InitializeComponent();
			fillHelpDictionary();
		}

		private void fillHelpDictionary()
		{
			helpDictionary.Add("√", HelpContent.Root);
			helpDictionary.Add("^", HelpContent.Exponentation);
			helpDictionary.Add("ln", HelpContent.Logarithm);
			helpDictionary.Add("!", HelpContent.Factorial);
			helpDictionary.Add("+/-", HelpContent.Negation);
		}

		private void topBar_MouseDown(object sender, MouseButtonEventArgs e)
		{
			
			if (e.ChangedButton == MouseButton.Left)
				this.DragMove();
		}

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

		private void closingAnimation_Completed(object sender, EventArgs e)
		{
			this.Close();
		}

		private void functionButton_Click(object sender, RoutedEventArgs e)
		{
			string caseText = (sender as Button).Content.ToString();
			HelpContent ActualHelpContent = helpDictionary[caseText];
			fillLabels(ActualHelpContent);
		}

		private void fillLabels(HelpContent ActualHelpContent)
		{
			titleLabel.Content = ActualHelpContent.Title;
			descriptionLabel.Content = ActualHelpContent.Description;
			caseOneLabel.Content = ActualHelpContent.Case[0];
			caseTwoLabel.Content = ActualHelpContent.Case[1];
			caseThreeLabel.Content = ActualHelpContent.Case[2];
			caseFourLabel.Content = ActualHelpContent.Case[3];
		}

		private void DoubleAnimation_Completed(object sender, EventArgs e)
		{
			helpWindow.MinHeight = 500;
		}
	}
}
