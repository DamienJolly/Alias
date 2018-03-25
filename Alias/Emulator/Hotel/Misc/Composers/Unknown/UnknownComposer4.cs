using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Misc.Composers
{
	public class UnknownComposer4 : IPacketComposer
	{
		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.UnknownComposer4);
			result.Boolean(false); //Think something related to promo. Not sure though.
			return result;
		}
	}
}
