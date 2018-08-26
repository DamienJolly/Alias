using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Marketplace.Composers
{
    class MarketplaceConfigComposer : IPacketComposer
	{
		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.MarketplaceConfigMessageComposer);
			message.WriteBoolean(true);
			message.WriteInteger(1); //Commision Percentage.
			message.WriteInteger(10); //Credits
			message.WriteInteger(5); //Advertisements
			message.WriteInteger(1); //Min price
			message.WriteInteger(1000000); //Max price
			message.WriteInteger(48); //Hours in marketplace
			message.WriteInteger(7); //Days to display
			message.WriteInteger(1);
			return message;
		}
	}
}
