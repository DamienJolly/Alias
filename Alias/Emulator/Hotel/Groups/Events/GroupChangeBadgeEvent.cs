using Alias.Emulator.Hotel.Groups.Composers;
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
			if (group == null || group.OwnerId != session.Player.Id)
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
			
			if (!Alias.Server.RoomManager.TryGetRoomData(group.RoomId, out RoomData roomData))
			{
				return;
			}

			if (roomData.Group == null)
			{
				return;
			}

			roomData.Group = group;

			if (session.Player.CurrentRoom != null)
			{
				session.Player.CurrentRoom.UpdateGroup(group);
			}
		}
	}
}
