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

			GroupMember member = group.GetMember(session.Player.Id);
			if (member != null)
			{
				return;
			}

			if (group.State == GroupState.CLOSED)
			{
				session.Send(new GroupJoinErrorComposer(GroupJoinErrorComposer.GROUP_CLOSED));
				return;
			}

			group.JoinGroup(session, session.Player.Id, false);
			session.Send(new GroupInfoComposer(group, session.Player, false, member));

			Room room = session.Player.CurrentRoom;
			if (room != null && room.RoomData.Group == group)
			{
				room.RoomRights.RefreshRights(session.Player);
			}
		}
	}
}
