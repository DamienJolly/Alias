using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Misc.Composers
{
	public class UnknownComposer5 : IPacketComposer
	{
		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UnknownComposer5);
			message.WriteInteger(0);
			return message;
		}
	}
}
