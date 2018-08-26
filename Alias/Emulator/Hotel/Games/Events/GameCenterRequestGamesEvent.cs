using Alias.Emulator.Hotel.Games.Composer;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Games.Events
{
    class GameCenterRequestGamesEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			session.Send(new GameCenterAchievementsConfigurationComposer());
		}
	}
}
