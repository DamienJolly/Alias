using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Composers
{
	public class UserEffectsListComposer : IPacketComposer
	{
		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserEffectsListMessageComposer);
			message.WriteInteger(0);
			return message;
		}
	}
}
