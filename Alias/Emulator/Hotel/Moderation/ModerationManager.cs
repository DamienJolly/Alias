using System.Collections.Generic;
using System.Linq;

namespace Alias.Emulator.Hotel.Moderation
{
    public class ModerationManager
    {
		private static List<ModerationPresets> presets;
		private static List<ModerationTicket> modTickets;

		public static void Initialize()
		{
			presets = ModerationDatabase.ReadPresets();
			modTickets = ModerationDatabase.ReadTickets();
		}

		public static void Reload()
		{
			presets.Clear();
			modTickets.Clear();
			Initialize();
		}

		public static List<ModerationPresets> GetPresets(string type)
		{
			return presets.Where(preset => preset.Type == type).ToList();
		}

		public static List<ModerationTicket> GetModerationTickets
		{
			get
			{
				return modTickets;
			}
		}
	}
}
