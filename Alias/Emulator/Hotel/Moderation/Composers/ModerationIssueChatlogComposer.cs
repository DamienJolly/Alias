using System;
using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms.Entities.Chat;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Moderation.Composers
{
	public class ModerationIssueChatlogComposer : IPacketComposer
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

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.ModerationIssueChatlogMessageComposer);
			message.WriteInteger(this.issue.Id);
			message.WriteInteger(this.issue.SenderId);
			message.WriteInteger(this.issue.ReportedId);
			message.WriteInteger(this.issue.RoomId);

			message.WriteByte(1); //1 = context, 2 = im session, 3 = forum thread, 4 = forum message, 5 = selfie report, 6 = photo report, 7 =
			message.WriteShort(2);
			message.WriteString("roomName");
			message.WriteByte(2);
			message.WriteString(this.roomName);
			message.WriteString("roomId");
			message.WriteByte(1);
			message.WriteInteger(this.issue.RoomId);

			message.WriteShort(this.chatlog.Count);
			this.chatlog.ForEach(chatlog =>
			{
				DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
				dt = dt.AddSeconds(chatlog.Timestamp).ToLocalTime();

				message.WriteString(dt.ToString("HH:mm"));
				message.WriteInteger(chatlog.UserId);
				message.WriteString(chatlog.Username);
				message.WriteString(chatlog.Message);
				message.WriteBoolean(chatlog.Type == ChatType.SHOUT);
			});
			return message;
		}
	}
}
