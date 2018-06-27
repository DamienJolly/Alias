using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Hotel.Users.Achievements;
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

		public GroupInfoComposer(Group group, Habbo habbo, bool newWindow)
		{
			this.group = group;
			this.habbo = habbo;
			this.newWindow = newWindow;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.GroupInfoMessageComposer);
			message.WriteInteger(this.group.Id);
			message.WriteBoolean(true); // ??
			message.WriteInteger(0); // state
			message.WriteString(this.group.Name);
			message.WriteString(this.group.Description);
			message.WriteString(""); // badge
			message.WriteInteger(this.group.RoomId);
			message.WriteString("hello"); // room name
			message.WriteInteger(3); // ?
			message.WriteInteger(69); // memember count
			message.WriteBoolean(false); // ??
			message.WriteString(""); // created
			message.WriteBoolean(this.group.OwnerId == this.habbo.Id);
			message.WriteBoolean(true); // is admin
			message.WriteString("Damien"); // owner name
			message.WriteBoolean(this.newWindow);
			message.WriteBoolean(false); // user can furni
			message.WriteInteger(0); // invite count
			message.WriteBoolean(true); // forum
			return message;
		}
	}
}
