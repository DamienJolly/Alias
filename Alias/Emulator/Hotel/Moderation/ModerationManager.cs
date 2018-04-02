using System.Collections.Generic;
using System.Linq;
using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation
{
    sealed class ModerationManager
    {
		private List<ModerationPresets> _presets;
		private List<ModerationTicket> _modTickets;

		public ModerationManager()
		{
			this._presets = new List<ModerationPresets>();
			this._modTickets = new List<ModerationTicket>();
		}

		public void Initialize()
		{
			this._presets = ModerationDatabase.ReadPresets();
			this._modTickets = ModerationDatabase.ReadTickets();
		}
		
		public List<ModerationPresets> GetPresets(string type)
		{
			return this._presets.Where(preset => preset.Type == type).ToList();
		}

		public void QuickTicket(Habbo reported, string message)
		{
			ModerationTicket issue = new ModerationTicket()
			{
				Id             = 999,
				SenderId       = reported.Id,
				SenderUsername = reported.Username,
				Message        = message,
				Type           = ModerationTicketType.AUTOMATIC
			};

			AddTicket(issue);
		}

		public void AddTicket(ModerationTicket issue)
		{
			//todo: add to db
			this._modTickets.Add(issue);
			Alias.Server.SocketServer.SessionManager.SendWithPermission(new ModerationIssueInfoComposer(issue), "acc_modtool_ticket_queue");
		}

		public List<ModerationChatlog> GetRoomChatlog(int roomId)
		{
			return ModerationDatabase.ReadRoomChatlogs(roomId);
		}

		public List<ModerationChatlog> GetUserChatlog(int senderId, int targetId)
		{
			return ModerationDatabase.ReadUserChatlogs(senderId, targetId);
		}

		public void BanUser(int targetUserId, Session moderator, string reason, int duration, ModerationBanType type, int topic)
		{
			Habbo target = Alias.Server.SocketServer.SessionManager.HabboById(targetUserId);
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

		public ModerationTicket GetTicket(int ticketId)
		{
			return this._modTickets.Where(ticket => ticket.Id == ticketId).FirstOrDefault();
		}

		public void RemoveTicket(ModerationTicket issue)
		{
			this._modTickets.Remove(issue);
		}

		public void PickTicket(ModerationTicket issue, Habbo habbo)
		{
			issue.ModId       = habbo.Id;
			issue.ModUsername = habbo.Username;
			issue.State       = ModerationTicketState.PICKED;

			//todo: update in db
			Alias.Server.SocketServer.SessionManager.SendWithPermission(new ModerationIssueInfoComposer(issue), "acc_modtool_ticket_queue");
		}

		public void ReleaseTicket(ModerationTicket issue)
		{
			issue.ModId       = 0;
			issue.ModUsername = "";
			issue.State       = ModerationTicketState.OPEN;

			//todo: update in db
			Alias.Server.SocketServer.SessionManager.SendWithPermission(new ModerationIssueInfoComposer(issue), "acc_modtool_ticket_queue");
		}

		public void ResolveTicket(ModerationTicket issue, Habbo sender, int state)
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

			Alias.Server.SocketServer.SessionManager.SendWithPermission(new ModerationIssueInfoComposer(issue), "acc_modtool_ticket_queue");
			RemoveTicket(issue);
		}

		public List<ModerationTicket> GetTickets => this._modTickets;
	}
}
