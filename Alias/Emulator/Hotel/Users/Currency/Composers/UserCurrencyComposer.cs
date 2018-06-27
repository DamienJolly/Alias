using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Currency.Composers
{
	class UserCurrencyComposer : IPacketComposer
	{
		private Habbo habbo;

		public UserCurrencyComposer(Habbo habbo)
		{
			this.habbo = habbo;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserCurrencyMessageComposer);
			message.WriteInteger(habbo.Currency.RequestCurrencies().Count);
			habbo.Currency.RequestCurrencies().ForEach(currency =>
			{
				message.WriteInteger(currency.Type);
				message.WriteInteger(currency.Amount);
			});
			return message;
		}
	}
}
