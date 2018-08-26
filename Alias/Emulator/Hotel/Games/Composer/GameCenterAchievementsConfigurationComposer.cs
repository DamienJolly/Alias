using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Games.Composer
{
    class GameCenterAchievementsConfigurationComposer : IPacketComposer
	{
		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.GameCenterAchievementsConfigurationMessageComposer);
			message.WriteInteger(0);
			message.WriteInteger(0);
			message.WriteInteger(0);
			message.WriteInteger(0);
			message.WriteInteger(0);
			message.WriteInteger(0);
			message.WriteInteger(0);
			message.WriteInteger(0);
			message.WriteInteger(0);
			message.WriteInteger(0);
			message.WriteInteger(3);
			message.WriteInteger(1);
			message.WriteInteger(1);
			message.WriteString("BaseJumpBigParachute");
			return message;
		}
    }
}
