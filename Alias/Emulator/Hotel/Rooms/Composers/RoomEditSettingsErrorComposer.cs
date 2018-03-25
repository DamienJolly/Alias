using Alias.Emulator.Network.Packets;
using Alias.Emulator.Network.Packets.Headers;
using Alias.Emulator.Network.Protocol;

namespace Alias.Emulator.Hotel.Rooms.Composers
{
	public class RoomEditSettingsErrorComposer : IPacketComposer
	{
		public static int PASSWORD_REQUIRED = 5;
		public static int ROOM_NAME_MISSING = 7;
		public static int ROOM_NAME_BADWORDS = 8;
		public static int ROOM_DESCRIPTION_BADWORDS = 10;
		public static int ROOM_TAGS_BADWWORDS = 11;
		public static int RESTRICTED_TAGS = 12;
		public static int TAGS_TOO_LONG = 13;

		private int roomId;
		private int errorCode;
		private string info;

		public RoomEditSettingsErrorComposer(int roomId, int errorCode, string info = "")
		{
			this.roomId = roomId;
			this.errorCode = errorCode;
			this.info = info;
		}

		public ServerMessage Compose()
		{
			ServerMessage result = new ServerMessage(Outgoing.RoomEditSettingsErrorMessageComposer);
			result.Int(this.roomId);
			result.Int(this.errorCode);
			result.String(this.info);
			return result;
		}
	}
}
