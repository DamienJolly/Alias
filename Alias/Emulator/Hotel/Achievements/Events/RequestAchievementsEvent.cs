using Alias.Emulator.Hotel.Achievements.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Achievements.Events
{
    public class RequestAchievementsEvent : MessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			session.Send(new AchievementListComposer(session.Habbo()));
		}
    }
}
