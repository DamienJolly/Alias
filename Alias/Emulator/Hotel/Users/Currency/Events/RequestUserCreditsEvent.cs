using Alias.Emulator.Hotel.Users.Currency.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Currency.Events
{
	public class RequestUserCreditsEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			session.Send(new UserCreditsComposer(session.Habbo()));
			session.Send(new UserCurrencyComposer(session.Habbo()));
		}
	}
}
