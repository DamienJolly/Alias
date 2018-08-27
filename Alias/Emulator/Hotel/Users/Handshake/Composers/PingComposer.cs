using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Handshake.Composers
{
	public class PingComposer : IPacketComposer
	{
		private int id;

		public PingComposer(int id)
		{
			this.id = id;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.PingMessageComposer);
			message.WriteInteger(this.id);
			return message;
		}
	}
}
