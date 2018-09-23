using System.Collections.Generic;
using System.Threading.Tasks;
using Alias.Emulator.Hotel.Players.Messenger.Composers;

namespace Alias.Emulator.Hotel.Players.Messenger
{
    internal class MessengerComponent
    {
		private readonly MessengerDao _dao;
		private readonly Player _player;

		public IDictionary<int, MessengerRequest> Requests { get; set; }
		public IDictionary<int, MessengerFriend> Friends { get; set; }

		internal MessengerComponent(MessengerDao dao, Player player)
		{
			_dao = dao;
			_player = player;
			Requests = new Dictionary<int, MessengerRequest>();
			Friends = new Dictionary<int, MessengerFriend>();
		}

		public async Task Initialize()
		{
			Requests = await _dao.ReadPlayerFriendRequestsAsync(_player.Id);
			Friends = await _dao.ReadPlayerFriendshipsAsync(_player.Id);
		}

		public async Task UpdateStatus()
		{
			foreach (MessengerFriend i in Friends.Values)
			{
				Player targetPlayer = await Alias.Server.PlayerManager.ReadPlayerByIdAsync(i.Id);
				if (targetPlayer == null || targetPlayer.Session == null)
				{
					continue;
				}

				if (targetPlayer.Messenger.TryGetFriend(_player.Id, out MessengerFriend friend))
				{
					friend.Look = _player.Look;
					friend.Username = _player.Username;
					friend.Motto = _player.Motto;
					friend.InRoom = _player.CurrentRoom != null;
					targetPlayer.Session.Send(new UpdateFriendComposer(friend));
				}
			}
		}

		public async Task AddFriendShip(MessengerFriend friend)
		{
			if (!Friends.ContainsKey(friend.Id))
			{
				Friends.Add(friend.Id, friend);
				await _dao.AddPlayerFriendshipAsync(friend.Id, _player.Id);
			}
		}

		public async Task UpdateRelation(MessengerFriend friend)
		{
			await _dao.UpdatePlayerFriendshipsAsync(friend, _player.Id);
		}

		public async Task RemoveFriend(int targetId)
		{
			if (Friends.ContainsKey(targetId))
			{
				Friends.Remove(targetId);
				await _dao.RemovePlayerFriendshipAsync(targetId, _player.Id);
			}
		}

		public async Task AddRequest(MessengerRequest request, int targetId)
		{
			if (!Requests.ContainsKey(request.Id))
			{
				await _dao.AddPlayerFriendRequestAsync(request.Id, targetId);
				Requests.Add(request.Id, request);
			}
		}

		public async Task RemoveRequest(int id)
		{
			Requests.Remove(id);
			await _dao.RemovePlayerFriendRequestAsync(id, _player.Id);
		}

		public bool TryGetFriend(int id, out MessengerFriend friend) => Friends.TryGetValue(id, out friend);

		public bool TryGetRequest(int id, out MessengerRequest request) => Requests.TryGetValue(id, out request);
	}
}
