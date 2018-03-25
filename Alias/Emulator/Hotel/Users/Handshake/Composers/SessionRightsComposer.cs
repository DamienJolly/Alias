using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Handshake.Composers
{
	public class SessionRightsComposer : IPacketComposer
	{
		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.SessionRightsMessageComposer);
			result.Boolean(true);
			result.Boolean(false);
			result.Boolean(true);
			return result;
		}
	}
}
