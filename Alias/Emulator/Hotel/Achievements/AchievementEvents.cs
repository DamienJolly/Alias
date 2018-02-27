using Alias.Emulator.Hotel.Achievements.Events;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;

namespace Alias.Emulator.Hotel.Achievements
{
    public class AchievementEvents
    {
		public static void Register()
		{
			MessageHandler.Register(Incoming.RequestAchievementsMessageEvent, new RequestAchievementsEvent());
		}
	}
}
