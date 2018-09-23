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
			if (!session.Player.HasPermission("acc_modtool_room_logs"))
			{
				return;
			}

			message.PopInt(); // Dunno?
			int roomId = message.PopInt();
			if (!Alias.Server.RoomManager.TryGetRoomData(roomId, out RoomData roomData))
			{
				return;
			}

			session.Send(new ModerationRoomChatlogComposer(roomData, Alias.Server.ModerationManager.GetRoomChatlog(roomData.Id)));
		}
	}
}
