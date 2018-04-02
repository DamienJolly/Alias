using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Composers
{
	public class UserPermissionsComposer : IPacketComposer
	{
		int Rank;

		public UserPermissionsComposer(int rank)
		{
			this.Rank = rank;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserPermissionsMessageComposer);
			message.WriteInteger(2); //club level
			message.WriteInteger(this.Rank);
			message.WriteBoolean(false); //ambassador
			return message;
		}
	}
}
