using Alias.Emulator.Hotel.Achievements.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Achievements.Events
{
    class RequestAchievementsEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			session.Send(new AchievementListComposer(session.Habbo));
		}
    }
}
