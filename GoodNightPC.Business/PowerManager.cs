using GoodNightPC.Entities.Enums;
using GoodNightPC.Entities.POCO;
using System.Diagnostics;

namespace GoodNightPC.Business
{
	public class PowerManager
	{
		private JsonManager _jsonManager;
        public PowerManager(JsonManager jm)
        {
            _jsonManager = jm;
        }

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
			var prompt = $"/c shutdown /a";
			SendToCommandPrompt(prompt);
			SaveWorkingCommand(ModesEnum.NONE, TimeSpan.Zero, false);
		}

		private void Shutdown(CommandStructure command)
		{
			var prompt = $"/c shutdown /s /t {(int)command.Duration.TotalSeconds}";
			SendToCommandPrompt(prompt);
			SaveWorkingCommand(ModesEnum.SHUTDOWN, command.Duration);
		}

		private void Hibernate(CommandStructure command)
		{
			var prompt = $"/c shutdown /h /t {(int)command.Duration.TotalSeconds}";
			SendToCommandPrompt(prompt);
			SaveWorkingCommand(ModesEnum.HIBERNATE, command.Duration);
		}

		private void Restart(CommandStructure command)
		{
			var prompt = $"/c shutdown /r /t {(int)command.Duration.TotalSeconds}";
			SendToCommandPrompt(prompt);
			SaveWorkingCommand(ModesEnum.RESTART, command.Duration);
		}

		private void SendToCommandPrompt(string prompt)
		{
			var processStartInfo = new ProcessStartInfo
			{
				FileName = "cmd.exe",
				Arguments = prompt,
				CreateNoWindow = true,
				UseShellExecute = false 
			};

			using (Process process = new Process())
			{
				process.StartInfo = processStartInfo;
				process.Start();
			}
		}

		private void SaveWorkingCommand(ModesEnum mode , TimeSpan duration, bool isRunning = true)
		{
			var command = new CommandJSON()
			{
				IsRunning = isRunning,
				Mode = mode,
				CommandTime = DateTime.Now.AddSeconds(duration.TotalSeconds)
			};

			_jsonManager.WriteCommandToJson(command);
		}

		public (bool Exist, CommandJSON Command) IsThereAWorkingCommand()
		{
			var command = _jsonManager.ReadJsonToCommand();
			return (command.IsRunning, command);
		}

	}
}
