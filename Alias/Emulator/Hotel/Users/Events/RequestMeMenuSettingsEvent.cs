using Alias.Emulator.Hotel.Users.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Events
{
	public class RequestMeMenuSettingsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			session.Send(new MeMenuSettingsComposer(session.Habbo));
		}
	}
}
