using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    public class ModerationRequestRoomChatlogEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			if (!session.Habbo.HasPermission("acc_modtool_room_logs"))
			{
				return;
			}

			message.Integer();
			int roomId = message.Integer();
			if (roomId <= 0)
			{
				return;
			}

			Room room = RoomManager.Room(roomId);
			if (room != null)
			{
				session.Send(new ModerationRoomChatlogComposer(room, ModerationManager.GetRoomChatlog(room.RoomData.Id)));
			}
		}
	}
}
