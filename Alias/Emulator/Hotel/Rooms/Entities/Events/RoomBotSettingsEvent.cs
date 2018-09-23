using Alias.Emulator.Hotel.Rooms.Entities.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Rooms.Entities.Events
{
	class RoomBotSettingsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Player.CurrentRoom;
			if (room == null)
			{
				return;
			}

			if (room.RoomData.OwnerId != session.Player.Id)
			{
				return;
			}

			int botId = message.PopInt();
			RoomEntity bot = room.EntityManager.BotById(botId);
			if (bot == null)
			{
				return;
			}

			int settingId = message.PopInt();
			session.Send(new RoomBotSettingsComposer(bot, settingId));
		}
	}
}
