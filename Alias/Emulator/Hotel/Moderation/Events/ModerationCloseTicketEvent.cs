using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    public class ModerationCloseTicketEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			if (!session.Habbo.HasPermission("acc_modtool_ticket_queue"))
			{
				return;
			}

			int state = message.Integer();
			message.Integer();

			int ticketId = message.Integer();
			if (ticketId <= 0)
			{
				return;
			}

			ModerationTicket issue = ModerationManager.GetTicket(ticketId);
			if (issue == null || issue.ModId != session.Habbo.Id)
			{
				return;
			}

			Habbo habbo = SessionManager.HabboById(issue.SenderId);
			if (habbo == null)
			{
				return;
			}

			ModerationManager.ResolveTicket(issue, habbo, state);
		}
	}
}
