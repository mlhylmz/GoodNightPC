using Caliburn.Micro;
using GoodNightPC.UI.Modules.MainModule;
using System.Reflection;
using System.Windows;

namespace GoodNightPC.UI
{
	public class Bootstrapper : BootstrapperBase
	{

		private SimpleContainer _container = new();

		public Bootstrapper()
		{
			Initialize();
		}

		protected override void Configure()
		{
			_container.Singleton<IWindowManager, WindowManager>();
			_container.Singleton<IEventAggregator, EventAggregator>();

			_container.PerRequest<ShellViewModel>();
			_container.PerRequest<MainViewModel>();
		}

		protected override object GetInstance(Type service, string key)
		{
			return _container.GetInstance(service, key);
		}

		protected override IEnumerable<object> GetAllInstances(Type service)
		{
			return _container.GetAllInstances(service);
		}

		protected override void BuildUp(object instance)
		{
			_container.BuildUp(instance);
		}

		protected override void OnStartup(object sender, StartupEventArgs e)
		{
			DisplayRootViewForAsync<ShellViewModel>();
		}

		protected override IEnumerable<Assembly> SelectAssemblies()
		{
			return new[] { Assembly.GetExecutingAssembly() };
		}
	}
}
