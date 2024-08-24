using GoodNightPC.Entities.DTO;
using System.Diagnostics;

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
			var prompt = $"/c shutdown /a";
			SendToCommandPrompt(prompt);
		}

		private void Shutdown(CommandStructure command)
		{
			var prompt = $"/c shutdown /s /t {command.Duration.TotalSeconds}";
			SendToCommandPrompt(prompt);
		}

		private void Hibernate(CommandStructure command)
		{
			var prompt = $"/c shutdown /h /t {command.Duration.TotalSeconds}";
			SendToCommandPrompt(prompt);
		}

		private void Restart(CommandStructure command)
		{
			var prompt = $"/c shutdown /r /t {command.Duration.TotalSeconds}";
			SendToCommandPrompt(prompt);
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
	}
}
