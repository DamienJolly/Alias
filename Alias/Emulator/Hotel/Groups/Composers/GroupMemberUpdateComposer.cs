using System;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Groups.Composers
{
    class GroupMemberUpdateComposer : IPacketComposer
	{
		private Group group;
		private GroupMember member;
		private DateTime created;

		public GroupMemberUpdateComposer(Group group, GroupMember member)
		{
			this.group = group;
			this.member = member;
			this.created = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(member.JoinData);
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.GroupMemberUpdateMessageComposer);
			message.WriteInteger(this.group.Id);
			message.WriteInteger((int)this.member.Rank);
			message.WriteInteger(this.member.UserId);
			message.WriteString(this.member.Username);
			message.WriteString(this.member.Look);
			message.WriteString(this.created.Day + "/" + this.created.Month + "/" + this.created.Year);
			return message;
		}
	}
}
