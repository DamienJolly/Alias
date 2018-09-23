using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Currency.Composers
{
	class UserCurrencyComposer : IPacketComposer
	{
		private readonly Player _player;

		public UserCurrencyComposer(Player player)
		{
			_player = player;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserCurrencyMessageComposer);
			message.WriteInteger(_player.Currency.Currencies.Count);
			foreach (CurrencyType currency in _player.Currency.Currencies.Values)
			{
				message.WriteInteger(currency.Type);
				message.WriteInteger(currency.Amount);
			}
			return message;
		}
	}
}
