using Alias.Emulator.Hotel.Groups.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Groups.Events
{
    class GroupRemoveAdminEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int groupId = message.PopInt();
			int userId = message.PopInt();

			Group group = Alias.Server.GroupManager.GetGroup(groupId);
			if (group == null || group.OwnerId != session.Habbo.Id)
			{
				return;
			}

			group.SetMemberRank(userId, 2);
			GroupMember member = group.GetMember(userId);
			session.Send(new GroupMemberUpdateComposer(group, member));

			Habbo targetHabbo = Alias.Server.SocketServer.SessionManager.HabboById(userId);
			if (targetHabbo != null)
			{
				Room room = targetHabbo.CurrentRoom;
				if (room != null)
				{
					if (room.RoomData.Group == group)
					{
						targetHabbo.Session.Send(new GroupInfoComposer(group, targetHabbo, false, member));
						room.RoomRights.RefreshRights(targetHabbo);
					}
				}
			}
		}
	}
}
