using Alias.Emulator.Hotel.Players.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Players.Events
{
	class RequestUserClubEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (!session.Player.HasSubscription)
			{
				return;
			}

			session.Send(new UserClubComposer(session.Player.ClubExpireTimestamp));
		}
	}
}
