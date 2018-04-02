using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Messenger.Composers
{
	public class MessengerInitComposer : IPacketComposer
	{
		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.MessengerInitMessageComposer);
			message.WriteInteger(Constant.MaximalFriends);
			message.WriteInteger(300);
			message.WriteInteger(800);
			message.WriteInteger(0);
			message.WriteBoolean(true);
			return message;
		}
	}
}
