using System;
using System.Collections.Generic;
using Alias.Emulator.Hotel.Rooms;
using Alias.Emulator.Hotel.Rooms.Entities.Chat;
using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Moderation.Composers
{
	class ModerationRoomChatlogComposer : IPacketComposer
	{
		private RoomData roomData;
		private List<ModerationChatlog> chatlog;

		public ModerationRoomChatlogComposer(RoomData roomData, List<ModerationChatlog> chatlog)
		{
			this.roomData = roomData;
			this.chatlog = chatlog;
		}

		public ServerPacket Compose()
		{
			ServerPacket message = new ServerPacket(Outgoing.ModerationRoomChatlogMessageComposer);
			message.WriteByte(1);
			message.WriteShort(2);
			message.WriteString("roomName");
			message.WriteByte(2);
			message.WriteString(this.roomData.Name);
			message.WriteString("roomId");
			message.WriteByte(1);
			message.WriteInteger(this.roomData.Id);

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
