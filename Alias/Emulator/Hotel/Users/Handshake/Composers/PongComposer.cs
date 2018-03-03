using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Handshake.Composers
{
	public class PongComposer : IMessageComposer
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
