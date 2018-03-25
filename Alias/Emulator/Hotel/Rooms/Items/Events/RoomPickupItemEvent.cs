using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Hotel.Users.Inventory.Composers;
using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Items.Events
{
	public class RoomPickupItemEvent : IPacketEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			int unknown = message.Integer();
			int itemId = message.Integer();

			RoomItem rItem = room.ItemManager.GetItem(itemId);
			if (rItem == null)
			{
				return;
			}

			RoomUser user = room.UserManager.UserBySession(session);
			if (user == null)
			{
				return;
			}

			room.ItemManager.RemoveItem(rItem);
			room.UserManager.Send(new RemoveFloorItemComposer(rItem));

			InventoryItem iItem = new InventoryItem()
			{
				Id = rItem.Id,
				LimitedNumber = rItem.LimitedNumber,
				LimitedStack = rItem.LimitedStack,
				ItemData = rItem.ItemData,
				UserId = rItem.Owner
			};

			session.Habbo.Inventory.UpdateItem(iItem);
			session.Send(new InventoryRefreshComposer());
		}
	}
}
