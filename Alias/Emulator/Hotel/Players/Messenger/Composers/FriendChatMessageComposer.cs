using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Players.Messenger.Composers
{
	public class FriendChatMessageComposer : IPacketComposer
	{
		private int _fromId;
		private string _message;

		public FriendChatMessageComposer(int fromId, string message)
		{
			_fromId = fromId;
			_message = message;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.FriendChatMessageMessageComposer);
			message.WriteInteger(_fromId);
			message.WriteString(_message);
			message.WriteInteger(0); //time
			return message;
		}
	}
}
