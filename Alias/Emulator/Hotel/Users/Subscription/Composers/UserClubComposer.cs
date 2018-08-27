using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Subscription.Composers
{
    class UserClubComposer : IPacketComposer
	{
		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.UserClubMessageComposer);
			message.WriteString("club_habbo");
			message.WriteInteger(0);
			message.WriteInteger(2);
			message.WriteInteger(0);
			message.WriteInteger(1);
			message.WriteBoolean(true);
			message.WriteBoolean(true);
			message.WriteInteger(0);
			message.WriteInteger(0);
			message.WriteInteger(int.MaxValue);
			return message;
		}
	}
}
