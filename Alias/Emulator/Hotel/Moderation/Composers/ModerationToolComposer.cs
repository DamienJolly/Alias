using Alias.Emulator.Hotel.Users;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Moderation.Composers
{
	public class ModerationToolComposer : IMessageComposer
	{
		private Habbo habbo;

		public ModerationToolComposer(Habbo habbo)
		{
			this.habbo = habbo;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.ModerationToolMessageComposer);
			if (this.habbo.HasPermission("acc_modtool_ticket_queue"))
			{
				result.Int(ModerationManager.GetTickets.Count);
				ModerationManager.GetTickets.ForEach(ticket =>
				{
					result.Int(ticket.Id);
					result.Int(ModerationTicketStates.GetIntFromState(ticket.State));
					result.Int(ModerationTicketTypes.GetIntFromType(ticket.Type));
					result.Int(ticket.Category);
					result.Int((int)AliasEnvironment.Time() - ticket.Id);
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

			result.Int(ModerationManager.GetPresets("user").Count);
			ModerationManager.GetPresets("user").ForEach(preset =>
			{
				result.String(preset.Data);
			});

			result.Int(ModerationManager.GetPresets("category").Count);
			ModerationManager.GetPresets("category").ForEach(preset =>
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

			result.Int(ModerationManager.GetPresets("room").Count);
			ModerationManager.GetPresets("room").ForEach(preset =>
			{
				result.String(preset.Data);
			});
			return result;
		}
	}
}
