using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    public class ModerationRequestRoomInfoEvent : IPacketEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			if (!session.Habbo.HasPermission("acc_modtool_room_info"))
			{
				return;
			}

			int roomId = message.Integer();
			if (roomId <= 0)
			{
				return;
			}

			RoomData roomData = Alias.GetServer().GetRoomManager().RoomData(roomId);
			if (roomData != null)
			{
				session.Send(new ModerationRoomInfoComposer(roomData));
			}
		}
	}
}
