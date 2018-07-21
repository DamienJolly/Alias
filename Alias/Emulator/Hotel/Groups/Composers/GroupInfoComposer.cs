using System;
using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Groups.Composers
{
    class GroupInfoComposer : IPacketComposer
	{
		private Group group;
		private Habbo habbo;
		private bool newWindow;
		private DateTime created;
		private GroupMember member;

		public GroupInfoComposer(Group group, Habbo habbo, bool newWindow, GroupMember member)
		{
			this.group = group;
			this.habbo = habbo;
			this.newWindow = newWindow;
			this.created = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(group.CreatedAt);
			this.member = member;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.GroupInfoMessageComposer);
			message.WriteInteger(this.group.Id);
			message.WriteBoolean(true); // ??
			message.WriteInteger((int)this.group.State);
			message.WriteString(this.group.Name);
			message.WriteString(this.group.Description);
			message.WriteString(this.group.Badge);
			message.WriteInteger(this.group.RoomId);
			message.WriteString("Unknown"); // room name
			message.WriteInteger(this.member == null ? 0 : (this.member.Rank == GroupRank.MEMBER ? 1 : (this.member.Rank == GroupRank.REQUESTED ? 2 : (this.member.Rank == GroupRank.MOD ? 3 : (this.member.Rank == GroupRank.ADMIN ? 4 : 0)))));
			message.WriteInteger(this.group.GetMembers);
			message.WriteBoolean(this.habbo.GroupId == this.group.Id);
			message.WriteString(this.created.Day + "-" + this.created.Month + "-" + this.created.Year);
			message.WriteBoolean(this.group.OwnerId == this.habbo.Id);
			message.WriteBoolean(this.member != null && (this.member.Rank == GroupRank.ADMIN));
			message.WriteString("Unknow"); // owner name
			message.WriteBoolean(this.newWindow);
			message.WriteBoolean(this.group.Rights);
			message.WriteInteger(this.group.OwnerId == this.habbo.Id ? this.group.GetRequests : 0);
			message.WriteBoolean(true); // forum
			return message;
		}
	}
}
