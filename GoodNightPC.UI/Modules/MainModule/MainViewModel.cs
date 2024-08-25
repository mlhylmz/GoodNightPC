using Caliburn.Micro;
using GoodNightPC.Business;
using GoodNightPC.Entities.DTO;
using GoodNightPC.Entities.Enums;

namespace GoodNightPC.UI.Modules.MainModule
{
	public class MainViewModel : Screen
	{
		private readonly PowerManager _powerManager;

		#region Properties

		private bool _isTimerWorking;
		public bool IsTimerWorking
		{
			get => _isTimerWorking;
			set
			{
				if (value == _isTimerWorking) return;
				_isTimerWorking = value;
				NotifyOfPropertyChange(() => IsTimerWorking);
            }
		}

		private TimeSpan _countDownTimer;
		public TimeSpan CountDownTimer
		{
			get => _countDownTimer;
			set
			{
				if(value == _countDownTimer) return;
				_countDownTimer = value;
				NotifyOfPropertyChange(() => CountDownTimer);
			}
		}

		#region Selected Time Properties

		public List<TimeUnits> TimeUnits { get; set; }

		private TimeUnits _selectedTimeUnit = Entities.Enums.TimeUnits.Second;
		public TimeUnits SelectedTimeUnit
		{
			get => _selectedTimeUnit;
			set
			{
				if (value == _selectedTimeUnit) return;
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
				if (value == _timeDuration) return;
				_timeDuration = value;
				NotifyOfPropertyChange(() => TimeDuration);
			}
		}


		private DateTime _selectedDateTime = DateTime.Now.AddHours(1);

		public DateTime SelectedDateTime
		{
			get => _selectedDateTime;
			set
			{
				if (value == _selectedDateTime) return;
				_selectedDateTime = value;
				NotifyOfPropertyChange(() => SelectedDateTime);
			}
		}

		#endregion

		#region Power Mode Properties

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

		#region Time Mode Properties

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

		#endregion

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

		
		#region Methods

		public void StartTimerButton()
		{
			var mode = GetSelectedMode();
			if (mode != ModesEnum.NONE)
			{
				var durationTimeSpan = GetDuration();
				CommandStructure cs = new CommandStructure()
				{
					Mode = mode,
					Duration = durationTimeSpan
				};

				_powerManager.ExecuteAction(cs);

				if(_timerCts != null)
					_timerCts.Cancel();
			
				_timerCts = new CancellationTokenSource();

				Task.Run(() => StartTimer(durationTimeSpan, _timerCts.Token));
			}
		}

		private CancellationTokenSource _timerCts;
		private async Task StartTimer(TimeSpan durationTimeSpan, CancellationToken token)
		{
			CountDownTimer = durationTimeSpan;
			IsTimerWorking = true;

			while (CountDownTimer.TotalSeconds > 1)
			{
				if (token.IsCancellationRequested)
					break;

				var oneSecTimeSpan = TimeSpan.FromSeconds(1);
				CountDownTimer = CountDownTimer - oneSecTimeSpan;
				await Task.Delay(1000);
			}

			IsTimerWorking = false;
		}

		public void StopTimerButton()
		{
			_powerManager.StopAction();

			if(_timerCts != null)
				_timerCts.Cancel();
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
