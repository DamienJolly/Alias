using Alias.Emulator.Hotel.Catalog.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Catalog.Events
{
	public class RequestDiscountEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			session.Send(new DiscountComposer());
		}
	}
}
