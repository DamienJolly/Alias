using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Composers
{
	public class UserClothesComposer : IPacketComposer
	{
		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserClothesMessageComposer);
			message.WriteInteger(0);
			message.WriteInteger(0);
			return message;
		}
	}
}
