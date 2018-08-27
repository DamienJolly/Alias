using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Hotel.Users.Achievements;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Achievements.Composers
{
    class AchievementUnlockedComposer : IPacketComposer
	{
		private Habbo habbo;
		private Achievement achievement;

		public AchievementUnlockedComposer(Habbo habbo, Achievement achievement)
		{
			this.habbo = habbo;
			this.achievement = achievement;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.AchievementUnlockedMessageComposer);

			if (!habbo.Achievements.GetAchievementProgress(this.achievement.Name, out int progress))
			{
				progress = 0;
			}
			AchievementLevel level = achievement.GetLevelForProgress(progress);
			message.WriteInteger(this.achievement.Id);
			message.WriteInteger(level.Level);
			message.WriteInteger(144);
			message.WriteString("ACH_" + this.achievement.Name + level.Level);
			message.WriteInteger(level.RewardAmount);
			message.WriteInteger(level.RewardType);
			message.WriteInteger(0);
			message.WriteInteger(10);
			message.WriteInteger(21);
			message.WriteString(level.Level > 1 ? "ACH_" + this.achievement.Name + (level.Level - 1) : "");
			message.WriteString(this.achievement.Category.ToString().ToLower());
			message.WriteBoolean(true);
			return message;
		}
	}
}
