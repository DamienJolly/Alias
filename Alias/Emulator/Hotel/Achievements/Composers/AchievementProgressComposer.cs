using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Hotel.Users.Achievements;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Achievements.Composers
{
    public class AchievementProgressComposer : IPacketComposer
	{
		private Habbo habbo;
		private Achievement achievement;

		public AchievementProgressComposer(Habbo habbo, Achievement achievement)
		{
			this.habbo = habbo;
			this.achievement = achievement;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.AchievementProgressMessageComposer);

			int amount = 0;
			AchievementProgress achievementProgress = habbo.Achievements.GetAchievementProgress(this.achievement);
			if (achievementProgress != null)
			{
				amount = achievementProgress.Progress;
			}

			AchievementLevel currentLevel = achievement.GetLevelForProgress(amount);
			AchievementLevel nextLevel = achievement.GetNextLevel(currentLevel != null ? currentLevel.Level : 0);

			if (currentLevel != null && currentLevel.Level == achievement.Levels.Count)
			{
				nextLevel = null;
			}

			int targetLevel = 1;

			if (nextLevel != null)
			{
				targetLevel = nextLevel.Level;
			}

			if (currentLevel != null && currentLevel.Level == achievement.Levels.Count)
			{
				targetLevel = currentLevel.Level;
			}

			result.Int(this.achievement.Id);
			result.Int(targetLevel);
			result.String("ACH_" + this.achievement.Name + targetLevel);
			result.Int(currentLevel != null ? currentLevel.Progress : 0);
			result.Int(nextLevel != null ? nextLevel.Progress : 0);
			result.Int(nextLevel != null ? nextLevel.RewardAmount : 0);
			result.Int(nextLevel != null ? nextLevel.RewardType : 0);
			result.Int(amount);
			result.Boolean(Alias.GetServer().GetAchievementManager().HasAchieved(habbo, this.achievement));
			result.String(this.achievement.Category.ToString().ToLower());
			result.String(string.Empty);
			result.Int(this.achievement.Levels.Count);
			result.Int(0); //1 = Progressbar visible if the achievement is completed
			return result;
		}
	}
}
