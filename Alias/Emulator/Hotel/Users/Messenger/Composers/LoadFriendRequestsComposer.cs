using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Users.Messenger.Composers
{
	public class LoadFriendRequestsComposer : IMessageComposer
	{
		private MessengerComponent messenger;

		public LoadFriendRequestsComposer(MessengerComponent messenger)
		{
			this.messenger = messenger;
		}

		public ServerMessage Compose()
		{
			ServerMessage message = new ServerMessage(Outgoing.LoadFriendRequestsMessageComposer);
			message.Int(this.messenger.RequestList().Count);
			message.Int(this.messenger.RequestList().Count);
			this.messenger.RequestList().ForEach(req =>
			{
				message.Int(req.Id);
				message.String(req.Username);
				message.String(req.Look);
			});
			return message;
		}
	}
}
