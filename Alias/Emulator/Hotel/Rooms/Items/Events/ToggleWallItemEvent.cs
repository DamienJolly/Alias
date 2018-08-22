using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Items.Events
{
	class ToggleWallItemEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			int itemId = message.PopInt();
			int state = message.PopInt();

			RoomItem item = room.ItemManager.GetItem(itemId);
			if (item == null && item.ItemData.Type == "i")
			{
				return;
			}

			item.GetInteractor().OnUserInteract(session.Habbo.Entity, room, item, state);
		}
	}
}
