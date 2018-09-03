using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    class ModerationRequestRoomInfoEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (!session.Habbo.HasPermission("acc_modtool_room_info"))
			{
				return;
			}

			int roomId = message.PopInt();
			if (!Alias.Server.RoomManager.TryGetRoomData(roomId, out RoomData roomData))
			{
				return;
			}

			session.Send(new ModerationRoomInfoComposer(roomData));
		}
	}
}
