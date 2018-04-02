using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Messenger.Composers
{
	public class FriendChatMessageComposer : IPacketComposer
	{
		private int FromId;
		private string Message;

		public FriendChatMessageComposer(int fromId, string message)
		{
			this.FromId = fromId;
			this.Message = message;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.FriendChatMessageMessageComposer);
			message.WriteInteger(this.FromId);
			message.WriteString(this.Message);
			message.WriteInteger(0); //time
			return message;
		}
	}
}
