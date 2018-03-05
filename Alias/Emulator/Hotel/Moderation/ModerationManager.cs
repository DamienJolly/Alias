using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation
{
    public class ModerationManager
    {
		private static List<ModerationPresets> presets;
		private static List<ModerationTicket> modTickets;

		public static void Initialize()
		{
			presets = ModerationDatabase.ReadPresets();
			modTickets = ModerationDatabase.ReadTickets();
		}

		public static void Reload()
		{
			presets.Clear();
			modTickets.Clear();
			Initialize();
		}

		public static List<ModerationPresets> GetPresets(string type)
		{
			return presets.Where(preset => preset.Type == type).ToList();
		}

		public static void BanUser(int targetUserId, Session moderator, string reason, int duration, ModerationBanType type, int topic)
		{
			Habbo target = SessionManager.HabboById(targetUserId);
			if (target.Rank >= moderator.Habbo.Rank)
			{
				return;
			}

			//todo: insert ban
			if (target.Session != null)
			{
				target.Session.Disconnect();
			}
		}

		public static ModerationTicket GetTicket(int ticketId)
		{
			return modTickets.Where(ticket => ticket.Id == ticketId).FirstOrDefault();
		}

		public static void RemoveTicket(ModerationTicket issue)
		{
			modTickets.Remove(issue);
		}

		public static void PickTicket(ModerationTicket issue, Habbo habbo)
		{
			issue.ModId = habbo.Id;
			issue.ModUsername = habbo.Username;
			issue.State = ModerationTicketState.PICKED;

			//todo: update in db
			SessionManager.SendWithPermission(new ModerationIssueInfoComposer(issue), "acc_modtool_ticket_queue");
		}

		public static void ReleaseTicket(ModerationTicket issue)
		{
			issue.ModId = 0;
			issue.ModUsername = "";
			issue.State = ModerationTicketState.OPEN;

			//todo: update in db
			SessionManager.SendWithPermission(new ModerationIssueInfoComposer(issue), "acc_modtool_ticket_queue");
		}

		public static void ResolveTicket(ModerationTicket issue, Habbo sender, int state)
		{
			issue.State = ModerationTicketState.CLOSED;

			//todo: update in db

			if (sender.Session != null)
			{
				switch (state)
				{
					case 1:
						sender.Session.Send(new ModerationIssueHandledComposer(ModerationIssueHandledComposer.USELESS));
						break;
					case 2:
						sender.Session.Send(new ModerationIssueHandledComposer(ModerationIssueHandledComposer.ABUSIVE));
						break;
					case 3:
						sender.Session.Send(new ModerationIssueHandledComposer(ModerationIssueHandledComposer.HANDLED));
						break;
				}
			}

			SessionManager.SendWithPermission(new ModerationIssueInfoComposer(issue), "acc_modtool_ticket_queue");
			ModerationManager.RemoveTicket(issue);
		}

		public static List<ModerationTicket> GetTickets
		{
			get
			{
				return modTickets;
			}
		}
	}
}
