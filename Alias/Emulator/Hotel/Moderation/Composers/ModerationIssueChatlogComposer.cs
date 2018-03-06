using System;
using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms.Users.Chat;
using Alias.Emulator.Network.Messages;
using Alias.Emulator.Network.Messages.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Moderation.Composers
{
	public class ModerationIssueChatlogComposer : IMessageComposer
	{
		private ModerationTicket issue;
		private List<ModerationChatlog> chatlog;
		private string roomName;

		public ModerationIssueChatlogComposer(ModerationTicket issue, List<ModerationChatlog> chatlog, string roomName)
		{
			this.issue = issue;
			this.chatlog = chatlog;
			this.roomName = roomName;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.ModerationIssueChatlogMessageComposer);
			result.Int(this.issue.Id);
			result.Int(this.issue.SenderId);
			result.Int(this.issue.ReportedId);
			result.Int(this.issue.RoomId);

			result.Byte(1); //1 = context, 2 = im session, 3 = forum thread, 4 = forum message, 5 = selfie report, 6 = photo report, 7 =
			result.Short(2);
			result.String("roomName");
			result.Byte(2);
			result.String(this.roomName);
			result.String("roomId");
			result.Byte(1);
			result.Int(this.issue.RoomId);

			result.Short(this.chatlog.Count);
			this.chatlog.ForEach(chatlog =>
			{
				DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
				dt = dt.AddSeconds(chatlog.Timestamp).ToLocalTime();

				result.String(dt.ToString("HH:mm"));
				result.Int(chatlog.UserId);
				result.String(chatlog.Username);
				result.String(chatlog.Message);
				result.Boolean(chatlog.Type == ChatType.SHOUT);
			});
			return result;
		}
	}
}
