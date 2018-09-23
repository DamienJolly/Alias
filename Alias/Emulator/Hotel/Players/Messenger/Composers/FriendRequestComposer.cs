using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Messenger.Composers
{
	public class FriendRequestComposer : IPacketComposer
	{
		private MessengerRequest _request;

		public FriendRequestComposer(MessengerRequest request)
		{
			_request = request;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.FriendRequestMessageComposer);
			message.WriteInteger(_request.Id);
			message.WriteString(_request.Username);
			message.WriteString(_request.Look);
			return message;
		}
	}
}
