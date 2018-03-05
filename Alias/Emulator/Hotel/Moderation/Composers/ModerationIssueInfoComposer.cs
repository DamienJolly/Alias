using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Moderation.Composers
{
	public class ModerationIssueInfoComposer : IMessageComposer
	{
		private ModerationTicket issue;

		public ModerationIssueInfoComposer(ModerationTicket issue)
		{
			this.issue = issue;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.ModerationIssueInfoMessageComposer);
			result.Int(this.issue.Id);
			result.Int(ModerationTicketStates.GetIntFromState(this.issue.State));
			result.Int(ModerationTicketTypes.GetIntFromType(this.issue.Type));
			result.Int(this.issue.Category);
			result.Int((int)AliasEnvironment.Time() - this.issue.Id);
			result.Int(this.issue.Priority);
			result.Int(1); // ??
			result.Int(this.issue.SenderId);
			result.String(this.issue.SenderUsername);
			result.Int(this.issue.ReportedId);
			result.String(this.issue.ReportedUsername);
			result.Int(this.issue.ModId);
			result.String(this.issue.ModUsername);
			result.String(this.issue.Message);
			result.Int(this.issue.RoomId);
			result.Int(0);
			return result;
		}
	}
}
