using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Events
{
	public class SaveIgnoreRoomInvitesEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			bool ignoreInvites = message.PopBoolean();

			session.Habbo.Settings.IgnoreInvites = ignoreInvites;
		}
	}
}
