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

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.AchievementProgressMessageComposer);

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

			message.WriteInteger(this.achievement.Id);
			message.WriteInteger(targetLevel);
			message.WriteString("ACH_" + this.achievement.Name + targetLevel);
			message.WriteInteger(currentLevel != null ? currentLevel.Progress : 0);
			message.WriteInteger(nextLevel != null ? nextLevel.Progress : 0);
			message.WriteInteger(nextLevel != null ? nextLevel.RewardAmount : 0);
			message.WriteInteger(nextLevel != null ? nextLevel.RewardType : 0);
			message.WriteInteger(amount);
			message.WriteBoolean(Alias.Server.AchievementManager.HasAchieved(habbo, this.achievement));
			message.WriteString(this.achievement.Category.ToString().ToLower());
			message.WriteString(string.Empty);
			message.WriteInteger(this.achievement.Levels.Count);
			message.WriteInteger(0); //1 = Progressbar visible if the achievement is completed
			return message;
		}
	}
}
