using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Events
{
	class SaveIgnoreRoomInvitesEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			bool ignoreInvites = message.PopBoolean();

			session.Player.Settings.IgnoreInvites = ignoreInvites;
		}
	}
}
