using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;
using Alias.Emulator.Hotel.Users.Composers;

namespace Alias.Emulator.Hotel.Users.Handshake.Events
{
	public class RequestUserDataEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			session.Send(new UserDataComposer(session.Habbo));
			session.Send(new UserPerksComposer(session.Habbo));
		}
	}
}
