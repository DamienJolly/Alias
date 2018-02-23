using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Hotel.Users.Handshake.Composers;

namespace Alias.Emulator.Hotel.Users.Handshake.Events
{
	public class RequestUserDataEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			session.Send(new UserDataComposer(session.Habbo()));
			session.Send(new UserPerksComposer(session.Habbo()));
		}
	}
}
