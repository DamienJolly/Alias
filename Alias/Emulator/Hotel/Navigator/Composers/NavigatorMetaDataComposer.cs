using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Navigator.Composers
{
	public class NavigatorMetaDataComposer : IPacketComposer
	{
		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.NavigatorMetaDataMessageComposer);
			message.WriteInteger(4);
			message.WriteString("official_view");
			message.WriteInteger(0);
			message.WriteString("hotel_view");
			message.WriteInteger(0);
			message.WriteString("roomads_view");
			message.WriteInteger(0);
			message.WriteString("myworld_view");
			message.WriteInteger(0);
			return message;
		}
	}
}
