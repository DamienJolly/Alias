using Alias.Emulator.Hotel.Misc.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Misc.Events
{
	class PongEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			session.Send(new PingComposer(message.PopInt()));
		}
	}
}
