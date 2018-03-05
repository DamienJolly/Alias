using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    public class ModerationRequestRoomInfoEvent : IMessageEvent
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

			RoomData roomData = RoomManager.RoomData(roomId);
			if (roomData != null)
			{
				session.Send(new ModerationRoomInfoComposer(roomData));
			}
		}
	}
}
