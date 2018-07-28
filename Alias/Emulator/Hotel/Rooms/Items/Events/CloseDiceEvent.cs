using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Items.Events
{
	class CloseDiceEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			int itemId = message.PopInt();
			RoomItem item = room.ItemManager.GetItem(itemId);
			if (item == null)
			{
				return;
			}

			RoomUser user = room.UserManager.UserBySession(session);
			if (user == null)
			{
				return;
			}

			if (room.Mapping.Tiles[item.Position.X, item.Position.Y].TilesAdjecent(room.Mapping.Tiles[user.Position.X, user.Position.Y]))
			{
				if (item.ItemData.Interaction != ItemInteraction.DICE || item.Mode == -1)
				{
					return;
				}

				item.Mode = 0;
				room.UserManager.Send(new FloorItemUpdateComposer(item));
			}
		}
	}
}
