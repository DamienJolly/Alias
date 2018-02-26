using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Messenger.Composers
{
	public class FriendChatMessageComposer : MessageComposer
	{
		private int FromId;
		private string Message;

		public FriendChatMessageComposer(int fromId, string message)
		{
			this.FromId = fromId;
			this.Message = message;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.FriendChatMessageMessageComposer);
			message.Int(this.FromId);
			message.String(this.Message);
			message.Int(0); //time
			return message;
		}
	}
}
