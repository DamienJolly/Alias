using Alias.Emulator.Hotel.Achievements.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Achievements
{
    public class AchievementEvents
    {
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestAchievementsMessageEvent, new RequestAchievementsEvent());
		}
	}
}
