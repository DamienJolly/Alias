using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Composers
{
	class UserPermissionsComposer : IPacketComposer
	{
		private Player _player;

		public UserPermissionsComposer(Player player)
		{
			_player = player;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserPermissionsMessageComposer);
			message.WriteInteger(_player.HasSubscription ? 2 : 0);
			message.WriteInteger(_player.Rank);
			message.WriteBoolean(false); //ambassador
			return message;
		}
	}
}
