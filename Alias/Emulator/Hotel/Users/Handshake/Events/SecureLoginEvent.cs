using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Handshake.Events
{
	class SecureLoginEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			Handshake.OnLogin(message.PopString(), session);
		}
	}
}
