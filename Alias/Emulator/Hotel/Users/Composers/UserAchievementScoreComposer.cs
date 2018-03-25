using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Composers
{
	public class UserAchievementScoreComposer : IPacketComposer
	{
		private int achievementScore;

		public UserAchievementScoreComposer(int achievementScore)
		{
			this.achievementScore = achievementScore;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.UserAchievementScoreMessageComposer);
			result.Int(this.achievementScore);
			return result;
		}
	}
}
