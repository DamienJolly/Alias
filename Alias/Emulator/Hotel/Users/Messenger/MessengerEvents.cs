using Alias.Emulator.Hotel.Users.Messenger.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Users.Messenger
{
	public class MessengerEvents
	{
		public static void Register()
		{
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestInitFriendsMessageEvent, new RequestInitFriendsEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.SearchUserMessageEvent, new SearchUserEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.FriendRequestMessageEvent, new FriendRequestEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.AcceptFriendRequestMessageEvent, new AcceptFriendRequestEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.DeclineFriendRequestMessageEvent, new DeclineFriendRequestEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestFriendRequestsMessageEvent, new RequestFriendRequestsEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.FriendPrivateMessageMessageEvent, new FriendPrivateMessageEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RemoveFriendMessageEvent, new RemoveFriendEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.InviteFriendsMessageEvent, new InviteFriendsEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.RequestFriendsMessageEvent, new RequestFriendsEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.FindNewFriendsMessageEvent, new FindNewFriendsEvent());
			Alias.GetServer().GetPacketManager().Register(Incoming.ChangeRelationMessageEvent, new ChangeRelationEvent());
		}
	}
}
