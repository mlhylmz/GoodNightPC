using Caliburn.Micro;
using GoodNightPC.Business;
using GoodNightPC.Entities.DTO;
using GoodNightPC.Entities.Enums;

namespace GoodNightPC.UI.Modules.MainModule
{
    public class MainViewModel : Screen
	{
		private readonly PowerManager _powerManager;

        #region Constructor

        public MainViewModel(PowerManager pm)
        {
            _powerManager = pm;
        }

        #endregion

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

		#region Methods

		public void StartTimer()
		{
			var mode = GetSelectedMode();
			if(mode != ModesEnum.NONE)
			{
				CommandStructure cs = new CommandStructure()
				{
					Mode = mode
				};

				_powerManager.ExecuteAction(cs);
			}
		}

		public void StopTimer()
		{
			_powerManager.StopAction();
		}

		#endregion

	}
}
