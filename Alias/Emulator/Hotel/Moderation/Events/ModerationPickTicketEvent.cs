using Alias.Emulator.Hotel.Misc.Composers;
using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    class ModerationPickTicketEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (!session.Habbo.HasPermission("acc_modtool_ticket_queue"))
			{
				return;
			}

			message.PopInt();
			int ticketId = message.PopInt();
			if (ticketId <= 0)
			{
				return;
			}

			ModerationTicket issue = Alias.Server.ModerationManager.GetTicket(ticketId);
			if (issue == null)
			{
				return;
			}

			if (issue.State == ModerationTicketState.PICKED)
			{
				session.Send(new ModerationIssueInfoComposer(issue));
				return;
			}

			Alias.Server.ModerationManager.PickTicket(issue, session.Habbo);
		}
	}
}
