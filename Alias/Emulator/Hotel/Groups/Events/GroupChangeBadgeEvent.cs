using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Groups.Events
{
    class GroupChangeBadgeEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int groupId = message.PopInt();

			Group group = Alias.Server.GroupManager.GetGroup(groupId);
			if (group == null || group.OwnerId != session.Habbo.Id)
			{
				return;
			}

			int count = message.PopInt();

			string badge = "";
			for (int i = 0; i < count; i += 3)
			{
				int id = message.PopInt();
				int colour = message.PopInt();
				int pos = message.PopInt();

				if (i == 0)
				{
					badge += "b";
				}
				else
				{
					badge += "s";
				}

				badge += (id < 100 ? "0" : "") + (id < 10 ? "0" : "") + id + (colour < 10 ? "0" : "") + colour + "" + pos;
			}

			if (badge == group.Badge)
			{
				return;
			}

			group.Badge = badge;
			group.Save();

			//todo: gen badge

			RoomData room = Alias.Server.RoomManager.RoomData(group.RoomId);
			if (room != null && room.Group != null)
			{
				room.Group = group;
			}

			if (Alias.Server.RoomManager.IsRoomLoaded(group.RoomId))
			{
				Alias.Server.RoomManager.Room(group.RoomId).RefreshGroup();
			}
		}
	}
}
