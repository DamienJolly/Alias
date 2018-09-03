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
			if (!Alias.Server.RoomManager.TryGetRoomData(roomId, out RoomData roomData))
			{
				return;
			}

			bool lockDoor = message.PopInt() == 1;
			bool changeTitle = message.PopInt() == 1;
			bool kickUsers = message.PopInt() == 1;

			if (changeTitle)
			{
				roomData.Name = "Inappropriate to hotel management!";
			}

			if (lockDoor)
			{
				roomData.DoorState = RoomDoorState.CLOSED;
			}

			if (kickUsers)
			{
				//todo:
			}
		}
	}
}
