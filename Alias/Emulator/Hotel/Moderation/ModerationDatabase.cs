using System.Collections.Generic;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Rooms.Entities.Chat;
using MySql.Data.MySqlClient;

namespace Alias.Emulator.Hotel.Moderation
{
	public class ModerationDatabase
	{
		public static List<ModerationTicket> ReadTickets()
		{
			List<ModerationTicket> tickets = new List<ModerationTicket>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `support_tickets` WHERE `state` != 0"))
				{
					while (Reader.Read())
					{
						string senderUsername   = "Unknown";
						string reportedUsername = "Unknown";
						string modUsername      = "";
						ModerationTicket ticket = new ModerationTicket()
						{
							Id               = Reader.GetInt32("id"),
							State            = ModerationTicketStates.GetStateFromInt(Reader.GetInt32("state")),
							Timestamp        = Reader.GetInt32("timestamp"),
							Priority         = Reader.GetInt32("score"),
							SenderId         = Reader.GetInt32("sender_id"),
							SenderUsername   = senderUsername,
							ReportedId       = Reader.GetInt32("reported_id"),
							ReportedUsername = reportedUsername,
							ModId            = Reader.GetInt32("mod_id"),
							ModUsername      = modUsername,
							Message          = Reader.GetString("issue"),
							Type             = ModerationTicketTypes.GetTypeFromInt(Reader.GetInt32("type")),
							RoomId           = Reader.GetInt32("room_id"),
							Category         = Reader.GetInt32("category")
						};

						if (ticket.ModId <= 0)
						{
							ticket.ModUsername = "";
							ticket.State       = ModerationTicketState.OPEN;
						}

						tickets.Add(ticket);
					}
				}
			}
			return tickets;
		}

		public static int AddTicket(ModerationTicket ticket)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("time", ticket.Timestamp);
				dbClient.AddParameter("senderId", ticket.SenderId);
				dbClient.AddParameter("reporterID", ticket.ReportedId);
				dbClient.AddParameter("issue", ticket.Message);
				dbClient.AddParameter("type", ModerationTicketTypes.GetIntFromType(ticket.Type));
				dbClient.AddParameter("roomId", ticket.RoomId);
				dbClient.AddParameter("category", ticket.Category);
				dbClient.Query("INSERT INTO `support_tickets` (`timestamp`, `sender_id`, `reported_id`, `issue`, `type`, `room_id`, `category`) VALUES (@time, @senderId, @reporterId, @issue, @type, @roomId, @category)");
				return dbClient.LastInsertedId();
			}
		}

		public static void UpdateTicket(ModerationTicket ticket)
		{
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("id", ticket.Id);
				dbClient.AddParameter("modId", ticket.ModId);
				dbClient.AddParameter("state", (int)ticket.State);
				dbClient.Query("UPDATE `support_tickets` SET `state` = @state, `mod_id` = @modId WHERE `id` = @id");
			}
		}

		public static List<ModerationPresets> ReadPresets()
		{
			List<ModerationPresets> presets = new List<ModerationPresets>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT `type`, `preset` FROM `support_presets`"))
				{
					while (Reader.Read())
					{
						ModerationPresets preset = new ModerationPresets()
						{
							Type = Reader.GetString("type"),
							Data = Reader.GetString("preset")
						};
						presets.Add(preset);
					}
				}
			}
			return presets;
		}

		public static List<ModerationChatlog> ReadRoomChatlogs(int roomId)
		{
			List<ModerationChatlog> chatlogs = new List<ModerationChatlog>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("roomId", roomId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `chatlogs` WHERE `room_id` = @roomId ORDER BY `timestamp` DESC LIMIT 150"))
				{
					while (Reader.Read())
					{
						string targetUsername = "";
						ModerationChatlog chatlog = new ModerationChatlog()
						{
							UserId         = Reader.GetInt32("user_id"),
							Username       = "",
							TargetId       = Reader.GetInt32("target_id"),
							TargetUsername = targetUsername,
							Timestamp      = Reader.GetInt32("timestamp"),
							Message        = Reader.GetString("message"),
							Type           = ChatType.CHAT
						};
						chatlogs.Add(chatlog);
					}
				}
			}
			return chatlogs;
		}

		public static List<ModerationChatlog> ReadUserChatlogs(int senderId, int targetId)
		{
			List<ModerationChatlog> chatlogs = new List<ModerationChatlog>();
			using (DatabaseConnection dbClient = Alias.Server.DatabaseManager.GetConnection())
			{
				dbClient.AddParameter("senderId", senderId);
				dbClient.AddParameter("targetId", targetId);
				using (MySqlDataReader Reader = dbClient.DataReader("SELECT * FROM `chatlogs` WHERE `user_id` = @senderId OR `user_id` = @targetId ORDER BY `timestamp` DESC LIMIT 150"))
				{
					while (Reader.Read())
					{
						string targetUsername = "";
						ModerationChatlog chatlog = new ModerationChatlog()
						{
							UserId         = Reader.GetInt32("user_id"),
							Username       = "",
							TargetId       = Reader.GetInt32("target_id"),
							TargetUsername = targetUsername,
							Timestamp      = Reader.GetInt32("timestamp"),
							Message        = Reader.GetString("message"),
							Type           = ChatType.CHAT
						};
						chatlogs.Add(chatlog);
					}
				}
			}
			return chatlogs;
		}
	}
}
