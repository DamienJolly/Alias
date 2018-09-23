using System.Data.Common;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Players.Badges
{
    public class BadgeDefinition
    {
		public string Code { get; set; }
		public int Slot { get; set; }

		internal BadgeDefinition(DbDataReader reader)
		{
			Code = reader.ReadData<string>("badge_code");
			Slot = reader.ReadData<int>("slot_id");
		}
	}
}
