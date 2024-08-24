using GoodNightPC.Entities.Enums;

namespace GoodNightPC.Entities.DTO
{
	public class CommandStructure
	{
		public ModesEnum Mode { get; set; }
		public Preferences Preferences { get; set; }
		public TimeSpan DurationSecond { get; set; }
	}
}
