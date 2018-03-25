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

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.ModerationToolMessageComposer);
			if (this.habbo.HasPermission("acc_modtool_ticket_queue"))
			{
				result.Int(tickets.Count);
				tickets.ForEach(ticket =>
				{
					result.Int(ticket.Id);
					result.Int(ModerationTicketStates.GetIntFromState(ticket.State));
					result.Int(ModerationTicketTypes.GetIntFromType(ticket.Type));
					result.Int(ticket.Category);
					result.Int((int)Alias.GetUnixTimestamp() - ticket.Id);
					result.Int(ticket.Priority);
					result.Int(1); // ??
					result.Int(ticket.SenderId);
					result.String(ticket.SenderUsername);
					result.Int(ticket.ReportedId);
					result.String(ticket.ReportedUsername);
					result.Int(ticket.ModId);
					result.String(ticket.ModUsername);
					result.String(ticket.Message);
					result.Int(ticket.RoomId);
					result.Int(0);
				});
			}
			else
			{
				result.Int(0);
			}

			result.Int(Alias.GetServer().GetModerationManager().GetPresets("user").Count);
			Alias.GetServer().GetModerationManager().GetPresets("user").ForEach(preset =>
			{
				result.String(preset.Data);
			});

			result.Int(Alias.GetServer().GetModerationManager().GetPresets("category").Count);
			Alias.GetServer().GetModerationManager().GetPresets("category").ForEach(preset =>
			{
				result.String(preset.Data);
			});

			result.Boolean(this.habbo.HasPermission("acc_modtool_ticket_queue")); //ticket queue
			result.Boolean(this.habbo.HasPermission("acc_modtool_user_logs")); //user chatlogs
			result.Boolean(this.habbo.HasPermission("acc_modtool_user_alert")); //can send caution
			result.Boolean(this.habbo.HasPermission("acc_modtool_user_kick")); //can send kick
			result.Boolean(this.habbo.HasPermission("acc_modtool_user_ban")); //can send ban
			result.Boolean(this.habbo.HasPermission("acc_modtool_room_info")); //room info ??Not sure
			result.Boolean(this.habbo.HasPermission("acc_modtool_room_logs")); //room chatlogs ??Not sure

			result.Int(Alias.GetServer().GetModerationManager().GetPresets("room").Count);
			Alias.GetServer().GetModerationManager().GetPresets("room").ForEach(preset =>
			{
				result.String(preset.Data);
			});
			return result;
		}
	}
}
