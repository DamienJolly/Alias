using System.Data.Common;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Players.Wardrobe
{
    class WardrobeItem
    {
		public int SlotId { get; set; }
		public string Figure { get; set; }
		public string Gender { get; set; }

		public WardrobeItem(DbDataReader reader)
		{
			SlotId = reader.ReadData<int>("slot_id");
			Figure = reader.ReadData<string>("figure");
			Gender = reader.ReadData<string>("gender");
		}

		public WardrobeItem(int slotId, string figure, string gender)
		{
			SlotId = slotId;
			Figure = figure;
			Gender = gender;
		}
	}
}
