using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Items.Events
{
	class ToggleFloorItemEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Player.CurrentRoom;
			if (room == null)
			{
				return;
			}

			int itemId = message.PopInt();
			int state = message.PopInt();

			RoomItem item = room.ItemManager.GetItem(itemId);
			if (item == null && item.ItemData.Type == "s")
			{
				return;
			}

			if (item.ItemData.Interaction == ItemInteraction.EXCHANGE ||
				item.ItemData.Interaction == ItemInteraction.DIAMOND_EXCHANGE ||
				item.ItemData.Interaction == ItemInteraction.POINTS_EXCHANGE ||
				item.ItemData.Interaction == ItemInteraction.DICE)
			{
				return;
			}

			item.GetInteractor().OnUserInteract(session.Player.Entity, room, item, state);
		}
	}
}
