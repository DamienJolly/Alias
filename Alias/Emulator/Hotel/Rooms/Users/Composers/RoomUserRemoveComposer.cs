using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Users.Composers
{
	public class RoomUserRemoveComposer : IPacketComposer
	{
		private int VirtualId;

		public RoomUserRemoveComposer(int virtualId)
		{
			this.VirtualId = virtualId;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.RoomUserRemoveMessageComposer);
			message.WriteString(this.VirtualId.ToString());
			return message;
		}
	}
}
