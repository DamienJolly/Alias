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

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserAchievementScoreMessageComposer);
			message.WriteInteger(this.achievementScore);
			return message;
		}
	}
}
