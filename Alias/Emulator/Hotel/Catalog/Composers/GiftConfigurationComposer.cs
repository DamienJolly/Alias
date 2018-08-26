using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Catalog.Composers
{
	public class GiftConfigurationComposer : IPacketComposer
	{
		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.GiftConfigurationMessageComposer);
			message.WriteBoolean(true);
			message.WriteInteger(10); //?

			message.WriteInteger(0); //wrappers
			{
				//message.WriteInteger(0); //id
			}

			message.WriteInteger(8);
			message.WriteInteger(0);
			message.WriteInteger(1);
			message.WriteInteger(2);
			message.WriteInteger(3);
			message.WriteInteger(4);
			message.WriteInteger(5);
			message.WriteInteger(6);
			message.WriteInteger(8);

			message.WriteInteger(11);
			message.WriteInteger(0);
			message.WriteInteger(1);
			message.WriteInteger(2);
			message.WriteInteger(3);
			message.WriteInteger(4);
			message.WriteInteger(5);
			message.WriteInteger(6);
			message.WriteInteger(7);
			message.WriteInteger(8);
			message.WriteInteger(9);
			message.WriteInteger(10);

			message.WriteInteger(0); // ?
			{
				//message.WriteInteger(0); // ?
			}

			return message;
		}
	}
}
