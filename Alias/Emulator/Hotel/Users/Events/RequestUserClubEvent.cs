using Alias.Emulator.Hotel.Users.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Events
{
	class RequestUserClubEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (!session.Habbo.HasSubscription)
			{
				return;
			}

			session.Send(new UserClubComposer(session.Habbo.ClubExpireTimestamp));
		}
	}
}
