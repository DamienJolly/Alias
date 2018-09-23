using Alias.Emulator.Hotel.Players;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    class ModerationCloseTicketEvent : IPacketEvent
	{
		public async void Handle(Session session, ClientPacket message)
		{
			if (!session.Player.HasPermission("acc_modtool_ticket_queue"))
			{
				return;
			}

			int state = message.PopInt();
			int count = message.PopInt();
			for (int i = 0; i < count; i++)
			{
				int ticketId = message.PopInt();
				if (ticketId <= 0)
				{
					continue;
				}

				ModerationTicket issue = Alias.Server.ModerationManager.GetTicket(ticketId);
				if (issue == null || issue.ModId != session.Player.Id)
				{
					continue;
				}

				Player habbo = await Alias.Server.PlayerManager.ReadPlayerByIdAsync(issue.SenderId);
				Alias.Server.ModerationManager.ResolveTicket(issue, habbo, state);
			}
		}
	}
}
