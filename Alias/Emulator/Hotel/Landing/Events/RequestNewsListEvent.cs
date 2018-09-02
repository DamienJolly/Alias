using Alias.Emulator.Hotel.Landing.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Landing.Events
{
	class RequestNewsListEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			session.Send(new HotelViewDataComposer("2013-05-08 13:0", "gamesmaker"));

			if (int.TryParse(Alias.Server.Settings.GetSetting("competition.id"), out int competitionId))
			{
				if (Alias.Server.LandingManager.TryGetCompetition(competitionId, out LandingCompetition competition))
				{
					session.Send(new HallOfFameComposer(competition));
				}
			}

			session.Send(new NewsListComposer(Alias.Server.LandingManager.Articles));
		}
	}
}
