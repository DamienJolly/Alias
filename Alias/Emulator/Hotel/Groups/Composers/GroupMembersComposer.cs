using System;
using System.Collections.Generic;
using Alias.Emulator.Hotel.Players;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Groups.Composers
{
    class GroupMembersComposer : IPacketComposer
	{
		private Group group;
		private int membersCount;
		private List<GroupMember> members;
		private Player habbo;
		private int pageId;
		private int levelId;
		private string query;

		public GroupMembersComposer(Group group, int membersCount, List<GroupMember> members, Player habbo, int pageId, int levelId, string query)
		{
			this.group = group;
			this.membersCount = membersCount;
			this.members = members;
			this.habbo = habbo;
			this.pageId = pageId;
			this.levelId = levelId;
			this.query = query;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.GroupMembersMessageComposer);
			message.WriteInteger(this.group.Id);
			message.WriteString(this.group.Name);
			message.WriteInteger(this.group.RoomId);
			message.WriteString(this.group.Badge);
			message.WriteInteger(this.membersCount);

			message.WriteInteger(this.members.Count);
			this.members.ForEach(member =>
			{
				DateTime created = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(member.JoinData);
				message.WriteInteger((int)member.Rank);
				message.WriteInteger(member.UserId);
				message.WriteString(member.Username);
				message.WriteString(member.Look);
				message.WriteString((int)member.Rank < 3 && (int)member.Rank > 0 ? created.Day + "/" + created.Month + "/" + created.Year : "");
			});

			message.WriteBoolean(this.group.OwnerId == this.habbo.Id);
			message.WriteInteger(14);
			message.WriteInteger(this.pageId);
			message.WriteInteger(this.levelId);
			message.WriteString(this.query);
			return message;
		}
	}
}
