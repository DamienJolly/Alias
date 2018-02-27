using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Users.Handshake;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Users.Messenger
{
	public class MessengerDatabase
	{
		public static void InitMessenger(Messenger messenger)
		{
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				dbClient.AddParameter("id", messenger.Habbo().Id);
				foreach (DataRow row in dbClient.DataTable("SELECT `user_one`, `user_two` FROM `messenger_friends` WHERE `user_one` = @id OR `user_two` = @id").Rows)
				{
					MessengerFriend messengerFriend = new MessengerFriend();
					messengerFriend.Id = (int)row["user_one"] == messenger.Habbo().Id ? (int)row["user_two"] : (int)row["user_one"];
					Habbo habbo = SessionManager.Habbo(messengerFriend.Id);
					messengerFriend.Username = habbo.Username;
					messengerFriend.Look = habbo.Look;
					messengerFriend.Motto = habbo.Motto;
					messengerFriend.InRoom = habbo.CurrentRoom != null;
					messenger.FriendList().Add(messengerFriend);
					row.Delete();
				}

				dbClient.ClearParameters();
				dbClient.AddParameter("id", messenger.Habbo().Id);
				foreach (DataRow row in dbClient.DataTable("SELECT `sender` FROM `messenger_requests` WHERE `reciever` = @id").Rows)
				{
					MessengerRequest messengerRequest = new MessengerRequest();
					messengerRequest.Id = (int)row["sender"];
					Habbo habbo = SessionManager.Habbo(messengerRequest.Id);
					messengerRequest.Username = habbo.Username;
					messengerRequest.Look = habbo.Look;
					messenger.RequestList().Add(messengerRequest);
					row.Delete();
				}
			}
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
						result.Add(SessionManager.Habbo((int)row["Id"]));
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
