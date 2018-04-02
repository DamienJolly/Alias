using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Messenger.Composers
{
	public class FriendRequestComposer : IPacketComposer
	{
		private MessengerRequest Request;

		public FriendRequestComposer(MessengerRequest request)
		{
			this.Request = request;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.FriendRequestMessageComposer);
			message.WriteInteger(this.Request.Id);
			message.WriteString(this.Request.Username);
			message.WriteString(this.Request.Look);
			return message;
		}
	}
}
