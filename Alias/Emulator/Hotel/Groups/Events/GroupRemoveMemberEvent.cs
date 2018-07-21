using Alias.Emulator.Hotel.Groups.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Groups.Events
{
    class GroupRemoveMemberEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int groupId = message.PopInt();
			int userId = message.PopInt();

			Group group = Alias.Server.GroupManager.GetGroup(groupId);
			if (group == null)
			{
				return;
			}

			if ((group.OwnerId == session.Habbo.Id && userId != session.Habbo.Id) || userId == session.Habbo.Id)
			{
				group.RemoveMember(userId);

				if (userId != session.Habbo.Id)
				{
					session.Send(new GroupRefreshMembersListComposer(group));
				}

				Habbo targetHabbo = Alias.Server.SocketServer.SessionManager.HabboById(userId);
				if (targetHabbo != null)
				{
					//todo: remove fav group
					Room room = targetHabbo.CurrentRoom;
					if (room != null)
					{
						if (room.RoomData.Group == group)
						{
							targetHabbo.Session.Send(new GroupInfoComposer(group, targetHabbo, false, null));
							room.RoomRights.RefreshRights(targetHabbo);
						}
					}
				}
				// todo: eject furni
			}
		}
	}
}
