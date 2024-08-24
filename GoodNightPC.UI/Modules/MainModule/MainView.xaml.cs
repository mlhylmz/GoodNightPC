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
		}

		private void ToggleButton_Checked(object sender, System.Windows.RoutedEventArgs e)
		{
			var name = ((ToggleButton)sender).Name;

			switch(name)
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

	}
}
