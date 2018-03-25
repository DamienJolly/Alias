using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Rooms.States;
using Alias.Emulator.Hotel.Rooms.Users;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    public class ModerationChangeRoomSettingsEvent : IPacketEvent
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

			Room room = Alias.GetServer().GetRoomManager().Room(roomId);
			if (room != null)
			{
				bool lockDoor = message.Integer() == 1;
				bool changeTitle = message.Integer() == 1;
				bool kickUsers = message.Integer() == 1;

				if (changeTitle)
				{
					room.RoomData.Name = "Inappropriate to hotel management!";
				}

				if (lockDoor)
				{
					room.RoomData.DoorState = RoomDoorState.CLOSED;
				}

				if (kickUsers)
				{
					foreach (RoomUser user in room.UserManager.Users)
					{
						if (user.Habbo.HasPermission("acc_unkickable") || user.Habbo.HasPermission("acc_modtool") || user.Habbo.Id == room.RoomData.OwnerId)
						{
							continue;
						}
						room.UserManager.OnUserLeave(user.Habbo.Session);
					}
				}
			}
		}
	}
}
