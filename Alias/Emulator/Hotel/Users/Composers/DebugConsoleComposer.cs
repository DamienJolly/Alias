using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Composers
{
	public class DebugConsoleComposer : IPacketComposer
	{
		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.DebugConsoleMessageComposer);
			message.WriteBoolean(true);
			return message;
		}
	}
}
