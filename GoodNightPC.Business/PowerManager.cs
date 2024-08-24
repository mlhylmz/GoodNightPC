using GoodNightPC.Entities.DTO;

namespace GoodNightPC.Business
{
	public class PowerManager
	{
		public void ExecuteAction(CommandStructure command)
		{
			switch (command.Mode)
			{
				case Entities.Enums.ModesEnum.SHUTDOWN:
					Shutdown(command);
					break;

				case Entities.Enums.ModesEnum.HIBERNATE:
					Hibernate(command);
					break;

				case Entities.Enums.ModesEnum.RESTART:
					Restart(command);
					break;
			}
		}

		public void StopAction()
		{

		}

		private void Shutdown(CommandStructure command)
		{

		}

		private void Hibernate(CommandStructure command)
		{

		}

		private void Restart(CommandStructure command)
		{

		}
	}
}
