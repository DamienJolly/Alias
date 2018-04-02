using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    public class ModerationReleaseTicketEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (!session.Habbo.HasPermission("acc_modtool_ticket_queue"))
			{
				return;
			}
			
			for (int i = 0; i < message.PopInt(); i++)
			{
				int ticketId = message.PopInt();
				if (ticketId <= 0)
				{
					continue;
				}

				ModerationTicket issue = Alias.Server.ModerationManager.GetTicket(ticketId);
				if (issue == null || issue.ModId != session.Habbo.Id)
				{
					continue;
				}

				Alias.Server.ModerationManager.ReleaseTicket(issue);
			}
		}
	}
}
