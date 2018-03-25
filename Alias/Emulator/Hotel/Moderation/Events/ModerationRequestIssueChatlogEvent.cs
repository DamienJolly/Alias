using System.Collections.Generic;
using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    public class ModerationRequestIssueChatlogEvent : IPacketEvent
	{
		public void Handle(Session session, ClientMessage message)
		{
			if (!session.Habbo.HasPermission("acc_modtool_ticket_queue"))
			{
				return;
			}

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

			string roomName = "";
			List<ModerationChatlog> chatlogs = new List<ModerationChatlog>();
			if (issue.RoomId > 0)
			{
				Room room = Alias.GetServer().GetRoomManager().Room(issue.RoomId);
				if (room != null)
				{
					roomName = room.RoomData.Name;
				}
				chatlogs = Alias.GetServer().GetModerationManager().GetRoomChatlog(issue.RoomId);
			}
			else
			{
				chatlogs = Alias.GetServer().GetModerationManager().GetUserChatlog(issue.SenderId, issue.ReportedId);
			}
			
			session.Send(new ModerationIssueChatlogComposer(issue, chatlogs, roomName));
		}
	}
}
