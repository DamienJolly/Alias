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

		public GroupInfoComposer(Group group, Habbo habbo, bool newWindow)
		{
			this.group = group;
			this.habbo = habbo;
			this.newWindow = newWindow;
			this.created = new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(group.CreatedAt);
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.GroupInfoMessageComposer);
			message.WriteInteger(this.group.Id);
			message.WriteBoolean(true); // ??
			message.WriteInteger(0); // state
			message.WriteString(this.group.Name);
			message.WriteString(this.group.Description);
			message.WriteString(this.group.Badge);
			message.WriteInteger(this.group.RoomId);
			message.WriteString("hello"); // room name
			message.WriteInteger(3); // ?
			message.WriteInteger(this.group.GetMembers);
			message.WriteBoolean(false); // ??
			message.WriteString(this.created.Day + "-" + this.created.Month + "-" + this.created.Year);
			message.WriteBoolean(this.group.OwnerId == this.habbo.Id);
			message.WriteBoolean(true); // is admin
			message.WriteString("Damien"); // owner name
			message.WriteBoolean(this.newWindow);
			message.WriteBoolean(false); // user can furni
			message.WriteInteger(this.group.GetRequests);
			message.WriteBoolean(true); // forum
			return message;
		}
	}
}
