using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Composers
{
	class UserPermissionsComposer : IPacketComposer
	{
		private Habbo habbo;

		public UserPermissionsComposer(Habbo habbo)
		{
			this.habbo = habbo;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserPermissionsMessageComposer);
			message.WriteInteger(this.habbo.HasSubscription ? 2 : 0);
			message.WriteInteger(this.habbo.Rank);
			message.WriteBoolean(false); //ambassador
			return message;
		}
	}
}
