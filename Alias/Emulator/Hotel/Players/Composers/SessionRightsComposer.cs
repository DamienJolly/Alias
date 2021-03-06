using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Composers
{
	public class SessionRightsComposer : IPacketComposer
	{
		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.SessionRightsMessageComposer);
			message.WriteBoolean(true);
			message.WriteBoolean(false);
			message.WriteBoolean(true);
			return message;
		}
	}
}
