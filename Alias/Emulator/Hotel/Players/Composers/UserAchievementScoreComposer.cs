using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Composers
{
	public class UserAchievementScoreComposer : IPacketComposer
	{
		private int _achievementScore;

		public UserAchievementScoreComposer(int achievementScore)
		{
			_achievementScore = achievementScore;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserAchievementScoreMessageComposer);
			message.WriteInteger(_achievementScore);
			return message;
		}
	}
}
