using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    public class ModerationCloseTicketEvent : IPacketEvent
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

			ModerationTicket issue = Alias.GetServer().GetModerationManager().GetTicket(ticketId);
			if (issue == null || issue.ModId != session.Habbo.Id)
			{
				return;
			}

			Habbo habbo = SessionManager.HabboById(issue.SenderId);
			if (habbo == null)
			{
				return;
			}

			Alias.GetServer().GetModerationManager().ResolveTicket(issue, habbo, state);
		}
	}
}
