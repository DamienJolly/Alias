using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Composers
{
	public class SomeConnectionComposer : IPacketComposer
	{
		public ServerPacket Compose()
		{
			return new ServerPacket(Outgoing.SomeConnectionMessageComposer);
		}
	}
}
