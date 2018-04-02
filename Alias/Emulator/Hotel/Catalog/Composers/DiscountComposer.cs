using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Composers
{
	public class DiscountComposer : IPacketComposer
	{
		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.DiscountMessageComposer);
			message.WriteInteger(100); //Most you can get.
			message.WriteInteger(6);
			message.WriteInteger(1);
			message.WriteInteger(1);
			message.WriteInteger(2); //Count
			{
				message.WriteInteger(40);
				message.WriteInteger(99);
			}
			return message;
		}
	}
}
