using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Groups.Composers
{
    class GroupRefreshMembersListComposer : IPacketComposer
	{
		private Group group;

		public GroupRefreshMembersListComposer(Group group)
		{
			this.group = group;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.GroupRefreshMembersListMessageComposer);
			message.WriteInteger(this.group.Id);
			message.WriteInteger(0);
			return message;
		}
	}
}
