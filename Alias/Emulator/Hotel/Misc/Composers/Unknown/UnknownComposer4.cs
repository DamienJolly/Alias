using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Misc.Composers
{
	public class UnknownComposer4 : IPacketComposer
	{
		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UnknownComposer4);
			message.WriteBoolean(false); //Think something related to promo. Not sure though.
			return message;
		}
	}
}
