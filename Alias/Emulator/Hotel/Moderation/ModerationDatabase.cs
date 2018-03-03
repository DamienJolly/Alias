using System.Collections.Generic;
using System.Data;
using Alias.Emulator.Database;
using Alias.Emulator.Hotel.Users;

namespace Alias.Emulator.Hotel.Moderation
{
    public class ModerationDatabase
    {
		public static List<ModerationTicket> ReadTickets()
		{
			List<ModerationTicket> tickets = new List<ModerationTicket>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				foreach (DataRow row in dbClient.DataTable("SELECT * FROM `support_tickets` WHERE `state` != 0").Rows)
				{
					string senderUsername = (int)row["sender_id"] > 0 ? (string)UserDatabase.Variable((int)row["sender_id"], "username") : "Unknown";
					string reportedUsername = (int)row["reported_id"] > 0 ? (string)UserDatabase.Variable((int)row["reported_id"], "username") : "Unknown";
					string modUsername = (int)row["mod_id"] > 0 ? (string)UserDatabase.Variable((int)row["mod_id"], "username") : "";
					ModerationTicket ticket = new ModerationTicket()
					{
						Id = (int)row["id"],
						State = ModerationTicketStates.GetStateFromInt((int)row["state"]),
						Timestamp = (int)row["timestamp"],
						Priority = (int)row["score"],
						SenderId = (int)row["sender_id"],
						SenderUsername = senderUsername,
						ReportedId = (int)row["reported_id"],
						ReportedUsername = reportedUsername,
						ModId = (int)row["mod_id"],
						ModUsername = modUsername,
						Message = (string)row["issue"],
						Type = ModerationTicketTypes.GetTypeFromInt((int)row["type"]),
						RoomId = (int)row["room_id"],
						Category = (int)row["category"]
					};

					if (ticket.ModId <= 0)
					{
						ticket.ModUsername = "";
						ticket.State = ModerationTicketState.OPEN;
					}

					tickets.Add(ticket);
					row.Delete();
				}
			}
			return tickets;
		}

		public static List<ModerationPresets> ReadPresets()
		{
			List<ModerationPresets> presets = new List<ModerationPresets>();
			using (DatabaseClient dbClient = DatabaseClient.Instance())
			{
				foreach (DataRow row in dbClient.DataTable("SELECT `type`, `preset` FROM `support_presets`").Rows)
				{
					ModerationPresets preset = new ModerationPresets()
					{
						Type = (string)row["type"],
						Data = (string)row["preset"]
					};
					presets.Add(preset);
					row.Delete();
				}
			}
			return presets;
		}
	}
}
