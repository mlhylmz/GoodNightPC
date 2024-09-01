using GoodNightPC.Entities.Enums;

namespace GoodNightPC.Entities.POCO
{
	public class CommandStructure
	{
		public ModesEnum Mode { get; set; }
		public Preferences Preferences { get; set; }
		public TimeSpan Duration { get; set; }
	}
}
