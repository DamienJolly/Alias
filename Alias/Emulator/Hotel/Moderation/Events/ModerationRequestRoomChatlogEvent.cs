using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    class ModerationRequestRoomChatlogEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (!session.Habbo.HasPermission("acc_modtool_room_logs"))
			{
				return;
			}

			message.PopInt();
			int roomId = message.PopInt();
			if (roomId <= 0)
			{
				return;
			}

			Room room = Alias.Server.RoomManager.Room(roomId);
			if (room != null)
			{
				session.Send(new ModerationRoomChatlogComposer(room, Alias.Server.ModerationManager.GetRoomChatlog(room.RoomData.Id)));
			}
		}
	}
}
