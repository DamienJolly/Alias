using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Handshake.Composers
{
	public class PongComposer : IPacketComposer
	{
		private int id;

		public PongComposer(int id)
		{
			this.id = id;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.PongMessageComposer);
			message.WriteInteger(this.id);
			return message;
		}
	}
}
