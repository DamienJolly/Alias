using Alias.Emulator.Hotel.Players;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Achievements.Composers
{
    class AchievementProgressComposer : IPacketComposer
	{
		private Player habbo;
		private Achievement achievement;

		public AchievementProgressComposer(Player habbo, Achievement achievement)
		{
			this.habbo = habbo;
			this.achievement = achievement;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.AchievementProgressMessageComposer);
			
			if (!habbo.Achievements.GetAchievementProgress(this.achievement.Id, out int amount))
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

			message.WriteInteger(this.achievement.Id);
			message.WriteInteger(targetLevel);
			message.WriteString("ACH_" + this.achievement.Name + targetLevel);
			message.WriteInteger(currentLevel != null ? currentLevel.Progress : 0);
			message.WriteInteger(nextLevel != null ? nextLevel.Progress : 0);
			message.WriteInteger(nextLevel != null ? nextLevel.RewardAmount : 0);
			message.WriteInteger(nextLevel != null ? nextLevel.RewardType : 0);
			message.WriteInteger(amount);
			message.WriteBoolean(this.habbo.Achievements.HasAchieved(this.achievement));
			message.WriteString(this.achievement.Category.ToString().ToLower());
			message.WriteString(string.Empty);
			message.WriteInteger(this.achievement.Levels.Count);
			message.WriteInteger(0); //1 = Progressbar visible if the achievement is completed
			return message;
		}
	}
}
