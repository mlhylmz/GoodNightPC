using GoodNightPC.Entities.Enums;

namespace GoodNightPC.Entities.POCO
{
	public class CommandJSON
	{
		public bool IsRunning { get; set; }
		public ModesEnum Mode { get; set; }
		public DateTime CommandTime { get; set; }
	}
}
