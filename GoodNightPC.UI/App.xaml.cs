using HandyControl.Tools;
using System.Windows;

namespace GoodNightPC.UI
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
        public App()
        {
			ConfigHelper.Instance.SetLang("en");

		}
	}

}
