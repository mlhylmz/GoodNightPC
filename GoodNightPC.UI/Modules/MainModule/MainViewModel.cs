using Caliburn.Micro;
using GoodNightPC.Entities;

namespace GoodNightPC.UI.Modules.MainModule
{
	public class MainViewModel : Screen
	{
		#region Mode Properties

		private bool _shutdownIsChecked;
		private bool _hibernateIsChecked;
		private bool _restartIsChecked;

		public bool ShutdownIsChecked
		{
			get => _shutdownIsChecked;
			set
			{
				if (_shutdownIsChecked == value) return;
				_shutdownIsChecked = value;
				NotifyOfPropertyChange(() => ShutdownIsChecked);
			}
		}

		public bool HibernateIsChecked
		{
			get => _hibernateIsChecked;
			set
			{
				if (_hibernateIsChecked == value) return;
				_hibernateIsChecked = value;
				NotifyOfPropertyChange(() => HibernateIsChecked);
			}
		}

		public bool RestartIsChecked
		{
			get => _restartIsChecked;
			set
			{
				if (_restartIsChecked == value) return;
				_restartIsChecked = value;
				NotifyOfPropertyChange(() => RestartIsChecked);
			}
		}

		private ModesEnum GetSelectedMode()
		{
			if (ShutdownIsChecked)
				return ModesEnum.SHUTDOWN;
			else if (HibernateIsChecked)
				return ModesEnum.HIBERNATE;
			else if(RestartIsChecked)
				return ModesEnum.RESTART;

			return ModesEnum.NONE;
		}

		#endregion

	}
}
