using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Composers
{
	class UpdateUserLookComposer : IPacketComposer
	{
		private Player _player;

		public UpdateUserLookComposer(Player player)
		{
			_player = player;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UpdateUserLookMessageComposer);
			message.WriteString(_player.Look);
			message.WriteString(_player.Gender);
			return message;
		}
	}
}
