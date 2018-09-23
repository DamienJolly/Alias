using Alias.Emulator.Hotel.Players.Currency.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Currency.Events
{
	class RequestUserCreditsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			session.Send(new UserCreditsComposer(session.Player));
			session.Send(new UserCurrencyComposer(session.Player));
		}
	}
}
