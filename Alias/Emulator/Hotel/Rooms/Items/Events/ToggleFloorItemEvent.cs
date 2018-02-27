using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Hotel.Users.Inventory.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Items.Events
{
	public class ToggleFloorItemEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			Room room = session.Habbo().CurrentRoom;
			if (room == null)
			{
				return;
			}

			int itemId = message.Integer();
			int state = message.Integer();

			RoomItem item = room.ItemManager.GetItem(itemId);
			if (item == null)
			{
				return;
			}

			//todo: Item interaction
		}
	}
}
