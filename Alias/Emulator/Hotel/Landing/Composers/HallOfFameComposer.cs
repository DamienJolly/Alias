using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Landing.Composers
{
	public class HallOfFameComposer : IPacketComposer
	{
		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.HallOfFameMessageComposer);
			message.WriteString("testing");
			message.WriteInteger(1);
			message.WriteInteger(1);
			message.WriteString("Damien");
			message.WriteString("ha-1006-64.lg-275-64.hd-209-1370.ch-3030-82");
			message.WriteInteger(1);
			message.WriteInteger(69);
			return message;
		}
	}
}
