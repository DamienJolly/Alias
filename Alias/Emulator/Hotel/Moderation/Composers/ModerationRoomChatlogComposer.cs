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

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.ModerationRoomChatlogMessageComposer);
			result.Byte(1);
			result.Short(2);
			result.String("roomName");
			result.Byte(2);
			result.String(this.room.RoomData.Name);
			result.String("roomId");
			result.Byte(1);
			result.Int(this.room.RoomData.Id);

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
