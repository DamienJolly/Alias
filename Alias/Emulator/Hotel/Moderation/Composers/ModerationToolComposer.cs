using System.Collections.Generic;
using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Moderation.Composers
{
	public class ModerationToolComposer : IPacketComposer
	{
		private Habbo habbo;
		private List<ModerationTicket> tickets;

		public ModerationToolComposer(Habbo habbo, List<ModerationTicket> tickets)
		{
			this.habbo = habbo;
			this.tickets = tickets;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.ModerationToolMessageComposer);
			if (this.habbo.HasPermission("acc_modtool_ticket_queue"))
			{
				message.WriteInteger(tickets.Count);
				tickets.ForEach(ticket =>
				{
					message.WriteInteger(ticket.Id);
					message.WriteInteger(ModerationTicketStates.GetIntFromState(ticket.State));
					message.WriteInteger(ModerationTicketTypes.GetIntFromType(ticket.Type));
					message.WriteInteger(ticket.Category);
					message.WriteInteger((int)Alias.GetUnixTimestamp() - ticket.Id);
					message.WriteInteger(ticket.Priority);
					message.WriteInteger(1); // ??
					message.WriteInteger(ticket.SenderId);
					message.WriteString(ticket.SenderUsername);
					message.WriteInteger(ticket.ReportedId);
					message.WriteString(ticket.ReportedUsername);
					message.WriteInteger(ticket.ModId);
					message.WriteString(ticket.ModUsername);
					message.WriteString(ticket.Message);
					message.WriteInteger(ticket.RoomId);
					message.WriteInteger(0);
				});
			}
			else
			{
				message.WriteInteger(0);
			}

			message.WriteInteger(Alias.Server.ModerationManager.GetPresets("user").Count);
			Alias.Server.ModerationManager.GetPresets("user").ForEach(preset =>
			{
				message.WriteString(preset.Data);
			});

			message.WriteInteger(Alias.Server.ModerationManager.GetPresets("category").Count);
			Alias.Server.ModerationManager.GetPresets("category").ForEach(preset =>
			{
				message.WriteString(preset.Data);
			});

			message.WriteBoolean(this.habbo.HasPermission("acc_modtool_ticket_queue")); //ticket queue
			message.WriteBoolean(this.habbo.HasPermission("acc_modtool_user_logs")); //user chatlogs
			message.WriteBoolean(this.habbo.HasPermission("acc_modtool_user_alert")); //can send caution
			message.WriteBoolean(this.habbo.HasPermission("acc_modtool_user_kick")); //can send kick
			message.WriteBoolean(this.habbo.HasPermission("acc_modtool_user_ban")); //can send ban
			message.WriteBoolean(this.habbo.HasPermission("acc_modtool_room_info")); //room info ??Not sure
			message.WriteBoolean(this.habbo.HasPermission("acc_modtool_room_logs")); //room chatlogs ??Not sure

			message.WriteInteger(Alias.Server.ModerationManager.GetPresets("room").Count);
			Alias.Server.ModerationManager.GetPresets("room").ForEach(preset =>
			{
				message.WriteString(preset.Data);
			});
			return message;
		}
	}
}
