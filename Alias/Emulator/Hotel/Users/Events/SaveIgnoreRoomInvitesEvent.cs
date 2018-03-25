using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Events
{
	public class SaveIgnoreRoomInvitesEvent : IPacketEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			bool ignoreInvites = message.Boolean();

			session.Habbo.Settings.IgnoreInvites = ignoreInvites;
		}
	}
}
