using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Moderation.Composers
{
	public class ModerationTopicsComposer : IPacketComposer
	{
		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.ModerationTopicsMessageComposer);
			message.WriteInteger(1);
			message.WriteString("Sexually Explicit");
			message.WriteInteger(2);
			message.WriteString("test");
			message.WriteInteger(1);
			message.WriteString("testing");
			message.WriteString("test2");
			message.WriteInteger(2);
			message.WriteString("testing2");
			return message;
		}
	}
}
