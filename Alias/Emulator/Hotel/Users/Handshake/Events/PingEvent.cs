using Alias.Emulator.Hotel.Users.Handshake.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Handshake.Events
{
	public class PingEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			session.Send(new PongComposer(message.Integer()));
		}
	}
}
