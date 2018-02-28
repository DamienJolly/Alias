using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Hotel.Users.Achievements;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Achievements.Composers
{
    public class AchievementUnlockedComposer : IMessageComposer
	{
		private Habbo habbo;
		private Achievement achievement;

		public AchievementUnlockedComposer(Habbo habbo, Achievement achievement)
		{
			this.habbo = habbo;
			this.achievement = achievement;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.AchievementUnlockedMessageComposer);
			
			AchievementProgress achievementProgress = habbo.Achievements.GetAchievementProgress(this.achievement);
			AchievementLevel level = achievement.GetLevelForProgress(achievementProgress.Progress);
			result.Int(this.achievement.Id);
			result.Int(level.Level);
			result.Int(144);
			result.String("ACH_" + this.achievement.Name + level.Level);
			result.Int(level.RewardAmount);
			result.Int(level.RewardType);
			result.Int(0);
			result.Int(10);
			result.Int(21);
			result.String(level.Level > 1 ? "ACH_" + this.achievement.Name + (level.Level - 1) : "");
			result.String(this.achievement.Category.ToString().ToLower());
			result.Boolean(true);
			return result;
		}
	}
}
