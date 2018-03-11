using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger
{
	public class MessengerDatabase
	{
		public static List<MessengerFriend> ReadFriendships(int userId)
		{
			List<MessengerFriend> friends = new List<MessengerFriend>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", userId);
				foreach (DataRow row in dbClient.DataTable("SELECT `target_id`, `relation` FROM `messenger_friends` WHERE `user_id` = @id").Rows)
				{
					int targetId = (int)row["target_id"];
					Habbo habbo = SessionManager.HabboById(targetId);
					MessengerFriend friend = new MessengerFriend
					{
						Id       = targetId,
						Username = habbo.Username,
						Look     = habbo.Look,
						Motto    = habbo.Motto,
						InRoom   = habbo.CurrentRoom != null,
						Relation = (int)row["relation"]
					};
					friends.Add(friend);
					row.Delete();
				}
			}
			return friends;
		}
		
		public static List<MessengerRequest> ReadFriendRequests(int userId)
		{
			List<MessengerRequest> requests = new List<MessengerRequest>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", userId);
				foreach (DataRow row in dbClient.DataTable("SELECT `sender` FROM `messenger_requests` WHERE `reciever` = @id").Rows)
				{
					int targetId = (int)row["sender"];
					Habbo habbo = SessionManager.HabboById(targetId);
					MessengerRequest request = new MessengerRequest
					{
						Id       = targetId,
						Username = habbo.Username,
						Look     = habbo.Look
					};
					requests.Add(request);
					row.Delete();
				}
			}
			return requests;
		}

		public static List<Habbo> Search(string query)
		{
			if (!string.IsNullOrEmpty(query))
			{
				query = query.Replace("%", "");
				List<Habbo> result = new List<Habbo>();
				using (DatabaseClient dbClient = DatabaseClient.Instance())
				{
					dbClient.AddParameter("query", "%" + query + "%");
					foreach (DataRow row in dbClient.DataTable("SELECT `id` FROM `habbos` WHERE `username` LIKE @query").Rows)
					{
						result.Add(SessionManager.HabboById((int)row["Id"]));
						row.Delete();
					}
				}
				return result;
			}
			return new List<Habbo>();
		}

		public static void CreateRequest(int sender, int reciever)
		{
			if (sender > 0 && reciever > 0)
			{
				using (DatabaseClient dbClient = DatabaseClient.Instance())
				{
					dbClient.AddParameter("sender", sender);
					dbClient.AddParameter("reciever", reciever);
					dbClient.Query("INSERT INTO `messenger_requests` (`sender`, `reciever`) VALUES (@sender, @reciever)");
				}
			}
		}

		public static bool RequestExists(int userId, int otherUser)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", userId);
				dbClient.AddParameter("other", otherUser);
				return dbClient.DataTable("SELECT `sender` FROM `messenger_requests` WHERE (`sender` = @id AND `reciever` = @other) OR (`reciever` = @id AND `sender` = @other)").Rows.Count > 0;
			}
		}

		public static void RemoveRequest(int Sender, int Reciever)
		{
			if (Sender > 0 && Reciever > 0)
			{
				using (DatabaseClient dbClient = DatabaseClient.Instance())
				{
					dbClient.AddParameter("sender", Sender);
					dbClient.AddParameter("reciever", Reciever);
					dbClient.Query("DELETE FROM `messenger_requests` WHERE `sender` = @sender AND `reciever` = @reciever");
				}
			}
		}

		public static void UpdateRelation(int targetId, int userId, int type)
		{
			if (targetId > 0 && userId > 0 && type > 0)
			{
				using (DatabaseClient dbClient = DatabaseClient.Instance())
				{
					dbClient.AddParameter("targetId", targetId);
					dbClient.AddParameter("userId", userId);
					dbClient.AddParameter("type", type);
					dbClient.Query("UPDATE `messenger_friends` SET `relation` = @type WHERE `target_id` = @targetId AND `user_id` = @userId");
				}
			}
		}

		public static void CreateFriendship(int targetId, int userId)
		{
			if (targetId > 0 && userId > 0)
			{
				using (DatabaseClient dbClient = DatabaseClient.Instance())
				{
					dbClient.AddParameter("targetId", targetId);
					dbClient.AddParameter("userId", userId);
					dbClient.Query("INSERT INTO `messenger_friends` (`target_id`, `user_id`) VALUES (@targetId, @userId)");
					dbClient.Query("INSERT INTO `messenger_friends` (`user_id`, `target_id`) VALUES (@targetId, @userId)");
				}
			}
		}

		public static void RemoveFriend(int targetId, int userId)
		{
			if (targetId > 0 && userId > 0)
			{
				using (DatabaseClient dbClient = DatabaseClient.Instance())
				{
					dbClient.AddParameter("targetId", targetId);
					dbClient.AddParameter("userId", userId);
					dbClient.Query("DELETE FROM `messenger_friends` WHERE (`target_id` = @targetId AND `user_id` = @userId) OR (`target_id` = @userId AND `user_id` = @targetId)");
				}
			}
		}

		public static void StoreMessage(int from, int to, string message)
		{
			if (from > 0 && to > 0 && !string.IsNullOrEmpty(message) && !string.IsNullOrWhiteSpace(message))
			{
				using (DatabaseClient dbClient = DatabaseClient.Instance())
				{
					dbClient.AddParameter("from", from);
					dbClient.AddParameter("to", to);
					dbClient.AddParameter("message", message);
					dbClient.AddParameter("timestamp", (int)AliasEnvironment.GetUnixTimestamp());
					dbClient.Query("INSERT INTO `messenger_chatlogs` (`sender`, `reciever`, `message`, `time`) VALUES (@from, @to, @message, @timestamp)");
				}
			}
		}

		public static void StoreRoomInvitation(int from, string message)
		{
			if (from > 0 && !string.IsNullOrEmpty(message) && !string.IsNullOrWhiteSpace(message))
			{
				using (DatabaseClient dbClient = DatabaseClient.Instance())
				{
					dbClient.AddParameter("from", from);
					dbClient.AddParameter("message", message);
					dbClient.AddParameter("timestamp", (int)AliasEnvironment.GetUnixTimestamp());
					dbClient.Query("INSERT INTO `messenger_roominvitations` (`sender`, `message`, `time`) VALUES (@from, @message, @timestamp)");
				}
			}
		}
	}
}
