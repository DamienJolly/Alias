using Alias.Emulator.Hotel.Groups.Composers;
using Alias.Emulator.Hotel.Rooms;
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

				// todo: update rights and remove fav group
				// todo: eject furni
			}
		}
	}
}
