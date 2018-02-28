using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Handshake.Events
{
	public class MachineIDEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			message.String();
			session.UniqueId = message.String();
		}
	}
}
