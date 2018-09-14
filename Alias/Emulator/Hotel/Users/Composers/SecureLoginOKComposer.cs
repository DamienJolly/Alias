using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Composers
{
	public class SecureLoginOKComposer : IPacketComposer
	{
		public ServerPacket Compose()
		{
			return new ServerPacket(Outgoing.SecureLoginOKMessageComposer);
		}
	}
}
