using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Landing.Composers
{
	public class BonusRareComposer : IPacketComposer
	{
		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.BonusRareMessageComposer);
			message.WriteString("prizetrophy_breed_gold");
			message.WriteInteger(230);
			message.WriteInteger(120);
			message.WriteInteger(120);
			return message;
		}
	}
}
