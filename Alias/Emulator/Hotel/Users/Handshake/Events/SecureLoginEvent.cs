using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Handshake.Events
{
	public class SecureLoginEvent : IPacketEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			Handshake.OnLogin(message.String(), session);
		}
	}
}
