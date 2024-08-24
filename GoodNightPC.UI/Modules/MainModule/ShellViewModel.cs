using Caliburn.Micro;

namespace GoodNightPC.UI.Modules.MainModule
{
	public class ShellViewModel : Conductor<object>
	{
        public ShellViewModel(MainViewModel mainView)
        {
            ActiveItem = mainView;
        }
    }
}
