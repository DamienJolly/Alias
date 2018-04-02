using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Navigator.Composers
{
	public class PrivateRoomsComposer : IPacketComposer
	{
		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.PrivateRoomsMessageComposer);
			message.WriteInteger(2);
			message.WriteString("");
			message.WriteInteger(0);
			message.WriteBoolean(true);
			message.WriteInteger(0);
			message.WriteString("A");
			message.WriteString("B");
			message.WriteInteger(1);
			message.WriteString("C");
			message.WriteString("D");
			message.WriteInteger(1);
			message.WriteInteger(1);
			message.WriteInteger(1);
			message.WriteString("E");
			return message;
		}
	}
}
