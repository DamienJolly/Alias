using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Groups.Composers
{
    class RemoveGroupFromRoomComposer : IPacketComposer
	{
		private int groupId;

		public RemoveGroupFromRoomComposer(int groupId)
		{
			this.groupId = groupId;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RemoveGroupFromRoomMessageComposer);
			message.WriteInteger(this.groupId);
			return message;
		}
	}
}
