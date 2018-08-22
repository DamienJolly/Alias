using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Hotel.Users.Inventory.Composers;
using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Items.Events
{
	class RoomPickupItemEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			int unknown = message.PopInt();
			int itemId = message.PopInt();

			RoomItem item = room.ItemManager.GetItem(itemId);
			if (item == null)
			{
				return;
			}

			room.Mapping.RemoveItem(item);
			room.ItemManager.RemoveItem(item);
			if (item.ItemData.Type == "s")
			{
				room.EntityManager.Send(new RemoveFloorItemComposer(item));
			}
			else
			{
				room.EntityManager.Send(new RemoveWallItemComposer(item));
			}
			session.Send(new InventoryRefreshComposer());

			InventoryItem iItem = new InventoryItem()
			{
				Id = item.Id,
				LimitedNumber = item.LimitedNumber,
				LimitedStack = item.LimitedStack,
				ItemData = item.ItemData,
				UserId = item.Owner,
				ExtraData = item.ExtraData
			};

			session.Habbo.Inventory.UpdateItem(iItem);
		}
	}
}
