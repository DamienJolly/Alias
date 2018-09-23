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
				List<GroupMember> members = group.SearchMembers(levelId, query);
				while (pageId * 14 > members.Count)
				{
					pageId--;
				}

				List<GroupMember> actuallMembers = members.GetRange(pageId * 14, (pageId * 14) + 14 > members.Count ? members.Count - pageId * 14 : (pageId * 14) + 14);
				session.Send(new GroupMembersComposer(group, members.Count, actuallMembers, session.Player, pageId, levelId, query));
			}
		}
	}
}
