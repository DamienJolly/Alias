using System;
using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Rooms.Users.Chat;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Moderation.Composers
{
	public class ModerationRoomChatlogComposer : IPacketComposer
	{
		private Room room;
		private List<ModerationChatlog> chatlog;

		public ModerationRoomChatlogComposer(Room room, List<ModerationChatlog> chatlog)
		{
			this.room = room;
			this.chatlog = chatlog;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.ModerationRoomChatlogMessageComposer);
			message.WriteByte(1);
			message.WriteShort(2);
			message.WriteString("roomName");
			message.WriteByte(2);
			message.WriteString(this.room.RoomData.Name);
			message.WriteString("roomId");
			message.WriteByte(1);
			message.WriteInteger(this.room.RoomData.Id);

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
