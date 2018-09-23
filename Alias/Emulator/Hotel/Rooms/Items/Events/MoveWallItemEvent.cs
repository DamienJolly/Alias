using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Items.Events
{
	class MoveWallItemEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Player.CurrentRoom;
			if (room == null)
			{
				return;
			}

			RoomItem item = room.ItemManager.GetItem(message.PopInt());
			if (item == null)
			{
				return;
			}

			if (!room.RoomRights.HasRights(session.Player.Id))
			{
				return;
			}

			string wallPosition = message.PopString();
			item.Position.WallPosition = wallPosition;
			room.EntityManager.Send(new WallItemUpdateComposer(item));
		}
	}
}
