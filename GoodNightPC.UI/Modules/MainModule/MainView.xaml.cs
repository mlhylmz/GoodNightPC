using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace GoodNightPC.UI.Modules.MainModule
{
	/// <summary>
	/// Interaction logic for MainView.xaml
	/// </summary>
	public partial class MainView : UserControl
	{
		public MainView()
		{
			InitializeComponent();
			ShutdownToggleButton.IsChecked = true;
		}

		private void ToggleButton_Checked(object sender, System.Windows.RoutedEventArgs e)
		{
			var name = ((ToggleButton)sender).Name;

			switch (name)
			{
				case "ShutdownToggleButton":
					HibernateToggleButton.IsChecked = false;
					RestartToggleButton.IsChecked = false;
					break;

				case "HibernateToggleButton":
					ShutdownToggleButton.IsChecked = false;
					RestartToggleButton.IsChecked = false;
					break;

				case "RestartToggleButton":
					HibernateToggleButton.IsChecked = false;
					ShutdownToggleButton.IsChecked = false;
					break;
			}
		}

		Regex _regex = new Regex("[^0-9]+");
		private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
		{
			e.Handled = _regex.IsMatch(e.Text);
		}
	}
}
