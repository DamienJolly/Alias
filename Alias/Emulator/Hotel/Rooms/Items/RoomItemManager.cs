using System.Collections.Generic;
using System.Linq;

namespace Alias.Emulator.Hotel.Rooms.Items
{
	class RoomItemManager
	{
		public List<RoomItem> Items
		{
			get; set;
		}

		private Room Room
		{
			get; set;
		}

		public RoomItemManager(Room room)
		{
			this.Items = RoomItemDatabase.ReadRoomItems(room);
			this.Room = room;
		}

		public void AddItem(RoomItem item)
		{
			RoomItemDatabase.AddItem(item);
			Items.Add(item);
		}

		public void RemoveItem(RoomItem item)
		{
			RoomItemDatabase.RemoveItem(item.Id);
			Items.Remove(item);
		}

		public RoomItem GetItem(int itemId)
		{
			return this.Items.Where(item => item.Id == itemId).FirstOrDefault();
		}
	}
}
