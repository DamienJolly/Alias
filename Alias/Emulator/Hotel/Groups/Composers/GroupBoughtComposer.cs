using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Groups.Composers
{
    class GroupBoughtComposer : IPacketComposer
	{
		private Group group;

		public GroupBoughtComposer(Group group)
		{
			this.group = group;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.GroupBoughtMessageComposer);
			message.WriteInteger(this.group.RoomId);
			message.WriteInteger(this.group.Id);
			return message;
		}
	}
}
