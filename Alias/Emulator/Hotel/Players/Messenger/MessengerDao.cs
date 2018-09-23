using System.Collections.Generic;
using System.Threading.Tasks;
using Alias.Emulator.Database;

namespace Alias.Emulator.Hotel.Players.Messenger
{
    internal class MessengerDao : AbstractDao
    {
		internal async Task<Dictionary<int, MessengerFriend>> ReadPlayerFriendshipsAsync(int id)
		{
			Dictionary<int, MessengerFriend> friends = new Dictionary<int, MessengerFriend>();
			await SelectAsync(async reader =>
			{
				while (await reader.ReadAsync())
				{
					MessengerFriend friend = new MessengerFriend(reader);
					if (friends.ContainsKey(friend.Id))
					{
						friends.Add(friend.Id, friend);
					}
				}
			}, "SELECT `habbos`.`id`, `habbos`.`username`, `habbos`.`look`, `habbos`.`motto`, `messenger_friends`.`relation` FROM `messenger_friends` INNER JOIN `habbos` ON `habbos`.`id` = `messenger_friends`.`target_id` WHERE `messenger_friends`.`user_id` = @0", id);
			return friends;
		}

		internal async Task AddPlayerFriendshipAsync(int friendId, int userId)
		{
			await InsertAsync("INSERT INTO `messenger_friends` (`target_id`, `user_id`) VALUES (@0, @1);INSERT INTO `messenger_friends` (`userId`, `target_id`) VALUES (@1, @0);, code, userId);", friendId, userId);
		}

		internal async Task UpdatePlayerFriendshipsAsync(MessengerFriend friend, int userId)
		{
			await InsertAsync("UPDATE `messenger_friends` SET `relation` = @0 WHERE `target_id` = @1 AND `user_id` = @2;", friend.Relation, friend.Id, userId);
		}

		internal async Task RemovePlayerFriendshipAsync(int friendId, int userId)
		{
			await InsertAsync("DELETE FROM `messenger_friends` WHERE (`target_id` = @0 AND `user_id` = @1) OR (`target_id` = @1 AND `user_id` = @0);", friendId, userId);
		}

		internal async Task<Dictionary<int, MessengerRequest>> ReadPlayerFriendRequestsAsync(int id)
		{
			Dictionary<int, MessengerRequest> requests = new Dictionary<int, MessengerRequest>();
			await SelectAsync(async reader =>
			{
				while (await reader.ReadAsync())
				{
					MessengerRequest request = new MessengerRequest(reader);
					if (requests.ContainsKey(request.Id))
					{
						requests.Add(request.Id, request);
					}
				}
			}, "SELECT `habbos`.`id`, `habbos`.`username`, `habbos`.`look` FROM `messenger_requests` INNER JOIN `habbos` ON `habbos`.`id` = `messenger_requests`.`sender` WHERE `messenger_requests`.`reciever` = @0", id);
			return requests;
		}

		internal async Task AddPlayerFriendRequestAsync(int friendId, int userId)
		{
			await InsertAsync("INSERT INTO `messenger_requests` (`sender`, `reciever`) VALUES (@0, @1);", friendId, userId);
		}

		internal async Task RemovePlayerFriendRequestAsync(int friendId, int userId)
		{
			await InsertAsync("DELETE FROM `messenger_requests` WHERE `sender` = @0 AND `reciever` = @1;", friendId, userId);
		}
	}
}
