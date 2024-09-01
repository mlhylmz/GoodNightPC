using GoodNightPC.Entities.POCO;
using System.Text.Json;

namespace GoodNightPC.Business
{
	public class JsonManager
	{
		private string _appDataPath;
		public JsonManager()
		{
			_appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GoodNightPC\\runningCommand.json";
		}

		public void WriteCommandToJson(CommandJSON commandJson)
		{
			var jsonString = JsonSerializer.Serialize(commandJson);
			File.WriteAllText(_appDataPath, jsonString);
		}

		public CommandJSON ReadJsonToCommand()
		{
			if (File.Exists(_appDataPath))
			{
				var command = JsonSerializer.Deserialize<CommandJSON>(File.ReadAllText(_appDataPath));
				if(command != null)
				{
					return command;
				}
			}
			return new CommandJSON();
		}
	}
}
