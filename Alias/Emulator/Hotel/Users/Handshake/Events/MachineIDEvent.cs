using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Handshake.Events
{
	public class MachineIDEvent : IPacketEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			message.String();
			session.UniqueId = message.String();
		}
	}
}
