using Alias.Emulator.Hotel.Rooms.Items.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Items.Events
{
	class RedeemItemEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}
			
			int itemId = message.PopInt();
			RoomItem rItem = room.ItemManager.GetItem(itemId);
			if (rItem == null || rItem.Owner != session.Habbo.Id)
			{
				return;
			}

			//todo: recode

			room.ItemManager.RemoveItem(rItem);
			room.UserManager.Send(new RemoveFloorItemComposer(rItem));
		}
	}
}
