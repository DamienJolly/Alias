using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Rooms.States;
using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    class ModerationChangeRoomSettingsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (!session.Habbo.HasPermission("acc_modtool_room_settings"))
			{
				return;
			}

			int roomId = message.PopInt();
			if (roomId <= 0)
			{
				return;
			}

			Room room = Alias.Server.RoomManager.Room(roomId);
			if (room != null)
			{
				bool lockDoor = message.PopInt() == 1;
				bool changeTitle = message.PopInt() == 1;
				bool kickUsers = message.PopInt() == 1;

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
					//todo: fix
					foreach (RoomEntity entity in room.EntityManager.Entities)
					{
						if (entity.Habbo.HasPermission("acc_unkickable") || entity.Habbo.HasPermission("acc_modtool") || entity.Habbo.Id == room.RoomData.OwnerId)
						{
							continue;
						}
						room.EntityManager.OnUserLeave(entity);
					}
				}
			}
		}
	}
}
