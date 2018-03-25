using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    public class ModerationReleaseTicketEvent : IPacketEvent
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

				ModerationTicket issue = Alias.GetServer().GetModerationManager().GetTicket(ticketId);
				if (issue == null || issue.ModId != session.Habbo.Id)
				{
					continue;
				}

				Alias.GetServer().GetModerationManager().ReleaseTicket(issue);
			}
		}
	}
}
