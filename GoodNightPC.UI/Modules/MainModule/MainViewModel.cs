using Caliburn.Micro;
using GoodNightPC.Business;
using GoodNightPC.Entities.DTO;
using GoodNightPC.Entities.Enums;

namespace GoodNightPC.UI.Modules.MainModule
{
	public class MainViewModel : Screen
	{
		private readonly PowerManager _powerManager;

		private bool _durationModeChecked = true;
		private bool _dateTimeModeChecked;

		public bool DurationModeChecked
		{
			get => _durationModeChecked;
			set
			{
				if (_durationModeChecked == value) return;
				_durationModeChecked = value;
				NotifyOfPropertyChange(() => DurationModeChecked);
			}
		}

		public bool DateTimeModeChecked
		{
			get => _dateTimeModeChecked;
			set
			{
				if (_dateTimeModeChecked == value) return;
				_dateTimeModeChecked = value;
				NotifyOfPropertyChange(() => DateTimeModeChecked);
			}
		}


		#region Properties
		public List<TimeUnits> TimeUnits { get; set; }

		private TimeUnits _selectedTimeUnit = Entities.Enums.TimeUnits.Second;
		public TimeUnits SelectedTimeUnit
		{
			get => _selectedTimeUnit;
			set
			{
				if(value == _selectedTimeUnit) return;
				_selectedTimeUnit = value;
				NotifyOfPropertyChange(() => SelectedTimeUnit);
			}
		}


		private int _timeDuration = 1;
		public int TimeDuration
		{
			get => _timeDuration;
			set
			{
				if(value == _timeDuration) return;
				_timeDuration = value;
				NotifyOfPropertyChange(() => TimeDuration);
			}
		}


		private DateTime _selectedDateTime = DateTime.Now;

		public DateTime SelectedDateTime
		{
			get => _selectedDateTime;
			set
			{
				if(value == _selectedDateTime) return;
				_selectedDateTime = value;
				NotifyOfPropertyChange(() => SelectedDateTime);
			}
		}

		#endregion

		#region Constructor

		public MainViewModel(PowerManager pm)
		{
			_powerManager = pm;
			TimeUnits = new List<TimeUnits>()
			{
				Entities.Enums.TimeUnits.Second,
				Entities.Enums.TimeUnits.Minute,
				Entities.Enums.TimeUnits.Hour,
				Entities.Enums.TimeUnits.Day
			};
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
			else if (RestartIsChecked)
				return ModesEnum.RESTART;

			return ModesEnum.NONE;
		}

		#endregion

		#region Methods

		public void StartTimer()
		{
			var mode = GetSelectedMode();
			if (mode != ModesEnum.NONE)
			{
				CommandStructure cs = new CommandStructure()
				{
					Mode = mode,
					Duration =  GetDuration()
				};

				_powerManager.ExecuteAction(cs);
			}
		}

		public void StopTimer()
		{
			_powerManager.StopAction();
		}

		private TimeSpan GetDuration()
		{
			if (DurationModeChecked)
			{
				switch (SelectedTimeUnit)
				{
					case Entities.Enums.TimeUnits.Second:
						return TimeSpan.FromSeconds(TimeDuration);

					case Entities.Enums.TimeUnits.Minute:
						return TimeSpan.FromMinutes(TimeDuration);

					case Entities.Enums.TimeUnits.Hour:
						return TimeSpan.FromHours(TimeDuration);

					case Entities.Enums.TimeUnits.Day:
						return TimeSpan.FromDays(TimeDuration);
				}
			}
			else if (DateTimeModeChecked)
			{
				TimeSpan timeDifference = SelectedDateTime - DateTime.Now;

				if(timeDifference.TotalSeconds > 0)
					return timeDifference;
				else
					return TimeSpan.Zero;
			}

			return TimeSpan.Zero;
		}

		#endregion

	}
}
