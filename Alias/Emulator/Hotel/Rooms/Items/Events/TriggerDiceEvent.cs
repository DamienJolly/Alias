using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Items.Events
{
	public class TriggerDiceEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			int itemId = message.Integer();
			RoomItem item = room.ItemManager.GetItem(itemId);
			if (item == null)
			{
				return;
			}
			
			item.GetInteractor().OnUserInteract(session, room, item, 0);
		}
	}
}