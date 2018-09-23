using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Currency.Composers
{
	class UserCreditsComposer : IPacketComposer
	{
		private readonly Player _player;

		public UserCreditsComposer(Player player)
		{
			_player = player;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserCreditsMessageComposer);
			message.WriteString(_player.Credits + ".0");
			return message;
		}
	}
}
