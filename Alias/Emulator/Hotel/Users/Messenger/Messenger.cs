using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Users.Messenger.Composers;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger
{
	public class Messenger
	{
		private List<MessengerFriend> Friends;
		private List<MessengerRequest> Requests;
		private Habbo habbo;

		public Messenger(Habbo h)
		{
			this.Friends = new List<MessengerFriend>();
			this.Requests = new List<MessengerRequest>();
			this.habbo = h;
		}

		public void UpdateStatus(bool notif)
		{
			foreach (MessengerFriend friend in this.FriendList())
			{
				if (SessionManager.IsOnline(friend.Id))
				{
					SessionManager.SessionById(friend.Id).Habbo().Messenger().Update(this.Habbo().Id);
				}
			}
		}

		public void Update(int userId)
		{
			if (this.IsFriend(userId))
			{
				this.Habbo().Session().Send(new UpdateFriendComposer(this.Friend(userId)));
			}
		}

		public void Request(string username)
		{
			int userId = UserDatabase.Id(username);

			if (userId == 0 || this.RequestExists(userId))
			{
				return;
			}

			MessengerDatabase.CreateRequest(this.Habbo().Id, userId);

			if (SessionManager.IsOnline(userId))
			{
				SessionManager.SessionById(userId).Habbo().Messenger().OnRequest(this.Habbo());
			}
		}

		public void OnRequest(Habbo h)
		{
			MessengerRequest request = new MessengerRequest()
			{
				Id       = h.Id,
				Username = h.Username,
				Look     = h.Look
			};
			this.RequestList().Add(request);
			this.Habbo().Session().Send(new FriendRequestComposer(request));
		}

		public void RemoveRequest(int UserId)
		{
			if (this.RequestExists(UserId))
			{
				this.RequestList().Remove(this.RequestList().Where(o => o.Id == UserId).First());
			}
		}

		public void Accept(int UserId)
		{
			if (this.RequestExists(UserId) && !this.IsFriend(UserId))
			{
				MessengerDatabase.RemoveRequest(UserId, this.Habbo().Id);
				MessengerDatabase.CreateFriendship(UserId, this.Habbo().Id);
				this.RemoveRequest(UserId);
				this.OnFriend(UserId);

				if (SessionManager.IsOnline(UserId))
				{
					SessionManager.SessionById(UserId).Habbo().Messenger().OnFriend(this.Habbo().Id);
				}
			}
		}

		public void OnFriend(int UserId)
		{
			if (!this.IsFriend(UserId))
			{
				MessengerFriend friend = new MessengerFriend();
				Habbo h                = SessionManager.Habbo(UserId);
				friend.Id              = h.Id;
				friend.Username        = h.Username;
				friend.Look            = h.Look;
				friend.Motto           = h.Motto;
				friend.FriendWith      = this.Habbo().Id;
				this.FriendList().Add(friend);
				this.Habbo().Session().Send(new UpdateFriendComposer(friend));
			}
		}

		public void RemoveFriend(int friend)
		{
			if (this.IsFriend(friend))
			{
				MessengerDatabase.RemoveFriend(friend, this.Habbo().Id);
				this.OnRemoveFriend(friend);

				if (SessionManager.IsOnline(friend))
				{
					SessionManager.SessionById(friend).Habbo().Messenger().OnRemoveFriend(this.Habbo().Id);
				}
			}
		}

		public void OnRemoveFriend(int friendId)
		{
			this.FriendList().Remove(this.FriendList().Where(o => o.Id == friendId).First());
			this.Habbo().Session().Send(new UpdateFriendComposer(friendId));
		}

		public void Decline(int UserId)
		{
			if (this.RequestExists(UserId) && !this.IsFriend(UserId))
			{
				MessengerDatabase.RemoveRequest(UserId, this.Habbo().Id);
				this.RemoveRequest(UserId);
			}
		}

		public void DeclineAll()
		{
			this.RequestList().ForEach(req =>
			{
				if (this.RequestExists(req.Id) && !this.IsFriend(req.Id))
				{
					MessengerDatabase.RemoveRequest(req.Id, this.Habbo().Id);
				}
			});
			this.RequestList().Clear();
		}

		public void Message(int toUser, string message)
		{
			if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message) || message.Length > 200)
			{
				return;
			}

			if (this.Habbo().Muted)
			{
				this.Habbo().Session().Send(new RoomInviteErrorComposer(RoomInviteErrorComposer.YOU_ARE_MUTED, toUser));
				return;
			}

			if (this.IsFriend(toUser) && SessionManager.IsOnline(toUser))
			{
				SessionManager.SessionById(toUser).Habbo().Messenger().OnMessage(this.Habbo().Id, message);

				if (SessionManager.SessionById(toUser).Habbo().Muted)
				{
					this.Habbo().Session().Send(new RoomInviteErrorComposer(RoomInviteErrorComposer.FRIEND_MUTED, toUser));
				}
			}
			else if (this.IsFriend(toUser) && !SessionManager.IsOnline(toUser))
			{
				//todo: Offline messages
			}
			else if (!this.IsFriend(toUser))
			{
				this.Habbo().Session().Send(new RoomInviteErrorComposer(RoomInviteErrorComposer.NO_FRIENDS, toUser));
			}
		}

		public void RoomInvitation(List<int> friends, string message)
		{
			if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message) || message.Length > 200)
			{
				return;
			}
			
			friends.ForEach(id =>
			{
				if (this.Habbo().Messenger().IsFriend(id) && SessionManager.IsOnline(id))
				{
					SessionManager.SessionById(id).Send(new RoomInviteComposer(this.Habbo().Id, message), false);
				}
			});

			MessengerDatabase.StoreRoomInvitation(this.Habbo().Id, message);
		}

		public void OnMessage(int fromUser, string message)
		{
			if (this.IsFriend(fromUser))
			{
				MessengerDatabase.StoreMessage(fromUser, this.Habbo().Id, message);
				this.Habbo().Session().Send(new FriendChatMessageComposer(fromUser, message));
			}
		}

		public bool RequestExists(int userId)
		{
			return this.RequestList().Where(o => o.Id == userId).Count() > 0 ? true : MessengerDatabase.RequestExists(userId, this.Habbo().Id);
		}

		public bool IsFriend(int userId)
		{
			return this.FriendList().Where(o => o.Id == userId).Count() > 0;
		}

		public MessengerFriend Friend(int userId)
		{
			return this.FriendList().Where(o => o.Id == userId).First();
		}

		public Habbo Habbo()
		{
			return this.habbo;
		}

		public List<MessengerFriend> FriendList()
		{
			return this.Friends;
		}

		public List<MessengerRequest> RequestList()
		{
			return this.Requests;
		}
	}
}
