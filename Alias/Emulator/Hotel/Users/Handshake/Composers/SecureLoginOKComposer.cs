using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Handshake.Composers
{
	public class SecureLoginOKComposer : IPacketComposer
	{
		public ServerMessage Compose()
		{
			return new ServerMessage(Outgoing.SecureLoginOKMessageComposer);
		}
	}
}
