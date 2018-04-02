using Alias.Emulator.Hotel.Users.Messenger.Events;
using Alias.Emulator.Network.Packets.Headers;

namespace Alias.Emulator.Hotel.Users.Messenger
{
	public class MessengerEvents
	{
		public static void Register()
		{
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestInitFriendsMessageEvent, new RequestInitFriendsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.SearchUserMessageEvent, new SearchUserEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.FriendRequestMessageEvent, new FriendRequestEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.AcceptFriendRequestMessageEvent, new AcceptFriendRequestEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.DeclineFriendRequestMessageEvent, new DeclineFriendRequestEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestFriendRequestsMessageEvent, new RequestFriendRequestsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.FriendPrivateMessageMessageEvent, new FriendPrivateMessageEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RemoveFriendMessageEvent, new RemoveFriendEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.InviteFriendsMessageEvent, new InviteFriendsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.RequestFriendsMessageEvent, new RequestFriendsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.FindNewFriendsMessageEvent, new FindNewFriendsEvent());
			Alias.Server.SocketServer.PacketManager.Register(Incoming.ChangeRelationMessageEvent, new ChangeRelationEvent());
		}
	}
}
