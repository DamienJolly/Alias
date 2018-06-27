using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Handshake.Events
{
	class MachineIDEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			message.PopString();
			session.UniqueId = message.PopString();
		}
	}
}
