using Alias.Emulator.Hotel.Groups.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Groups.Events
{
    class RequestGroupJoinEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int groupId = message.PopInt();

			Group group = Alias.Server.GroupManager.GetGroup(groupId);
			if (group == null)
			{
				return;
			}

			if (group.State == GroupState.CLOSED)
			{
				session.Send(new GroupJoinErrorComposer(GroupJoinErrorComposer.GROUP_CLOSED));
				return;
			}

			group.JoinGroup(session, session.Habbo.Id, false);
			session.Send(new GroupInfoComposer(group, session.Habbo, false, group.GetMember(session.Habbo.Id)));

			Room room = session.Habbo.CurrentRoom;
			if (room != null && room.RoomData.Group == group)
			{
				room.RoomRights.RefreshRights(session.Habbo);
			}
		}
	}
}
