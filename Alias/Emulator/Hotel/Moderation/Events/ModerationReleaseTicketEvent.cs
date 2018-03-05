using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    public class ModerationReleaseTicketEvent : IMessageEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			if (!session.Habbo.HasPermission("acc_modtool_ticket_queue"))
			{
				return;
			}
			
			for (int i = 0; i < message.Integer(); i++)
			{
				int ticketId = message.Integer();
				if (ticketId <= 0)
				{
					continue;
				}

				ModerationTicket issue = ModerationManager.GetTicket(ticketId);
				if (issue == null || issue.ModId != session.Habbo.Id)
				{
					continue;
				}

				ModerationManager.ReleaseTicket(issue);
			}
		}
	}
}
