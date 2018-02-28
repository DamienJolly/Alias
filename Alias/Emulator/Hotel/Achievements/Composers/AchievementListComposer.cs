using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Hotel.Users.Achievements;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Achievements.Composers
{
    public class AchievementListComposer : IMessageComposer
	{
		private Habbo habbo;

		public AchievementListComposer(Habbo habbo)
		{
			this.habbo = habbo;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.AchievementListMessageComposer);
			result.Int(AchievementManager.GetAchievements().Count);

			AchievementManager.GetAchievements().ForEach(achievement =>
			{
				int amount = 0;
				AchievementProgress achievementProgress = habbo.Achievements.GetAchievementProgress(achievement);
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

				result.Int(achievement.Id);
				result.Int(targetLevel);
				result.String("ACH_" + achievement.Name + targetLevel);
				result.Int(currentLevel != null ? currentLevel.Progress : 0);
				result.Int(nextLevel != null ? nextLevel.Progress : 0);
				result.Int(nextLevel != null ? nextLevel.RewardAmount : 0);
				result.Int(nextLevel != null ? nextLevel.RewardType : 0);
				result.Int(amount);
				result.Boolean(AchievementManager.HasAchieved(habbo, achievement));
				result.String(achievement.Category.ToString().ToLower());
				result.String(string.Empty);
				result.Int(achievement.Levels.Count);
				result.Int(0); //1 = Progressbar visible if the achievement is completed
			});
			result.String(string.Empty);
			return result;
		}
	}
}
