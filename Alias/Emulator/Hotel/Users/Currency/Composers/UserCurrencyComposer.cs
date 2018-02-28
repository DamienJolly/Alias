using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Currency.Composers
{
	public class UserCurrencyComposer : IMessageComposer
	{
		private Habbo habbo;

		public UserCurrencyComposer(Habbo habbo)
		{
			this.habbo = habbo;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.UserCurrencyMessageComposer);
			result.Int(habbo.Currency.RequestCurrencies().Count);
			habbo.Currency.RequestCurrencies().ForEach(currency =>
			{
				result.Int(currency.Type);
				result.Int(currency.Amount);
			});
			return result;
		}
	}
}
