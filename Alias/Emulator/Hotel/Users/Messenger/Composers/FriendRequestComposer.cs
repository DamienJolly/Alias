using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Messenger.Composers
{
	public class FriendRequestComposer : IMessageComposer
	{
		private MessengerRequest Request;

		public FriendRequestComposer(MessengerRequest request)
		{
			this.Request = request;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.FriendRequestMessageComposer);
			message.Int(this.Request.Id);
			message.String(this.Request.Username);
			message.String(this.Request.Look);
			return message;
		}
	}
}