using System.Collections.Generic;
using Alias.Emulator.Hotel.Groups.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Rooms.Entities;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Groups.Events
{
    class GetPlayerGroupBadgesEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Room room = session.Player.CurrentRoom;
			if (room == null)
			{
				return;
			}
			
			Dictionary<int, string> badges = new Dictionary<int, string>();
			foreach (RoomEntity user in room.EntityManager.Users)
			{
				if (user.Player.GroupId == 0 || badges.ContainsKey(user.Player.GroupId))
				{
					continue;
				}

				Group group = Alias.Server.GroupManager.GetGroup(user.Player.GroupId);
				if (group == null)
				{
					continue;
				}

				badges.Add(group.Id, group.Badge);
			}

			room.EntityManager.Send(new RoomUsersGroupBadgesComposer(badges));
		}
	}
}
