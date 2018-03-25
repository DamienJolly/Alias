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

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.PongMessageComposer);
			result.Int(this.id);
			return result;
		}
	}
}
