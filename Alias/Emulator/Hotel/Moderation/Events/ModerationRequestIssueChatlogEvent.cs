using System.Collections.Generic;
using Alias.Emulator.Hotel.Moderation.Composers;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Protocol;
using Alias.Emulator.Network.Sessions;

namespace Alias.Emulator.Hotel.Moderation.Events
{
    class ModerationRequestIssueChatlogEvent : IPacketEvent
	{
		public void Handle(Session session, ClientPacket message)
		{
			if (!session.Habbo.HasPermission("acc_modtool_ticket_queue"))
			{
				return;
			}

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

			string roomName = "";
			List<ModerationChatlog> chatlogs = new List<ModerationChatlog>();
			if (issue.RoomId > 0)
			{
				Room room = Alias.Server.RoomManager.Room(issue.RoomId);
				if (room != null)
				{
					roomName = room.RoomData.Name;
				}
				chatlogs = Alias.Server.ModerationManager.GetRoomChatlog(issue.RoomId);
			}
			else
			{
				chatlogs = Alias.Server.ModerationManager.GetUserChatlog(issue.SenderId, issue.ReportedId);
			}
			
			session.Send(new ModerationIssueChatlogComposer(issue, chatlogs, roomName));
		}
	}
}
