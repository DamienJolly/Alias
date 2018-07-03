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
			
			if (group.TrySetMemberRank(userId, 2))
			{
				Room room = session.Habbo.CurrentRoom;
				if (room != null && room.RoomData.Group == group)
				{
					//room.RoomRights.RefreshRights(habbo);
				}

				GroupMember member = group.GetMember(userId);
				session.Send(new GroupMemberUpdateComposer(group, member));
			}
		}
	}
}
