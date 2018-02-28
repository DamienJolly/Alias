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
				foreach (DataRow row in dbClient.DataTable("SELECT `user_one`, `user_two` FROM `messenger_friends` WHERE `user_one` = @id OR `user_two` = @id").Rows)
				{
					int targetId = (int)row["user_one"] == userId ? (int)row["user_two"] : (int)row["user_one"];
					Habbo habbo = SessionManager.HabboById(targetId);
					MessengerFriend friend = new MessengerFriend
					{
						Id       = targetId,
						Username = habbo.Username,
						Look     = habbo.Look,
						Motto    = habbo.Motto,
						InRoom   = habbo.CurrentRoom != null
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

		public static void CreateFriendship(int UserOne, int UserTwo)
		{
			if (UserOne > 0 && UserTwo > 0)
			{
				using (DatabaseClient dbClient = DatabaseClient.Instance())
				{
					dbClient.AddParameter("one", UserOne);
					dbClient.AddParameter("two", UserTwo);
					dbClient.Query("INSERT INTO `messenger_friends` (`user_one`, `user_two`) VALUES (@one, @two)");
				}
			}
		}

		public static void RemoveFriend(int UserOne, int UserTwo)
		{
			if (UserOne > 0 && UserTwo > 0)
			{
				using (DatabaseClient dbClient = DatabaseClient.Instance())
				{
					dbClient.AddParameter("one", UserOne);
					dbClient.AddParameter("two", UserTwo);
					dbClient.Query("DELETE FROM `messenger_friends` WHERE (`user_one` = @one AND `user_two` = @two) OR (`user_one` = @two AND `user_two` = @one)");
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
					dbClient.AddParameter("timestamp", (int)AliasEnvironment.Time());
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
					dbClient.AddParameter("timestamp", (int)AliasEnvironment.Time());
					dbClient.Query("INSERT INTO `messenger_roominvitations` (`sender`, `message`, `time`) VALUES (@from, @message, @timestamp)");
				}
			}
		}
	}
}
