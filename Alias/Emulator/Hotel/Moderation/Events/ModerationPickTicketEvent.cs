using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    class ModerationPickTicketEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (!session.Player.HasPermission("acc_modtool_ticket_queue"))
			{
				return;
			}

			int count = message.PopInt();
			for (int i = 0; i < count; i++)
			{
				int ticketId = message.PopInt();
				if (ticketId <= 0)
				{
					continue;
				}

				ModerationTicket ticket = Alias.Server.ModerationManager.GetTicket(ticketId);
				if (ticket == null || ticket.State != ModerationTicketState.OPEN)
				{
					continue;
				}

				Alias.Server.ModerationManager.PickTicket(ticket, session.Player);
			}
		}
	}
}
