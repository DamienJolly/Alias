using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Composers
{
	public class UserAchievementScoreComposer : IMessageComposer
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
