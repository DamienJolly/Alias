using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Moderation.Composers
{
	public class ModerationIssueInfoComposer : IPacketComposer
	{
		private ModerationTicket issue;

		public ModerationIssueInfoComposer(ModerationTicket issue)
		{
			this.issue = issue;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.ModerationIssueInfoMessageComposer);
			message.WriteInteger(this.issue.Id);
			message.WriteInteger(ModerationTicketStates.GetIntFromState(this.issue.State));
			message.WriteInteger(ModerationTicketTypes.GetIntFromType(this.issue.Type));
			message.WriteInteger(this.issue.Category);
			message.WriteInteger((int)Alias.GetUnixTimestamp() - this.issue.Id);
			message.WriteInteger(this.issue.Priority);
			message.WriteInteger(1); // ??
			message.WriteInteger(this.issue.SenderId);
			message.WriteString(this.issue.SenderUsername);
			message.WriteInteger(this.issue.ReportedId);
			message.WriteString(this.issue.ReportedUsername);
			message.WriteInteger(this.issue.ModId);
			message.WriteString(this.issue.ModUsername);
			message.WriteString(this.issue.Message + " - AutomaticAlertWord");
			message.WriteInteger(this.issue.RoomId);
			message.WriteInteger(1);
			message.WriteString("Banned Word");
			message.WriteInteger(0);
			message.WriteInteger(4);
			return message;
		}
	}
}
