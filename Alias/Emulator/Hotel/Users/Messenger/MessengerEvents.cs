using Alias.Emulator.Hotel.Users.Messenger.Events;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;

namespace Alias.Emulator.Hotel.Users.Messenger
{
	public class MessengerEvents
	{
		public static void Register()
		{
			MessageHandler.Register(Incoming.RequestInitFriendsMessageEvent, new RequestInitFriendsEvent());
			MessageHandler.Register(Incoming.SearchUserMessageEvent, new SearchUserEvent());
			MessageHandler.Register(Incoming.FriendRequestMessageEvent, new FriendRequestEvent());
			MessageHandler.Register(Incoming.AcceptFriendRequestMessageEvent, new AcceptFriendRequestEvent());
			MessageHandler.Register(Incoming.DeclineFriendRequestMessageEvent, new DeclineFriendRequestEvent());
			MessageHandler.Register(Incoming.RequestFriendRequestsMessageEvent, new RequestFriendRequestsEvent());
			MessageHandler.Register(Incoming.FriendPrivateMessageMessageEvent, new FriendPrivateMessageEvent());
			MessageHandler.Register(Incoming.RemoveFriendMessageEvent, new RemoveFriendEvent());
			MessageHandler.Register(Incoming.InviteFriendsMessageEvent, new InviteFriendsEvent());
			MessageHandler.Register(Incoming.RequestFriendsMessageEvent, new RequestFriendsEvent());
		}
	}
}
