using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Misc.Composers
{
	public class UnknownComposer6 : IPacketComposer
	{
		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UnknownComposer6);
			message.WriteInteger(0);
			return message;
		}
	}
}
