using Alias.Emulator.Hotel.Users.Inventory;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Entities.Events
{
	class RoomUserPlaceBotEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Habbo.CurrentRoom;
			if (room == null)
			{
				return;
			}

			InventoryBots bot = session.Habbo.Inventory.GetBot(message.PopInt());
			if (bot == null)
			{
				return;
			}

			int x = message.PopInt();
			int y = message.PopInt();


		}
	}
}
