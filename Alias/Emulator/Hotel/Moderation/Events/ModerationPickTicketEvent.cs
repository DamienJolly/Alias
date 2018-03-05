using Alias.Emulator.Hotel.Misc.Composers;
using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    public class ModerationPickTicketEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			if (!session.Habbo.HasPermission("acc_modtool_ticket_queue"))
			{
				return;
			}

			message.Integer();
			int ticketId = message.Integer();
			if (ticketId <= 0)
			{
				return;
			}

			ModerationTicket issue = ModerationManager.GetTicket(ticketId);
			if (issue == null)
			{
				session.Send(new GenericAlertComposer("Picking issue failed: \rTicket already picked or does not exist!", session));
				return;
			}

			if (issue.State == ModerationTicketState.PICKED)
			{
				session.Send(new ModerationIssueInfoComposer(issue));
				session.Send(new GenericAlertComposer("Picking issue failed: \rTicket already picked or does not exist!", session));
				return;
			}

			ModerationManager.PickTicket(issue, session.Habbo);
		}
	}
}
