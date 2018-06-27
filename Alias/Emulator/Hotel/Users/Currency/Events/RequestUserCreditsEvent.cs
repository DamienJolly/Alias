using Alias.Emulator.Hotel.Users.Currency.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Currency.Events
{
	class RequestUserCreditsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			session.Send(new UserCreditsComposer(session.Habbo));
			session.Send(new UserCurrencyComposer(session.Habbo));
		}
	}
}
