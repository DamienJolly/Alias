using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    class ModerationCloseTicketEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (!session.Habbo.HasPermission("acc_modtool_ticket_queue"))
			{
				return;
			}

			int state = message.PopInt();
			message.PopInt();

			int ticketId = message.PopInt();
			if (ticketId <= 0)
			{
				return;
			}

			ModerationTicket issue = Alias.Server.ModerationManager.GetTicket(ticketId);
			if (issue == null || issue.ModId != session.Habbo.Id)
			{
				return;
			}

			Habbo habbo = Alias.Server.SocketServer.SessionManager.HabboById(issue.SenderId);
			if (habbo == null)
			{
				return;
			}

			Alias.Server.ModerationManager.ResolveTicket(issue, habbo, state);
		}
	}
}
