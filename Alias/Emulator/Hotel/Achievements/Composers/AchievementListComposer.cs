using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Hotel.Users.Achievements;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Achievements.Composers
{
    class AchievementListComposer : IPacketComposer
	{
		private Habbo habbo;

		public AchievementListComposer(Habbo habbo)
		{
			this.habbo = habbo;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.AchievementListMessageComposer);
			message.WriteInteger(Alias.Server.AchievementManager.GetAchievements().Count);

			Alias.Server.AchievementManager.GetAchievements().ForEach(achievement =>
			{
				if (!habbo.Achievements.GetAchievementProgress(achievement.Name, out int amount))
				{
					amount = 0;
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

				message.WriteInteger(achievement.Id);
				message.WriteInteger(targetLevel);
				message.WriteString("ACH_" + achievement.Name + targetLevel);
				message.WriteInteger(currentLevel != null ? currentLevel.Progress : 0);
				message.WriteInteger(nextLevel != null ? nextLevel.Progress : 0);
				message.WriteInteger(nextLevel != null ? nextLevel.RewardAmount : 0);
				message.WriteInteger(nextLevel != null ? nextLevel.RewardType : 0);
				message.WriteInteger(amount);
				message.WriteBoolean(Alias.Server.AchievementManager.HasAchieved(habbo, achievement));
				message.WriteString(achievement.Category.ToString().ToLower());
				message.WriteString(string.Empty);
				message.WriteInteger(achievement.Levels.Count);
				message.WriteInteger(0); //1 = Progressbar visible if the achievement is completed
			});
			message.WriteString(string.Empty);
			return message;
		}
	}
}
