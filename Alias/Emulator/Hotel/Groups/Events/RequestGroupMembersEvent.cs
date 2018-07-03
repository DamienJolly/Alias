using System.Collections.Generic;
using Alias.Emulator.Hotel.Groups.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Groups.Events
{
    class RequestGroupMembersEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			int groupId = message.PopInt();
			int pageId = message.PopInt();
			string query = message.PopString();
			int levelId = message.PopInt();

			Group group = Alias.Server.GroupManager.GetGroup(groupId);
			if (group != null)
			{
				List<GroupMember> members = group.SearchMembers(pageId, levelId, query);
				session.Send(new GroupMembersComposer(group, members, session.Habbo, pageId, levelId, query));
			}
		}
	}
}
