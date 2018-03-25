using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Messenger.Composers
{
	public class MessengerInitComposer : IPacketComposer
	{
		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.MessengerInitMessageComposer);
			message.Int(Constant.MaximalFriends);
			message.Int(300);
			message.Int(800);
			message.Int(0);
			message.Boolean(true);
			return message;
		}
	}
}
