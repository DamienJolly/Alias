using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    public class ModerationChangeRoomSettingsEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			if (!session.Habbo.HasPermission("acc_modtool_room_settings"))
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
				bool kickUsers = message.Integer() == 1;
				bool lockDoor = message.Integer() == 1;
				bool changeTitle = message.Integer() == 1;
				//todo: code room settings
			}
		}
	}
}
